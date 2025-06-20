using Microsoft.AspNetCore.Mvc;
using OpenIddict.AuthorizationServer.Validations;

namespace OpenIddict.AuthorizationServer.ViewModels
{
    [ModelMetadataType(typeof(ClientCreateVMMetadata))]
    public class ClientCreateVM
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DisplayName { get; set; }
        public string RedirectUrl { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
