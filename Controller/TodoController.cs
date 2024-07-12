using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoEFConsole.Model;

namespace TodoEFConsole.Controller
{
    public class TodoController
    {
        public TodoController() { }

        public static List<Todo>? GetTodos()
        {
            var context = new TodoDbContext();
            try
            {
                return context.TodoItems.ToList();
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"DB Error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public static void AddTodo(Todo todoItem)
        {
            using var context = new TodoDbContext();
            try
            {
                todoItem.DateCreated = DateTime.Now;
                todoItem.DateModified = DateTime.Now;
                todoItem.IsCompleted = false;

                context.TodoItems.Add(todoItem);
                context.SaveChanges();
                Console.WriteLine("Task entered successfully");
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"DB Error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void UpdateTodo(int id, Todo todo)
        {
            var context = new TodoDbContext();
            try
            {
                var todoItem = context.TodoItems.Find(id); 
                if (todoItem != null)
                {
                    todoItem.Title = todo.Title;
                    todoItem.Description = todo.Description;
                    todoItem.DateModified = DateTime.Now;
                    todoItem.IsCompleted = todo.IsCompleted;

                    context.SaveChanges();

                    Console.WriteLine("Task successfully updated");
                } else
                {
                    Console.WriteLine("Could not find Task");
                }
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"DB Error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void DeleteTodo(int id) { 
            var context = new TodoDbContext();
            try
            {
                var todoItem = context.TodoItems.Find(id);
                if (todoItem != null) 
                {
                    context.TodoItems.Remove(todoItem);
                    context.SaveChanges();
                    Console.WriteLine("Task was successfully removed");
                } else
                {
                    Console.WriteLine("Could not find task");
                }
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"DB Error: {dbEx.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
