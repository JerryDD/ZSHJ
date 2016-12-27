using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindData();
        }
    }


    private void BindData()
    {
        string bgh = Request.QueryString["bgh"] != null ? HttpUtility.UrlDecode(Request.QueryString["bgh"], Encoding.GetEncoding("utf-8")) : "";
        if (!string.IsNullOrEmpty(bgh))
        {           
            DataTable dt = BaseADO.GetSdsmReport(bgh);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                litCompanyName.Text = dr["公司名称"].ToString();         
                lblAmount1.Text = dr["营业收入1"].ToString();                
                lblAmount2.Text = dr["营业成本2"].ToString();
                lblAmount3.Text = dr["营业税金及附加3"].ToString();
                lblAmount4.Text = dr["销售费用4"].ToString();
                lblAmount5.Text = dr["管理费用5"].ToString();
                lblAmount6.Text = dr["财务费用6"].ToString();
                lblAmount7.Text = dr["资产减值损失7"].ToString();
                lblAmount8.Text = dr["公允价值变动收益8"].ToString();
                lblAmount9.Text = dr["投资收益9"].ToString();
                lblAmount10.Text = dr["营业利润10"].ToString();
                lblAmount11.Text = dr["营业外收入11"].ToString();
                lblAmount12.Text = dr["营业外支出12"].ToString();
                lblAmount13.Text = dr["利润额13"].ToString();
                lblAmount14.Text = dr["境外所得14"].ToString();
                lblAmount15.Text = dr["调增利润15"].ToString();
                lblAmount16.Text = dr["调减利润16"].ToString();
                lblAmount17.Text = dr["免减收入加计扣17"].ToString();
                lblAmount18.Text = dr["境外抵减亏损18"].ToString();
                lblAmount19.Text = dr["调整后所得19"].ToString();
                lblAmount20.Text = dr["所得减免20"].ToString();
                lblAmount21.Text = dr["抵扣应纳税所得额21"].ToString();
                lblAmount22.Text = dr["弥补以前年度亏损22"].ToString();
                lblAmount23.Text = dr["应纳税所得额23"].ToString();
                lblAmount24.Text = dr["税率24"].ToString();
                lblAmount25.Text = dr["应纳税额25"].ToString();
                lblAmount26.Text = dr["减免所得税额26"].ToString();
                lblAmount27.Text = dr["抵免所得税额27"].ToString();
                lblAmount28.Text = dr["应纳税额28"].ToString();
                lblAmount29.Text = dr["境外应纳税额29"].ToString();
                lblAmount30.Text = dr["境外抵免税额30"].ToString();
                lblAmount31.Text = dr["实际应纳所得税额31"].ToString();
                lblAmount32.Text = dr["已缴税32"].ToString();
                lblAmount33.Text = dr["应补税33"].ToString();
            }           
        }       
    }
}
