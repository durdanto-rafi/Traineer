using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Traineer.Models;

namespace Traineer
{
    public partial class SiteMaster : MasterPage
    {
        public DataTable MenuData = new DataTable();
        public DataTable dt;
        TraineerEntities db = new TraineerEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt = (DataTable)Session["UserInfo"];
            if (!IsPostBack)
            {
                lblUser.Text = dt.Rows[0]["Name"].ToString();
            }

            try
            {
                if (Session["MenuData"] != null)
                {
                    MenuData = (DataTable)Session["MenuData"];
                }
                else
                {
                    GetMenuInfo(Convert.ToInt32(dt.Rows[0]["ID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx/" + ex);
            }
        }
        private void GetMenuInfo(int userId)
        {
            var data = db.UserWiseMenus.Join(db.Menus, x => x.MenuId, y => y.ID, (x, y) => new { x, y }).Where(x => x.x.Active && x.x.UserId == userId)
                .Select(x => new { x.y.MenuName, x.y.MenuAddress, x.y.MenuSerial }).OrderBy(x => x.MenuSerial).ToList();
            Session["MenuData"] = MenuData = ToDataTable(data);
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dt = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        protected void lblLogout_OnClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}