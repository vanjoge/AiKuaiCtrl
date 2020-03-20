using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AiKuaiCtrl.Models;
using SQ.Base;

namespace AiKuaiCtrl
{
    public class AiKuaiHttp
    {
        public AiKuaiHttp(string url = "http://192.168.1.1")
        {
            Url = url;
        }
        HttpHelper http = new HttpHelper();
        private string Url;
        public bool Login()
        {
            var html = http.PostPage(Url + "/Action/login", "{\"username\":\"admin\",\"passwd\":\"463151a514b185f5807a03393353434e\",\"pass\":\"c2FsdF8xMXdhbmppYWp1MQ==\",\"remember_password\":\"\"}");
            var m = html.ParseJSON<RModel>();
            //return html == "{\"Result\":10000,\"ErrMsg\":\"Succeess\"}";
            return m.Result == 10000;

        }
        public string MyPost(string url, string postData)
        {
            var html = http.PostPage(url, postData);
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
        public ACL17 Acl_17_Get(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"show\",\"param\":{\"TYPE\":\"total,data\",\"limit\":\"0,20\",\"ORDER_BY\":\"\",\"ORDER\":\"\"}}");
            var m = html.ParseJSON<RData<ACL17>>();
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
        public bool Acl_17_Down(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"down\",\"param\":{\"id\":\"" + id + "\"}}");
            var m = html.ParseJSON<RModel>();
            return m.Result == 30000;
        }
        public bool Acl_17_Up(int id)
        {
            var html = MyPost(Url + "/Action/call", "{\"func_name\":\"acl_l7\",\"action\":\"up\",\"param\":{\"id\":\"" + id + "\"}}");
            var m = html.ParseJSON<RModel>();
            return m.Result== 30000;
        }
    }
}
