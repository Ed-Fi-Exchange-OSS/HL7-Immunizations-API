using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using EdFi.Ods.Api.Common.Models.Resources.Student.EdFi;
using EdFi.Ods.Api.Common.Models.Resources.StudentEducationOrganizationAssociation.EdFi;


namespace Wi.Dpi.Immunizations
{
    public interface IImmunizationsResponseProvider
    {
        ImmunizationsStudentResponse GetImmunizationsResponse(string message, ImmunizationsPatientIdentification patient);
    }

    public class ImmunizationsResponseProvider : IImmunizationsResponseProvider
    {
        public const string SegmentDelimiter = "(?<!\\\\)\r";
        public const string FieldDelimiter = "(?<!\\\\)\\|";
        public const string PartDelimiter = "(?<!\\\\)\\^";

        private static List<string> GetSegments(string message) => Regex.Split(message, SegmentDelimiter).ToList();
        private static List<string> GetFields(string segment) => Regex.Split(segment, FieldDelimiter).ToList();
        private static List<string> GetParts(string field) => Regex.Split(field, PartDelimiter).ToList();

        public static class MessageType
        {
            public const string MessageHeader = "MSH";
            public const string MessageAcknowledgement = "MSA";
            public const string Error = "ERR";
            public const string PatientIdentification = "PID";
            //public const string PatientAdditionalDemographic = "PD1";
            //public const string NextOfKinAssociatedParties = "NK1";
            public const string PharmacyTreatmentAdministration = "RXA";
            public const string OrderRequest = "ORC";
            //public const string PharmacyTreatmentRoute = "RXR";
            //public const string ObservationResult = "OBX";
        }

        public static class MessageProfile
        {
            public const string ListofCanidates = "Z31^CDCPHINVS";
            public const string CompleteImmunizationHistory = "Z32^CDCPHINVS";
            public const string NoPersonRecords = "Z33^CDCPHINVS";
        }

        public static class AcknowledgmentCode
        {
            public const string Accept = "AA";
            public const string Error = "AE";
            public const string Reject = "AR";
        }

