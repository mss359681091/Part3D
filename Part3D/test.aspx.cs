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
            string sql = " select * from dp_Part where Name=@Name ";

            //若有参，请加上这句
            DBHelper.CreateParameters(1);//参数个数，1个
            DBHelper.AddParameters(0, "@Name", "丢雷螺母");



            IDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                Response.Write(reader["Name"].ToString());
            }
            DBHelper.Close();//关闭reader



        }
    }
}