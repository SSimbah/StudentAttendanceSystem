using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.ViewModels;

namespace StudentAttendanceSystem.Controllers
{
    public class ClassModelsController : Controller
    {
        private IClassRepository classRepository;

        public ClassModelsController(DatabaseDbContext context)
        {
            classRepository = new ClassRepository(context);
        }

        // GET: ClassModels
        public async Task<IActionResult> Index()
        {
            var classes = await classRepository.GetClassesAsync();

            return View(classes);
        }

        // GET: ClassModels/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var classModel = await classRepository.GetClassByIdAsync(id);
            if (id == null || classModel == null)
            {
                return NotFound();
            }
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // GET: ClassModels/Create
        public IActionResult Create()
        {
            var classViewModel = new ClassViewModel();
            classViewModel.Subjects = classRepository.GetSubjects();
            classViewModel.Instructors= classRepository.GetInstructors();

            return View(classViewModel);
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel classViewModel)
        {
            //Create a Class
            var classModel = classViewModel.Class;
            classRepository.CreateClassesAsync(classModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassModels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var classModel = await classRepository.GetClassByIdAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }
            var classViewModel = new ClassViewModel();
            classViewModel.Subjects = classRepository.GetSubjects();
            classViewModel.Instructors = classRepository.GetInstructors();
            classViewModel.Class = classModel;
            return View(classViewModel);
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassViewModel classViewModel)
        {
            classViewModel.Subjects = classRepository.GetSubjects();
            classViewModel.Instructors = classRepository.GetInstructors();

            //Update Class Model
            var classModel = classViewModel.Class;
            classRepository.UpdateClassesAsync(classModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await classRepository.DeleteClassAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // Class Student List
        public async Task<IActionResult> StudentList(int? id)
        {
            // redirect to another action and controller with ID
            return RedirectToAction( "Index", "ClassStudents" , new { id = id });
        }
    }
}
