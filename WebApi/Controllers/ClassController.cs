using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository classRepository;

        public ClassController(DatabaseDbContext context)
        {
            classRepository = new ClassRepository(context);
        }

        //// GET: ClassModels
        //public async Task<IActionResult> Index()
        //{
        //    var classes = await classRepository.GetClassesAsync();

        //    return View(classes);
        //}

        [HttpGet]
        public async Task<ActionResult> GetAllClasses()
        {
            try
            {
                return Ok(await classRepository.GetClassesAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        //// GET: ClassModels/Details/5
        //public async Task<IActionResult> Details(int id)
        //{

        //    var classModel = await classRepository.GetClassByIdAsync(id);
        //    if (id == null || classModel == null)
        //    {
        //        return NotFound();
        //    }
        //    if (classModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(classModel);
        //}

        // API - Get Class Details / Details
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassModel>> GetClass(int id)
        {
            var classModel = await classRepository.GetClassByIdAsync(id);

            if (id == 0 || classModel == null)
            {
                return NotFound();
            }

            return classModel;
        }
        //// GET: ClassModels/Create
        //public IActionResult Create()
        //{
        //    var classViewModel = new ClassViewModel();
        //    classViewModel.Subjects = classRepository.GetSubjects();
        //    classViewModel.Instructors = classRepository.GetInstructors();

        //    return View(classViewModel);
        //}

        //// POST: ClassModels/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ClassViewModel classViewModel)
        //{
        //    //Create a Class
        //    var classModel = classViewModel.Class;
        //    classRepository.CreateClassesAsync(classModel);
        //    return RedirectToAction(nameof(Index));
        //}

        // API - Post Class / Create
        [HttpPost]
        public async Task<ActionResult<ClassModel>> PostClass(ClassModel classModel)
        {
            await classRepository.CreateClassesAsync(classModel);
            if (classModel == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetClass),
                    new { id = classModel.ClassID }, classModel);
        }

        //// GET: ClassModels/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var classModel = await classRepository.GetClassByIdAsync(id);
        //    if (classModel == null)
        //    {
        //        return NotFound();
        //    }
        //    var classViewModel = new ClassViewModel();
        //    classViewModel.Subjects = classRepository.GetSubjects();
        //    classViewModel.Instructors = classRepository.GetInstructors();
        //    classViewModel.Class = classModel;
        //    return View(classViewModel);
        //}

        //// POST: ClassModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ClassViewModel classViewModel)
        //{
        //    classViewModel.Subjects = classRepository.GetSubjects();
        //    classViewModel.Instructors = classRepository.GetInstructors();

        //    //Update Class Model
        //    var classModel = classViewModel.Class;
        //    classRepository.UpdateClassesAsync(classModel);
        //    return RedirectToAction(nameof(Index));
        //}

        // API - PUT Class / Update
        [HttpPut]
        public async Task<IActionResult> UpdateClass(ClassModel classModel)
        {
            await classRepository.CheckInputAsync(classModel);
            await classRepository.UpdateClassesAsync(classModel);

            return Ok();
        }

        //// GET: ClassModels/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    await classRepository.DeleteClassAsync(id);

        //    return RedirectToAction(nameof(Index));
        //}

        // API - DELETE Class / Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassModel>> DeleteClass(int id)
        {
            try
            {
                var classModel = await classRepository.GetClassByIdAsync(id);

                if (classModel == null)
                {
                    return NotFound($"Class with Id = {id} not found");
                }

                await classRepository.DeleteClassAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSubject()
        {
            try
            {

                return Ok(await classRepository.GetSubjects());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        //// Class Student List
        //public async Task<IActionResult> StudentList(int? id)
        //{
        //    // redirect to another action and controller with ID
        //    return RedirectToAction("Index", "ClassStudents", new { id = id });
        //}
    }
}
