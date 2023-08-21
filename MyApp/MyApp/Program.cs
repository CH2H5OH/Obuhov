using ConsoleTables;
using System.Diagnostics;
using System.Globalization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string comand = args[0];

            if (comand == "1")
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ConsoleTable consoleTable = new ConsoleTable("ФИО", "Дата рождения", "Пол", "Количество полных лет");

                    if (context.Persons != null)
                    {
                        List<Person> persons = context.Persons.ToList();

                        ShowDataTable(persons);
                    }
                    else
                    {
                        consoleTable.Write();
                    }
                }
            }
            else if (comand == "2")
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    string fullName = args[1];
                    DateTime birthDate = DateTime.Parse(args[2]);
                    Gender gender;
                    if (args[3] == "m" || args[3] == "M")
                    {
                        gender = Gender.Male;
                    }
                    else
                    {
                        gender = Gender.Female;
                    }
                    Person person = new Person();
                    person.FullName = fullName;
                    person.BirthDate = birthDate;
                    person.Gender = gender;
                    context.Persons.Add(person);
                    context.SaveChanges();

                    List<Person> list = context.Persons.ToList();
                    list = list.DistinctBy(x => x.FullName + x.BirthDate).OrderBy(x => x.FullName).ToList();

                    ShowDataTable(list);
                }
            }
            else if (comand == "3")
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    List<Person> people = DataGenerator1KK();

                    context.Persons.AddRange(people);
                    context.SaveChanges();

                    List<Person> peopleF = DataGenerator100();

                    context.Persons.AddRange(peopleF);
                    context.SaveChanges();

                    //ShowDataTable(context.Persons.ToList());
                }
            }
            else if (comand == "4")
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    List <Person> people = context.Persons.Where(x => x.Gender == Gender.Male).Where(x => x.FullName.StartsWith("F")).ToList();

                    stopwatch.Stop();

                    ShowDataTable(people);

                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                }
            }
        }

        public static void ShowDataTable(List<Person> people)
        {
            ConsoleTable consoleTable = new ConsoleTable("ФИО", "Дата рождения", "Пол", "Количество полных лет");

            foreach (Person person in people)
            {
                consoleTable.AddRow(person.FullName, person.BirthDate.ToShortDateString(), person.Gender.ToString(), person.Age.ToString());
            }

            consoleTable.Write();
        }

        public static List<Person> DataGenerator1KK()
        {

            string[] secondnamesM =
            { 
              "Gordeev", "Komarov", "Dmitriev", "Pavlov", "Kornilov", "Eliseev", "Kabanov", "Mironov", "Larionov", "Nazarov",
              "Konovalov", "Petukhov", "Gushchin", "Sidorov", "Tsvetkov", "Seleznev", "Pavlov", "Shubin", "Mikheev", "Semyonov",
              "Mikhailov", "Komissarov", "Lavrentiev", "Drozdov", "Panfilov", "Ovchinnikov", "Emelyanov", "Isakov", "Savin", "Kabanov",
              "Khokhlov", "Popov", "Bykov", "Abramov", "Vishnyakov", "Bobrov", "Myasnikov", "Isakov", "Gavrilov", "Pavlov",
              "Zakharov", "Yudin", "Egorov", "Efimov", "Tretyakov", "Bespalov", "Romanov", "Morozov", "Voronov", "Danilov"
            };
            string[] firstnamesM =
            { 
              "Adam", "Afanasy", "Grigory", "Anton", "Platon", "Gordey", "Gayane", "Ovid", "Anatoly", "Willy",
              "Benedikt", "Gleb", "Veniamin", "Illarion", "Venedikt", "Nelli", "Ignatiy", "Efim", "Dmitry", "Panteleimon",
              "Adrian", "Robert", "Gayane", "Garry", "Arseny", "Nazariy", "Anatoly", "Kondrat", "Akim", "Yuri", "Yan",
              "Natan", "Averky", "Vitold", "Kondrat", "Ippolit", "Martyn", "Stanislav", "Arkhip", "Leonty",
              "Velor", "Natan", "Walter", "May", "Igor", "Vadim", "Lev", "Panteleimon", "Iosif", "Stanislav"
            };
            string[] thirdnamesM =
            { 
              "Yaroslavovich", "Tarasovich", "Egorovich", "Evgenievich", "Robertovich", "Semyonovich", "Antonovich", "Oskarovich", "Fedoseevich", "Pavlovich",
              "Evgenievich", "Ulebovich", "Mironovich", "Vitalievich", "Alekseevich", "Avdeevich", "Romanovich", "Stanislavovich", "Boguslavovich", "Germanovich",
              "Natanovich", "Robertovich", "Yulianovich", "Sozonovich", "Vsevolodovich", "Avksentevich", "Yakunovich", "Ignatievich", "Lukievich", "Natanovich",
              "Davidovich", "Sergeevich", "Natanovich", "Lavrentievich", "Mironovich", "Mikhailovich", "Semyonovich", "Veniaminovich", "Matveevich", "Onisimovich",
              "Maksimovich", "Vitalievich", "Melsovich", "Mironovich", "Petrovich", "Timofeevich", "Vladlenovich", "Matveevich", "Tarasovich", "Irineevich"
            };

            string[] secondnamesF =
            { 
              "Gordeeva", "Komarova", "Dmitrieva", "Pavlova", "Kornilova", "Eliseeva", "Kabanova", "Mironova", "Larionova", "Nazarova",
              "Konovalova", "Petukhova", "Gushchina", "Sidorova", "Tsvetkova", "Selezneva", "Pavlova", "Shubina", "Mikheeva", "Semyonova",
              "Mikhailova", "Komissarova", "Lavrentieva", "Drozdova", "Panfilova", "Ovchinnikova", "Emelyanova", "Isakova", "Savina", "Kabanova",
              "Khokhlova", "Popova", "Bykova", "Abramova", "Vishnyakova", "Bobrova", "Myasnikova", "Isakova", "Gavrilova", "Pavlova",
              "Zakharova", "Yudina", "Egorova", "Efimova", "Tretyakova", "Bespalova", "Romanova", "Morozova", "Voronova", "Danilova"
            };
            string[] firstnamesF =
            { 
              "Gaby", "Harita", "Sylvia", "Xenia", "Sabina", "Daria", "Nadezhda", "Vlada", "Adeliya", "Lana",
              "Maya", "Vasilisa", "Oktyabrina", "Azalia", "Ruslana", "Aurora", "Romana", "Eliza", "Marisha", "Vera",
              "Yuzefa", "Sandra", "Landysh", "Inga", "Eugenie", "Louise", "Tamara", "Iren", "Aurelia", "Anfisa",
              "Alice", "Eliza", "Uliana", "Faina", "Yasmina", "Elina", "Elvira", "Alberta", "Margarita", "Bronislava",
              "Naomi", "Neolina", "Flora", "Georgina", "Teresa", "Valeria", "Camilla", "Genevieve", "Zoya", "Mariya"
            };
            string[] thirdnamesF =
            { 
              "Yaroslavovna", "Tarasovna", "Egorovna", "Evgenievna", "Robertovna", "Semyonovna", "Antonovna", "Oskarovna", "Fedoseevna", "Pavlovna",
              "Evgenievna", "Ulebovna", "Mironovna", "Vitalievna", "Alekseevna", "Avdeevna", "Romanovna", "Stanislavovna", "Boguslavovna", "Germanovna",
              "Natanovna", "Robertovna", "Yulianovna", "Sozonovna", "Vsevolodovna", "Avksentevna", "Yakunovna", "Ignatievna", "Lukievna", "Natanovna",
              "Davidovna", "Sergeevna", "Natanovna", "Lavrentievna", "Mironovna", "Mikhailovna", "Semyonovna", "Veniaminovna", "Matveevna", "Onisimovna",
              "Maksimovna", "Vitalievna", "Melsovna", "Mironovna", "Petrovna", "Timofeevna", "Vladlenovna", "Matveevna", "Tarasovna", "Irineevna"
            };

            Random rnd = new Random();

            List<Person> people = new List<Person>();

            string secondname;
            string firstname;
            string thirdname;

            Gender gender;

            for (int i = 0; i < 1000000; i++)
            {

                if (i % 2 == 0)
                {
                    gender = Gender.Male;

                    secondname = secondnamesM[rnd.Next(secondnamesM.Length)];
                    firstname = firstnamesM[rnd.Next(firstnamesM.Length)];
                    thirdname = thirdnamesM[rnd.Next(thirdnamesM.Length)];
                }
                else
                {
                    gender = Gender.Female;

                    secondname = secondnamesF[rnd.Next(secondnamesF.Length)];
                    firstname = firstnamesF[rnd.Next(firstnamesF.Length)];
                    thirdname = thirdnamesF[rnd.Next(thirdnamesF.Length)];
                }

                DateTime birthDay = SetBirthday();

                Person person = new Person();

                person.FullName = $"{secondname} {firstname} {thirdname}";
                person.BirthDate = birthDay;
                person.Gender = gender;

                people.Add(person);
            }

            return people;
        }

        public static List<Person> DataGenerator100()
        {
            string[] secondnamesM =
            { 
              "Frolov", "Fokin", "Fedorov", "Filatov", "Fomichev", "Fedotov", "Fadeev", "Fedoseev", "Fedosov", "Fomin" 
            };
            string[] firstnamesM =
            { 
              "Adam", "Afanasy", "Grigory", "Anton", "Platon", "Gordey", "Gayane", "Ovid", "Anatoly", "Willy",
              "Benedikt", "Gleb", "Veniamin", "Illarion", "Venedikt", "Nelli", "Ignatiy", "Efim", "Dmitry", "Panteleimon",
              "Adrian", "Robert", "Gayane", "Garry", "Arseny", "Nazariy", "Anatoly", "Kondrat", "Akim", "Yuri", "Yan",
              "Natan", "Averky", "Vitold", "Kondrat", "Ippolit", "Martyn", "Stanislav", "Arkhip", "Leonty",
              "Velor", "Natan", "Walter", "May", "Igor", "Vadim", "Lev", "Panteleimon", "Iosif", "Stanislav"
            };
            string[] thirdnamesM =
            { 
              "Yaroslavovich", "Tarasovich", "Egorovich", "Evgenievich", "Robertovich", "Semyonovich", "Antonovich", "Oskarovich", "Fedoseevich", "Pavlovich",
              "Evgenievich", "Ulebovich", "Mironovich", "Vitalievich", "Alekseevich", "Avdeevich", "Romanovich", "Stanislavovich", "Boguslavovich", "Germanovich",
              "Natanovich", "Robertovich", "Yulianovich", "Sozonovich", "Vsevolodovich", "Avksentevich", "Yakunovich", "Ignatievich", "Lukievich", "Natanovich",
              "Davidovich", "Sergeevich", "Natanovich", "Lavrentievich", "Mironovich", "Mikhailovich", "Semyonovich", "Veniaminovich", "Matveevich", "Onisimovich",
              "Maksimovich", "Vitalievich", "Melsovich", "Mironovich", "Petrovich", "Timofeevich", "Vladlenovich", "Matveevich", "Tarasovich", "Irineevich"
            };

            Random rnd = new Random();

            List<Person> people = new List<Person>();

            string secondname;
            string firstname;
            string thirdname;

            Gender gender;

            for (int i = 0; i < 100; i++)
            {

                gender = Gender.Male;

                secondname = secondnamesM[rnd.Next(secondnamesM.Length)];
                firstname = firstnamesM[rnd.Next(firstnamesM.Length)];
                thirdname = thirdnamesM[rnd.Next(thirdnamesM.Length)];

                DateTime birthDay = SetBirthday();

                Person person = new Person();

                person.FullName = $"{secondname} {firstname} {thirdname}";
                person.BirthDate = birthDay;
                person.Gender = gender;

                people.Add(person);
            }

            return people;
        }

        public static DateTime SetBirthday()
        {
            int startYear = DateTime.Now.Year - 60;
            int endYear = DateTime.Now.Year - 18;

            Random rnd = new Random();

            int randomYear = rnd.Next(startYear, endYear);
            int randomMonth = rnd.Next(1, 13);
            int randomDay = rnd.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));

            DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);

            return randomDate;
        }
    }
}