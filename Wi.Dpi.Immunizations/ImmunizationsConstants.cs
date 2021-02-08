using EdFi.Ods.Common.Constants;
using EdFi.Ods.Common.Conventions;

namespace Wi.Dpi.Immunizations
{
    public static class ImmunizationsConstants
    {
        public const string FeatureName = "Immunizations";

        public const string FeatureVersion = "1";

        public static readonly string ImmunizationsMetadataRouteName = EdFiConventions.GetOpenApiMetadataRouteName(FeatureName);

        public static readonly string ImmunizationsRoutePrefix = $"immunizations/v{FeatureVersion}";

        public static readonly ApiFeature Immunizations = new ApiFeature("immunizations", "Immunizations");

    }
}
