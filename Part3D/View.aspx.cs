using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _3DPart.DAL.BULayer;
using System.Data;
using _3DPart.DAL.BULayer.Schema;
using System.Collections;
using Part3D.models;
using log4net;
using System.Configuration;

namespace Part3D
{
    public partial class View : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["partid"] != null)
                {
                    try
                    {
                        string count = "0";
                        dpPartManager mydpPartManager = new dpPartManager();
                        dpPartQuery mydpPartQuery = new dpPartQuery();
                        mydpPartQuery.ID = Request.QueryString["partid"];
                        DataSet myDataSet = mydpPartManager.SearchSingle(mydpPartQuery);
                        if (myDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.hidid.Value = Request.QueryString["partid"];
                            this.hidclassid.Value = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                            dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                            dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                            mydpAdvertisementQuery.ClassifyID = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                            mydpAdvertisementQuery.ADPosition = "下载页";
                            DataSet ds = mydpAdvertisementManager.Search(mydpAdvertisementQuery);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.lnkad.HRef = ds.Tables[0].Rows[0][dpAdvertisement.ADLink].ToString();
                                this.imgad.Src = ds.Tables[0].Rows[0][dpAdvertisement.PicturePath].ToString();
                                this.lnkad.Visible = true;
                            }
                            else
                            {
                                this.lnkad.Visible = false;
                            }

                            this.btitle.InnerText = myDataSet.Tables[0].Rows[0][dpPart.Name].ToString();
                            this.ausername.InnerText = myDataSet.Tables[0].Rows[0][sysUser.Nickname].ToString();
                            this.acreate.InnerText = myDataSet.Tables[0].Rows[0]["CreateDate"].ToString();
                            this.imgPreview.Src = myDataSet.Tables[0].Rows[0][dpPart.Preview].ToString();
                            count = myDataSet.Tables[0].Rows[0][dpPart.Accesslog].ToString();
                        }

                        count = count == "" ? "0" : count;
                        mydpPartManager.UpdateParams(dpPart.Accesslog, (Convert.ToInt32(count) + 1).ToString(), Request.QueryString["partid"]);


                        //插入搜索记录
                        Hashtable myht = new Hashtable();
                        if (Session[sysUser.ID] != null)
                        {
                            myht.Add(dpDownRecord.UserID, Session[sysUser.ID].ToString());
                        }
                        else
                        {
                            myht.Add(dpDownRecord.UserID, "0");
                        }
                        myht.Add(dpDownRecord.PartID, Request.QueryString["partid"].ToString());
                        myht.Add(dpDownRecord.IPAddress, CommonManager.GetClientIPv4Address());
                        myht.Add(dpDownRecord.RecordType, "0");
                        SQLHelper.ExcuteProc("sp_DownRecord", myht);//执行存储过程注册
                    }
                    catch (Exception ex)
                    {
                        m_log.Error(ex.Message);
                    }
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }

            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = this.hidfileid.Value;
            if (Index.ChkIP())
            {
                Index.DowmLoad(id);
            }
            else
            {
                Response.Write("<script>alert('非注册用户每天只能下载10次！')</script>");
            }
        }


    }
}