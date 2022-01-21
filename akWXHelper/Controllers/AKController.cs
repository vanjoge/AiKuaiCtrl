using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQ.Base;

namespace akWXHelper.Controllers
{
    public class AKController : Controller
    {
        static AiKuaiHttp http = new AiKuaiHttp(ConfigurationHelper.GetAppSetting("username"), ConfigurationHelper.GetAppSetting("passwd"), ConfigurationHelper.GetAppSetting("passH"), ConfigurationHelper.GetAppSetting("akurl"));
        int aclId = Convert.ToInt32(ConfigurationHelper.GetAppSetting("aclId"));
        public IActionResult Index()
        {
            var h = http.Acl_l7_Get(aclId);

            return View(h);
        }

        public int ChangeAcl(int id, int state)
        {

            if (state == 0)
            {
                if (http.Acl_l7_Up(id))
                {
                    return 1;
                }
            }
            else
            {
                if (http.Acl_l7_Down(id))
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}