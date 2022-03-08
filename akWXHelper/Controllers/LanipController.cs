using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using akWXHelper.EF.WebApplication1.Models;
using akWXHelper.Models;
using SQ.Base;

namespace akWXHelper.EF
{
    public class LanipController : Controller
    {
        private readonly SqlContext _context = MyTask.Singleton.context;
        static bool akdaily = bool.Parse(ConfigurationHelper.GetAppSetting("akdaily"));
        public LanipController()
        {
        }

        public async Task<IActionResult> Index(string ip, DateTime dt)
        {
            if (dt == DateTime.MinValue)
            {
                dt = DateTime.Today;
            }
            var ips = ip.Split(',');

            List<StatDayLanip> lst = new List<StatDayLanip>(ips.Length);
            foreach (var tip in ips)
            {
                lst.Add(new StatDayLanip(tip, await _context.MonitorLanip.Where(p => p.ip_addr == tip && p.GetTime >= dt.Date && p.GetTime <= dt.Date.AddHours(25)).OrderBy(p => p.GetTime).ToListAsync(), akdaily));
            }
            return View(lst);
        }


        // GET: MonitorLanips
        public async Task<IActionResult> Index2()
        {
            return View(await _context.MonitorLanip.ToListAsync());
        }

        // GET: MonitorLanips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitorLanip = await _context.MonitorLanip
                .FirstOrDefaultAsync(m => m.id == id);
            if (monitorLanip == null)
            {
                return NotFound();
            }

            return View(monitorLanip);
        }

        // GET: MonitorLanips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonitorLanips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("apname,id,bssid,ip_addr_int,downrate,ppptype,client_device,uprate,mac,reject,webid,total_up,signal,ac_gid,apmac,connect_num,ssid,username,hostname,client_type,upload,uptime,total_down,auth_type,dtalk_name,comment,timestamp,ip_addr,download,frequencies")] MonitorLanip monitorLanip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitorLanip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monitorLanip);
        }

        // GET: MonitorLanips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitorLanip = await _context.MonitorLanip.FindAsync(id);
            if (monitorLanip == null)
            {
                return NotFound();
            }
            return View(monitorLanip);
        }

        // POST: MonitorLanips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("apname,id,bssid,ip_addr_int,downrate,ppptype,client_device,uprate,mac,reject,webid,total_up,signal,ac_gid,apmac,connect_num,ssid,username,hostname,client_type,upload,uptime,total_down,auth_type,dtalk_name,comment,timestamp,ip_addr,download,frequencies")] MonitorLanip monitorLanip)
        {
            if (id != monitorLanip.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitorLanip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitorLanipExists(monitorLanip.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(monitorLanip);
        }

        // GET: MonitorLanips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitorLanip = await _context.MonitorLanip
                .FirstOrDefaultAsync(m => m.id == id);
            if (monitorLanip == null)
            {
                return NotFound();
            }

            return View(monitorLanip);
        }

        // POST: MonitorLanips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monitorLanip = await _context.MonitorLanip.FindAsync(id);
            _context.MonitorLanip.Remove(monitorLanip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitorLanipExists(int id)
        {
            return _context.MonitorLanip.Any(e => e.id == id);
        }
    }
}
