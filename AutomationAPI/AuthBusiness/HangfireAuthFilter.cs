using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace AutomationAPI.AuthBusiness
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
