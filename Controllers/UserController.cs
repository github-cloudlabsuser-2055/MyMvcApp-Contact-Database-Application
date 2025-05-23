using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Assign a new Id if needed
                if (userlist.Count == 0)
                {
                    user.Id = 1;
                }
                else
                {
                    user.Id = userlist.Max(u => u.Id) + 1;
                }
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    // Add other properties as needed
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                userlist.Remove(user);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        // GET: User/Search
        public ActionResult Search(string query)
        {
            List<User> results = new List<User>();
            if (!string.IsNullOrEmpty(query))
            {
                results = userlist.Where(u =>
                    (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }
            ViewBag.Query = query;
            return View(results);
        }
}
