using Microsoft.AspNetCore.Mvc;
using OpenIddict.AuthorizationServer.Validations;

namespace OpenIddict.AuthorizationServer.ViewModels
{
    [ModelMetadataType(typeof(AuthorizeVMMetadata))]
    public class AuthorizeVM
    {
        public string ApplicationName { get; set; }
        public string Scopes { get; set; }
        public string Button { get; set; }
    }
}
