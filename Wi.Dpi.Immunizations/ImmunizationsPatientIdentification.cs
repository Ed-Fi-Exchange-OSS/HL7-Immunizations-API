using System;
using System.Collections.Generic;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsPatientName
    {
        public bool Primary { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastSurname { get; set; }
    }

    public class ImmunizationsPatientAddress
    {
        public bool Primary { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }

    public class ImmunizationsPatientPhoneNumber
    {
        public bool Primary { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ImmunizationsPatientIdentification
    {
        public string UniqueId { get; set; }
        public int EducationOrganizationId { get; set; }
        public Guid StudentId { get; set; }
        public Guid StudentEducationOrganizationAssociationId { get; set; }
        public List<ImmunizationsPatientName> Names { get; set; } = new List<ImmunizationsPatientName>();
        public string Birthdate { get; set; }
        public string Gender { get; set; }
        public List<ImmunizationsPatientAddress> Addresses { get; set; } = new List<ImmunizationsPatientAddress>();
        public List<ImmunizationsPatientPhoneNumber> PhoneNumbers { get; set; } = new List<ImmunizationsPatientPhoneNumber>();
        public bool StudentExists { get; set; }
        public bool SsaExists { get; set; }
        public bool SeoaExists { get; set; }
        public string WirSchoolId { get; set; }

    }
}
