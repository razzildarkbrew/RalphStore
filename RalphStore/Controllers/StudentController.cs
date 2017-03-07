using RalphStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RalphStore.Controllers
{
    public class StudentController : Controller
    {
        private List<StudentModel> students = new List<StudentModel>();
        // GET: Student
        public ActionResult Index(string id)
        {
            if(students.Count == 0)
            {
                students.Add(new StudentModel { ID = 1, FirstName = "Ralph", LastName = "Comb", FavoriteFood = "Coffee" });
                students.Add(new StudentModel { ID = 2, FirstName = "Jinseong", LastName = "Kim", FavoriteFood = "Pasta" });
                students.Add(new StudentModel { ID = 3, FirstName = "Sam", LastName = "Fessler", FavoriteFood = "Shrimp" });
                students.Add(new StudentModel { ID = 4, FirstName = "Erica", LastName = "Wasilenko", FavoriteFood = "Hummus" });
                students.Add(new StudentModel { ID = 5, FirstName = "Will", LastName = "Mabrey", FavoriteFood = "Ice Cream" });
                students.Add(new StudentModel { ID = 6, FirstName = "Joe", LastName = "Johnson", FavoriteFood = "Nacho" });
            }

            return View(students);
        }
        public ActionResult GetMoreCoffee()
        {
            return Json("I'm going to get more coffee", JsonRequestBehavior.AllowGet);
        }
    }
}