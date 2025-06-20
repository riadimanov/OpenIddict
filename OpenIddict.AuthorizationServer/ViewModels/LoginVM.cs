using Microsoft.AspNetCore.Mvc;
using OpenIddict.AuthorizationServer.Validations;
using System.ComponentModel.DataAnnotations;

namespace OpenIddict.AuthorizationServer.ViewModels
{
    [ModelMetadataType(typeof(LoginVMMetadata))]
    public class LoginVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
