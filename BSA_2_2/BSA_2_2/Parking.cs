using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSA_2_2
{
	class Parking
	{
		private List<Car> parkingLot;
		private Queue<Transaction> transactionLog;
		private double balance;

		public Parking()
		{
			parkingLot = new List<Car>();			
		}

		public void AddCar()
		{
			if (!(parkingLot.Count >= 100))
			{
				int? tempType;
				double? tempbalance;

				Console.WriteLine("Enter your`s car balance [x,y]");
				try
				{
					tempbalance = Convert.ToDouble(Console.ReadLine());
				}
				catch (FormatException)
				{
					tempbalance = null;
				}

				if (tempbalance < 0)
					tempbalance = null;

				Console.WriteLine("Enter number of car type (Motorcycle - 1,Bus - 2,PassengerCar - 3,Truck - 4)");
				switch (Console.ReadLine())
				{
					case "1":
						tempType = 1;
						break;
					case "2":
						tempType = 2;
						break;
					case "3":
						tempType = 3;
						break;
					case "4":
						tempType = 4;
						break;
					default:
						tempType = null;
						break;
				}

				if (tempbalance != null && tempType != null)
					parkingLot.Add(new Car((int)tempType, (double)tempbalance));
				else
					Console.WriteLine("wrong data, try again");
			}
			else
			{
				Console.WriteLine("no cars can be added");
			}
		}

		public void DeleteCar()
		{
			int? tempIndex;

			DisplayCars();

			Console.WriteLine("\nWrite the index of car to delete from parking lot");
			Console.Write("Delete Car # ");
			try
			{
				tempIndex = Convert.ToInt32(Console.ReadLine());
			}
			catch (FormatException)
			{
				Console.WriteLine("wrong data");
				tempIndex = null;
			}

			if (tempIndex != null && parkingLot.Count >= tempIndex)
			{
				if (parkingLot[(int)tempIndex].debt == 0)
				{
					parkingLot.Remove(parkingLot[(int)tempIndex]);
				}
				else
				{
					Console.WriteLine("your car can not be deleted because of debt");
				}
			}
		}

		public void DisplayCars()
		{
			if (parkingLot.Count > 0)
			{
				Console.WriteLine();
				Console.WriteLine("{0,3}\t{1,36}\t{2,16}\t{3,8}\t{4,8}",
					"#",
					"ID",
					"Type",
					"Balance",
					"Debt");
				Console.WriteLine();
				for (int i = 0; i < parkingLot.Count; i++)
				{
					Console.Write("{0,3}\t", i);
					parkingLot[i].DisplayCarInfo();
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("Parking Lot is empty!");
			}
		}
	}
}
