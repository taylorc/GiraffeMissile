using System.Collections.Generic;
using Ardalis.ApiEndpoints;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.WebUI.Controllers.OidcConfiguration
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OidcConfigurationEndpoint : BaseEndpoint
        .WithRequest<string>
        .WithResponse<IDictionary<string, string>>
    {
        private readonly IClientRequestParametersProvider _clientRequestParametersProvider;
        private readonly ILogger<OidcConfigurationEndpoint> _logger;

        public OidcConfigurationEndpoint(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationEndpoint> logger)
{
            _clientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        [HttpGet("_configuration/{request}")]
        public override ActionResult<IDictionary<string, string>> Handle(string request)
        {
            var parameters = _clientRequestParametersProvider.GetClientParameters(HttpContext, request);
            return Ok(parameters);
        }
    }
}
