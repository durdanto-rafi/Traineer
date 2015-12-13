using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Traineer.Models;


namespace Traineer
{
    public partial class Login : System.Web.UI.Page
    {
        Common common = new Common();
        TraineerEntities db = new TraineerEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var password = common.Encode(txtPassword.Text.Trim());
                var data = db.Users.Where(x => x.username == txtUserName.Text.Trim() && x.password == password && x.Active == true).ToList();

                if (data.Count > 0)
                {
                    var dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Name");
                    dt.Rows.Add(data[0].ID, data[0].Name);
                    Session["UserInfo"] = dt;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblMessage.Text = "Login failed ! Please enter correct information to access";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

    }
}