        public ImmunizationsStudentResponse GetImmunizationsResponse(string message, ImmunizationsPatientIdentification patient)
        {
            var result = new ImmunizationsStudentResponse();
            var segments = GetSegments(message);

            result.Data = new ImmunizationsStudentHistory
            {
                StudentReference = patient.StudentExists == false ? null : new StudentReference
                {
                    StudentUniqueId = patient.UniqueId,
                    ResourceId = patient.StudentId
                },
                StudentEducationOrganizationAssociationReference = patient.SeoaExists == false ? null : new StudentEducationOrganizationAssociationReference
                {
                    StudentUniqueId = patient.UniqueId,
                    EducationOrganizationId = patient.EducationOrganizationId,
                    ResourceId = patient.StudentEducationOrganizationAssociationId
                }
            };

            foreach (var segment in segments)
            {
                var fields = GetFields(segment);
                var messageType = fields[0];

                switch (messageType)
                {
                    case MessageType.MessageHeader:
                        var profile = fields[MshMessageProfileIdentifier];
                        switch (profile)
                        {
                            case MessageProfile.CompleteImmunizationHistory:
                                result.StatusCode = ImmunizationsStatusCode.Success;
                                break;
                            case MessageProfile.ListofCanidates:
                                result.StatusCode = ImmunizationsStatusCode.MultipleMatches;
                                break;
                            case MessageProfile.NoPersonRecords:
                                result.StatusCode = ImmunizationsStatusCode.NotFound;
                                break;
                        }
                        break;
                    case MessageType.MessageAcknowledgement:
                        var acknowledgmentCode = fields[MsaAcknowledgmentCode];
                        if(acknowledgmentCode == AcknowledgmentCode.Reject)
                        {
                            result.StatusCode = ImmunizationsStatusCode.Reject;
                        }
                        break;
                    case MessageType.PatientIdentification:
                        var patientIdentification = GetParts(fields[PidOrdinalInternalId]);
                        result.Data.PatientIdentifier.Id = patientIdentification.Count > 0 ? patientIdentification[0] : null;
                        result.Data.PatientIdentifier.AssigningAuthority = patientIdentification.Count > 3 ? patientIdentification[3] : null;
                        result.Data.PatientIdentifier.IdentifierTypeCode = patientIdentification.Count > 4 ? patientIdentification[4] : null;
                        
                        var name = GetParts(fields[PidOrdinalPatientName]);
                        result.Data.PatientIdentifier.Name = GetName(name);

                        var mothersMaidenName = GetParts(fields[PidOrdinalMothersMaidenName]);
                        result.Data.PatientIdentifier.MothersMaidenName = GetMaidenName(mothersMaidenName);

                        result.Data.PatientIdentifier.BirthDate = fields.Count > PidOrdinalBirthDate ? ToEdFiDate(fields[PidOrdinalBirthDate]) : null; 
                        result.Data.PatientIdentifier.Sex = fields.Count > PidOrdinalSex ? fields[PidOrdinalSex] : null; 
                        result.Data.PatientIdentifier.Race = fields.Count > PidOrdinalRace ? fields[PidOrdinalRace] : null;

                        var address = fields[PidOrdinalAddress];
                        result.Data.PatientIdentifier.Address = GetAddress(GetParts(address));

                        //result.Data.PatientIdentifier.EthnicGroup = GetCodedElement(GetParts(fields[PidOrdinalEthnicGroup]));
                        result.Data.PatientIdentifier.MultipleBirthIndicator = ToBool(fields[PidOrdinalMultipleBirthIndicator]);
                        result.Data.PatientIdentifier.BirthOrder = fields[PidOrdinalBirthOrder]; 
                        //result.Data.PatientIdentifier.Citizenship = GetCodedElement(GetParts(fields[PidOrdinalCitizenship]));

                        var telephone = fields[PidOrdinalPhoneNumber];
                        result.Data.PatientIdentifier.ExtendedTelephoneNumber = GetTelephoneNumber(GetParts(telephone));


                        break;
                    case MessageType.PharmacyTreatmentAdministration:
                        var pharmacyTreatmentAdministration = GetPharmacyTreatmentAdministration(fields);
                        if (pharmacyTreatmentAdministration.CompletionStatus != CompletionStatus.Refusal)
                            result.Data.PharmacyTreatmentAdministrations.Add(pharmacyTreatmentAdministration);
                        break;
                    case MessageType.Error:
                        result.Errors.Add(new ImmunizationsError
                        {
                            Id = "100106",
                            Source = "WiR",
                            Error = segment,
                            ErrorMessage = fields.Count > 8 ? fields[8] : null
                        });
                        break;
                }
            }

            if (result.StatusCode != ImmunizationsStatusCode.Success)
            {
                result.Data.PatientIdentifier = null;
                result.Data.PharmacyTreatmentAdministrations = null;
            }

            result.Data.PharmacyTreatmentAdministrations = result.Data.PharmacyTreatmentAdministrations?.OrderByDescending(p => p.AdministeredDate).ToList();

            return result;
        }
        public const int MshMessageProfileIdentifier = 20;
        public const int MsaAcknowledgmentCode = 1;

        public const int XadOrdinalStreet = 0;
        public const int XadOrdinalOther = 1;
        public const int XadOrdinalCity = 2;
        public const int XadOrdinalState = 3;
        public const int XadOrdinalZip = 4;
        public const int XadOrdinalCountry = 5;

        public const int XpnOrdinalLastName = 0;
        public const int XpnOrdinalFirstName = 1;
        public const int XpnOrdinalSecondName = 2;
        public const int XpnOrdinalSuffix = 3;
        public const int XpnOrdinalPrefix = 4;

