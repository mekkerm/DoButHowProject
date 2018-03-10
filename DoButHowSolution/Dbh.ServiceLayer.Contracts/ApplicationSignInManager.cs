
using Dbh.Model.EF.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace Dbh.ServiceLayer.Contracts
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes) :base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
                                                                                                                                                                                                                                                                            //base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            Console.WriteLine("Application signing manager constructor");
        }
    }
}
