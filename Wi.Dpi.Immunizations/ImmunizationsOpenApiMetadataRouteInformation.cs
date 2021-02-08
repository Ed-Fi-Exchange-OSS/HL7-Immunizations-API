using System;
using System.Collections.Generic;
using System.Text;
using EdFi.Common.Configuration;
using EdFi.Common.Extensions;
using EdFi.Ods.Api.Constants;
using EdFi.Ods.Api.Dtos;
using EdFi.Ods.Api.Routing;
using EdFi.Ods.Common.Configuration;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsOpenApiMetadataRouteInformation : IOpenApiMetadataRouteInformation
    {
        private readonly ApiSettings _apiSettings;

        public ImmunizationsOpenApiMetadataRouteInformation(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public RouteInformation GetRouteInformation()
            => new RouteInformation
            {
                Name = ImmunizationsConstants.ImmunizationsMetadataRouteName,
                Template = CreateRoute() + "/swagger.json"
            };

        private string CreateRoute()
        {
            string prefix = $"metadata/{{other:regex(Immunizations)}}/v{ImmunizationsConstants.FeatureVersion}/";

            if (_apiSettings.GetApiMode() == ApiMode.YearSpecific)
            {
                prefix += RouteConstants.SchoolYearFromRoute;
            }

            if (_apiSettings.GetApiMode() == ApiMode.InstanceYearSpecific)
            {
                prefix += RouteConstants.InstanceIdFromRoute;
                prefix += RouteConstants.SchoolYearFromRoute;
            }

            return prefix.TrimSuffix("/");
        }
    }
}
