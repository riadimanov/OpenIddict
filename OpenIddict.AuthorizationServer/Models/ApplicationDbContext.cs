using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using System.Diagnostics.CodeAnalysis;

namespace OpenIddict.AuthorizationServer.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        // OpenIddict entities
        //public DbSet<OpenIddictEntityFrameworkCoreApplication> Applications { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreAuthorization> Authorizations { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreScope> Scopes { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreToken> Tokens { get; set; }
    }
}