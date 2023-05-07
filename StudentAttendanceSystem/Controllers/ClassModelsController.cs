using Azure;
using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentAttendanceSystem.ViewModels;
using System.Text;

namespace StudentAttendanceSystem.Controllers
{
    public class ClassModelsController : Controller
    {
        private readonly HttpClient _httpClient;
        public ClassModelsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: ClassModels
        public async Task<IActionResult> Index()
        {
            //var classes = await classRepository.GetClassesAsync();\
            //return View(classes);

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Class/GetAllClasses");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<ClassModel>? classModels = JsonConvert.DeserializeObject<IEnumerable<ClassModel>>(responseBody);

                return View(classModels);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: ClassModels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //var classModel = await classRepository.GetClassByIdAsync(id);
            //if (id == null || classModel == null)
            //{
            //    return NotFound();
            //}
            //if (classModel == null)
            //{
            //    return NotFound();
            //}
            //return View(classModel);

            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7297/api/Class/GetClass/{id}");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                ClassModel? classModels = JsonConvert.DeserializeObject<ClassModel>(responseBody);

                return View(classModels);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: ClassModels/Create
        public async Task<IActionResult> Create()
        {
            var classViewModel = new ClassViewModel();
            //classViewModel.Subjects = classRepository.GetSubjects();
            //classViewModel.Instructors = classRepository.GetInstructors();
            HttpResponseMessage response1 = await _httpClient.GetAsync("https://localhost:7297/api/Class/GetAllSubject");
            HttpResponseMessage response2 = await _httpClient.GetAsync("https://localhost:7297/api/Instructors/GetAllInstructors");
            // Check if the response was successful
            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody1 = await response1.Content.ReadAsStringAsync();
                string responseBody2 = await response2.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                List<Subject>? subjects = JsonConvert.DeserializeObject<List<Subject>>(responseBody1);
                List<Instructor>? instructors = JsonConvert.DeserializeObject<List<Instructor>>(responseBody2);

                classViewModel.Subjects = subjects;
                classViewModel.Instructors = instructors;

                return View(classViewModel);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response1.StatusCode, response1.ReasonPhrase);
            }

            //return View(classViewModel);
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel classViewModel)
        {
            ////Create a Class
            //var classModel = classViewModel.Class;
            //classRepository.CreateClassesAsync(classModel);
            //return RedirectToAction(nameof(Index));
            string json = JsonConvert.SerializeObject(classViewModel.Class);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the API
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7297/api/Class/PostClass", content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: ClassModels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var classViewModel = new ClassViewModel();
            HttpResponseMessage response1 = await _httpClient.GetAsync($"https://localhost:7297/api/Class/GetClass/{id}");
            HttpResponseMessage response2 = await _httpClient.GetAsync("https://localhost:7297/api/Class/GetAllSubject");
            HttpResponseMessage response3 = await _httpClient.GetAsync("https://localhost:7297/api/Instructors/GetAllInstructors");
            // Check if the response was successful
            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode && response3.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody1 = await response1.Content.ReadAsStringAsync(); 
                string responseBody2 = await response2.Content.ReadAsStringAsync();
                string responseBody3 = await response3.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                ClassModel? classModels = JsonConvert.DeserializeObject<ClassModel>(responseBody1);
                List<Subject>? subjects = JsonConvert.DeserializeObject<List<Subject>>(responseBody2);
                List<Instructor>? instructors = JsonConvert.DeserializeObject<List<Instructor>>(responseBody3);
                classViewModel.Subjects = subjects;
                classViewModel.Instructors = instructors;
                classViewModel.Class = classModels;

                return View(classViewModel);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response1.StatusCode, response1.ReasonPhrase);
            }
            
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassViewModel classViewModel)
        {
            //classViewModel.Subjects = classRepository.GetSubjects();
            //classViewModel.Instructors = classRepository.GetInstructors();

            ////Update Class Model
            //var classModel = classViewModel.Class;
            //classRepository.UpdateClassesAsync(classModel);
            //return RedirectToAction(nameof(Index));

            string json = JsonConvert.SerializeObject(classViewModel.Class);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a PUT request to the API with the ID in the URL
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7297/api/Class/UpdateClass", content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: ClassModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == 0)
            //{
            //    return NotFound();
            //}

            //await classRepository.DeleteClassAsync(id);

            //return RedirectToAction(nameof(Index));

            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7297/api/Class/DeleteClass/{id}");

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // Class Student List
        public async Task<IActionResult> StudentList(int? id)
        {
            // redirect to another action and controller with ID
            return RedirectToAction( "Index", "ClassStudents" , new { id = id });
        }
    }
}
