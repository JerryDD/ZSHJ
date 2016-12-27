using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Util
{
    /// <summary>
    /// 数字相关工具类
    /// </summary>
    public class NumberTools
    {
        public NumberTools()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 将对象转换为整形，转换失败则返回0
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int getInt(Object src)
        {
            try
            {
                return Convert.ToInt32(src);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 对象转换为长整形，转换失败则返回0
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static long getLong(Object src)
        {
            try
            {
                return Convert.ToInt64(src);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
