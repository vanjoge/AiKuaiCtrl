using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AiKuaiCtrl.Controllers
{
    public class AKController : Controller
    {
        static AiKuaiHttp http = new AiKuaiHttp("http://127.0.0.1:808");
        int aclId = 2;
        public IActionResult Index()
        {
            var h = http.Acl_17_Get(aclId);

            return View(h);
        }

        public int ChangeAcl(int id, int state)
        {
            
            if (state == 0)
            {
                if (http.Acl_17_Up(id))
                {
                    return 1;
                }
            }
            else
            {
                if (http.Acl_17_Down(id))
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}