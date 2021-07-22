using DevExpress.DashboardWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetCore31Dashboard
{
    public class MultiTenantDashboardConfigurator : DashboardConfigurator
    {
        private readonly IHttpContextAccessor contextAccessor;

        public MultiTenantDashboardConfigurator(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;

            SetConnectionStringsProvider(new CustomConnectionStringProvider(contextAccessor));
            SetDashboardStorage(new CustomDashboardStorage(hostingEnvironment, contextAccessor));

            VerifyClientTrustLevel += MultiTenantDashboardConfigurator_VerifyClientTrustLevel;
        }

        private void MultiTenantDashboardConfigurator_VerifyClientTrustLevel(object sender, VerifyClientTrustLevelEventArgs e)
        {
            var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");

          
                e.ClientTrustLevel = ClientTrustLevel.Full;
        }
    }
}