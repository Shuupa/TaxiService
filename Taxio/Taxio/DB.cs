using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Taxio.Models;

namespace Taxio
{
    public class DB
    {
        private readonly SQLiteAsyncConnection database;

        public DB(string dPath)
        {
            database = new SQLiteAsyncConnection(dPath);
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<CarsV3>().Wait();
            database.CreateTableAsync<Car_vendorV2>().Wait();

        }

        public async Task<List<User>> GetUsers()
        {
            var user = await database.Table<User>().ToListAsync();
            if (!user.Any())
            {
                await database.InsertAllAsync(new User[]
                {
                    new User{ID = 1, User_id = 122, User_name = "NONE"},
                    new User{ID = 2, User_id = 123, User_name = "NONE"}
                });
                return await database.Table<User>().ToListAsync();
            }
            return user;
        }

        public async Task<List<CarsV3>> GetCarsV3()
        {
            var CarsV3 = await database.Table<CarsV3>().ToListAsync();

            if (!CarsV3.Any())
            {
                var carVendors = await GetVendors();
                await database.InsertAllAsync(new CarsV3[]
                {
                    new CarsV3{Car_Stat = "3", Driver_name = "Михайлов Александр Иванович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 1)?.Car_Vendors, Car_Gos = "K 153 AT"},
                    new CarsV3{Car_Stat = "3", Driver_name = "Воробьев Михаил Алиевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 2)?.Car_Vendors, Car_Gos = "K 425 XT"},
                    new CarsV3{Car_Stat = "3", Driver_name = "Черный Максим Тимофеевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 3)?.Car_Vendors, Car_Gos = "C 763 HP"},
                    new CarsV3{Car_Stat = "3", Driver_name = "Захаров Елисей Никитич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 4)?.Car_Vendors, Car_Gos = "A 751 HB"},
                    new CarsV3{Car_Stat = "3", Driver_name = "Афонасьев Анатолий Петрович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 5)?.Car_Vendors, Car_Gos = "A 121 KA"},
                    new CarsV3{Car_Stat = "2", Driver_name = "Михайлов Максим Степанович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 6)?.Car_Vendors, Car_Gos = "K 843 XT"},
                    new CarsV3{Car_Stat = "2", Driver_name = "Быстров Антон Павлович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 7)?.Car_Vendors, Car_Gos = "K 327 XT"},
                    new CarsV3{Car_Stat = "2", Driver_name = "Степанов Виктор Алексеевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 8)?.Car_Vendors, Car_Gos = "K 931 ET"},
                    new CarsV3{Car_Stat = "2", Driver_name = "Нахромов Андрей Валентинович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 9)?.Car_Vendors, Car_Gos = "T 627 AE"},
                    new CarsV3{Car_Stat = "2", Driver_name = "Власов Матвей Георгиевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 10)?.Car_Vendors, Car_Gos = "K 774 XT"},
                    new CarsV3{Car_Stat = "1", Driver_name = "Петров Мирослав Тимофеевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 11)?.Car_Vendors, Car_Gos = "K 278 XT"},
                    new CarsV3{Car_Stat = "1", Driver_name = "Гончаров Демид Михайлович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 12)?.Car_Vendors, Car_Gos = "B 025 XC"},
                    new CarsV3{Car_Stat = "1", Driver_name = "Романов Михаил Тимофеевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 13)?.Car_Vendors, Car_Gos = "T 327 XB"},
                    new CarsV3{Car_Stat = "1", Driver_name = "Леонтьев Павел Андреевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 14)?.Car_Vendors, Car_Gos = "K 531 TT"},
                    new CarsV3{Car_Stat = "1", Driver_name = "Исаев Максим Евгеньевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 15)?.Car_Vendors, Car_Gos = "C 854 BT"}
                });
                return await database.Table<CarsV3>().ToListAsync();
            }
            return CarsV3;
        }
        public async Task<List<Car_vendorV2>> GetVendors()
        {
            var vendors = await database.Table<Car_vendorV2>().ToListAsync();
            if (!vendors.Any())
            {
                await database.InsertAllAsync(new Car_vendorV2[]
                {
                    new Car_vendorV2{ID = 1, Car_Vendors = "Mercedes W223 Pullman"},
                    new Car_vendorV2{ID = 2, Car_Vendors = "BMW 3"},
                    new Car_vendorV2{ID = 3, Car_Vendors = "Mercedes E-Class"},
                    new Car_vendorV2{ID = 4, Car_Vendors = "Mercedes S-Class"},
                    new Car_vendorV2{ID = 5, Car_Vendors = "Mercedes AMG"},
                    new Car_vendorV2{ID = 6, Car_Vendors = "Skoda Rapid"},
                    new Car_vendorV2{ID = 7, Car_Vendors = "Toyota Prius"},
                    new Car_vendorV2{ID = 8, Car_Vendors = "Toyota Camry"},
                    new Car_vendorV2{ID = 9, Car_Vendors = "Honda Accord"},
                    new Car_vendorV2{ID = 10, Car_Vendors = "Lada Priora"},
                    new Car_vendorV2{ID = 11, Car_Vendors = "Lada Granta"},
                    new Car_vendorV2{ID = 12, Car_Vendors = "Lada Priora"},
                    new Car_vendorV2{ID = 13, Car_Vendors = "Lada 2107"},
                    new Car_vendorV2{ID = 14, Car_Vendors = "Lada 2106"},
                    new Car_vendorV2{ID = 15, Car_Vendors = "Lada 2114"}
                });
                return await database.Table<Car_vendorV2>().ToListAsync();
            }
            return vendors;
        }
    }
}
