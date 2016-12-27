using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Util;

namespace UserInfo
{
    /// <summary>
    /// UserGlory 用户信息类
    /// </summary>
    public class UserCopy
    {
        public UserCopy()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public UserCopy(string uid, string name)
        {
            this._userId = uid;
            this._userName = name;
        }

        private string _userId;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
    }
}
