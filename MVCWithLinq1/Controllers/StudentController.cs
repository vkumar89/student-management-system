using MVCWithLinq1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWithLinq1.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL dal = new StudentDAL();
        #region DisplayStudents
        public ViewResult DisplayStudents()
        {
            return View(dal.GetStudents(true));
        }
        #endregion

        #region AddStudent
        [HttpGet]
        public ViewResult AddStudent()
        {
            return View(new Student());
        }

        [HttpPost]
        public RedirectToRouteResult AddStudent(Student student, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                //Checking whether the folder "Uploads" is exists or not and creating it if not exists
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            student.Status = true;
            dal.AddStudent(student);
            return RedirectToAction("DisplayStudents");
        }
        #endregion


        #region DisplayStudent

        public ViewResult DisplayStudent(int sid)
        {
            return View(dal.GetStudent(sid,true));
        }


        #endregion



        #region Edit Student
        [HttpGet]
        public ViewResult EditStudent(int Sid)
        {
            Student stu = dal.GetStudent(Sid, true);
            TempData["Photo"] = stu.Photo;
            return View(stu);
        }
        [HttpPost]
        public RedirectToRouteResult UpdateStudent(Student student, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            else if (TempData["Photo"] != null)
            {
                student.Photo = TempData["Photo"].ToString();
            }
            dal.UpdateStudent(student);
            return RedirectToAction("DisplayStudents");
        }


        #endregion


        public RedirectToRouteResult DeleteStudent(int Sid)
        {
            dal.DeleteStudent(Sid);
            return RedirectToAction("DisplayStudents");
        }


    }
}