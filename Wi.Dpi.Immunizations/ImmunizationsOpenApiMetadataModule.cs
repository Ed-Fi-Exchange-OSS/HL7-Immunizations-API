using Autofac;
using EdFi.Ods.Api.Providers;
using EdFi.Ods.Api.Routing;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Constants;
using EdFi.Ods.Common.Container;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Wi.Dpi.Immunizations
{
    public class EnabledImmunizationsOpenApiMetadataModule : ConditionalModule
    {
        public EnabledImmunizationsOpenApiMetadataModule(ApiSettings apiSettings)
            : base(apiSettings, nameof(EnabledImmunizationsOpenApiMetadataModule)) { }

        public override bool IsSelected()
            => IsFeatureEnabled(ImmunizationsConstants.Immunizations)
               && IsFeatureEnabled(ApiFeature.OpenApiMetadata);

        public override void ApplyConfigurationSpecificRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<ImmunizationsOpenApiContentProvider>()
                .As<IOpenApiContentProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ImmunizationsOpenApiMetadataRouteInformation>()
                .As<IOpenApiMetadataRouteInformation>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ImmunizationsControllerRouteConvention>()
                .As<IApplicationModelConvention>()
                .SingleInstance();
        }
    }
}