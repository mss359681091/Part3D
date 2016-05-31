using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbManager;
using System.Data;

namespace Part3D
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql = " select * from dp_Part ";
            IDataReader reader = IDBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                Response.Write(reader["Name"].ToString());
            }
            IDBHelper.Close();//关闭reader
        }
    }
}