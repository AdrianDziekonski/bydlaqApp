using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Globalization;

namespace Bydlaq
{
    class BydlaqClass
    {
        public List<Animal> Animals { get; set; } = new List<Animal>();



        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }
        private void DisplayAnimal(Animal animal)

        {
            
            //var utylitytyttype = animal.utylityType;
            if (animal.utylityType == "byk" || animal.utylityType == "jałówka")
            {
                Console.WriteLine($"Zwierzę: rasa-{animal.type}, numer identyfikacyjny-{animal.number}, nazwa-{animal.name}, data urodzenia-{animal.dateOfBirth}, typ użytkowy-{animal.utylityType}");
            }
            else
            {

                Console.WriteLine($"Krowa: rasa-{animal.type}, numer identyfikacyjny-{animal.number}, nazwa-{animal.name}, data urodzenia-{animal.dateOfBirth}," +
                    $" typ użytkowy-{animal.utylityType}, data zacielenia-{animal.dateOfFertilization}, data ostatniego wycielenia-{animal.dateOfCalving}, czy krowa jest cielna-{animal.pregnant}, planowane wycielenie-{animal.dateOfFutureCalving} ");
            }
            //cos tu jest nie halo z instancja

        }



        private void DisplayAnimalForeach(List<Animal> animals)
        {
            foreach (var animal in animals)
            {
                DisplayAnimal(animal);
            }
        }

        public void DisplayAnimalByNumber(string number)
        {
            var animalDisplay = Animals.FirstOrDefault(c => c.number == number);
            if (AddAnimal == null)
            {
                Console.WriteLine("Zwierze nie odnalezione");
            }
            else
            {
                DisplayAnimal(animalDisplay);
            }
        }



        public void DisplayAnimalByName(string searchPhrase)
        {
            var matchingAnimal = Animals.Where(c => c.name.Contains(searchPhrase)).ToList();
            DisplayAnimalForeach(matchingAnimal);

        }

        public void DisplayAllAnimals()
        {
            DisplayAnimalForeach(Animals);
        }

        public void CheckPregnant(string searchName)//do sprawdzenia jeszcze
        {

            var searchedPregnant = Animals.Where(c => c.name.Contains(searchName)).ToList();
            for (int i = 0; i < Animals.Count; i++)
            {
                if (Animals[i].name == searchName)
                {
                    if (Animals[i].pregnant == "tak")
                    {
                        Console.WriteLine("Krowa jest cielna");
                        string result = JsonConvert.SerializeObject(Animals[i]);
                        Console.WriteLine(result);
                        
                    }
                    else if ((Animals[i].pregnant == "nie"))
                    {
                        Console.WriteLine("Krowa nie jest cielna");
                        string result2 = JsonConvert.SerializeObject(Animals[i]);
                        Console.WriteLine(result2);

                    }
                    
                }
                else //if (Animals[i].name != searchName)
                {
                    //Console.WriteLine("Brak zwierzęcia o podanej nazwie");
                }
            }

        }
        public void DeleteAnimalByName(string deleteName)
        {
            for (int i = 0; i < Animals.Count; i++)
            {
                if (Animals[i].name == deleteName)
                {
                    Animals.Remove(Animals[i]);
                }
                else if (Animals[i].name != deleteName)
                {
                    Console.WriteLine("Brak zwierzęcia o podanej nazwie");
                }
            }

            DisplayAllAnimals();

        }
        //Json chce pusty kontruktor
        public void DeserializeJsonDatabase()
        {
            
            string json = File.ReadAllText(@"BydlaqBazaDanych.json");
            //string txt = File.ReadAllText(@"BydlaqBazaDanych.txt");
            Animals = JsonConvert.DeserializeObject<List<Animal>>(json);
            //var deser= JsonConvert.DeserializeObject<Animal>(txt);
        }

        public void JsonAnimalsSaveToDatabase()
        {
            //DeserializeJsonDatabase();  

            string AnimalsString = JsonConvert.SerializeObject(Animals);

            //File.WriteAllText(@"BydlaqBazaDanych.txt", AnimalsString);
            File.WriteAllText(@"BydlaqBazaDanych.json", AnimalsString);//trzeba z tym pomyśleć czemu to tak nadpisuje 
        }

      

        public void ShowNearestCalving()
        {
            //var AnimalNearestFutureCalving=Animals.Where(c => c.dateOfFutureCalving.Contains("-")).ToList();

            for (int i = 0; i < Animals.Count; i++)
            {  DateTime now=DateTime.Now;
                DateTime futereCal;

                //(Animals[i].dateOfFutureCalving.Contains("-"))
                //RUTEK PROPONUJE DateTime.TryParseExact(Animals[i].dateOfFutureCalving,dd-MM-yyyy,CultureInfo.InvariantCulture, out futureCal)
                if (DateTime.TryParseExact(Animals[i].dateOfFutureCalving, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out futereCal))
                {
                    
                   // DateTime futereCal//=DateTime.Parse(Animals[i].dateOfFutureCalving);
                    
                    TimeSpan interval = futereCal - now;
                    if(interval.Days<31)
                    {
                        Console.WriteLine($"Krowy z terminem wycielenia w ciagu 30 dni od dziś({now}");
                        string result1= JsonConvert.SerializeObject(Animals[i]);
                        Console.WriteLine(result1);
                    }
                    else 
                    {
                        //Console.WriteLine($"Brak planowanych wycieleń w ciągu 30 dni od dziś({now}");
                    }
                }
                else 
                {
                    //Console.WriteLine("Brak cielnych krów w bazie");
                }
            
            }
            //DateTime now=DateTime.Now;
            //DateTime dateOfFutureCalvingDateTime=DateTime.Parse(animal.dateOf)
        }

        // private void SaveAnimalToDatabase(List<Animal> Animals)
        // {
        //     System.IO.StreamWriter file = new System.IO.StreamWriter(@"BydlaqBazaDanych.txt");
        //     for (int i = 0; i < Animals.Count; i++)
        //     {
        //         file.WriteLine(Animals[i]);
        //     }
        //     file.Close();
        // }

        //public void SaveChanges()
        // {
        //     SaveAnimalToDatabase(Animals);
        // }
    }
       
}


