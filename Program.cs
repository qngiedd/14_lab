using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
//Транспортное средство, Управление авто, Машина, Двигатель, Разумное существо, Человек, Трансформер;
namespace Lab5
{
    abstract class ThinkingCreature
    {
        public abstract void Think();
    }
    interface CarControl
    {
        bool CheckStatus();
        void MoveForward();
        void MoveBack();
        void TurnLeft();
        void TurnRight();
        void Stop();
    }
    [Serializable]
    public abstract class Vehicle
    {
        public int WheelsNumber { get; set; }
        public abstract bool CheckStatus();
    }
    [Serializable]
    public class Car : Vehicle
    {
        public bool IsActive { get; set; }
        public string Model { get; set; }
        public int Speed { get; set; }
        public string ManufacturerCountry { get; set; }
        public int Cost { get; set; }
        public Car()
        {
            Model = "Toyota Camry";
            Speed = 100;
            ManufacturerCountry = "Japan";
            Cost = 10000;
            IsActive = false;
        }
        public Car(string model, int speed, string manufacturerCountry, int cost, bool isActive)
        {
            Model = model;
            Speed = speed;
            ManufacturerCountry = manufacturerCountry;
            Cost = cost;
            IsActive = isActive;
        }
        override public bool CheckStatus()
        {
            return IsActive;
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} " +
                $"type, with {this.GetHashCode()} hashcode.\n" +
                $"Model: {Model}\n" +
                $"Speed: {Speed}\n" +
                $"Cost: {Cost}\n" +
                $"TurnedOn:({IsActive})\n";

        }
    }
    class CarWithControl : Car, CarControl
    {

        public void MoveForward()
        {
            Console.WriteLine("The car is moving forward");
        }
        public void MoveBack()
        {
            Console.WriteLine("The car is moving back");
        }
        public void TurnLeft()
        {
            Console.WriteLine("The car is turning left");
        }
        public void TurnRight()
        {
            Console.WriteLine("The car is turning right");
        }
        public void Stop()
        {
            Console.WriteLine("The car has stopped!");
        }
    }
    sealed class Engine
    {
        public int Capacity { get; set; }
    }
    class Human : ThinkingCreature
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public override void Think()
        {

            Console.WriteLine("I can think");
        }
        public Human()
        {
            Name = "undefined Name";
            SecondName = "undefined SecondName";
            Age = 0;
        }
        public Human(string name, string secondName, int age)
        {
            Name = name;
            SecondName = secondName;
            Age = age;
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} type, " +
                $"with {this.GetHashCode()} hashcode.\n" +
                $"Name of the human is {Name}\n" +
                $"Surname: {SecondName}";
        }
    }

    class Transformer : Car
    {
        public bool IsPreparedForBattle { get; set; }
        public int NumberOfGuns { get; set; }
        public void Shoot()
        {
            Console.WriteLine("Poof!");
        }
        override public bool CheckStatus()
        {
            return IsPreparedForBattle;
        }
        public Transformer()
        {
            IsPreparedForBattle = false;
            NumberOfGuns = 0;
        }
        public Transformer(bool isPreparedForBattle, int numberOfGuns)
        {
            IsPreparedForBattle = isPreparedForBattle;
            NumberOfGuns = numberOfGuns;
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} " +
                $"type, with {this.GetHashCode()} hashcode.\n" +
                $"Transformer prepared for battle ({IsPreparedForBattle})\n" +
                $"Number of guns: {NumberOfGuns}\n" +
                $"Model: {Model}\n" +
                $"Speed: {Speed}\n" +
                $"Cost: {Cost}\n" +
                $"TurnedOn:({IsActive})\n";
        }
    }
    class Printer
    {
        public virtual string IAmPrinting(Object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
    }
    class Phone
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {/*
            Human oHuman1 = new Human();
            Human oHuman2 = new Human("Angelina", "Draguts", 21);
            Human refHuman2 = oHuman2 as Human;

            Car oCar1 = new Car();
            Car oCar2 = new Car("Volvo XC90", 220, "Germany", 80000, true);
            Car refCar2 = oCar2 as Car;


            Transformer oTransfromer1 = new Transformer();
            Transformer oTransfromer2 = new Transformer(true,4);
            Transformer refTransfromer2 = oTransfromer2 as Transformer;

            Printer oPrinter = new Printer();
            Object[] arr = { refHuman2, refCar2, refTransfromer2 };
            
            foreach (object element in arr)
                Console.WriteLine(oPrinter.IAmPrinting(element)+ "\n_____________________________\n");
                */
            //////////////////////////////LABA15<3/////////////////////////
            ///для сериализации/десериалезации выбрала Car
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////a)бинарный формат");
            ///Сериализация представляет процесс преобразования какого-либо объекта в поток байтов.
            ///После преобразования мы можем этот поток байтов или записать на диск или сохранить его временно в памяти.
            ///А при необходимости можно выполнить обратный процесс - десериализацию, 
            ///то есть получить из потока байтов ранее сохраненный объект.
            ///
            ///Чтобы объект определенного класса можно было сериализовать, надо этот класс пометить атрибутом Serializable
            ///При отстутствии данного атрибута объект Person не сможет быть сериализован, и при попытке сериализации будет выброшено исключение SerializationException.
            ///
            ///Сериализация применяется к свойствам и полям класса.
            ///Если мы не хотим, чтобы какое - то поле класса сериализовалось,
            ///то мы его помечаем атрибутом NonSerialized:
            ///
            /// При наследовании подобного класса, следует учитывать, что атрибут Serializable автоматически не наследуется.
            ///Для бинарной сериализации применяется класс BinaryFormatter:
            ///// объект для сериализации
            Car CarForBinSerialization = new Car("BMW", 250, "Germany", 100000, true);
            Console.WriteLine("Объект создан");

            // создаем объект BinaryFormatter
            BinaryFormatter Bformatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("FileWithSerializedCar.dat", FileMode.OpenOrCreate))
            {
                Bformatter.Serialize(fs, CarForBinSerialization);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация из файла FileWithSerializedCar
            using (FileStream fs = new FileStream("FileWithSerializedCar.dat", FileMode.OpenOrCreate))
            {
                Car BinDeserializedCar = (Car)Bformatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"" +
                    $"Модель: {BinDeserializedCar.Model} ---" +
                    $"Скорость: {BinDeserializedCar.Speed}--- " +
                    $"Страна: {BinDeserializedCar.ManufacturerCountry}--- " +
                    $"Цена: {BinDeserializedCar.Cost}--- " +
                    $"Заведена: {BinDeserializedCar.IsActive}"
                    );
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////b)SOAP");
            Car SOAPCar = new Car("Mercedes", 260, "Germany", 120000, true);
            Car SOAPCar2 = new Car("Porsche", 270, "Germany", 140000, true);
            Car[] SOAPCars = new Car[] { SOAPCar, SOAPCar2 };

            // создаем объект SoapFormatter
            SoapFormatter Sformatter = new SoapFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("SOAPCarsFile.soap", FileMode.OpenOrCreate))
            {
                Sformatter.Serialize(fs, SOAPCars);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream("SOAPCarsFile.soap", FileMode.OpenOrCreate))
            {
                Car[] newSOAPCars = (Car[])Sformatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                foreach (Car car in newSOAPCars)
                {
                    Console.WriteLine("Модель: {0} --- Скорость: {1} --- Страна: {2} --- Цена: {3} --- Заведена: {4}", car.Model, car.Speed, car.ManufacturerCountry, car.Cost, car.IsActive);
                }
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////c)JSON");
            Car JSONCar = new Car { Model = "Mazda", Speed = 265, ManufacturerCountry = "Japan", Cost = 55000, IsActive = true };

            string json = JsonSerializer.Serialize<Car>(JSONCar);
            Console.WriteLine(json);
            Car restoredJSONCar = JsonSerializer.Deserialize<Car>(json);
            Console.WriteLine($"" +
             $"Модель: {restoredJSONCar.Model} ---" +
             $"Скорость: {restoredJSONCar.Speed}--- " +
             $"Страна: {restoredJSONCar.ManufacturerCountry}--- " +
             $"Цена: {restoredJSONCar.Cost}--- " +
             $"Заведена: {restoredJSONCar.IsActive}"
             );
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////d)XML");
            // объект для сериализации
            Car XMLCar1 = new Car("BELAZ", 40, "BELARUS", 40000000, true);
            Car XMLCar2 = new Car("MAZ", 90, "BELARUS", 60000, true);
            Car XMLCar3 = new Car("UAZ", 120, "BELARUS", 5000, true);
            Car[] XMLCars = new Car[] { XMLCar1, XMLCar2, XMLCar3 };
            Console.WriteLine("Объект создан");

            // передаем в конструктор тип класса
            XmlSerializer xformatter = new XmlSerializer(typeof(Car[]));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("SerializedXMLCarFile.xml", FileMode.OpenOrCreate))
            {
                xformatter.Serialize(fs, XMLCars);

                Console.WriteLine("Объект сериализован");
            }
            // десериализация
            using (FileStream fs = new FileStream("SerializedXMLCarFile.xml", FileMode.OpenOrCreate))
            {
                Car[] newXMLCars = (Car[])xformatter.Deserialize(fs);
                foreach(var DeserializedCAr in newXMLCars)
                {
                    Console.WriteLine("Объект десериализован");
                    Console.WriteLine($"" +
                        $"Модель: {DeserializedCAr.Model} ---" +
                        $"Скорость: {DeserializedCAr.Speed}--- " +
                        $"Страна: {DeserializedCAr.ManufacturerCountry}--- " +
                        $"Цена: {DeserializedCAr.Cost}--- " +
                        $"Заведена: {DeserializedCAr.IsActive}"
                        );
                }
                
            }
            //Используя XPath напишите два селектора для вашего XML документа.
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////3)XPath");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("SerializedXMLCarFile.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            XmlNodeList 
                allChildNodes = xRoot.SelectNodes("*");
            foreach (XmlNode node in allChildNodes)
                Console.WriteLine(node.OuterXml);
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////3.2)");
            //Выберем все узлы <Car>:
            XmlNodeList CarChildNodes = xRoot.SelectNodes("//Car/Model");
            //только значения Model в cars
            foreach (XmlNode node in CarChildNodes)
                if (node != null)
                    Console.WriteLine(node.InnerText);
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////3.3)");
            //Выберем узел, у которого Model имеет значение "BELAZ"
            XmlNode childnode = xRoot.SelectSingleNode("Car[Model='BELAZ']");
            if (childnode != null)
                Console.WriteLine(childnode.OuterXml);
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////4)Linq");
            //Используя Linq to XML (или Linq to JSON) создайте новый xml (json) -
            //документ и напишите несколько запросов
            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement iphone6 = new XElement("phone");
            // создаем атрибут
            XAttribute iphoneNameAttr = new XAttribute("name", "iPhone 6");
            XElement iphoneCompanyElem = new XElement("company", "Apple");
            XElement iphonePriceElem = new XElement("price", "40000");
            // добавляем атрибут и элементы в первый элемент
            iphone6.Add(iphoneNameAttr);
            iphone6.Add(iphoneCompanyElem);
            iphone6.Add(iphonePriceElem);

            // создаем второй элемент
            XElement galaxys5 = new XElement("phone");
            XAttribute galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
            XElement galaxysCompanyElem = new XElement("company", "Samsung");
            XElement galaxysPriceElem = new XElement("price", "33000");
            galaxys5.Add(galaxysNameAttr);
            galaxys5.Add(galaxysCompanyElem);
            galaxys5.Add(galaxysPriceElem);
            // создаем корневой элемент
            XElement phones = new XElement("phones");
            // добавляем в корневой элемент
            phones.Add(iphone6);
            phones.Add(galaxys5);
            // добавляем корневой элемент в документ
            xdoc.Add(phones);
            //сохраняем документ
            xdoc.Save("phones.xml");
            //
            //
            //Переберем его элементы и выведем их значения на консоль
            XDocument xdoc2 = XDocument.Load("phones.xml");
            foreach (XElement phoneElement in xdoc2.Element("phones").Elements("phone"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                    Console.WriteLine($"Компания: {companyElement.Value}");
                    Console.WriteLine($"Цена: {priceElement.Value}");
                }
                Console.WriteLine();
            }
            //
            //
            //Сочетая операторы Linq и LINQ to XML можно довольно просто извлечь из документа данные и затем обработать их. Например, имеется следующий класс:
            //    class Phone
            //{
            //    public string Name { get; set; }
            //    public string Price { get; set; }
            //}
            //Создадим на основании данных в xml объекты этого класса:
            XDocument xdoc3 = XDocument.Load("phones.xml");
            var items = from xe in xdoc3.Element("phones").Elements("phone")
                        where xe.Element("company").Value == "Samsung"
                        select new Phone
                        {
                            Name = xe.Attribute("name").Value,
                            Price = xe.Element("price").Value
                        };

            foreach (var item in items)
                Console.WriteLine($"{item.Name} - {item.Price}");
            Console.ReadKey();
        }
    }
}
