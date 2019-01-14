using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PalindromeChecker.Models;

namespace PalindromeChecker.Controllers
{
    public class PalindromesController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: Palindromes
        public ActionResult Index()
        {
            return View(db.Palindromes.ToList());
        }

        // GET: Palindromes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palindrome palindrome = db.Palindromes.Find(id);
            if (palindrome == null)
            {
                return HttpNotFound();
            }
            return View(palindrome);
        }

        // GET: Palindromes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Palindromes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text")] Palindrome palindrome)
        {
            if (ModelState.IsValid)
            {
                if (IsPalindrome(palindrome.Text))
                {
                    db.Palindromes.Add(palindrome);

                    db.SaveChanges();
                    TempData["Message"] = "";
                   
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["Message"] = "Not a Palindrome, please try again";
                    return RedirectToAction("Create");
                    
                }
               
            }

            return View(palindrome);
        }

       
       

        
    
        public bool IsPalindrome(string text)
        {

            int min = 0;
            int max = text.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = text[min];
                char b = text[max];

                // Scan forward for a while invalid.
                while (!char.IsLetterOrDigit(a))
                {
                    min++;
                    if (min > max)
                    {
                        return true;
                    }
                    a = text[min];
                }

                // Scan backward for b while invalid.
                while (!char.IsLetterOrDigit(b))
                {
                    max--;
                    if (min > max)
                    {
                        return true;
                    }
                    b = text[max];
                }

                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
            
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
