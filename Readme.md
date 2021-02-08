# HL7 Immunizations API

The WI Department of Public instruction partnered with the WI Department of Health Services to integrate the WI Immunization Registry's (WIR) HL7 compliant web service into the Ed-Fi API.  The service uses a student's uniqueId to craft an HL7 compliant request for immunization data, using only authorized data, submits that request to the WIR, and returns the results within the Ed-Fi API.  

This service will NOT work out of the box, but can be used as a starting point for integration with an HL7 compliant service.   

## Getting Started
### Ed-Fi API changes
Add the Wi.Dpi.Immunizations project to your Ed-Fi solution

Modify EdFi.Ods.Api.Startup.OdsStartupBase class to add an overrideable method to register the immunization service:
```
	protected virtual void AddSettingsHook(IServiceCollection services) { } 
```
and call this in ConfigureServices.

Modify EdFi.Ods.WebApi.Startup to bind the immunization settings and override the AddSettingsHook to register the ImmunizationsSettings:
```
public class Startup : OdsStartupBase
    {
        public ImmunizationsSettings ImmunizationsSettings { get; } = new ImmunizationsSettings();

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
            : base(env, configuration)
        {
            configuration.Bind("ImmunizationsSettings", ImmunizationsSettings);
        }

        protected override void AddSettingsHook(IServiceCollection services)
        {
            services.AddSingleton(ImmunizationsSettings);
        }
    }
```

In appsettings.json:

	Add to ApiSetting.Features:
```
	{
        "Name": "Immunizations",
        "IsEnabled": true
    }
```	
	Add:
```	
	"ImmunizationsSettings": {
		"WirUrl": "URL_TO_HL7_WCF_SERVICE",
		"WirClientCertificateName": "YOUR_CERT_HERE",
		"WirPassword": "YOUR_PWD_HERE"
	}
```

### Project files which require particular review/revision:
ImmunizationsImmunizationsPatientIdentificationProvider:
	The query for retrieving student data should be reviewed to account for address and identification code descriptors as well as any localized logic or data format expectations. 
ImmunizationsRequestProvider:
	The message header should be updated based upon your service's requirements.  The query portion should also be reviewed and updated to conform to local requirements. 
ImmunizationsWirClient:
	The username and facilityID must be updated to your service's requirements.
ImmunizationsWirSchoolProvider:
	For each school which requires access to the service, an appropriate ID code for the service must exist in  EducationOrganizationIdenficationCodes.  The appropriate descriptorId for your instance can be set within the SQL.
	

## Copyright
Copyright 2020 Wisconsin Department of Public Instruction.

## License
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.