        public const int PidOrdinalInternalId = 3;
        public const int PidOrdinalPatientName = 5;
        public const int PidOrdinalMothersMaidenName = 6;
        public const int PidOrdinalBirthDate = 7;
        public const int PidOrdinalSex = 8;         //value set 0001
        public const int PidOrdinalRace = 10;       //value set 0005
        public const int PidOrdinalAddress = 11;
        public const int PidOrdinalPhoneNumber = 13;
        //public const int PidOrdinalEthnicGroup = 22; //CDCREC
        public const int PidOrdinalMultipleBirthIndicator = 24; //value set 0136
        public const int PidOrdinalBirthOrder = 25;
        //public const int PidOrdinalCitizenship = 26; //value set 0171
        public const int RxaOrdinalAdministeredDate = 3;
        public const int RxaOrdinalAdministeredCode = 5;
        public const int RxaOrdinalSubstanceRefusalReason = 18;
        public const int RxaOrdinalCompletionStatus = 20;

        public static class CompletionStatus
        {
            public const string Complete = "CP";
            public const string Partial = "PA";
            public const string Refusal = "RE";
        }
        private static ImmunizationsPharmacyTreatmentAdministration GetPharmacyTreatmentAdministration(List<string> fields)
        {
            var pharmacyTreatmentAdministration = new ImmunizationsPharmacyTreatmentAdministration
            {
                AdministeredDate = ToEdFiDate(fields[RxaOrdinalAdministeredDate]),
                AdministeredCode = GetCodedElement(GetParts(fields[RxaOrdinalAdministeredCode])),
                Waiver = fields[RxaOrdinalSubstanceRefusalReason],
                CompletionStatus = fields[RxaOrdinalCompletionStatus]
            };
            return pharmacyTreatmentAdministration;
        }

        private static DateTime? ToEdFiDate(string d) =>
            DateTime.TryParseExact(d, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                ? (DateTime?)date
                : null;

        private static bool ToBool(string d) => d == "Y";

        private static ImmunizationsCodedElement GetCodedElement(List<string> parts)
        {
            var element = parts.Count > 5
                ? new ImmunizationsCodedElement
                {
                    Identifier = parts[0],
                    Text = parts[1],
                    NameOfCodingSystem = parts[2],
                    AlternateIdentifier = parts[3],
                    AlternateText = parts[4],
                    AlternateNameOfCodingSystem = parts[5]
                }
                : null;
            return element;
        }

        private static ImmunizationsName GetName(List<string> parts)
        {
            var element = parts.Count > XpnOrdinalPrefix
                ? new ImmunizationsName
                {
                    LastSurname = parts[XpnOrdinalLastName],
                    FirstName = parts[XpnOrdinalFirstName],
                    MiddleName = parts[XpnOrdinalSecondName],
                    GenerationCodeSuffix = parts[XpnOrdinalSuffix],
                    Prefix = parts[XpnOrdinalPrefix]
                }
                : null;
            return element;
        }

        private static ImmunizationsMaidenName GetMaidenName(List<string> parts)
        {
            var element = parts.Count > XpnOrdinalFirstName
                ? new ImmunizationsMaidenName
                {
                    LastSurname = parts[XpnOrdinalLastName],
                    FirstName = parts[XpnOrdinalFirstName]
                }
                : null;
            return element;
        }

        private static ImmunizationsAddress GetAddress(List<string> parts)
        {
            var element = parts.Count > XadOrdinalCountry
                ? new ImmunizationsAddress
                {
                    Street = parts[XadOrdinalStreet],
                    Other = parts[XadOrdinalOther],
                    City = parts[XadOrdinalCity],
                    State = parts[XadOrdinalState],
                    Zip = parts[XadOrdinalZip],
                    Country = parts[XadOrdinalCountry]
                }
                : null;
            return element;
        }

        private static ImmunizationsExtendedTelephoneNumber GetTelephoneNumber(List<string> parts)
        {
            var element = parts.Count > 7
                ? new ImmunizationsExtendedTelephoneNumber
                {
                    UseCode = parts[1],
                    EquitpmentType = parts[2],
                    EmailAddress = parts[3],
                    AreaCode = parts[5],
                    LocalNumber = parts[6],
                    Extension = parts[7]
        }
                : null;
            return element;
        }
    }
}
