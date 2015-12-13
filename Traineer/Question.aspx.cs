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
    public partial class Question : System.Web.UI.Page
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
                            lblPageTitle.Text = "Question";
                        }
                        else
                        {
                            //Response.Redirect("~/Error.aspx");
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
                    var question = db.QuestionInfoes.SingleOrDefault(x => x.ID.Equals(id));
                    if (question != null)
                    {
                        question.Question = txtQuestion.Text.Trim();
                        question.Serial = Convert.ToInt32(txtSerial.Text.Trim());
                        question.Active = chkActive.Checked;
                        db.Entry(question).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    lblPopupMessage.Text = "<div role='alert' class='span7 text-center alert alert-custom alert-success'>" +
                                           "<strong>Well done!</strong> Information Updated !</div>";
                }
                else
                {
                    var question = new QuestionInfo()
                    {
                        Question = txtQuestion.Text.Trim(),
                        Serial = Convert.ToInt32(txtSerial.Text.Trim()),
                        Active = chkActive.Checked
                    };
                    db.QuestionInfoes.Add(question);
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
            grdData.DataSource = db.QuestionInfoes.ToList();
            grdData.DataBind();
        }
        private void DeleteData(int deleteId)
        {
            var data = db.QuestionInfoes.ToList().FirstOrDefault(a => a.ID == deleteId);
            db.QuestionInfoes.Remove(data);
            db.SaveChanges();

            grdData.DataSource = db.QuestionInfoes.ToList();
            grdData.DataBind();
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
                    txtQuestion.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                    txtSerial.Text = e.Item.Cells[3].Text.Replace("&nbsp;", "");
                    chkActive.Checked = Convert.ToBoolean(e.Item.Cells[4].Text);
                    txtID.Text = e.Item.Cells[1].Text.Replace("&nbsp;", "");
                    btnPopUpSave.Text = "Update";
                    lblPopupMessage.Text = string.Empty;
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
            txtQuestion.Text = string.Empty;
            txtSerial.Text = string.Empty;
            chkActive.Checked = false;
            txtID.Text = string.Empty;
            lblPopupMessage.Text = string.Empty;
            btnPopUpSave.Text = "Save";
        }
    }
}