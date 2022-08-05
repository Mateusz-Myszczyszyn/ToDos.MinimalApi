using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.MinimalApi;

public interface IToDoService
{
    void Create(ToDo todo);
    void Delete(Guid id);
    List<ToDo> GetAll();
    ToDo GetById(Guid id);
    void Update(ToDo todo);
}

public class ToDoService : IToDoService
{
    public ToDoService()
    {
        var sampleToDo = new ToDo { Value = "Learn MinimalApi" };
        _toDos[sampleToDo.Id] = sampleToDo;
    }

    private readonly Dictionary<Guid, ToDo> _toDos = new();

    public ToDo GetById(Guid id)
    {
        return _toDos.GetValueOrDefault(id);
    }

    public List<ToDo> GetAll()
    {
        return _toDos.Values.ToList();
    }

    public void Create(ToDo todo)
    {
        if (todo is null)
        {
            return;
        }
        _toDos[todo.Id] = todo;
    }

    public void Update(ToDo todo)
    {
        var existingToDo = GetById(todo.Id);
        if (existingToDo is null)
        {
            return;
        }

        _toDos[todo.Id] = todo;
    }

    public void Delete(Guid id)
    {
        _toDos.Remove(id);
    }
}
