using Microsoft.AspNetCore.Mvc;
using to_do_list.Data;
using to_do_list.Models;

namespace to_do_list.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController: Controller
{
    private readonly ApplicationDbContext _dbContext;
    
    public ToDoController(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllTodos()
    {
        return Ok(_dbContext.ToDos.ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetTodo(int id)
    {
        var toDoEntity = _dbContext.ToDos.Find(id);
        
        if (toDoEntity is null)
        {
            return NotFound();
        }
        
        return Ok(_dbContext.ToDos.Find(id));
    }

    [HttpPost]
    public IActionResult CreateTodo(CreateToDoDto createToDoDto)
    {
        var toDoEntity = new ToDo() {
            Title = createToDoDto.Title,
            Description = createToDoDto.Description,
            IsDone = createToDoDto.IsDone
        };

        _dbContext.ToDos.Add(toDoEntity);
        _dbContext.SaveChanges();

        return Ok(toDoEntity);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateToDo(int id, UpdateToDoDto updateToDoDto)
    {
        var toDoEntity = _dbContext.ToDos.Find(id);

        if (toDoEntity is null)
        {
            return NotFound();
        }

        toDoEntity.Title = updateToDoDto.Title;
        toDoEntity.Description = updateToDoDto.Description;
        toDoEntity.IsDone = updateToDoDto.IsDone;

        _dbContext.SaveChanges();

        return Ok(toDoEntity);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteToDo(int id)
    {
        var toDoEntity = _dbContext.ToDos.Find(id);

        if (toDoEntity is null)
        {
            return NotFound();
        }

        _dbContext.ToDos.Remove(toDoEntity);
        _dbContext.SaveChanges();
        
        return Ok();
    }
    
}