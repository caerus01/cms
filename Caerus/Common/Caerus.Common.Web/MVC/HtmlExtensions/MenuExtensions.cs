using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Caerus.Common.Web.MVC.HtmlExtensions
{
    public static class MenuExtensions
    {
        public static IHtmlString Menu(this HtmlHelper helper)
        {
            var sb = new StringBuilder();

            sb.Append("<div class='navbar-collapse'>");
            sb.Append("<ul class='nav nav-pills navbar-nav full-width text-center'>");
            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                var menuItemRoles = node.Roles;
                if (!IsNodeVisible(node.Roles, HttpContext.Current.User))
                    continue;

                var childNodes = HasAnyChildNodes(node, HttpContext.Current.User, node.Roles);

                sb.AppendLine(string.Format("<li ng-class=\"{{'dropdown' : {1}, 'single': {2}, active:isActive('{0}')}}\" >", node.Url, childNodes.ToString().ToLower(), (!childNodes).ToString().ToLower()));
                sb.AppendLine(string.Format("<a href=\"#{0}\" ng-class=\"{{'btn': true,'btn-link': true,'dropdown-toggle':{1}}}\">{2}<i>{3}</i></a>", node.Url, childNodes.ToString().ToLower(), node.Title, node.Description));
                if (childNodes)
                    sb.Append(RenderSitemapNode(node, node.Roles));
                sb.AppendLine("</li>");
            }

            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        public static IHtmlString MobileMenu(this HtmlHelper helper)
        {
            var sb = new StringBuilder();

            sb.Append("<div class='buttons-container'>");
            sb.Append("<select class='full-width'>");
            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                var menuItemRoles = node["quickroles"];

                var roles = !string.IsNullOrEmpty(menuItemRoles) ? menuItemRoles.Split(',').Select(c => c).ToList() : new List<string>();
                foreach (var role in node.Roles)
                {
                    if (!roles.Contains(role.ToString()))
                        roles.Add(role.ToString());
                }
                if (!IsNodeVisible(roles, HttpContext.Current.User))
                    continue;
                sb.AppendLine(string.Format("<option value='#{0}' selected='isActive({0})'>{1}</option>", node.Url, node.Title));

                var childNodes = HasAnyChildNodes(node, HttpContext.Current.User, roles);
                if (childNodes)
                    sb.Append(RenderMobileSitemapNode(node, roles));
            }

            sb.AppendLine("</select>");
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }


        public static IHtmlString QuickMenu(this HtmlHelper helper)
        {
            var sb = new StringBuilder();

            sb.Append("<ul class='dropdown-menu full-width loggedinMenu' role='menu'>");
            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                var menuItemRoles = node["quickroles"];
                if (string.IsNullOrEmpty(menuItemRoles))
                    continue;
                var roles = menuItemRoles.Split(',').Select(c => c).ToList();

                if (!IsNodeVisible(roles, HttpContext.Current.User))
                    continue;


                sb.AppendLine(string.Format("<li>"));
                sb.AppendLine(string.Format("<a ng-href=\"#{0}\"><i class='{2}'></i> {1}</a>", node.Url, node.Title, node["css"]));
                var childNodes = HasAnyChildNodes(node, HttpContext.Current.User, roles);
                if (childNodes)
                    sb.Append(RenderQuickSitemapNode(node, roles));
                sb.AppendLine("</li>");
                if (childNodes)
                    sb.AppendLine("<li class='divider'></li>");
            }
            sb.AppendLine("<li class='divider'></li>");
            sb.AppendLine("<li class='logoutlink'><button type='submit' name='name' value='' class='btn btn-danger full-width'><i class='icon-signout'></i>Log Off</button></li>");
            sb.AppendLine("</ul>");

            return MvcHtmlString.Create(sb.ToString());
        }


        private static bool IsNodeVisible(IList Roles, IPrincipal User)
        {
            bool isVisible = false;
            foreach (var role in Roles)
            {
                // Show Anonymous Nodes
                if (role.ToString().ToLowerInvariant() == "anonymous" && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated == false)
                {
                    isVisible = true; break;
                }

                // Show Global Nodes
                if (role.ToString().ToLowerInvariant() == "all")
                {
                    isVisible = true; break;
                }

                // Security Trimmings
                if (HttpContext.Current.User != null && HttpContext.Current.User.IsInRole(role.ToString()))
                {
                    isVisible = true; break;
                }
            }
            return isVisible;
        }


        private static bool HasAnyChildNodes(SiteMapNode currentNode, IPrincipal User, IList roles)
        {
            if (!currentNode.HasChildNodes)
                return false;

            foreach (SiteMapNode node in currentNode.ChildNodes)
            {
                if (IsNodeVisible(node.Roles, User))
                    return true;
            }
            return false;
        }


        private static string RenderSitemapNode(SiteMapNode currentNode, IList roles)
        {
            var sb = new StringBuilder();
            if (!IsNodeVisible(currentNode.Roles, HttpContext.Current.User))
                return "";
            var hasChildNodes = HasAnyChildNodes(currentNode, HttpContext.Current.User, roles);
            sb.Append(string.Format("<ul class='{0}'>", hasChildNodes ? "dropdown-menu" : ""));
            var childNodes = currentNode.ChildNodes;

            foreach (SiteMapNode node in childNodes)
            {
                if (!IsNodeVisible(node.Roles, HttpContext.Current.User))
                    continue;
                sb.AppendLine("<li>");
                sb.AppendFormat("<a href='#{0}'>{1}</a>", node.Url, node.Title);
                if (hasChildNodes)
                    sb.Append(RenderSitemapNode(node, node.Roles));

                sb.AppendLine("</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }

        private static string RenderQuickSitemapNode(SiteMapNode currentNode, IList roles)
        {
            var sb = new StringBuilder();
            var hasChildNodes = HasAnyChildNodes(currentNode, HttpContext.Current.User, roles);
            var childNodes = currentNode.ChildNodes;

            foreach (SiteMapNode node in childNodes)
            {
                var menuItemRoles = node["quickroles"];
                if (string.IsNullOrEmpty(menuItemRoles))
                    continue;
                var childroles = menuItemRoles.Split(',').Select(c => c).ToList();
                if (!IsNodeVisible(childroles, HttpContext.Current.User))
                    continue;

                sb.AppendLine("<li>");
                sb.AppendLine(string.Format("<a ng-href=\"#{0}\"><i class='{2}'></i> {1}</a>", node.Url, node.Title, node["css"]));
                if (hasChildNodes)
                    sb.Append(RenderSitemapNode(node, node.Roles));
                sb.AppendLine("</li>");
            }
            return sb.ToString();
        }

        private static string RenderMobileSitemapNode(SiteMapNode currentNode, IList roles)
        {
            var sb = new StringBuilder();
            var hasChildNodes = HasAnyChildNodes(currentNode, HttpContext.Current.User, roles);
            var childNodes = currentNode.ChildNodes;
            foreach (SiteMapNode node in childNodes)
            {
                var menuItemRoles = node["quickroles"];

                var childroles = !string.IsNullOrEmpty(menuItemRoles) ? menuItemRoles.Split(',').Select(c => c).ToList() : new List<string>();
                foreach (var role in node.Roles)
                {
                    if (!childroles.Contains(role.ToString()))
                        childroles.Add(role.ToString());
                }

                if (!IsNodeVisible(childroles, HttpContext.Current.User))
                    continue;

                sb.AppendLine(string.Format("<option value='#{0}' selected='isActive({0})'>{1}</option>", node.Url, node.Title));
                if (hasChildNodes)
                    sb.Append(RenderMobileSitemapNode(node, node.Roles));
            }
            return sb.ToString();
        }
    }

    public class SystemSiteMapProvider : XmlSiteMapProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            var sitemapFile = System.Configuration.ConfigurationManager.AppSettings.Get("sitemap");
            if (!string.IsNullOrEmpty(sitemapFile) && attributes != null)
                attributes.Add("siteMapFile", sitemapFile);

            base.Initialize(name, attributes);
        }


        public override SiteMapNode CurrentNode
        {
            get
            {
                // Find the SiteMapNode that represents the current page.
                string currentUrl = FindCurrentUrl();
                SiteMapNode currentNode = FindSiteMapNode(currentUrl);
                if (currentNode == null) //Cannot find sitemap with current url, check absolute before returning null
                {
                    currentNode = FindSiteMapNode(FindCurrentAbsUrl());
                }
                return currentNode;
            }
        }

        protected override void AddNode(SiteMapNode node)
        {
            if (!string.IsNullOrEmpty(node["action"]))
                node.Url = string.Format(node.ParentNode.Url + "/{0}/{1}/", node["controller"], node["action"]);
            base.AddNode(node);
        }

        private string FindCurrentUrl()
        {
            try
            {
                // The current HttpContext.
                HttpContext currentContext = HttpContext.Current;
                if (currentContext != null)
                {
                    return currentContext.Request.RawUrl;
                }
                else
                {
                    throw new Exception("HttpContext.Current is Invalid");
                }
            }
            catch (Exception e)
            {
                throw new NotSupportedException("This provider requires a valid context.", e);
            }
        }

        private string FindCurrentAbsUrl()
        {
            try
            {
                // The current HttpContext.
                HttpContext currentContext = HttpContext.Current;
                if (currentContext != null)
                {
                    return currentContext.Request.Url.AbsoluteUri;
                }
                else
                {
                    throw new Exception("HttpContext.Current is Invalid");
                }
            }
            catch (Exception e)
            {
                throw new NotSupportedException("This provider requires a valid context.", e);
            }
        }
    }
}
