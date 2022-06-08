using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TablaDePosiciones_Patricio.Data;
using TablaDePosiciones_Patricio.Models;

namespace TablaDePosiciones_Patricio.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Http Get
        public IActionResult Index()
        {        
            return View(_context.User.ToList());
        }

        //Http Get
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User usr)
        {
            if (ModelState.IsValid)
            {
                bool nameOrEmailAlreadyExists = _context.User.Any(item => item.UserName == usr.UserName || item.Email == usr.Email);
                

                if (nameOrEmailAlreadyExists)
                {
                    ViewBag.ErrorMsg = ("El nombre de usuario o el mail ya existen.");
                    //ModelState.AddModelError("UserName", "El nombre de usuario o el Email ya existen.");
                    return View();
                }


                _context.User.Add(usr);
                _context.SaveChanges();

                //mensaje para avisar al cliente que se ha creado.
                TempData["mensaje"] = "El Usuario se ha creado correctamente";

                return RedirectToAction("Index");
            }
            return View();
        } 

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var a = _context.User.Where(item => item.Id == id).First();
           
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Update(user);
                _context.SaveChanges();
                //mensaje para avisar al cliente que se ha actualizado.
                TempData["mensaje"] = "El Usuario se ha actualizado correctamente";
                //redireccion al index
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var a = _context.User.Where(item => item.Id == id).First();

            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }

        [HttpPost]
        public IActionResult DeleteUser(int? id)
        {
            var user = _context.User.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            _context.SaveChanges();
            //mensaje para avisar al cliente que se ha removido.
            TempData["mensaje"] = "El Usuario se ha sido removido correctamente";
            //redireccion al index
            return RedirectToAction("Index");
        }

    }
}
