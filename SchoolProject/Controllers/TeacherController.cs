using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            return View(Teachers);
        }

        //GET : /Teacher/Show/{id} -> /Teacher/Show/1
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            // Get teacher information
            Teacher teacher = controller.GetTeacherInformation(id);
            // Get course information associated to the teacher
            teacher.Classes = controller.GetTeacherClasses(id);
            return View(teacher);
        }

        //GET : /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST : /Teacher/Add
        [HttpPost]
        public ActionResult Add([Bind(Include = "EmployeeNumber,HireDate,Salary,TeacherFname,TeacherLname")] Teacher newTeacher)
        {
            // validate the data (server-side)
            if (ModelState.IsValid)
            {

                TeacherDataController controller = new TeacherDataController();
                // pass newTeacher to AddTeacher and get the ID of the created teacher
                int teacherId = controller.AddTeacher(newTeacher);
                // Redirect to created teacher profile
                return RedirectToAction($"Show/{teacherId}");
            }
            
            return View(newTeacher);
        }


        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            // Get teacher information
            Teacher teacher = controller.GetTeacherInformation(id);
            // Get course information associated to the teacher
            teacher.Classes = controller.GetTeacherClasses(id);
            return View(teacher);
        }

        //POST : /Teacher/Delete/?teacherId={teacherId}&classCount={classCount}
        [HttpPost]
        public ActionResult Delete(int teacherId, int classCount)
        {
            TeacherDataController controller = new TeacherDataController();
            // Delete teacher 
            controller.DeleteTeacher(teacherId);
            // if deleted teacher has classes associated, remove them
            if(classCount > 0)
            {
                controller.RemoveTeacherClasses(teacherId);
            }
            return RedirectToAction("List");
        }

    }
}