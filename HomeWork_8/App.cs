using System;
using System.Collections.Generic;

namespace HomeWork_8
{
    class App
    {
        public static void Run()
        {
            
            List<Department> departments = new List<Department>();
            List<Worker> workers = new List<Worker>();
            string path = "_company.xml";

            CreateCompanyStructure.CreateWorkers(path);

            Worker.AddWorkersToList(workers, path);
            Department.AddDeptsToList(departments, workers);

            while (true)
            {
                Console.WriteLine($"Выберите действие:{Environment.NewLine}" +
                    $"1. Добавить отдел {Environment.NewLine}" +
                    $"2. Добавить работника в отдел  {Environment.NewLine}" +
                    $"3. Удалить работника из отдела  {Environment.NewLine}" +
                    $"4. Добавить новое имя или фамилию работника  {Environment.NewLine}" +
                    $"5. Сортировать работников в отделе по выбранным данным {Environment.NewLine}" +
                    $"6. Вывести информацию о работниках в департаменте  {Environment.NewLine}" +
                    $"7. Вывести информацию о департаментах  {Environment.NewLine}" +
                    $"8. Вывести информацию о работниках");
                int userNumber = CheckInput.UserNumber(1, 8);
                if (userNumber == 1)
                {
                    Console.WriteLine("Вы выбрали - Добавить отдел");
                    Department.AddDeptFromUser(departments);
                }
                else if (userNumber == 2)
                {
                    Console.WriteLine("Вы выбрали - Добавить работника в отдел");
                    Console.WriteLine("В какой отдел вы хотите добавить работника?");
                    int deptNumber = Department.Choose(departments);
                    string currentDept = departments[deptNumber].Name;
                    Console.WriteLine("ВВедите имя работника:");
                    string name = Console.ReadLine();
                    Console.WriteLine("ВВедите фамилию работника:");
                    string surname = Console.ReadLine();
                    Console.WriteLine("ВВедите зарплату работника:");
                    int salary = CheckInput.CheckUserData();
                    Console.WriteLine("ВВедите возраст работника:");
                    int age = CheckInput.CheckUserData();
                    Console.WriteLine("ВВедите количество проектов у работника:");
                    int projects = CheckInput.CheckUserData();
                    int workerID = workers[workers.Count - 1].UnicNumber - 1;
                    FileProvider.AddWorkerToFile(workers, path, name, surname, salary, age, projects, currentDept);
                    Worker.AddWorkerToList(workers, workerID, name, surname, salary, age, projects, currentDept);
                    Department.AddWorkerToDept(departments, deptNumber, workerID);
                    Console.WriteLine("Работник успешно добавлен");
                }
                else if (userNumber == 3)
                {
                    Console.WriteLine("Вы выбрали - Удалить работника из отдела");
                    Console.WriteLine("Из какого отдела вы хотите удалить работника?");
                    int deptNumber = Department.Choose(departments);
                    List<Worker> sortedByDept = Sort.SortByDept(workers, departments, deptNumber);
                    Worker.PrintAll(sortedByDept);
                    Console.WriteLine("Выберите работника для удаления и введите его номер №:");
                    int workerIDfromUser = CheckInput.CheckUserData();
                    if (Worker.DeleteWorkerFromList(sortedByDept, workers, workerIDfromUser))
                    {
                        Console.WriteLine("Работник успешно удален");
                    }
                    else
                    {
                        Console.WriteLine("Работника с таким номером не оказалось");
                    }
                    Worker.DeleteWorkerFromList(sortedByDept, workers, workerIDfromUser);
                    Department.DeleteWorkerFromDept(departments, deptNumber, workerIDfromUser);
                    FileProvider.SerializeWorker(path, workers);
                }
                else if (userNumber == 4)
                {
                    Console.WriteLine("Вы выбрали - Добавить новое имя или фамилию работника");
                    Console.WriteLine("Хотите добавить новое имя? y / n ?");
                    if (CheckInput.CheckUserAnswer())
                    {
                        Console.WriteLine("Введите новое имя:");
                        string name = Console.ReadLine();
                        FileProvider.AddNewLineToFile("Names.txt", $"\n {name}");
                    }
                    else
                    {
                        Console.WriteLine("Значит фамилию новую добавим? y / n ?");
                        if (CheckInput.CheckUserAnswer())
                        {
                            Console.WriteLine("Введите новую фамилию:");
                            string surname = Console.ReadLine();
                            FileProvider.AddNewLineToFile("Surnames.txt", $"\n {surname}");
                        }
                        else
                        {
                            Console.WriteLine("Я не понимаю что вы тогда хотите от меня!");
                        }
                    }
                }
                else if (userNumber == 5)
                {
                    Sort.Run(departments, workers);
                }
                else if (userNumber == 6)
                {
                    Console.WriteLine("Вы выбрали - Показать всех работников отдела");
                    Console.WriteLine("Из какого отдела вы хотите посмотреть работников?");
                    int deptNumber = Department.Choose(departments);
                    List<Worker> sortedByDept = Sort.SortByDept(workers, departments, deptNumber);
                    Worker.PrintAll(sortedByDept);
                }
                else if (userNumber == 7)
                {
                    Console.WriteLine("Вы выбрали - Вывести информацию о департаментах");
                    Department.PrintAll(departments);
                }
                else if (userNumber == 8)
                {
                    Console.WriteLine("Вы выбрали - ывести информацию о работниках");
                    Worker.PrintAll(workers);
                }
            }
        }
    }
}
