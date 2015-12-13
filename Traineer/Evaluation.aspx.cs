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
    public partial class Evaluation : System.Web.UI.Page
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
                            lblPageTitle.Text = "Evaluation";
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
            ddlTraineer.DataSource = db.TraineerInfoes.Where(x => x.Active == true).ToList();
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
                var quesData = db.QuestionSets.Join(db.QuestionInfoes, s => s.QuestionId, i => i.ID, (s, i) => new { s, i })
                   .Where(x => x.s.Active == true && x.s.TraineerId == traineerId)
                   .Select(x => new { x.i.Question, x.s.QuestionId }).ToList();

                var evalData = db.EvaluationInfoes.Where(x => x.TraineerId == traineerId).ToList();

                var result = quesData.GroupJoin(evalData, x => x.QuestionId, y => y.QuestionId, (x, y) => new { x, y }).
                    SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new
                    {
                        QuestionName = x.x.Question,
                        ID = x.x.QuestionId,
                        Marks = (y == null ? 0 : y.Mark)
                    }).ToList();

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
                var dt = new DataTable();
                dt = (DataTable)Session["UserInfo"];
                foreach (DataGridItem item in grdData.Items)
                {
                    var questionId = Convert.ToInt32(item.Cells[3].Text.Trim());
                    var ddlMarks = item.FindControl("ddlMarks") as DropDownList;
                    var marks = Convert.ToInt32(ddlMarks.SelectedValue);

                    var evaluation = db.EvaluationInfoes.SingleOrDefault(x => x.QuestionId.Equals(questionId) && x.TraineerId.Equals(traineerId));
                    if (evaluation != null)
                    {
                        evaluation.Mark = marks;
                        evaluation.UserId = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        db.Entry(evaluation).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var eval = new EvaluationInfo()
                        {
                            TraineerId = traineerId,
                            QuestionId = questionId,
                            UserId = Convert.ToInt32(dt.Rows[0]["ID"].ToString()),
                            Mark = marks
                        };
                        db.EvaluationInfoes.Add(eval);
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