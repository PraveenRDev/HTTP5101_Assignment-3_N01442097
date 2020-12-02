using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    // REFERENCE :- AUTHOR: CHRISTINE BRITTLE | SOURCE: https://github.com/christinebittle/BlogProject_1 | 
    public class TeacherDataController : ApiController
    {
        // The database context class to access MySQL school Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of Teachers Objects with fields mapped to the database column values (first name, last name, teacher id).
        /// </returns>
        /// <example>GET api/TeacherDataController/ListTeachers -> {Teacher Object, Teacher Object, ...}</example>
        [HttpGet]
        [Route("api/TeacherDataController/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();
            
            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query to select all teachers
            cmd.CommandText = "SELECT * FROM `teachers`";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers to store teacher names and id
            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                // Access column information by the DB column name as an index
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                int TeacherId = (int)ResultSet["teacherid"];

                // Create a newTeacher instance from Teacher
                Teacher newTeacher = new Teacher();

                // Fill newTeacher with appropriate properties
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.TeacherId = TeacherId;

                // Add newTeacher to the Teachers List
                Teachers.Add(newTeacher);
            }

            // close the established db connection after reading the ResultSet
            Conn.Close();
            // return the List of Teachers
            return Teachers;
        }

        /// <summary>
        /// Finds a teacher from the MySQL school Database through an id. 
        /// </summary>
        /// <param name="id">The Teacher ID</param>
        /// <returns>Teacher object containing information(employee number, hire date, salary, first name, last name, teacher id) about the teacher with a matching ID. Empty Teacher Object if the ID does not match any teachers in the system.</returns>
        /// <example>api/TeacherDataController/GetTeacherInformation/1 -> {Teacher Object}</example>
        /// <example>api/TeacherDataController/GetTeacherInformation/2 -> {Teacher Object}</example>
        [HttpGet]
        [Route("api/TeacherDataController/GetTeacherInformation/{id}")]
        public Teacher GetTeacherInformation(int id)
        {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query - filter a particular teacher
            cmd.CommandText = $"SELECT * FROM `teachers` WHERE teacherid={id}";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create a newTeacher instance from Teacher
            Teacher newTeacher = new Teacher();

            while (ResultSet.Read())
            {
                // Access column information by the DB column name as an index
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                int TeacherId = (int)ResultSet["teacherid"];

                // Fill newTeacher with appropriate properties
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.TeacherId = TeacherId;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;
            }

            // After reading the ResultSet close connection
            Conn.Close();
            // return the teacher Object
            return newTeacher;
        }

        /// <summary>
        /// Finds all courses that are associated to a teacher from the MySQL school Database through an id(of teacher). 
        /// </summary>
        /// <param name="id">The Teacher ID</param>
        /// <returns>A list of Courses Objects with fields mapped to the database column values (class code, class name, start date, finish date, class id). Empty Course Object if the ID does not match any teachers in the system.</returns>
        /// <example>api/TeacherDataController/GetTeacherClasses/1 -> {Course Object}</example>
        /// <example>api/TeacherDataController/GetTeacherClasses/2 -> {Course Object}</example>
        [HttpGet]
        [Route("api/TeacherDataController/GetTeacherClasses/{id}")]
        public List<Class> GetTeacherClasses(int id)
        {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query - select all courses of a particular teacher
            cmd.CommandText = $"SELECT * FROM `classes` WHERE teacherid={id}";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create a List to store the classes of the teacher
            List<Class> Classes = new List<Class> { };

            // Read through the ResultSet and assign to them into the list of Classes
            while (ResultSet.Read())
            {
                string ClassCode = (string)ResultSet["classcode"];
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];

                // Create a newClass instance from Class
                Class newClass = new Class();

                // Fill newClass with appropriate properties
                newClass.ClassCode = ClassCode;
                newClass.ClassId = ClassId;
                newClass.ClassName = ClassName;
                newClass.StartDate = StartDate;
                newClass.FinishDate = FinishDate;

                // Add newClass to the Classes List
                Classes.Add(newClass);
            }

            // After reading the ResultSet close connection
            Conn.Close();
            // return the List of Classes
            return Classes;
        }

        /// <summary>
        /// Add a new Teacher to the MySQL school Database.
        /// </summary>
        /// <param name="newTeacher">An Object with fields tha map to the columns of the Teacher's Table</param>
        /// <returns>Created Teacher ID to redirect to the created teacher's profile</returns>
        /// <example>api/TeacherDataController/AddTeacher -> 15(id is obtained from db)</example>
        /// <example>api/TeacherDataController/AddTeacher -> 16</example>
        [HttpPost]
        [Route("api/TeacherDataController/AddTeacher")]
        public int AddTeacher(Teacher newTeacher)
        {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query - insert particular teacher record to teachers table
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary); SELECT LAST_INSERT_ID()";
            cmd.Parameters.AddWithValue("@TeacherFname", newTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", newTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", newTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", newTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", newTeacher.Salary);
            cmd.Prepare();

            // GET the teacher ID that's inserted
            int insertedTeacherId = Convert.ToInt32(cmd.ExecuteScalar());
            
            // After reading the ResultSet close connection
            Conn.Close();

            // return teacherId, to redirect to the created teacher's profile
            return insertedTeacherId;
        }

        /// <summary>
        /// Delete a teacher from the MySQL school database through an id(of teacher).
        /// </summary>
        /// <param name="id">The Teacher ID</param>
        /// <example>api/TeacherDataController/DeleteTeacher/5</example>
        [HttpPost]
        [Route("api/TeacherDataController/DeleteTeacher")]
        public void DeleteTeacher(int id)
        {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query - delete particular teacher record from teachers table
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // After reading the ResultSet close connection
            Conn.Close();
        }

        /// <summary>
        /// Update deleted teacher's classes to null, in other words remove classes of deleted teacher
        /// </summary>
        /// <param name="id">The (deleted) Teacher ID</param>
        /// <example>api/TeacherDataController/RemoveTeacherClasses/6</example>
        [HttpPost]
        [Route("api/TeacherDataController/RemoveTeacherClasses")]
        public void RemoveTeacherClasses(int id) {
            // Create an instance of a db connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open/Associate a connection between the web server and the db
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query - Update the deleted teacher's classes to null
            cmd.CommandText = "UPDATE classes SET teacherid = null WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // After reading the ResultSet close connection
            Conn.Close();
        }
    }
}
