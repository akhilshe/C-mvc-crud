using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.AccessControl;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.Entity;

namespace WebApplication2.Controllers
{
    public class StudentsController : Controller

    {
        private readonly ApplicationDBContext dbContext;
        public StudentsController(ApplicationDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {

            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Honours = viewModel.Honours,
            };
            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Honours = viewModel.Honours;

                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Students");
        }

        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==viewModel.Id);

            if (student != null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Students");
        }
    }
}
