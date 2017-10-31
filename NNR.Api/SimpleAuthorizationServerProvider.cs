using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NNR.Api
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                identity.AddClaim(new Claim("UName", user.UserName));
                identity.AddClaims(user.Roles.Select(e => new Claim(e.RoleId, e.RoleId)));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("UserID", user.Id));
            }

            context.Validated(identity);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var identity = context.Identity;
            foreach (Claim claim in identity.Claims)
            {
                context.AdditionalResponseParameters.Add(claim.Type, claim.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}