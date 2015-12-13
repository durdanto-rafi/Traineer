using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Traineer
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserInfo"] != null)
                {
                    //var dt = (DataTable)Session["MenuData"];
                    //var exists = dt.AsEnumerable().Any(x => Path.GetFileName(Request.Url.AbsolutePath) == x.Field<string>("MenuAddress"));
                    //if (exists)
                    //{
                        
                    //}
                    //else
                    //{
                    //    //Response.Redirect("~/Error.aspx");
                    //}
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx");
            }
        }
    }
}