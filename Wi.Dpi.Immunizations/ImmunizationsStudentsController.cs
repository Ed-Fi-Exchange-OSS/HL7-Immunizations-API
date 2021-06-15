using log4net;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EdFi.Ods.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wi.Dpi.Immunizations
{
    [Description("Find student immunization records")]
    [Authorize/*(Policy = "immunizations")*/]
    [ApiController]
    [Route("immunizations/students")]
    [Produces("application/json")]
    [DisplayName("Immunizations")]
    public class ImmunizationsStudentsController : ControllerBase
    {
        private ILog _logger;
        private readonly IImmunizationsPatientIdentificationProvider _immunizationsPatientIdentificationProvider;
        private readonly IImmunizationsRequestProvider _immunizationsRequestProvider;
        private readonly IImmunizationsResponseProvider _immunizationsResponseProvider;
        private readonly IImmunizationsWirClient _immunizationsWirClient;
        private readonly IImmunizationsWirSchoolProvider _immunizationsWirSchoolProvider;
        private readonly IApiKeyContextProvider _apiKeyContextProvider;

        protected ILog Logger => _logger ??= LogManager.GetLogger(GetType());

        public ImmunizationsStudentsController(
            IImmunizationsPatientIdentificationProvider immunizationsPatientIdentificationProvider,
            IImmunizationsRequestProvider immunizationsRequestProvider,
            IImmunizationsResponseProvider immunizationsResponseProvider,
            IImmunizationsWirClient immunizationsWirClient,
             IImmunizationsWirSchoolProvider immunizationsWirSchoolProvider,
            IApiKeyContextProvider apiKeyContextProvider)
        {
            _immunizationsPatientIdentificationProvider = immunizationsPatientIdentificationProvider;
            _immunizationsRequestProvider = immunizationsRequestProvider;
            _immunizationsResponseProvider = immunizationsResponseProvider;
            _immunizationsWirClient = immunizationsWirClient;
             _immunizationsWirSchoolProvider = immunizationsWirSchoolProvider;
            _apiKeyContextProvider = apiKeyContextProvider;
        }


        ///<summary>Returns a set of immunization history for a student</summary>
        /// <param name="uniqueId">Unique Id of the person to be retrieved.</param>
        /// <response code="200">The requested resource was successfully retrieved.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ImmunizationsStudentResponse),StatusCodes.Status200OK)]
        [Route("{uniqueId}")]
        public async Task<IActionResult> Get([FromRoute(Name = "uniqueId")] string uniqueId)
        {
            try
            {
                if(uniqueId.Length != 10 || !long.TryParse(uniqueId, out _))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { Message = "The UniqueId was not valid."});
                }

                var localEducationOrganizationIds = _apiKeyContextProvider.GetApiKeyContext().EducationOrganizationIds.ToArray();
                
                var wirSchools = _immunizationsWirSchoolProvider.GetWirSchools(localEducationOrganizationIds);
                if (!wirSchools.Any())
                {
                    return StatusCode((int)HttpStatusCode.ServiceUnavailable,
                        new { Message = "DHS school code missing, immunization service unavailable. Submit a ticket to DPI Customer Service with questions https://dpi.wi.gov/wisedash/help/ticket"});
                }

                var patient = _immunizationsPatientIdentificationProvider.GetPatientIdentification(uniqueId, localEducationOrganizationIds);

                if (!patient.StudentExists || !patient.SsaExists)
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { Message = $"The student does not exist or this ApiKey is not Authorized to access the student"});
                }

                var request = _immunizationsRequestProvider.GetImmunizationsRequest(patient);
                var response = await _immunizationsWirClient.RequestImmunizationsAsync(request);
                var result = _immunizationsResponseProvider.GetImmunizationsResponse(response, patient);

                if (result.Errors != null && result.Errors.Any())
                {
                    var errors = string.Join(",", result.Errors.Select(e => e.Error));
                    Logger.Error($"ImmunizationsStudentsController GET WiR Error(s): {errors}");
                    var errorMessage = string.Join(",", result.Errors.Select(e => e.ErrorMessage)); 
                    result.Errors = null;
                    return StatusCode((int)HttpStatusCode.BadGateway, new { Message = $"The student immunization records cannot be retrieved because of a WIR error. Please login to the WIR to resolve the issue or contact DPI to help investigate. The error message WIR returned was {errorMessage}" });
                }
                result.Errors = null;

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.Error("ImmunizationsStudentsController GET FaultException", exception);
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, new { Message = $"There was a FaultException connecting to DHS WiR: {exception}"});
            }
        }
    }
}
