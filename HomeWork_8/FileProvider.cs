using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HomeWork_8
{
    class FileProvider
    {
        /// <summary>
        /// чтение XML файла работников компании
        /// </summary>
        /// <param name="path">путь к файлу для чтения</param>
        /// <returns>список работников</returns>
        public static List<XElement> DeserializeWorker(string path)
        {
            string xml = File.ReadAllText(path);
            var workersFromFile = XDocument.Parse(xml)
                .Descendants("CompanyStructure")
                .Descendants("WORKER")
                .ToList();
            return workersFromFile;
        }

        /// <summary>
        /// полностью перезаписывает файл с данными о работниках в формате XML
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="workers">список работников</param>
        public static void SerializeWorker(string path, List<Worker> workers)
        {
            XElement companyStruct = new XElement("CompanyStructure");
            foreach (var item in workers)
            {
                XElement unicNumber = new XElement("unicNumber", item.UnicNumber);
                XElement projects = new XElement("projects", item.Projects);
                XElement name = new XElement("name", item.Name);
                XElement surname = new XElement("surname", item.Surname);
                XElement salary = new XElement("salary", item.Salary);
                XElement age = new XElement("age", item.Age);
                XElement dept = new XElement("dept", item.Department);
                XElement worker = new XElement("WORKER", unicNumber, name, surname, dept, salary, age, projects);
                companyStruct.Add(worker);
            }
            companyStruct.Save(path);
        }

        /// <summary>
        /// дописывает строку в конец файла
        /// </summary>
        /// <param name="nameOfFile">в какой файл записываем</param>
        /// <param name="newLine">что записать в конец файла</param>
        public static void AddNewLineToFile(string nameOfFile, string newLine)
        {
            File.AppendAllText(nameOfFile, newLine);            
        }

        /// <summary>
        /// записывает данные по новому сотруднику, введенные пользователем в формате hml
        /// </summary>
        /// <param name="workers">список всех работников в компании</param>
        /// <param name="path">файл куда дозаписывается информация</param>
        /// <param name="curName">имя нового работника</param>
        /// <param name="curSurname">фамилия нового работника</param>
        /// <param name="curSalary">зарплата нового работника</param>
        /// <param name="curAge">возраст нового работника</param>
        /// <param name="curProjects">проекты нового работника</param>
        /// <param name="curDepartment">департамент нового работника</param>
        public static void AddWorkerToFile(List<Worker> workers, string path, string curName, string curSurname, int curSalary, int curAge, int curProjects, string curDepartment)
        {
            XDocument xml = XDocument.Load(path);
            XElement unicNumber = new XElement("unicNumber", workers.Count + 1);
            XElement name = new XElement("name", curName);
            XElement surname = new XElement("surname", curSurname);
            XElement salary = new XElement("salary", curSalary);
            XElement age = new XElement("age", curAge);
            XElement dept = new XElement("dept", curDepartment);
            XElement projects = new XElement("projects", curProjects);
            xml.Root.Add(new XElement("WORKER", unicNumber, name, surname, dept, salary, age, projects));
            xml.Save(path);
        }
    }
}
