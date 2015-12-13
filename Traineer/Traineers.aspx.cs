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
    public partial class Traineers : System.Web.UI.Page
    {
        int j;
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
                            lblPageTitle.Text = "Traineer";
                            lblModalTitle.Text = "Traineer Information";
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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearData();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }
        protected void btnPopUpSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text.Length > 0)
                {
                    var id = Convert.ToInt32(txtID.Text.Trim());
                    var traineer = db.TraineerInfoes.SingleOrDefault(x => x.ID.Equals(id));
                    if (traineer != null)
                    {
                        traineer.TraineerName = txtTraineerName.Text.Trim();
                        traineer.Active = chkActive.Checked;
                        db.Entry(traineer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                           "<strong>Well done!</strong> Information Updated !</div>";
                }
                else
                {
                    var traineer = new TraineerInfo
                    {
                        TraineerName = txtTraineerName.Text.Trim(),
                        Active = chkActive.Checked
                    };
                    db.TraineerInfoes.Add(traineer);
                    db.SaveChanges();
                    lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                           "<strong>Well done!</strong> Information Added !</div>";
                }

                GetData();
            }
            catch (Exception ex)
            {
                lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-danger'>" +
                                           "<strong>Error !</strong> " + ex.Message + "</div>";
            }
        }
        private void GetData()
        {
            grdData.DataSource = db.TraineerInfoes.ToList();
            grdData.DataBind();
        }
        private void DeleteData(int deleteId)
        {
            var data = db.TraineerInfoes.ToList().FirstOrDefault(a => a.ID == deleteId);
            db.TraineerInfoes.Remove(data);
            db.SaveChanges();

            GetData();
        }
        protected void grdData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            j = j + 1;
            e.Item.Cells[0].Text = j.ToString();
        }
        protected void grdData_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    ClearData();
                    txtTraineerName.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                    txtID.Text = e.Item.Cells[1].Text.Replace("&nbsp;", "");
                    chkActive.Checked = Convert.ToBoolean(e.Item.Cells[3].Text);
                    btnPopUpSave.Text = "Update";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    break;
                case "Delete":
                    DeleteData(Convert.ToInt32(e.Item.Cells[1].Text.Trim()));
                    GetData();
                    break;
            }
        }
        private void ClearData()
        {
            txtTraineerName.Text = string.Empty;
            chkActive.Checked = false;
            txtID.Text = string.Empty;
            lblPopupMessage.Text = string.Empty;
            btnPopUpSave.Text = "Save";
        }
    }
}