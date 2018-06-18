using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ConsoleSolution.Models;
using Newtonsoft.Json;

namespace core_console
{
    class Program
    {
        static void Main(string[] args)
        {
           var individuals = GetIndividuals().ToList();

            Console.WriteLine("Answers:");

            var individualsOverAge50 = individuals.Count(x => x.Age > 50);
            Console.WriteLine($"Individuals over age 50: {individualsOverAge50}");

            var latestRegistered = individuals
                .Where(x => x.IsActive)
                .OrderByDescending(y => y.RegisteredTime)
                .FirstOrDefault()
                ;
            Console.WriteLine("Latest registered out of active users: "
                + $"{latestRegistered?.Name.First} {latestRegistered?.Name.Last}");

            var fruitCounts = individuals
                .GroupBy(x => x.FavoriteFruit)
                .Select(r => new
                {
                    Fruit = r.Key,
                    Count = r.Count()
                });
            Console.WriteLine("Favorite fruit counts: ");
            foreach (var fruitCount in fruitCounts)
            {
                Console.WriteLine($"  ({fruitCount.Count}) {fruitCount.Fruit}");
            }

            var mostCommonEyeColor = individuals
                .GroupBy(x => x.EyeColor)
                .Select(r => new
                {
                    EyeColor = r.Key,
                    Count = r.Count()
                })
                .OrderByDescending(o => o.Count)
                .FirstOrDefault()
                ;
            Console.WriteLine($"Most common eye color: {mostCommonEyeColor?.EyeColor}");

            var totalBalance = individuals.Sum(x => x.BalanceAmount);
            Console.WriteLine($"Total balance: {totalBalance:C}");

            var findIndividual = individuals
                    .FirstOrDefault(x => x.Id == "5aabbca3e58dc67745d720b1")
                ;
            Console.WriteLine("Find individual by ID: "
                + $"{findIndividual?.Name.First} {findIndividual?.Name.Last}");
        }

        private static IEnumerable<Individual> GetIndividuals()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "data.json";

            using (Stream stream = assembly.GetManifestResourceStream(
                $"{assembly.GetName().Name}.{resourceName}"
                ))
            {
                Debug.Assert(stream != null, nameof(stream) + " != null");
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<IEnumerable<Individual>>(result);
                }
            }
        }
    }
}
