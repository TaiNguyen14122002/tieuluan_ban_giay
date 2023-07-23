using System.Web.Mvc;

namespace tieuluan_ban_giay.Areas.Quan_tri
{
    public class Quan_triAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Quan_tri";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Quan_tri_default",
                "Quan_tri/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}