using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StoreApp
{
    enum Category
    {
        Laptop,
        Phone,
        HeadPhones,
        Case,
        Tablet,
        Charger,
    }

    abstract class Device
    {
        private decimal price;
        private string vendor;
        private int yearOfRelease;
        private int warranty;
        private string model; 
        private Category category;

        public Device() { }

        public Device(decimal price, string vendor, Category category, int yearOfRelease, int warranty, string model)
        {
            this.price = price;
            this.vendor = vendor;
            this.category = category;
            this.yearOfRelease = yearOfRelease;
            this.warranty = warranty;
            this.model = model;
        }

        public string GetModel()
        {
            return model;
        }

        public void SetModel(string model)
        {
            this.model = model;
        }

        public int GetWarranty()
        {
            return warranty;
        }

        public void SetWarranty(int warranty)
        {
            this.warranty = warranty;
        }

        public int GetYearOfRelease()
        {
            return yearOfRelease;
        }

        public void SetYearOfRelease(int year)
        {
            this.yearOfRelease = year;
        }

        public Category GetCategory()
        {
            return category;
        }

        public void SetCategory(Category category)
        {
            this.category = category;
        }

        public string GetVendor()
        {
            return vendor;
        }

        public void SetVendor(string vendor)
        {
            this.vendor = vendor;
        }

        public decimal GetPrice()
        {
            return price;
        }

        public void SetPrice(decimal price)
        {
            this.price = price;
        }

        public override string ToString()
        {
            return $"{vendor} {model} ({category}, {yearOfRelease}) - {price}$";
        }
    }

    class Laptop : Device
    {
        public Laptop(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.Laptop, yearOfRelease, warranty, model) { }
    }

    class Phone : Device
    {
        public Phone(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.Phone, yearOfRelease, warranty, model) { }
    }

    class Tablet : Device
    {
        public Tablet(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.Tablet, yearOfRelease, warranty, model) { }
    }

    class Charger : Device
    {
        public Charger(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.Charger, yearOfRelease, warranty, model) { }
    }

    class Case : Device
    {
        public Case(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.Case, yearOfRelease, warranty, model) { }
    }

    class HeadPhone : Device
    {
        public HeadPhone(decimal price, string vendor, int yearOfRelease, int warranty, string model)
            : base(price, vendor, Category.HeadPhones, yearOfRelease, warranty, model) { }
    }

    class Store
    {
        private List<Device> devices = new List<Device>();

        public void AddDevice(Device device)
        {
            devices.Add(device);
        }

        public List<Device> FindByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return devices.Where(d => d.GetPrice() >= minPrice && d.GetPrice() <= maxPrice).ToList();
        }

        public List<Device> FindByModel(string modelPattern)
        {
            Regex regex = new Regex(modelPattern, RegexOptions.IgnoreCase);
            return devices.Where(d => regex.IsMatch(d.GetModel())).ToList();
        }

        public List<Device> FindByYear(int year)
        {
            return devices.Where(d => d.GetYearOfRelease() == year).ToList();
        }

        public List<Device> FindByType(Type deviceType)
        {
            return devices.Where(d => d.GetType() == deviceType).ToList();
        }

        public Device this[string model]
        {
            get
            {
                return devices.FirstOrDefault(d => d.GetModel().Equals(model, StringComparison.OrdinalIgnoreCase));
            }
        }

        public Device this[decimal minPrice, decimal maxPrice]
        {
            get
            {
                return devices.FirstOrDefault(d => d.GetPrice() >= minPrice && d.GetPrice() <= maxPrice);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            store.AddDevice(new Laptop(1200m, "HP", 2024, 3, "Spectre x360"));
            store.AddDevice(new Phone(899m, "Google", 2023, 2, "Pixel 7"));
            store.AddDevice(new Tablet(699m, "Lenovo", 2022, 1, "Tab P11"));
            store.AddDevice(new Charger(39m, "Belkin", 2024, 2, "Belkin Boost Charge 30W"));
            store.AddDevice(new Case(25m, "OtterBox", 2023, 1, "Galaxy S23 Case"));


            Console.WriteLine("Поиск устройств по цене от 25 до 100: ");
            var devicesInRange = store.FindByPriceRange(25m, 100m);
            foreach (var device in devicesInRange)
            {
                Console.WriteLine(device);
            }

            Console.WriteLine("\nПоиск устройств модели 'Google': ");
            var devicesByModel = store.FindByModel("Google");
            foreach (var device in devicesByModel)
            {
                Console.WriteLine(device);
            }

            Console.WriteLine("\nПоиск устройств, выпущенных в 2022: ");
            var devicesByYear = store.FindByYear(2023);
            foreach (var device in devicesByYear)
            {
                Console.WriteLine(device);
            }

            Console.WriteLine("\nПоиск всех ноутбуков: ");
            var laptops = store.FindByType(typeof(Laptop));
            foreach (var laptop in laptops)
            {
                Console.WriteLine(laptop);
            }

            Console.WriteLine("\nПоиск устройства по модели 'Tab P11': ");
            Device deviceByModel = store["Tab P11"];
            if (deviceByModel != null)
            {
                Console.WriteLine(deviceByModel);
            }

            Console.WriteLine("\nУстройство в ценовом диапазоне 700-800: ");
            Device deviceByPrice = store[700m, 800m];
            if (deviceByPrice != null)
            {
                Console.WriteLine(deviceByPrice);
            }
        }
    }
}