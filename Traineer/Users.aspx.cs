using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Traineer.Models;

namespace Traineer
{
    public partial class Users : System.Web.UI.Page
    {
        int j;
        Common common = new Common();
        TraineerEntities db = new TraineerEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["MenuData"] != null)
                    {
                        var dt = (DataTable)Session["MenuData"];
                        var exists = dt.AsEnumerable().Any(x => Path.GetFileName(Request.Url.AbsolutePath) == x.Field<string>("MenuAddress"));
                        if (exists)
                        {
                            GetData();
                            lblPageTitle.Text = "Users";
                            lblModalTitle.Text = "User Information";
                        }
                        else
                        {
                            Response.Redirect("~/Error.aspx");
                        }
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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearData();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }
        protected void btnPopUpSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
            {
                var password = common.Encode(txtPassword.Text.Trim());
                if (txtID.Text.Length > 0)
                {
                    var id = Convert.ToInt32(txtID.Text.Trim());
                    var user = db.Users.SingleOrDefault(x => x.ID.Equals(id));
                    if (user != null)
                    {
                        user.Name = txtName.Text.Trim();
                        user.username = txtUsername.Text.Trim();
                        user.password = password;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                       "<strong>Well done!</strong> User Updated Successfully!</div>";
                }
                else
                {
                    var user = new User
                    {
                        Name = txtName.Text.Trim(),
                        username = txtUsername.Text.Trim(),
                        password = password
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                       "<strong>Well done!</strong> User Saved Successfully!</div>";
                }
                GetData();
            }
            else
            {
                lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-danger'>" +
                                       "<strong>Sorry!</strong> Password and Confirm Password mismatched!</div>";
            }
        }
        private void GetData()
        {
            grdData.DataSource = db.Users.ToList();
            grdData.DataBind();
        }
        private void DeleteData(int deleteID)
        {
            var data = db.Users.ToList().FirstOrDefault(a => a.ID == deleteID);
            db.Users.Remove(data);
            db.SaveChanges();

            grdData.DataSource = db.Users.ToList();
            grdData.DataBind();
        }
        protected void grdData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                j = j + 1;
                e.Item.Cells[0].Text = j.ToString();
            }
        }
        protected void grdData_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                lblPopupMessage.Text = string.Empty;
                txtName.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                txtUsername.Text = e.Item.Cells[3].Text.Replace("&nbsp;", "");
                txtID.Text = e.Item.Cells[1].Text.Replace("&nbsp;", "");
                btnPopUpSave.Text = "Update";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            }

            else if (e.CommandName == "Delete")
            {
                DeleteData(Convert.ToInt32(e.Item.Cells[1].Text.Trim()));
                GetData();
            }
        }
        private void ClearData()
        {
            txtName.Text = string.Empty;
            txtID.Text = string.Empty;
            txtUsername.Text = string.Empty;
            lblPopupMessage.Text = string.Empty;
            btnPopUpSave.Text = "Save";
        }
    }
}
