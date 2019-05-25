using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using static French.Tools.Extensions.AppSettings;

namespace French.Tools.Library
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly bool debugmode;
        public CustomAuthorizeAttribute() : base()
        {
            debugmode = GetBoolean("ModoAutorizacao");

        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            if (debugmode)
            {
                return true;
            }

            var isAuthorize = base.IsAuthorized(actionContext);
            return isAuthorize;

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (debugmode)
            {

                var returnValue = actionContext.ActionDescriptor.ExecuteAsync(actionContext.ControllerContext, actionContext.ActionArguments, new CancellationToken());
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return;
            }
            base.OnAuthorization(actionContext);
        }

    }
}