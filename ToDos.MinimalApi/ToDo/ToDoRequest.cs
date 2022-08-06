using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.MinimalApi;

public static class ToDoRequest
{

    public static WebApplication RegisterEndpoints( this WebApplication app)
    {
        app.MapGet("/todos", ToDoRequest.GetAll)
            .Produces<List<ToDo>>()
            .WithTags("To Dos");

        app.MapGet("/todos/{id}", ToDoRequest.GetById)
            .Produces<ToDo>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("To Dos");

        app.MapPost("/todos", ToDoRequest.Create)
            .Produces<ToDo>(StatusCodes.Status201Created)
            .Accepts<ToDo>("application/json")
            .WithTags("To Dos")
            .WithValidator<ToDo>();

        app.MapPut("/todos/{id}", ToDoRequest.Update)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Accepts<ToDo>("application/json")
            .WithTags("To Dos")
            .WithValidator<ToDo>();

        app.MapDelete("/todos/{id}", ToDoRequest.Delete)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("To Dos")
            .ExcludeFromDescription();//hiding delete method in swagger

        return app;
    }
    public static IResult GetAll(IToDoService service)
    {
        var todos = service.GetAll();
        return Results.Ok(todos);
    }



    public static IResult GetById(IToDoService service,Guid id)
    {
        var todo = service.GetById(id);
        if(todo == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(todo);
    }

    public static IResult Create(IToDoService service, ToDo todo)
    {
        
        service.Create(todo);

        return Results.Created($"/todos/{todo.Id}",todo);
    }

    public static IResult Update(IToDoService service, ToDo toDo,Guid id)
    {

        var todo = service.GetById(id);
        if (todo == null)
        {
            return Results.NotFound();
        }
        service.Update(toDo);

        return Results.NoContent();
    }

    public static IResult Delete(IToDoService service, Guid id)
    {
        var todo = service.GetById(id);
        if (todo == null)
        {
            return Results.NotFound();
        }

        service.Delete(id);
        return Results.NoContent();
    }
}
