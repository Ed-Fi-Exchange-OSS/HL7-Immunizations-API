using Autofac;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Constants;
using EdFi.Ods.Common.Container;
using Microsoft.Extensions.Configuration;

namespace Wi.Dpi.Immunizations
{
    public class ImmunizationsModule : ConditionalModule
    {
        public ImmunizationsModule(ApiSettings apiSettings) : base(apiSettings, nameof(ImmunizationsModule)) { }

        public override bool IsSelected() => IsFeatureEnabled(ImmunizationsConstants.Immunizations);

        public override void ApplyConfigurationSpecificRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<ImmunizationsImmunizationsPatientIdentificationProvider>()
                .As<IImmunizationsPatientIdentificationProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImmunizationsRequestProvider>()
                .As<IImmunizationsRequestProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImmunizationsResponseProvider>()
                .As<IImmunizationsResponseProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImmunizationsWirClient>()
                .As<IImmunizationsWirClient>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImmunizationsWirSchoolProvider>()
                .As<IImmunizationsWirSchoolProvider>()
                .InstancePerLifetimeScope();
        }
    }
}