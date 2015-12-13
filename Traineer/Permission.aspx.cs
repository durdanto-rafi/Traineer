using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Traineer.Models;

namespace Traineer
{
    public partial class Permission : System.Web.UI.Page
    {
        private int j;
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
                            GetUserList();
                            lblPageTitle.Text = "Permission";
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
                    lblMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-danger'> " +
                                          "<strong>Error !</strong>" + ex.Message + "</div>";
                }
            }
        }
        private void GetUserList()
        {
            ddlUser.DataSource = db.Users.ToList().Where(x=>x.Active==true);
            ddlUser.DataTextField = "username";
            ddlUser.DataValueField = "ID";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("--Select User--", ""));
        }
        private void GetuserWiseMenu()
        {
            try
            {
                var userId = Convert.ToInt32(ddlUser.SelectedValue);
                var UserWiseMenu = db.UserWiseMenus.Where(x => x.UserId == userId);

                var result = db.Menus.GroupJoin(UserWiseMenu, x => x.ID, y => y.MenuId, (x, y) => new { x, y })
                   .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new
                   {
                       MenuName = x.x.MenuName,
                       ID = x.x.ID,
                       Active = (y == null ? false : y.Active),
                       Act = x.x.Active,
                       x.x.MenuSerial,
                   })
                   .Where(x => x.Act).OrderBy(x => x.MenuSerial).ToList();

                grdData.DataSource = result;
                grdData.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-danger'> " +
                                     "<strong>Error !</strong>" + ex.Message + "</div>";
            }
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                var userId = Convert.ToInt32(ddlUser.SelectedValue);
                foreach (DataGridItem item in grdData.Items)
                {
                    var menuId = Convert.ToInt32(item.Cells[3].Text.Trim());
                    var chkActive = (CheckBox)item.Cells[1].FindControl("chkActive");

                    var uMenu = db.UserWiseMenus.SingleOrDefault(x => x.MenuId.Equals(menuId) && x.UserId.Equals(userId));
                    if (uMenu != null)
                    {
                        uMenu.Active = chkActive.Checked;
                        db.Entry(uMenu).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var userMenu = new UserWiseMenu()
                        {
                            UserId = userId,
                            MenuId = menuId,
                            Active = chkActive.Checked
                        };
                        db.UserWiseMenus.Add(userMenu);
                        db.SaveChanges();
                    }
                }
                lblMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                          "<strong>Well done!</strong> Saved Successfully</div>";

            }
            catch (Exception ex)
            {
                lblMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-danger'> " +
                                      "<strong>Error !</strong>" + ex.Message + "</div>";
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            ddlUser.SelectedValue = "";
            grdData.DataSource = null;
            grdData.DataBind();
        }
        protected void grdData_OnItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                j = j + 1;
                e.Item.Cells[0].Text = j.ToString();
            }
        }
        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.SelectedValue == "")
            {
                lblMessage.Text = string.Empty;
                grdData.DataSource = null;
                grdData.DataBind();
                return;
            }
            GetuserWiseMenu();
        }
    }
}