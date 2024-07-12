using TodoEFConsole.Controller;
using TodoEFConsole.Model;

namespace TodoEFConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int choice = 0;

            do
            {
                try
                {
                    DisplayMenu();
                    Console.Write("Enter Option: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            List<Todo> todoItems = TodoController.GetTodos() ?? new List<Todo>();
                            DisplayTodoList(todoItems);
                            break;
                        case 2:
                            AddTask();
                            break;
                        case 3:
                            UpdateTask();
                            break;
                        case 4:
                            RemoveTask();
                            break;
                        case 5:
                            Console.WriteLine("Quiting program...............");
                            break;
                        default:
                            Console.WriteLine("Entered value is not a valid input");
                            break;
                    }
                } catch (FormatException)
                {
                    Console.WriteLine("Entered a non-numerical value, please only enter numerical values for option selection");
                } catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.ReadKey();
                Console.Clear();

            } while (choice != 5);
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("================");
            Console.WriteLine("\tTodo App\t");
            Console.WriteLine("================");
            Console.WriteLine("1. Display List of Tasks");
            Console.WriteLine("2. Add to list of Tasks");
            Console.WriteLine("3. Update a Task from list");
            Console.WriteLine("4. Remove a Task from list");
            Console.WriteLine("5. Exit");
        }

        public static void DisplayTodoList(List<Todo> TodoItems)
        {
            Console.WriteLine("Todo List: ");
            TodoItems.ForEach(item => 
            {
                Console.WriteLine($"#. {item.Id}");
                Console.WriteLine($"Title: \t\t{item.Title}");
                Console.WriteLine($"Description: \t{item.Description}");
                Console.WriteLine($"Date Created: \t{item.DateCreated}");
                Console.WriteLine($"Date Modified: \t{item.DateModified}");
                Console.WriteLine($"Completed: \t{item.IsCompleted}");
            });

            Console.WriteLine();
        }

        public static void AddTask()
        {
            Todo todo = new Todo();
            Console.Write("Enter the title: ");
            todo.Title = Console.ReadLine() ?? "";

            Console.WriteLine("Enter the description: ");
            todo.Description = Console.ReadLine() ?? "";

            TodoController.AddTodo(todo);
        }

        public static void RemoveTask() {
            DisplayTodoList(TodoController.GetTodos() ?? new List<Todo>());

            Console.Write("Enter the ID of the Task you wish to remove: ");
            int Id = Convert.ToInt32(Console.ReadLine());

            TodoController.DeleteTodo(Id);
        }

        public static void UpdateTask()
        {
            List<Todo> todoItems = TodoController.GetTodos() ?? new List<Todo>();
            DisplayTodoList(todoItems);

            Console.Write("Enter the ID of the Task you wish to update: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Todo todoItem = todoItems.Find(item => item.Id == Id) ?? new Todo();

            Console.Write("Do you wish to update the title: (yes/no)?");
            if(Console.ReadLine() == "yes")
            {
                Console.Write("Enter the title: ");
                todoItem.Title = Console.ReadLine() ?? "";
            }
            Console.Write("Do you wish to update the description: (yes/no)?");
            if (Console.ReadLine() == "yes") 
            {
                Console.Write("Enter the description: ");
                todoItem.Description = Console.ReadLine() ?? "";
            }
            Console.WriteLine();
            Console.Write("Were you able to complete the task: (yes/no)?");
            todoItem.IsCompleted = (Console.ReadLine() == "yes") ? true : false;

            TodoController.UpdateTodo(Id, todoItem);
        }
    }

}