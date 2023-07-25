using System;
using System.IO;
using System.Linq;

namespace ConvarianceAndContravarianceDelegates
{
    
    class Program
    {
        delegate Car CarFactoryDel(int id, string name);
        
        delegate void LogIceCarDetailsDel(ICECar car);
        delegate void LogEvCarDetailsDel(EVCar car);
        
        static void Main(string[] args)
        {
            Console.WriteLine("Convariance");
            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;
            Car iceCar = carFactoryDel(1,"Audo R8");
            Console.WriteLine($"Object Type: {iceCar.GetType()}");
            Console.WriteLine($"Car Details: {iceCar.GetCarDetails()}");
            
            Console.WriteLine(" ");

            carFactoryDel = CarFactory.ReturnEvCar;
            Car EvCar = carFactoryDel(2,"Tesla Model-3");
            Console.WriteLine($"Object Type: {EvCar.GetType()}");
            Console.WriteLine($"Car Details: {EvCar.GetCarDetails()}");
            
            Console.WriteLine("---------------------------------------------");
            
            //Console.ReadKey();

            Console.WriteLine("Contravariance");
            
            LogIceCarDetailsDel logIceDel = LogCarDetails;
            logIceDel((ICECar)iceCar);
            LogEvCarDetailsDel logEvDel = LogCarDetails;
            logEvDel((EVCar)EvCar);
            
            Console.ReadKey();
        }
        
        static void LogCarDetails(Car car)
        {
            if(car is ICECar)
            {
                using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IceCarsDetails.txt"), true);
                sw.WriteLine($"Object Type: {car.GetType()}");
                sw.WriteLine($"Car Details: {car.GetCarDetails()}");
            }
            else if(car is EVCar)
            {
                
                Console.WriteLine($"Object Type: {car.GetType()}");
                Console.WriteLine($"Car Details: {car.GetCarDetails()}");
            }
            else
            {
                throw new ArgumentException("Car Is Not Valid");
            }
        }   
    }
    
    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar   {Id=id, Name = name};
        }

        public static EVCar ReturnEvCar(int id, string name)
        {
            return new EVCar {Id=id, Name = name};
        }
    }
    public abstract class Car
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name} ";
        }
            
    }
    
    public class EVCar: Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Electric";
        }
    }
    public class ICECar: Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Inetrnal Combustion Engine";
        }
    }
    

    
}