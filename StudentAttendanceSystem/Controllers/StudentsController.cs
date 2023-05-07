using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Students/GetAllStudent");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<Student>? students = JsonConvert.DeserializeObject<IEnumerable<Student>>(responseBody);

                return View(students);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }

        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Students/GetStudent/" + id);
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                Student? student = JsonConvert.DeserializeObject<Student>(responseBody);

                return View(student);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            // Serialize the model to JSON
            string json = JsonConvert.SerializeObject(student);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the API
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7297/api/Students/PostStudent", content);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...

                //return Ok(responseBody);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Students/GetStudent/" + id);
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                Student? student = JsonConvert.DeserializeObject<Student>(responseBody);

                return View(student);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            // Serialize the model to JSON
            string json = JsonConvert.SerializeObject(student);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a PUT request to the API with the ID in the URL
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7297/api/Students/UpdateStudent", content);

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


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            // Make a DELETE request to the API with the ID in the URL
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7297/api/Students/DeleteStudent/{id}");

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...

                //return Ok(responseBody);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public async Task<IActionResult> StudentClassList(int? id)
        {
            // redirect to another action and controller with ID
            return RedirectToAction("Index", "StudentClasses", new { id = id });
        }
    }
}
