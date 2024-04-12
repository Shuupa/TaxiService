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
            database.CreateTableAsync<CarsV2>().Wait();
            database.CreateTableAsync<Car_vendor>().Wait();
            
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

        public async Task<List<CarsV2>> GetCarsV2()
        {
            var CarsV2 = await database.Table<CarsV2>().ToListAsync();
            
            if (!CarsV2.Any())
            {
                var carVendors = await GetVendors();
                await database.InsertAllAsync(new CarsV2[]
                {
                    new CarsV2{Car_Stat = "3", Driver_name = "Афонасьев Анатолий Петрович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 1)?.Car_Vendors, Car_Gos = "A 121 KA"},
                    new CarsV2{Car_Stat = "2", Driver_name = "Степанов Виктор Алексеевич", car_vendors = carVendors.FirstOrDefault(v => v.ID == 2)?.Car_Vendors, Car_Gos = "K 931 ET"},
                    new CarsV2{Car_Stat = "1", Driver_name = "Нахромов Андрей Валентинович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 3)?.Car_Vendors, Car_Gos = "T 627 AE"},
                    new CarsV2{Car_Stat = "1", Driver_name = "Быстров Антон Павлович", car_vendors = carVendors.FirstOrDefault(v => v.ID == 4)?.Car_Vendors, Car_Gos = "K 453 XT"}
                });
                return await database.Table<CarsV2>().ToListAsync();
            }
            return CarsV2;
        }
        public async Task<List<Car_vendor>> GetVendors()
        {
            var vendors = await database.Table<Car_vendor>().ToListAsync();
            if (!vendors.Any())
            {
                await database.InsertAllAsync(new Car_vendor[]
                {
                    new Car_vendor{ID = 1, Car_Vendors = "Mercedes W223 Pullman"},
                    new Car_vendor{ID = 2, Car_Vendors = "Skoda Rapid"},
                    new Car_vendor{ID = 3, Car_Vendors = "Lada Granta"},
                    new Car_vendor{ID = 4, Car_Vendors = "Lada Priora"}
                });
                return await database.Table<Car_vendor>().ToListAsync();
            }
            return vendors;
        }
    } 
}
