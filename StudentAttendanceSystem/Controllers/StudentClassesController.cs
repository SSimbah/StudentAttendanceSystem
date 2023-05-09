using Domain.DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentClassesController : Controller
    {
        private readonly HttpClient _httpClient;
        public StudentClassesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int id)
        {
            //var studentClasses = from s in _context.ClassStudents.Include(c => c.Class).Include(c => c.Student).Include(c => c.Class.Subject)
            //                     select s;
            //HttpContext.Session.SetInt32(studentId, id);
            //var student = await _context.Students.FindAsync(id);
            //ViewBag.StudentName = student.FullName;

            //var studentClasses = _context.ClassStudents.Include(c => c.Class).Include(c => c.Student).Include(c => c.Class.Subject).Include(i => i.Class.Instructor).Where(s => s.StudentID == id);
            //ViewBag.StudentID = id;
            //return View(await studentClasses.AsNoTracking().ToListAsync());

            

            HttpResponseMessage studentResponse = await _httpClient.GetAsync("https://localhost:7297/api/Students/GetStudent/" + id);
            // Check if the response was successful
            if (studentResponse.IsSuccessStatusCode)
            {
                string responseBody = await studentResponse.Content.ReadAsStringAsync();
                Student? student = JsonConvert.DeserializeObject<Student>(responseBody);
                ViewBag.StudentName = student.FullName;
            }
            else
            {
                // Handle the error
                return StatusCode((int)studentResponse.StatusCode, studentResponse.ReasonPhrase);
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7297/api/StudentClasses/GetStudentClasses/{id}");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<ClassStudent>? studentClasses = JsonConvert.DeserializeObject<IEnumerable<ClassStudent>>(responseBody);
                ViewBag.StudentID = id;
                return View(studentClasses);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }


        public async Task<IActionResult> AddClass(int studentId, int classId)
        {
            //var studentId  = HttpContext.Session.GetInt32(studentId);
            //ClassStudent classStudent = new ClassStudent();
            //classStudent.StudentID = studentId;
            //classStudent.ClassID = classId;
            //_context.Add(classStudent);

            //await _context.SaveChangesAsync();
            //return RedirectToAction("Index", "StudentClasses", new { id = studentId });

            ClassStudent classStudent = new ClassStudent();
            classStudent.StudentID = studentId;
            classStudent.ClassID = classId;

            string json = JsonConvert.SerializeObject(classStudent);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the API
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7297/api/StudentClasses/AddClass", content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "StudentClasses", new { id = studentId });
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public async Task<IActionResult> DeleteClass(int classStudentId, int studentId)
        {
            //if (_context.ClassStudents == null)
            //{
            //    return Problem("Entity set 'DatabaseDbContext.ClassModels'  is null.");
            //}
            //var classStudentModel = await _context.ClassStudents.FindAsync(classStudentId);
            //if (classStudentModel != null)
            //{
            //    _context.ClassStudents.Remove(classStudentModel);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction("Index", "StudentClasses", new { id = studentId });

            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7297/api/StudentClasses/DeleteClass/{classStudentId}");

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "StudentClasses", new { id = studentId });
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public async Task<IActionResult> ClassesLookup(int studentId)
        {
            //ViewBag.StudentID = studentId;
            //var classes = _context.ClassModels.Include(s => s.Subject).Include(i => i.Instructor);
            //return View(classes);

            ViewBag.StudentID = studentId;
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7297/api/StudentClasses/GetAvailableClass/{studentId}");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<ClassModel>? classes = JsonConvert.DeserializeObject<IEnumerable<ClassModel>>(responseBody);

                return View(classes);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
