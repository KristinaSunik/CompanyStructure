using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HomeWork_8
{
    struct Department
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<int> WorkersID { get; set; }

        public Department(string name, DateTime creationDate, List<int> workersID)
        {
            Name = name;
            CreationDate = creationDate;
            WorkersID = workersID;
        }

        /// <summary>
        /// считает количество работников в департаменте по списку их ID
        /// </summary>
        /// <returns>число работников в департаменте</returns>
        public int WorkersCount()
        {
            return WorkersID.Count();
        }

        /// <summary>
        /// Выводит на консоль информацию о департаменте по заданной схеме
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"{Name,40}  {CreationDate,25}  {WorkersCount(),30}");
        }

        /// <summary>
        /// выводит все названия департаментов, дату создания и количество сотрудников в департаменте 
        /// </summary>
        /// <param name="departments">список всех департаментов в компании</param>
        public static void PrintAll(List<Department> departments)
        {
            Console.WriteLine($"{"Отдел:",40} {"Дата создания:",25} {"Количество сотрудников:",30}");
            foreach (var item in departments)
            {
                item.Print();
            }
        }

        /// <summary>
        /// создает список работников в определенном департаменте
        /// </summary>
        /// <param name="nameDept">имя департамента</param>
        /// <param name="workers">список всех работников</param>
        /// <returns>список работников в этом департаменте</returns>
        public static List<Worker> GetAllWorkersInDept(string nameDept, List<Worker> workers)
        {
            List<Worker> workersInDept = new List<Worker>();
            foreach (var i in workers)
            {
                if (i.Department == nameDept)
                {
                    workersInDept.Add(i);
                }
            }

            return workersInDept;
        }

        /// <summary>
        /// выбирает ID работников, относящихся к выбранному департаменту
        /// </summary>
        /// <param name="nameDept">название департамента</param>
        /// <param name="workers">список всех работников</param>
        /// <returns>список ID работников, для выбранного департамента</returns>
        public static List<int> GetWorkersID(string nameDept, List<Worker> workers)
        {
            List<int> workersInDept = new List<int>();
            foreach (var i in workers)
            {
                if (i.Department == nameDept)
                {
                    workersInDept.Add(i.UnicNumber);
                }
            }

            return workersInDept;
        }

        /// <summary>
        /// заполняет список департаментов и их работников по структуре Departament
        /// </summary>
        /// <param name="departments">список всех департаментов</param>
        /// <param name="workers">список всех работников</param>
        public static void AddDeptsToList(List<Department> departments, List<Worker> workers)
        {
            Random random = new Random();
            int countDepartments = File.ReadAllLines("Depts.txt").Length;
            string[] deptName = new string[countDepartments];
            for (int j = 0; j < countDepartments; j++)
            {
                deptName[j] = CreateCompanyStructure.ChooseDepartment(j);
            }
            foreach (var item in deptName)
            {
                departments.Add(new Department(item,
                  new DateTime(random.Next(1988, 2021), random.Next(1, 12), random.Next(1, 31)),
                    GetWorkersID(item, workers)));
            }
        }

        /// <summary>
        /// добавляет название департамента  в файл и  в список c сегодняшней датой и пустым списком работников
        /// </summary>
        /// <param name="departments">список уже существующих департаментов</param>
        public static void AddDeptFromUser(List<Department> departments)
        {
            Console.WriteLine("Введите название отдела:");
            string nameForNewDept = Console.ReadLine();
            FileProvider.AddNewLineToFile("Depts.txt", $"\n {nameForNewDept}");
            departments.Add(new Department(nameForNewDept,
                  DateTime.Now, new List<int>()));

        }

        /// <summary>
        /// удаляет ID работника из департамента
        /// </summary>
        /// <param name="departments">список департаментов</param>
        /// <param name="deptNumber">номер департамента, в котором находится работник на удаление</param>
        /// <param name="workerIDfromUser">ID выбранное юзером для удаления работника</param>
        public static void DeleteWorkerFromDept(List<Department> departments, int deptNumber, int workerIDfromUser)
        {
            foreach (var id in departments[deptNumber].WorkersID)
            {
                if (id == workerIDfromUser)
                {
                    departments[deptNumber].WorkersID.Remove(id);
                    break;
                }
            }
        }

        /// <summary>
        /// Клиент выбирает из предложенных департаментов номер нужного ему для дальнейшей с ним работы
        /// </summary>
        /// <param name="departments">список департаментов в компании</param>
        /// <returns>номер выбранного клиентом департамента</returns>
        public static int Choose(List<Department> departments)
        {
            for (int i = 0; i < departments.Count; i++)
            {
                Console.WriteLine($"{i} - {departments[i].Name}");
            }

            int numberOfDept = CheckInput.UserNumber(0, departments.Count - 1);
            Console.WriteLine($"Вы выбрали номер {numberOfDept} - это {departments[numberOfDept].Name}," +
                $" количество сотрудников в нем {departments[numberOfDept].WorkersCount()}");
            return numberOfDept;
        }

        /// <summary>
        /// Добавляет ID работника в нужный департамент
        /// </summary>
        /// <param name="departments">список департаментов в компании</param>
        /// <param name="deptNumber">номер департамента, куда добавляем работника</param>
        /// <param name="workerID">ID работника для добавления</param>
        public static void AddWorkerToDept(List<Department> departments, int deptNumber, int workerID)
        {
            departments[deptNumber].WorkersID.Add(workerID);
        }
    }
}

