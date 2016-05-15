using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Caerus.Common.Web.MVC.Attributes;

namespace Caerus.Common.Web.MVC.Controllers
{
    [CaerusAuthorize]
    public class ControllerLessController : System.Web.Mvc.Controller
    {
        public virtual ActionResult Index()
        {
            var action = RouteData.Values["x-action"].ToString();
            var controller = RouteData.Values["x-controller"].ToString();
            RouteData.Values["action"] = action;
            RouteData.Values["controller"] = controller;
            if (RouteData.Values["area"] != null)
            {
                RouteData.DataTokens["area"] = RouteData.Values["area"].ToString();
            }

            return View(action);
        }
    }

    public sealed class ControllerLessRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Gets the IRouteHandler for the current request.
        /// </summary>
        /// <param name="requestContext">The RequestContext for the current request.</param>
        /// <returns>The IRouteHandler for the current request.</returns>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new ControllerLessHttpHandler(requestContext);
        }
    }

    public class ControllerLessHttpHandler : IHttpHandler
    {
        /// <summary>
        /// The request context.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        /// The configuration settings.
        /// </summary>
        private readonly RouteConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerLessHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The requestContext to be used in this instance.</param>
        public ControllerLessHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
            _configuration = RouteConfiguration.GetConfigurationSettings();

            var target = typeof(ControllerLessHttpHandler).Namespace;
            if (!ControllerBuilder.Current.DefaultNamespaces.Contains(target))
            {
                ControllerBuilder.Current.DefaultNamespaces.Add(target);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this class is reusable.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Process the current HTTP request.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing the request.</param>
        public void ProcessRequest(HttpContext httpContext)
        {
            var controller = _requestContext.RouteData.GetRequiredString("controller");
            var action = string.Empty;

            if (_requestContext.RouteData.Values["action"] != null)
            {
                action = _requestContext.RouteData.Values["action"].ToString();
            }

            if (action != string.Empty)
            {
                IController viewController = null;
                IControllerFactory controllerFactory = null;

                try
                {
                    controllerFactory = ControllerBuilder.Current.GetControllerFactory();

                    try
                    {
                        viewController = controllerFactory.CreateController(_requestContext, controller);
                        viewController.Execute(_requestContext);
                    }
                    catch
                    {
                        DispatchRequest(controllerFactory, controller, action);
                    }
                }
                finally
                {
                    if (controllerFactory != null)
                    {
                        controllerFactory.ReleaseController(viewController);
                    }
                }
            }
        }

        /// <summary>
        /// Dispatches the request.
        /// </summary>
        /// <param name="controllerFactory">The controller factory.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        private void DispatchRequest(IControllerFactory controllerFactory, string controller, string action)
        {
            var route = GetRoute(controller, action);
            _requestContext.RouteData.Values["x-action"] = action;
            _requestContext.RouteData.Values["x-controller"] = controller;

            if (route != null)
            {
                _requestContext.RouteData.Values["controller"] = route.Controller;
                _requestContext.RouteData.Values["action"] = route.Action;

                if (route.Area != string.Empty)
                {
                    _requestContext.RouteData.DataTokens["area"] = route.Area;
                }

                controller = route.Controller;
            }
            else
            {
                _requestContext.RouteData.Values["action"] = _configuration.DefaultAction;
                controller = _configuration.DefaultController;
            }

            var viewController = controllerFactory.CreateController(_requestContext, controller);
            if (viewController != null)
            {
                viewController.Execute(_requestContext);
            }
        }

        /// <summary>
        /// Gets the configured route.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns>The configured route (or null if the route is not configured).</returns>
        private RouteElement GetRoute(string controller, string action)
        {
            RouteElement route;

            if (_requestContext.RouteData.Values["area"] != null)
            {
                var area = _requestContext.RouteData.Values["area"].ToString();
                route = _configuration.Get(string.Format("/{0}/{1}/{2}", area, controller, action));
            }
            else
            {
                route = _configuration.Get(string.Format("/{0}/{1}", controller, action));
            }

            return route;
        }
    }

    public class RouteConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the default controller.
        /// </summary>
        /// <value>
        /// The default controller.
        /// </value>
        [ConfigurationProperty("defaultController", DefaultValue = "ControllerLess", IsRequired = false)]
        public string DefaultController
        {
            get
            {
                return (string)this["defaultController"];
            }

            set
            {
                this["defaultController"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default action.
        /// </summary>
        /// <value>
        /// The default action.
        /// </value>
        [ConfigurationProperty("defaultAction", DefaultValue = "Index", IsRequired = false)]
        public string DefaultAction
        {
            get
            {
                return (string)this["defaultAction"];
            }

            set
            {
                this["defaultAction"] = value;
            }
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        [ConfigurationProperty("routes")]
        public RouteElementCollection Routes
        {
            get
            {
                return this["routes"] as RouteElementCollection;
            }
        }

        /// <summary>
        /// Gets the configuration settings.
        /// </summary>
        /// <returns>The configuration settings.</returns>
        public static RouteConfiguration GetConfigurationSettings()
        {
            var configuration = (RouteConfiguration)ConfigurationManager.GetSection("controllerLessSettings");

            if (configuration == null)
            {
                configuration = new RouteConfiguration();
            }

            return configuration;
        }

        /// <summary>
        /// Gets route for the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The matched route element if found, otherwise null.</returns>
        public RouteElement Get(string url)
        {
            return Routes.Cast<RouteElement>().FirstOrDefault(element => element.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class RouteElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            object value = null;
            var property = typeof(RouteElement).GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name == "Url");
            if (property != null)
            {
                value = property.GetValue(element, BindingFlags.Instance, null, null, null);
            }

            return value;
        }
    }

    public class RouteElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the route URL.
        /// </summary>
        /// <value>
        /// The route URL.
        /// </value>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }

            set
            {
                this["url"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        [ConfigurationProperty("area", DefaultValue = "", IsRequired = false)]
        public string Area
        {
            get
            {
                return (string)this["area"];
            }

            set
            {
                this["area"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        [ConfigurationProperty("controller", DefaultValue = "ControllerLess", IsRequired = false)]
        public string Controller
        {
            get
            {
                return (string)this["controller"];
            }

            set
            {
                this["controller"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [ConfigurationProperty("action", DefaultValue = "Index", IsRequired = false)]
        public string Action
        {
            get
            {
                return (string)this["action"];
            }

            set
            {
                this["action"] = value;
            }
        }
    }
}
