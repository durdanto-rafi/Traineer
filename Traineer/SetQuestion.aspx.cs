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
    public partial class SetQuestion : System.Web.UI.Page
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
                            GetTraineerList();
                            lblPageTitle.Text = "Question Set";
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
        private void GetTraineerList()
        {
            ddlTraineer.DataSource = db.TraineerInfoes.ToList();
            ddlTraineer.DataTextField = "TraineerName";
            ddlTraineer.DataValueField = "ID";
            ddlTraineer.DataBind();
            ddlTraineer.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void GetQuestionList()
        {
            try
            {
                var traineerId = Convert.ToInt32(ddlTraineer.SelectedValue);
                var quesSetData = db.QuestionSets.Where(x => x.Active == true && x.TraineerId == traineerId);

                var result = db.QuestionInfoes.GroupJoin(quesSetData, i => i.ID, s => s.QuestionId, (i, s) => new { info = i, set = s })
                    .SelectMany(x => x.set.DefaultIfEmpty(), (x, ss) => new { QuestionName = x.info.Question, ID = x.info.ID, setId = (ss == null ? (int?)null : ss.ID), Act = x.info.Active })
                    .Where(x => x.Act == true).ToList();

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
                var traineerId = Convert.ToInt32(ddlTraineer.SelectedValue);
                foreach (DataGridItem item in grdData.Items)
                {
                    var questionId = Convert.ToInt32(item.Cells[3].Text.Trim());
                    var chkActive = (CheckBox)item.Cells[1].FindControl("chkSetQues");

                    var question = db.QuestionSets.SingleOrDefault(x => x.QuestionId.Equals(questionId) && x.TraineerId.Equals(traineerId));
                    if (question != null)
                    {
                        question.QuestionId = questionId;
                        question.Active = chkActive.Checked;
                        db.Entry(question).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var questionSet = new QuestionSet()
                        {
                            TraineerId = traineerId,
                            QuestionId = questionId,
                            Active = chkActive.Checked
                        };
                        db.QuestionSets.Add(questionSet);
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
            ddlTraineer.SelectedValue = "";
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
        protected void ddlTraineer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTraineer.SelectedValue == "")
            {
                lblMessage.Text = string.Empty;
                grdData.DataSource = null;
                grdData.DataBind();
                return;
            }
            GetQuestionList();
        }
    }
}