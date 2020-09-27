using System;
using System.Linq;

namespace MaterialsWebApp.Models
{
    public static class DbInitializer
    {
        public static void Initialize(RecordKeepingContext db)
        {
            Random random = new Random();

            db.Database.EnsureCreated();

            if (db.Details.Any() || db.Materials.Any() || db.Units.Any() || db.Components.Any() || db.IncomeExpenseBook.Any())
            {
                return;
            }

            int detailsNumber = 100;
            int materialsNumber = 100;
            int unitsNumber = 100;
            int componentsNumber = 100;
            int ieBookNumber = 100;

            string[] materialNames = { "Цемент", "Доска", "Шпаклевка", "Пенопласт", "Клей", "Плитка", "Краска", "Фанера", "Гипсокартон", "Ламинат", "Линолеум", "Гипс", "Обои" };
            string[] descriptions = { "Защита", "Сцепление", "Декор", "Утепление", "Покрытие" };

            for (int i = 0; i < detailsNumber; i++) db.Details.Add(new Detail { Code = random.Next(10000, 30000) });
            db.SaveChanges();

            for (int i = 0; i < materialsNumber; i++) db.Materials.Add(new Material { Code = random.Next(10000, 30000), Name = getRandomString(materialNames, random) });
            db.SaveChanges();

            for (int i = 0; i < unitsNumber; i++) db.Units.Add(new Unit { Description = getRandomString(descriptions, random) });
            db.SaveChanges();

            for (int i = 0; i < componentsNumber; i++) db.Components.Add(new Component { DetailId = random.Next(1, detailsNumber), MaterialId = random.Next(1, materialsNumber), UnitId = random.Next(1, unitsNumber) });
            db.SaveChanges();

            for (int i = 0; i < ieBookNumber; i++) db.IncomeExpenseBook.Add(new IncomeExpenseBook { ComponentId = random.Next(1, componentsNumber), Income = random.NextDouble() * 1000, Expense = random.NextDouble() * 1000 });
            db.SaveChanges();
        }

        private static string getRandomString(string[] array, Random random)
        {
            return array[random.Next(0, array.Length)];
        }
    }
}
