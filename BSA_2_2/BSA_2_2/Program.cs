using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSA_2_2
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello Binary Studio Academy 2018!");

			Parking myPark = new Parking();

			
			Console.WriteLine();
			myPark.AddCar();
			Console.WriteLine();
			myPark.AddCar();
			Console.WriteLine();
			myPark.AddCar();
			Console.WriteLine();
			myPark.AddCar();

			//myPark.DisplayCars();

			myPark.DeleteCar();

			myPark.DisplayCars();

			Console.ReadKey();
		}
	}
}
