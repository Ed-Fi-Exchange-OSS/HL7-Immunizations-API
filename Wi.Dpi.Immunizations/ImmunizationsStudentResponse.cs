using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EdFi.Ods.Api.Common.Models.Resources.Student.EdFi;
using EdFi.Ods.Api.Common.Models.Resources.StudentEducationOrganizationAssociation.EdFi;
using EdFi.Ods.Common.Serialization;
using Newtonsoft.Json;

namespace Wi.Dpi.Immunizations
{

    public class ImmunizationsError
    {
        public string Id { get; set; }
        public string Source { get; set; }
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 0=Reject (WiR found an error with the DPI request - contact DPI); <br />
    /// 1=Success; <br />
    /// 2=MultipleMatches (If there are multiple matches please make sure the students information match in both EdFi and WiR); <br />
    /// 3=NotFound (If there are no matches please make sure the students information match in both EdFi and WiR)
    /// </summary>
    public enum ImmunizationsStatusCode
    {
        Reject,
        Success,
        MultipleMatches,
        NotFound
    }

    /// <summary>
    /// The base response for the student immunizations endpoint.
    /// </summary>
    public class ImmunizationsStudentResponse
    {
        public ImmunizationsStatusCode StatusCode { get; set; }
        /// <summary>
        /// The message corresponding to the status.
        /// </summary>
        public string StatusMessage
        {
            get
            {
                switch (StatusCode)
                {
                    case ImmunizationsStatusCode.Success:
                        return "Success";
                    case ImmunizationsStatusCode.Reject:
                        return "Request rejected by WIR.";
                    case ImmunizationsStatusCode.MultipleMatches:
                        return "Multiple matches found in WIR. You need to log into WIR to see available matches.";
                    case ImmunizationsStatusCode.NotFound:
                        return "No match found in WIR.";
                }
                return "Unknown";
            }
        }
        public List<ImmunizationsError> Errors { get; set; } = new List<ImmunizationsError>();

        public ImmunizationsStudentHistory Data { get; set; }
    }

    /// <summary>
    /// The student identifiers and the student immunization history.
    /// </summary>
    public class ImmunizationsStudentHistory
    {
        [DataMember(Name = "patientIdentifier")]
        public ImmunizationsPatientIdentifier PatientIdentifier { get; set; } = new ImmunizationsPatientIdentifier();
        [DataMember(Name = "studentReference")]
        public StudentReference StudentReference { get; set; }
        [DataMember(Name = "studentEducationOrganizationAssociationReference")]
        public StudentEducationOrganizationAssociationReference StudentEducationOrganizationAssociationReference { get; set; }
        ///<summary>
        /// An array of the student immunization history.
        /// </summary>
        [DataMember(Name = "pharmacyTreatmentAdministrations")]
        public List<ImmunizationsPharmacyTreatmentAdministration> PharmacyTreatmentAdministrations { get; set; } = new List<ImmunizationsPharmacyTreatmentAdministration>();
    }

    public class ImmunizationsPharmacyTreatmentAdministration
    {
        /// <summary>
        /// The date the treatment was administered.
        /// </summary>
        [JsonConverter(typeof(Iso8601UtcDateOnlyConverter))]
        public DateTime? AdministeredDate { get; set; }
        public ImmunizationsCodedElement AdministeredCode { get; set; }
        /// <summary>
        /// The student's reason for not getting the treatment.
        /// </summary>
        public string Waiver { get; set; }
    }

    /// <summary>
    /// The WiR internal student identification.
    /// </summary>
    public class ImmunizationsPatientIdentifier
    {
        /// <summary>
        /// The WiR student identifier.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The authority issuing the student identifier.
        /// </summary>
        public string AssigningAuthority { get; set; }
        /// <summary>
        /// The type of student identifier.
        /// </summary>
        public string IdentifierTypeCode { get; set; }
        public ImmunizationsName Name { get; set; } = new ImmunizationsName();
        public ImmunizationsMaidenName MothersMaidenName { get; set; } = new ImmunizationsMaidenName();
        /// <summary>
        /// The student's birthdate.
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// The student's sex.
        /// </summary>
        public string Sex { get; set; }
        public string Race { get; set; }

        public ImmunizationsAddress Address { get; set; } = new ImmunizationsAddress();
        //public ImmunizationsCodedElement EthnicGroup { get; set; }
        /// <summary>
        /// The multiple birth indicator for the student.
        /// </summary>
        public bool MultipleBirthIndicator { get; set; }
        /// <summary>
        /// The birth order of the student.
        /// </summary>
        public string BirthOrder { get; set; }
        //public ImmunizationsCodedElement Citizenship { get; set; }
        public ImmunizationsExtendedTelephoneNumber ExtendedTelephoneNumber { get; set; } = new ImmunizationsExtendedTelephoneNumber();
    }

    public class ImmunizationsMaidenName
    {
        public string FirstName { get; set; }
        public string LastSurname { get; set; }
    }

    public class ImmunizationsName
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastSurname { get; set; }
        public string GenerationCodeSuffix { get; set; }
        public string Prefix { get; set; }

    }

    public class ImmunizationsAddress
    {
        public string Street { get; set; }
        public string Other { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

    }

    public class ImmunizationsExtendedTelephoneNumber
    {
        public string UseCode { get; set; }
        public string EquitpmentType { get; set; }
        public string EmailAddress { get; set; }
        public string AreaCode { get; set; }
        public string LocalNumber { get; set; }
        public string Extension { get; set; }

    }
    /// <summary>
    /// This data type transmits codes and the text associated with the code.
    /// </summary>
    public class ImmunizationsCodedElement
    {
        /// <summary>Identifying code.</summary>

        public string Identifier { get; set; }
        /// <summary>Human readable text that is not further used.</summary>

        public string Text { get; set; }
        /// <summary>
        /// Name of the coding system.
        /// </summary>
        public string NameOfCodingSystem { get; set; }
        /// <summary>
        /// Alternate identifying code.
        /// </summary>
        public string AlternateIdentifier { get; set; }
        /// <summary>
        /// Alternate human readable text that is not further used.
        /// </summary>
        public string AlternateText { get; set; }
        /// <summary>
        /// Alternate name of the coding system.
        /// </summary>
        public string AlternateNameOfCodingSystem { get; set; }
    }
}

