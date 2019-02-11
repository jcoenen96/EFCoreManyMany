using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Helpers
{
    public class UsernameTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsernameTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Attributes.Add("id", context.UniqueId);

            var fullName = "";
            using (var domainContext = new PrincipalContext(ContextType.Domain))
            {
                var principal = UserPrincipal.FindByIdentity(domainContext, httpContextAccessor.HttpContext.User.Identity.Name);
                if (principal != null)
                {
                    fullName = string.Format("{0} {1}", principal.GivenName, principal.Surname);
                }
            }

            output.PostContent.SetContent(fullName);
        }
    }
}
