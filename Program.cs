using System;
using Bydlaq;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Linq;


namespace Bydlaq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BYDLAQ");
            Console.WriteLine("Aplikacja do zarządzania Twoim stadem bydła");

            string path = @"BydlaqBazaDanych.txt";
            StreamWriter streamWriter;
            if (!File.Exists(path))
            {
                streamWriter = File.CreateText(path);
                Console.WriteLine("Baza danych zosatała utworzona");
            }
            else
            {
                streamWriter = new StreamWriter(path);
                Console.WriteLine("Baza danych została otwarta");
            }

            streamWriter.Close();
            

            

            Console.WriteLine("1 Dodaj zwierzę");
            Console.WriteLine("2 Wyświetl zwierzę po numerze identyfikacyjnym");
            Console.WriteLine("3 Wyświetl zwierzę po nazwie");
            Console.WriteLine("4 Wyświetl wszytskie zwierzęta z listy");
            Console.WriteLine("5 Sprawdź cielność");
            Console.WriteLine("6 Usuń zwierze");
            Console.WriteLine("7 Pokaż przyszłe wycielnia");
            Console.WriteLine("x Zamknij program");

            var userInput = Console.ReadLine();
            var Bydlaq = new BydlaqClass();//instancja klasy

            Bydlaq.DeserializeJsonDatabase();
            
            
            while (true)
            {
                switch (userInput)
                {
                    case "1":


                        Console.WriteLine("Wpisz rasę");
                        var type = Console.ReadLine();

                        Console.WriteLine("Wpisz numer identyfikacyjny");
                        var number = Console.ReadLine();

                        Console.WriteLine("Wpisz nazwę");
                        var name = Console.ReadLine();

                        Console.WriteLine("Wpisz datę urodzenia (dd-mm-yyyy)");
                        var dateOfBirth = Console.ReadLine();

                        Console.WriteLine("Wpisz typ użytkowy (krowa, byk, jałówka");
                        var utilityType = Console.ReadLine();


                        switch (utilityType)
                        {
                            case "jałówka":
                                Console.WriteLine("To wystarczające informacje o tym zwierzęciu");
                                var newAnimalJalowka = new Animal(type, number, name, dateOfBirth, utilityType);
                                Bydlaq.AddAnimal(newAnimalJalowka);
                                break;

                            case "byk":
                                Console.WriteLine("To wystarczające informacje o tym zwierzęciu");
                                var newAnimalByk = new Animal(type, number, name, dateOfBirth, utilityType);
                                Bydlaq.AddAnimal(newAnimalByk);
                                break;

                            case "krowa":
                                Console.WriteLine("Wpisz datę krycia(dd-mm-yyyy)");
                                var dateOfFertilization = Console.ReadLine();
                                Console.WriteLine("Wpisz datę ostatniego wycielenia(dd-mm-yyyy)");
                                var dateOfCalving = Console.ReadLine();
                                Console.WriteLine("Wpisz TAK jeśli zwierzę jest cielne lub NIE jeśli jest nie cielne");
                                var pregnant = Console.ReadLine();
                                Console.WriteLine("Wpisz przewidywany termin wycielenia(dd-mm-yyyy)");
                                var dateOfFutureCalving = Console.ReadLine();

                                var newAnimalKrowa = new Animal(type, number, name, dateOfBirth,
                                    utilityType, dateOfFertilization, dateOfCalving, pregnant, dateOfFutureCalving);

                                Bydlaq.AddAnimal(newAnimalKrowa);
                                break;

                        }

                        Bydlaq.JsonAnimalsSaveToDatabase();

                        break;
                        

                    //if (utilityType == "krowa")
                    //{
                    //    Console.WriteLine("Podaj datę zapłodnienia (dd-mm-yyyy)");
                    //    var dateOfFertilization = Console.ReadLine();

                    //    Console.WriteLine("Podaj datę ostatniego wycielenia (dd-mm-yyyy)");
                    //    var dateOfCalving = Console.ReadLine();

                    //    Console.WriteLine("Wpisz 0 jeśli krowa jest nie cielna lub 1 gdy krowa jest cielna");
                    //    var pregnant = Console.ReadLine();

                    //    Console.WriteLine("Podaj datę planowanego wycielenia (dd-mm-yyyy)");
                    //    var dateOfFutureCalving = Console.ReadLine();
                    //}
                    //else
                    //{
                    //    break;
                    //}

                    //System.IO.StreamWriter sw = new System.IO.StreamWriter(path);
                    //sw.WriteLine(userInput);
                    //sw.Close();
                    //var newAnimal = new Animal(type, number, name, dateOfBirth, utilityType, dateOfCalving, dateOfFertilization, pregnant, dateOfFutureCalving); 

                    //trzeba te daty i boola zamienic na stringi w konstruktorze chyba
                    case "2":
                        Console.WriteLine("Wpisz numer identyfikacyjny");
                        var animalByNumber=Console.ReadLine();
                      
                        Bydlaq.DisplayAnimalByNumber(animalByNumber);
                       
                        //Bydlaq.DisplayOtherThanCowByNumber(animalByNumber);
                        
                            break;

                    case "3":
                        Console.WriteLine("Wpisz nazwę zwierzęcia");
                        var searchedAnimal = Console.ReadLine();
                        Bydlaq.DisplayAnimalByName(searchedAnimal);


                        break;

                    case "4":
                        Console.WriteLine("Lista wszytskich zwierząt");
                        Bydlaq.DisplayAllAnimals();

                        break;

                    case "5":
                        Console.WriteLine("Wpisz nazwę zwierzęcia");
                        var pregnantAnimal=Console.ReadLine();
                        Bydlaq.CheckPregnant(pregnantAnimal);//co to wyjdzie?

                        break;

                    case "6":
                        Console.WriteLine("Wprowadź nazwę zwierzęcia do usunięcia");
                        var deleteAnimal=Console.ReadLine(); 
                        Bydlaq.DeleteAnimalByName(deleteAnimal);

                        break;

                    case "7":
                        Console.WriteLine("Opcja pokazuje krowy z planowanym terminem wycielenia w ciągu 30 dni od dziś");
                        Bydlaq.ShowNearestCalving();
                        break;

                       

                    case "x":
                        return;
                }
                Console.WriteLine("Wybierz operację");
                userInput = Console.ReadLine();
            }




        }
    }
}