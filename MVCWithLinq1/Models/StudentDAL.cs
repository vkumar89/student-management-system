using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
namespace MVCWithLinq1.Models
{
    public class StudentDAL
    {
        MVCDBDataContext db;
        #region Class constructor
        public StudentDAL() {
          string ConStr=  ConfigurationManager.ConnectionStrings["MVCDBConnectionString"].ConnectionString;
            db= new MVCDBDataContext(ConStr);
        }
        #endregion

        #region Display Student
        public List<Student> GetStudents(bool? Status)
        {
            List<Student> students;
            if(Status != null)
            {
                students = (from s in db.Students where s.Status == Status select s).ToList();
                return students;
            }
            else
            {
              students = (from s in db.Students select s).ToList();
                return students;
            }


        }
        #endregion


        #region Display Student

        public Student GetStudent(int sid,bool? status)
        {
            if(status != null)
            {
                Student stu = (from s in db.Students where s.Sid == sid && s.Status == status select s).Single();
                return stu;
            }
            else
            {
                Student stu =(from s in db.Students where s.Sid==sid select s).Single();
                return stu;
            }
        }

        #endregion

        #region Add Student

        public void AddStudent(Student student)
        {
            db.Students.InsertOnSubmit(student);
            db.SubmitChanges();
        }


        #endregion


        #region EditStudent

        public void UpdateStudent( Student newValues)
        {
            Student oldValues = db.Students.Single(S => S.Sid == newValues.Sid);
            oldValues.Name = newValues.Name;
            oldValues.Class = newValues.Class;
            oldValues.Fees = newValues.Fees;
            oldValues.Photo = newValues.Photo;

            db.SubmitChanges();


        }



        #endregion


        public void DeleteStudent(int sid)
        {
            Student oldValues = db.Students.First(S => S.Sid == sid);
            //dc.Students.DeleteOnSubmit(oldValues); 	//Permenant Deletion 
            oldValues.Status = false;           //Updates the status with-out deleting the record
            db.SubmitChanges();

        }




    }
}