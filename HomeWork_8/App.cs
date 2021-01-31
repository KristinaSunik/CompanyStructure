using System;
using System.Collections.Generic;

namespace HomeWork_8
{
    class App
    {
        public static void Run()
        {
            
            List<Department> departments = new List<Department>();  //Создаем пустой список департаментов
            List<Worker> workers = new List<Worker>();              //Создаем пустой список работников
            string path = "_company.xml";                           //Путь для хранения всей структуры компании

            CreateCompanyStructure.CreateWorkers(path);             //Заполняем списки департаментов и работников рандомными данными, сохраняя данные в XML

            Worker.AddWorkersToList(workers, path);                 //Заполняем список департаментов данными из XML файла
            Department.AddDeptsToList(departments, workers);        //Заполняем список работников данными из XML файла

            while (true) //Работа самого приложения(добавление/удаление/вывод данных на консоль)
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
                    int deptNumber = Department.Choose(departments);                    //Номер отдела с которым работаем
                    string currentDept = departments[deptNumber].Name;                  //Название отдела с которым работаем
                    Console.WriteLine("Введите имя работника:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите фамилию работника:");
                    string surname = Console.ReadLine();
                    Console.WriteLine("Введите зарплату работника:");
                    int salary = CheckInput.CheckUserData();
                    Console.WriteLine("Введите возраст работника:");
                    int age = CheckInput.CheckUserData();
                    Console.WriteLine("Введите количество проектов у работника:");
                    int projects = CheckInput.CheckUserData();
                    int workerID = workers[workers.Count - 1].UnicNumber - 1;           //Вычисляем последующий уникальный номер работника
                    FileProvider.AddWorkerToFile(workers, path, name, surname, salary, age, projects, currentDept);
                    Worker.AddWorkerToList(workers, workerID, name, surname, salary, age, projects, currentDept);
                    Department.AddWorkerToDept(departments, deptNumber, workerID);
                    Console.WriteLine("Работник успешно добавлен");
                }
                else if (userNumber == 3)
                {
                    Console.WriteLine("Вы выбрали - Удалить работника из отдела");
                    Console.WriteLine("Из какого отдела вы хотите удалить работника?");
                    int deptNumber = Department.Choose(departments);                                //Номер отдела с которым работаем
                    List<Worker> sortedByDept = Sort.SortByDept(workers, departments, deptNumber);   //Список работников только из этого департамента
                    Worker.PrintAll(sortedByDept);
                    Console.WriteLine("Выберите работника для удаления и введите его номер №:");
                    int workerIDfromUser = CheckInput.CheckUserData();                               //сохраняем выбранный пользователем уникальный номер работника
                    if (Worker.DeleteWorkerFromList(sortedByDept, workers, workerIDfromUser))
                    {
                        Console.WriteLine("Работник успешно удален");
                    }
                    else
                    {
                        Console.WriteLine("Работника с таким номером не оказалось");               //если такого номера не обнаружилось(ошибка ввода пользователем)
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
                    int deptNumber = Department.Choose(departments);                                 //Номер отдела с которым работаем
                    List<Worker> sortedByDept = Sort.SortByDept(workers, departments, deptNumber);   //Список работников только из этого департамента
                    Worker.PrintAll(sortedByDept);
                }
                else if (userNumber == 7)
                {
                    Console.WriteLine("Вы выбрали - Вывести информацию о департаментах");
                    Department.PrintAll(departments);
                }
                else if (userNumber == 8)
                {
                    Console.WriteLine("Вы выбрали - Вывести информацию о работниках");
                    Worker.PrintAll(workers);
                }
            }
        }
    }
}
