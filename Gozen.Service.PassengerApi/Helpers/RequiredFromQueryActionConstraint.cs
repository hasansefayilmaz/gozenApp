using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Gozen.Service.Passenger.Api.Helpers
{
    public class RequiredFromQueryActionConstraint : IActionConstraint
    {
        private readonly string _parameter;

        public RequiredFromQueryActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter);
        }
    }
}
