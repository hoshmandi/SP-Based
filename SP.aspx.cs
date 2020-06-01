using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class SP : System.Web.UI.Page
{
    public Int32 iSP; public string uid="0", curDate, inp, ret = "®";
    protected void Page_Load(object sender, EventArgs e)
    {
        ret = exec("EXEC [dbo].[RunSP] @iSP = " + Request["isp"] + ",@rawInput = N'" + Request["in"] + "'");
        if (ret.ToLower().IndexOf("primary key")>-1) { Response.Write("-1©The ID is duplicate!"); return; }
        Response.Write(ret); return;
    }
    public static string exec(string query)
    {
        SqlDataSource sdsGetData = new SqlDataSource();
        string rout;
        try
        {
            sdsGetData.ConnectionString = (ConfigurationManager.ConnectionStrings["SiteSqlServer"]).ToString();
            sdsGetData.SelectCommandType = SqlDataSourceCommandType.Text;
            sdsGetData.SelectCommand = query;
            DataView dv = (DataView)sdsGetData.Select(DataSourceSelectArguments.Empty);
            rout = dv[0][0].ToString();
        }
        catch (Exception e) { rout = "-1©" + e.Message; }
        return rout;
    }

}
