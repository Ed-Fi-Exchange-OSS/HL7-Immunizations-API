using System.Configuration;
using System.Linq;

namespace Wi.Dpi.Immunizations
{
    public interface IImmunizationsRequestProvider
    {
        string GetImmunizationsRequest(ImmunizationsPatientIdentification patient);
    }

    public class ImmunizationsRequestProvider : IImmunizationsRequestProvider
    {
        public string GetImmunizationsRequest(ImmunizationsPatientIdentification patient)
        {
            var name = patient.Names.First();
            var address = patient.Addresses.First();

            //DPI WiRId in DEV is 40442
            var environment = ConfigurationManager.AppSettings["Environment"];
            var wirOrgId = environment?.StartsWith("Prod") == true ? patient.WirSchoolId : "40442";

            var headerMessage = $"MSH|^~\\&|WIDPI|{wirOrgId}|WIR||20200407123605-0600||QBP^Q11^QBP_Q11|20200407-A1|P|2.5.1|||ER|AL|||||Z34^CDCPHINVS|\r\n";
            var queryMessage = $"QPD|Z34^Request PharmacyTreatmentAdministration History^CDCPHINVS|DPI{patient.UniqueId}||{name.LastSurname}^{name.FirstName}^{name.MiddleName}^^^^L||{patient.Birthdate}|{patient.Gender}|{address.Street}^^{address.City}^{address.State}^{address.Zipcode}^USA^M||\r\n";
            var requestCount = "RCP|I|5^RD&records&HL70126";

            return $"{headerMessage}{queryMessage}{requestCount}";
        }
    }
}
