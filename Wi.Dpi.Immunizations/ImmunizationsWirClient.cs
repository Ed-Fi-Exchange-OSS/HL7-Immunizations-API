using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Wi.Dpi.Immunizations.Dhs.Wir;

namespace Wi.Dpi.Immunizations
{
    public interface IImmunizationsWirClient
    {
        Task<string> RequestImmunizationsAsync(string request);
    }

    public class ImmunizationsWirClient : IImmunizationsWirClient
    {
        private readonly ImmunizationsSettings _immunizationsSettings;
        private readonly WSHttpBinding _binding;
        private readonly EndpointAddress _endpoint;
        private readonly StoreLocation _storeLocation;

        public ImmunizationsWirClient(IWebHostEnvironment env, ImmunizationsSettings immunizationsSettings)
        {
            _immunizationsSettings = immunizationsSettings;

            _storeLocation = env.EnvironmentName == "localhost"
                ? StoreLocation.CurrentUser
                : StoreLocation.LocalMachine;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _binding = new WSHttpBinding
            {
                Security =
                {
                    Mode = SecurityMode.Transport,
                    Transport = {ClientCredentialType = HttpClientCredentialType.Certificate}
                },
                TextEncoding = Encoding.Default
            };

            _endpoint = new EndpointAddress(immunizationsSettings.WirUrl);
        }

        public async Task<string> RequestImmunizationsAsync(string request)
        {
            var client = new IS_PortTypeClient(_binding, _endpoint);

            client.Endpoint.EndpointBehaviors.Add(new ImmunizationsEndpointBehavior());

            client.ClientCredentials?.ClientCertificate.SetCertificate(
                _storeLocation,
                StoreName.My,
                X509FindType.FindBySubjectName,
                _immunizationsSettings.WirClientCertificateName);

            var response = await client.submitSingleMessageAsync("WIDPI", _immunizationsSettings.WirPassword, "40442", request);

            return response.@return;
        }
    }
}
