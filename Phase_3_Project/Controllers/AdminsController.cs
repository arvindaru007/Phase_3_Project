using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using project3.Models;

namespace project3.Controllers
{
    public class AdminsController : Controller
    {
        private readonly phase3DBContext _context;

        public AdminsController(phase3DBContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblAdmin.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tblAdmin == null)
            {
                return NotFound();
            }

            return View(tblAdmin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminUname,AdminPwd")] TblAdmin tblAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblAdmin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin.FindAsync(id);
            if (tblAdmin == null)
            {
                return NotFound();
            }
            return View(tblAdmin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminUname,AdminPwd")] TblAdmin tblAdmin)
        {
            if (id != tblAdmin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblAdminExists(tblAdmin.AdminId))
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
            return View(tblAdmin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tblAdmin == null)
            {
                return NotFound();
            }

            return View(tblAdmin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblAdmin = await _context.TblAdmin.FindAsync(id);
            _context.TblAdmin.Remove(tblAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblAdminExists(int id)
        {
            return _context.TblAdmin.Any(e => e.AdminId == id);
        }

        //Admin Login




        string cs = string.Empty; string query = string.Empty;
        SqlConnection cn = null; SqlCommand cmd = null;



        public SqlConnection ConnectToSqlServer()
        {
            cs = this.cs = "server=(local);integrated security=true;database=phase3DB";
            cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }




        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(TblAdmin admin)
        {
            cn = ConnectToSqlServer();
            query = "select * from tbl_admin where admin_uname = @uname and admin_pwd =@password";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@uname", admin.AdminUname);
            cmd.Parameters.AddWithValue("@password", admin.AdminPwd);
            SqlDataReader dr = cmd.ExecuteReader();
            TblAdmin ad = new TblAdmin();
            if (dr.Read())
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();              
                //Console.WriteLine("logged In");
                return RedirectToAction("Home");
            }
            else
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                ViewBag.msg = "Invalid Credentials";
                return View();
            }
        }
        public IActionResult  Home()
        {
            return View();
        }
    }
}
