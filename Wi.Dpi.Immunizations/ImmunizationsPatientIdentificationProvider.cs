using EdFi.Ods.Common.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Wi.Dpi.Immunizations
{
    public interface IImmunizationsPatientIdentificationProvider
    {
        ImmunizationsPatientIdentification GetPatientIdentification(string uniqueId, int[] localEducationOrganizationIds);
    }

    public class ImmunizationsImmunizationsPatientIdentificationProvider : IImmunizationsPatientIdentificationProvider
    {
        private readonly IOdsDatabaseConnectionStringProvider _odsDatabaseConnectionStringProvider;

        public ImmunizationsImmunizationsPatientIdentificationProvider(IOdsDatabaseConnectionStringProvider odsDatabaseConnectionStringProvider)
        {
            _odsDatabaseConnectionStringProvider = odsDatabaseConnectionStringProvider;
        }

        public ImmunizationsPatientIdentification GetPatientIdentification(string uniqueId, int[] localEducationOrganizationIds)
        {
            var leaIds = string.Join(",", localEducationOrganizationIds);
            var patient = new ImmunizationsPatientIdentification();
            var name = new ImmunizationsPatientName{ Primary = true};
            var address = new ImmunizationsPatientAddress{Primary = true};
            var sql = $@"
;WITH SeoaAddress AS (
	SELECT [StudentUsi], [EducationOrganizationId],[StreetNumberName], [City], [PostalCode], [StateAbbreviationDescriptorId],
    ROW_NUMBER() OVER (PARTITION BY [StudentUsi], [EducationOrganizationId] ORDER BY [AddressTypeDescriptorId]) AS rn
	FROM [edfi].[StudentEducationOrganizationAssociationAddress] seoaa
	WHERE [AddressTypeDescriptorId] IN (12720, 12721) 
),
StudentSchool AS (
	SELECT ssa.[SchoolId]
		,ssa.[StudentUSI]
		,ssa.[Id]
		,ssa.[LastModifiedDate]
		,ic.[IdentificationCode]
	FROM [edfi].[StudentSchoolAssociation] ssa
    JOIN [edfi].[EducationOrganizationIdentificationCode] ic 
		ON ssa.SchoolId = ic.[EducationOrganizationId]
		AND ic.[EducationOrganizationIdentificationSystemDescriptorId] = 450
)
SELECT   s.[StudentUniqueId]
		,s.[StudentUsi]
        ,seoa.[EducationOrganizationId]
		,s.[Id] StudentId
		,seoa.[Id] StudentEducationOrganizationAssociationId
		,s.[FirstName]
		,s.[MiddleName]
		,s.[LastSurname]
		,s.[BirthDate]
		,isnull(bsd.[CodeValue], 'U') Sex
        ,CASE WHEN ssa.StudentUSI IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END SsaExists
		,CASE WHEN seoa.StudentUSI IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END SeoaExists
		,seoaa.[StreetNumberName]
		,seoaa.[City]
		,sad.[CodeValue] [State]
		,seoaa.[PostalCode]
        ,ssa.[IdentificationCode] [WirSchoolId]
  FROM [edfi].[Student] s
  LEFT JOIN StudentSchool ssa
    ON ssa.StudentUSI = s.StudentUSI
    AND ssa.Id 	= (SELECT TOP 1 Id
		FROM StudentSchool ssa2
		JOIN [edfi].[School] sc
			ON sc.SchoolId = ssa2.SchoolId
			AND (sc.LocalEducationAgencyId in ({leaIds}) OR sc.SchoolId in ({leaIds}))
		WHERE s.StudentUSI = ssa2.StudentUSI
		ORDER BY ssa2.LastModifiedDate)
  LEFT JOIN [edfi].[StudentEducationOrganizationAssociation] seoa
	ON seoa.Id = 
	(SELECT TOP 1 Id
		FROM [edfi].[StudentEducationOrganizationAssociation] seoa2
        JOIN [edfi].[School] sc2
			ON sc2.SchoolId = seoa2.EducationOrganizationId
			AND (sc2.LocalEducationAgencyId in ({leaIds}) OR sc2.SchoolId in ({leaIds}))
		WHERE s.StudentUSI = seoa2.StudentUSI
		ORDER BY seoa2.LastModifiedDate)
  LEFT JOIN [edfi].[Descriptor] bsd
	ON bsd.DescriptorId = seoa.SexDescriptorId
  LEFT JOIN SeoaAddress seoaa
	ON seoaa.StudentUSI = seoa.StudentUSI
	AND seoaa.EducationOrganizationId = seoa.EducationOrganizationId
    AND seoaa.rn = 1
  LEFT JOIN [edfi].[Descriptor] sad
	ON sad.DescriptorId = seoaa.StateAbbreviationDescriptorId
WHERE [StudentUniqueId] = '{uniqueId}'";

            using (var conn = new SqlConnection(_odsDatabaseConnectionStringProvider.GetConnectionString()))
            {
                conn.Open();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            patient.StudentExists = true;

                            patient.UniqueId = reader.GetString("StudentUniqueId");
                            patient.EducationOrganizationId = reader.GetInt32("EducationOrganizationId");
                            patient.StudentId = reader.GetGuid("StudentId");
                            patient.StudentEducationOrganizationAssociationId = reader.GetGuid("StudentEducationOrganizationAssociationId");
                            
                            name.FirstName = reader.GetString("FirstName");
                            name.MiddleName = reader.GetString("MiddleName");
                            name.LastSurname = reader.GetString("LastSurname");
                            patient.Birthdate = reader.GetDate("BirthDate")?.ToString("yyyyMMdd");
                            patient.SsaExists = reader.GetBoolean("SsaExists");
                            patient.SeoaExists = reader.GetBoolean("SeoaExists");
                            patient.WirSchoolId = reader.GetString("WirSchoolId");
                            
                            patient.Gender = reader.GetString("Sex"); 
                            address.City = reader.GetString("City");
                            address.State = reader.GetString("State");
                            address.Street = reader.GetString("StreetNumberName");
                            address.Zipcode = reader.GetString("PostalCode");
                        }
                        patient.Names.Add(name);
                        patient.Addresses.Add(address);
                    }
                }
            }
            return patient;
        }


    }

    internal static class ReaderExtensions
    {
        internal static string GetString(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        internal static int GetInt32(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }

        internal static Guid GetGuid(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? new Guid() : reader.GetGuid(ordinal);
        }

        internal static bool GetBoolean(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? false : reader.GetBoolean(ordinal);
        }

        internal static DateTime? GetDate(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? null : (DateTime?)reader.GetDateTime(ordinal);
        }
    }
}
