using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using akWXHelper.Models;
using SQ.Base;

namespace akWXHelper
{
    public class AiKuaiHttp
    {
        string usr, pwd, passH;
        public AiKuaiHttp(string usr, string pwd, string passH, string url = "http://192.168.1.1")
        {
            this.usr = usr;
            this.pwd = SQ.Base.EnDecrypt.EnDecrypt.MD5(pwd).ToLower();
            this.passH = Convert.ToBase64String(SQ.Base.StringHelper.ToUtf8Bytes(passH + pwd));
            Url = url;
        }
        HttpHelper http = new HttpHelper();
        private string Url;
        public bool Login()
        {
            string html = null;
            try
            {
                html = http.PostPage(Url + "/Action/login", "{\"username\":\"" + usr + "\",\"passwd\":\"" + pwd + "\",\"pass\":\"" + passH + "\",\"remember_password\":\"\"}");
                var m = html.ParseJSON<RModel>();
                //return html == "{\"Result\":10000,\"ErrMsg\":\"Succeess\"}";
                return m.Result == 10000;
            }
            finally
            {
                SQ.Base.Log.WriteLog4(html);
            }

        }
        public string MyPost(string url, string postData)
        {
            string html = null;
            try
            {
                html = http.PostPage(url, postData);
                var m = html.ParseJSON<RModel>();
                //if (html == "{\"Result\":10014,\"ErrMsg\":\"no login authentication\"}")
                if (m.Result == 10014)
                {
                    if (Login())
                    {
                        html = http.PostPage(url, postData);
                    }
                }
                return html;
            }
            finally
            {
                SQ.Base.Log.WriteLog4(html);
            }
        }
        public ACLl7 Acl_l7_Get(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"show\",\"param\":{\"TYPE\":\"total,data\",\"limit\":\"0,20\",\"ORDER_BY\":\"\",\"ORDER\":\"\"}}");
            var m = html.ParseJSON<RData<ACLl7>>();
            if (m.Result == 30000 && m.Data != null && m.Data.data != null && m.Data.data.Count > 0)
            {
                foreach (var item in m.Data.data)
                {
                    if (item.id == id)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        public bool Acl_l7_Down(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"down\",\"param\":{\"id\":\"" + id + "\"}}");
            var m = html.ParseJSON<RModel>();
            return m.Result == 30000;
        }
        public bool Acl_l7_Up(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"up\",\"param\":{\"id\":\"" + id + "\"}}");
            var m = html.ParseJSON<RModel>();
            return m.Result == 30000;
        }


        public RDataList<MonitorLanip> monitor_lanip()
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"monitor_lanip\",\"action\":\"show\",\"param\":{\"TYPE\":\"data,total\",\"ORDER_BY\":\"ip_addr_int\",\"orderType\":\"IP\",\"limit\":\"0,100\",\"ORDER\":\"\"}}");


            var m = html.ParseJSON<RData<MonitorLanip>>();
            if (m.Result == 30000)
            {
                return m.Data;
            }
            return null;
        }
    }
}
