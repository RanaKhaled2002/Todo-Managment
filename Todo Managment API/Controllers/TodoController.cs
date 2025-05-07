using Microsoft.AspNetCore.Mvc;
using Todo_Managment_API.Models;
using Todo_Managment_BLL.Interfaces;
using Todo_Managment_DAL.Models;

namespace Todo_Managment_API.Controllers
{
    public class TodoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string status,string priority)
        {
            var todos = await _unitOfWork.Repository<Todo>().GetAllAsync();

            if (!string.IsNullOrEmpty(status))
            {
                todos = todos.Where(t => t.Status.ToString() == status);
            }

            if (!string.IsNullOrEmpty(priority))
                todos = todos.Where(t => t.Priority.ToString() == priority);

            var viewModel = todos.Select(t => new IndexViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                DueDate = t.DueDate,
                LastModifiedDate = t.LastModifiedDate
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newTodo = new Todo
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Description = model.Description,
                    Status = Enum.Parse<TodoStatus>(model.Status),
                    Priority = Enum.Parse<TodoPriority>(model.Priority),
                    DueDate = model.DueDate,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow
                };

                await _unitOfWork.Repository<Todo>().AddAsync(newTodo);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var todo = await _unitOfWork.Repository<Todo>().GetById(id);
            if (todo is null) return NotFound();
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var todo = await _unitOfWork.Repository<Todo>().GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Todo>().Delete(todo);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var todo = await _unitOfWork.Repository<Todo>().GetById(id);

            if (todo is null) return BadRequest();

            return View(todo);
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(Todo model)
        {
            if (ModelState.IsValid)
            {
                var todo = await _unitOfWork.Repository<Todo>().GetById(model.Id);
                if (todo is null)
                {
                    return NotFound();
                }

                // Update properties
                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.Status = model.Status;
                todo.Priority = model.Priority;
                todo.DueDate = model.DueDate;
                todo.LastModifiedDate = DateTime.UtcNow;

                _unitOfWork.Repository<Todo>().Update(todo);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            var todo = await _unitOfWork.Repository<Todo>().GetById(id);

            if (todo is null) return NotFound();

            todo.Status = TodoStatus.Completed;
            todo.LastModifiedDate = DateTime.UtcNow;

            _unitOfWork.Repository<Todo>().Update(todo);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

    }

}


