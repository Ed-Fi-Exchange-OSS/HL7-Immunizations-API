using System;
using EdFi.Ods.Common.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using EdFi.Ods.Api.Constants;
using EdFi.Ods.Api.Models;
using EdFi.Ods.Api.Providers;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsOpenApiContentProvider : IOpenApiContentProvider
    {
        private const string Name = "Immunizations";

        public string RouteName => ImmunizationsConstants.ImmunizationsMetadataRouteName;

        public IEnumerable<OpenApiContent> GetOpenApiContent()
        {
            var assembly = GetType()
                .Assembly;

            return assembly
                .GetManifestResourceNames()
                .Where(x => x.EndsWith("Immunizations.json"))
                .Select(
                    x => new OpenApiContent(OpenApiMetadataSections.Other, Name, new Lazy<string>(() => assembly.ReadResource(x)), $"{Name}/v1", string.Empty));
        }
    }
}
