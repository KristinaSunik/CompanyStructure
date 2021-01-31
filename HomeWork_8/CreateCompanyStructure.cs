using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HomeWork_8
{

    class CreateCompanyStructure
    {
        /// <summary>
        /// считывает строку с названием департамента
        /// </summary>
        /// <param name="numberOfDepartment">номер строки, которую считать</param>
        /// <returns>названием департамента</returns>
        public static string ChooseDepartment(int numberOfDepartment)
        {
            string dept = File.ReadLines("Depts.txt").ElementAt(numberOfDepartment);

            return dept;
        }

        /// <summary>
        /// выбирает имя из файла с именами по рандомному числу
        /// </summary>
        /// <returns>имя работника</returns>
        public static string ChooseName()
        {
            Random random = new Random();
            int countStrings = File.ReadAllLines("Names.txt").Length;
            string name = File.ReadLines("Names.txt").ElementAt(random.Next(countStrings));

            return name;
        }

        /// <summary>
        /// выбирает фамилию из файла с фамилиями работнику
        /// </summary>
        /// <returns>фамилию работника</returns>
        public static string ChooseSurname()
        {
            Random random = new Random();
            int countStrings = File.ReadAllLines("Surnames.txt").Length;
            string surname = File.ReadLines("Surnames.txt").ElementAt(random.Next(countStrings));

            return surname;
        }

        /// <summary>
        /// Создаем рандомное количество работников в каждый отдел. Сохраняем все в XML
        /// </summary>
        /// <param name="path">имя файла куда сохраняется</param>
        public static void CreateWorkers(string path)
        {
            Random random = new Random();
            int workerID = 1;
            XElement companyStruct = new XElement("CompanyStructure");
            int countDepartments = File.ReadAllLines("Depts.txt").Length;
            for (int j = 0; j < countDepartments; j++)
            {
                for (int i = 0; i < random.Next(3, 15); i++)
                {
                    XElement unicNumber = new XElement("unicNumber", workerID);
                    XElement projects = new XElement("projects", random.Next(1, 6));
                    XElement name = new XElement("name", ChooseName());
                    XElement surname = new XElement("surname", ChooseSurname());
                    XElement salary = new XElement("salary", random.Next(21_000, 90_000));
                    XElement age = new XElement("age", random.Next(19, 48));
                    XElement dept = new XElement("dept", ChooseDepartment(j));
                    XElement worker = new XElement("WORKER", unicNumber, name, surname, dept, salary, age, projects);
                    companyStruct.Add(worker);
                    workerID++;
                }
            }

            companyStruct.Save(path);
        }
    }
}
