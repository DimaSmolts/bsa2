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
			Parking myPark = Parking.Instance;
			while (true)
			{
				Console.WriteLine("\n*******************************************");
				Console.WriteLine("  choose the task (enter only one number)");
				Console.WriteLine("  1 - View Parking Lot");
				Console.WriteLine("  2 - Add car");
				Console.WriteLine("  3 - Delete car");
				Console.WriteLine("  4 - Recharge car");
				Console.WriteLine("  5 - View Balance of Parking Lot");
				Console.WriteLine("  6 - View free places");
				Console.WriteLine("  7 - View Transactions.log");
				Console.WriteLine("  8 - View last minute transaction");
				Console.WriteLine("  9 - Exit");
				Console.WriteLine("*******************************************");
				Console.Write("===> ");
				string input = Console.ReadLine();
				Console.WriteLine("*******************************************");

				switch (input)
				{
					case "1":
						myPark.DisplayCars();
						break;
					case "2":
						myPark.AddCar();
						break;
					case "3":
						myPark.DeleteCar();
						break;
					case "4":
						myPark.Recharge();
						break;
					case "5":
						myPark.DisplayBalance();
						break;
					case "6":
						myPark.DisplayFreePlaces();
						break;
					case "7":
						myPark.LogFileInPut();
						break;
					case "8":
						myPark.DisplayTransactions();
						break;
					case "9":
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("wrong input");
						break;
				}
			}
		}
	}
}
