using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace StudentAttendanceSystem.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly HttpClient _httpClient;

        public InstructorsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            //var instructors = await instructorRepository.GetInstructorsAsync();

            //return View(instructors);

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Instructors/GetAllInstructors");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<Instructor>? instructors = JsonConvert.DeserializeObject<IEnumerable<Instructor>>(responseBody);

                return View(instructors);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //var instructor = await instructorRepository.GetInstructorByIdAsync(id);

            //if (id == 0 || instructor == null)
            //{
            //    return NotFound();
            //}

            //return View(instructor);

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Instructors/GetInstructor/" + id);
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                Instructor? instructor = JsonConvert.DeserializeObject<Instructor>(responseBody);

                return View(instructor);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            //if (ModelState.IsValid)
            //{
            //    instructorRepository.CreateInstructortAsync(instructor);
            //    //await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(instructor);

            // Serialize the model to JSON
            string json = JsonConvert.SerializeObject(instructor);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the API
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7297/api/Instructors/PostInstructor", content);

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

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //var instructor = await instructorRepository.GetInstructorByIdAsync(id);

            //if (id == 0 || instructor == null)
            //{
            //    return NotFound();
            //}

            //return View(instructor);

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7297/api/Instructors/GetInstructor/" + id);
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                Instructor? instructor = JsonConvert.DeserializeObject<Instructor>(responseBody);

                return View(instructor);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instructor)
        {
            //instructorRepository.UpdateInstructorAsync(instructor);
            ////await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            // Serialize the model to JSON
            string json = JsonConvert.SerializeObject(instructor);

            // Create a request content with the serialized JSON
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a PUT request to the API with the ID in the URL
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7297/api/Instructors/UpdateInstructor", content);

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

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == 0)
            //{
            //    return NotFound();
            //}

            //await instructorRepository.DeleteInstructorAsync(id);
            //return RedirectToAction(nameof(Index));

            // Make a DELETE request to the API with the ID in the URL
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7297/api/Instructors/DeleteInstructor/{id}");

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
        public async Task<IActionResult> InstructorClassList(int id)
        {
            //var instructor = await instructorRepository.GetInstructorByIdAsync(id);
            //ViewBag.InstructorName = instructor.FullName;
            //var instructorClasses = await instructorRepository.GetInstructorClassesAsync(id);
            ////ViewBag.StudentID = id;
            //return View(instructorClasses);

            // Get Instructor Details
            HttpResponseMessage content = await _httpClient.GetAsync($"https://localhost:7297/api/Instructors/GetInstructor/{id}");
            // Check if the response was successful
            string instructorContent = await content.Content.ReadAsStringAsync();
            Instructor? instructor = JsonConvert.DeserializeObject<Instructor>(instructorContent);
            ViewBag.InstructorName = instructor.FullName;


            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7297/api/Instructors/GetClassList/{id}");
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Do something with the response data
                // ...
                IEnumerable<ClassModel>? instructorClasses = JsonConvert.DeserializeObject<IEnumerable<ClassModel>>(responseBody);

                return View(instructorClasses);
            }
            else
            {
                // Handle the error
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
