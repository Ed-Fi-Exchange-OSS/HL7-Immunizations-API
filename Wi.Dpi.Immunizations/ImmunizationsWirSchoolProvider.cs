using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EdFi.Common.Database;
using EdFi.Ods.Common.Database;

namespace Wi.Dpi.Immunizations
{
    public class WirSchool
    {
        public int SchoolId { get; set; }
        public int LocalEducationOrganizationId { get; set; }
        public string WirSchoolId { get; set; }
    }

    public interface IImmunizationsWirSchoolProvider
    {
        List<WirSchool> GetWirSchools(int[] educationOrganizationIds);
    }

    public class ImmunizationsWirSchoolProvider : IImmunizationsWirSchoolProvider
    {
        private readonly IOdsDatabaseConnectionStringProvider _odsDatabaseConnectionStringProvider;

        public ImmunizationsWirSchoolProvider(IOdsDatabaseConnectionStringProvider odsDatabaseConnectionStringProvider)
        {
            _odsDatabaseConnectionStringProvider = odsDatabaseConnectionStringProvider;
        }

        public List<WirSchool> GetWirSchools(int[] educationOrganizationIds)
        {
            var result = new List<WirSchool>();
            var leaIds = string.Join(",", educationOrganizationIds);
            var sql = $@"
SELECT s.[SchoolId],s.[LocalEducationAgencyId],ic.[IdentificationCode]
FROM [edfi].[EducationOrganizationIdentificationCode] ic
JOIN [edfi].[School] s
    ON ic.[EducationOrganizationId] = s.[SchoolId]
WHERE (s.[SchoolId] in ({leaIds}) OR s.[LocalEducationAgencyId] in ({leaIds}))
AND [EducationOrganizationIdentificationSystemDescriptorId] = 450";//450=Wir

            using (var conn = new SqlConnection(_odsDatabaseConnectionStringProvider.GetConnectionString()))
            {
                conn.Open();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var schoolId = reader.GetInt32("SchoolId");
                            var localEducationOrganizationId = reader.GetInt32("LocalEducationAgencyId");
                            var wirSchoolId = reader.GetString("IdentificationCode");
                            result.Add(new WirSchool
                            {
                                SchoolId = schoolId,
                                LocalEducationOrganizationId = localEducationOrganizationId,
                                WirSchoolId = wirSchoolId
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}
