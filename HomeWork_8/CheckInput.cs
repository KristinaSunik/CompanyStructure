using System;

namespace HomeWork_8
{
    class CheckInput
    {
        /// <summary>
        /// проверяет возможность записи данных от юзера в int 
        /// </summary>
        /// <returns>возвращает проверенное число от юзера</returns>
        public static int CheckUserData()
        {
            bool flag = true;
            int userNumber = -1;
            while (flag)
            {
                string userData = Console.ReadLine();
                bool wasParsed = int.TryParse(userData, out userNumber);
                if (wasParsed)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Число введено неверно, введите правильно:");
                }
            }

            return userNumber;
        }

        /// <summary>
        /// проверка на правильность введенных данных юзером
        /// </summary>
        /// <returns>номер выбранного действия</returns>
        public static int UserNumber(int from, int to)
        {
            int userNumber = CheckUserData();
            while (userNumber < from || userNumber > to)
            {
                Console.WriteLine($"Выберите номер от {from} до {to}:");
                userNumber = CheckUserData();
            }

            return userNumber;
        }

        /// <summary>
        /// проверка запроса пользователя
        /// </summary>
        /// <returns>ответ пользователя</returns>
        public static bool CheckUserAnswer()
        {
            if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                return true;
            return false;
        }
    }
}
