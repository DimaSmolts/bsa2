using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace BSA_2_2
{
	class Parking
	{
		private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

		public static Parking Instance { get { return lazy.Value; } }

		private List<Car> parkingLot;
		private List<Transaction> transactionLog;
		private double balance;

		private Timer PayTime;
		private Timer ClearTime;

		static object locker;

		private Parking()
		{
			parkingLot = new List<Car>();
			transactionLog = new List<Transaction>();

			TimerCallback tc1 = new TimerCallback(Pay);
			PayTime = new Timer(tc1, 0, 0, 3000);

			TimerCallback tc2 = new TimerCallback(LogFileOutPut);
			ClearTime = new Timer(tc2, 0, 0, 60000);

			locker = new object();

		}

		public void AddCar()
		{
			if (!(parkingLot.Count >= 100))
			{
				int? tempType;
				double? tempbalance;

				Console.WriteLine("\nEnter your`s car balance [x,y]");
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

				Console.WriteLine("Enter number of car type\n(Motorcycle - 1,Bus - 2,PassengerCar - 3,Truck - 4)");
				Console.Write("===> ");
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
				{
					parkingLot.Add(new Car((int)tempType, (double)tempbalance));
				}
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
			if (parkingLot.Count != 0)
			{
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

				if (tempIndex != null && parkingLot.Count > tempIndex)
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
				else
				{
					Console.WriteLine("index error");
				}
			}
		}
		public void Recharge()
		{
			int? tempIndex;
			double? tempRecharge;			
			DisplayCars();
			if (parkingLot.Count != 0)
			{

				Console.WriteLine("\nWrite the index of car to recharge the balance");
				Console.Write("Recharge Car # ");
				try
				{
					tempIndex = Convert.ToInt32(Console.ReadLine());
				}
				catch (FormatException)
				{
					Console.WriteLine("wrong data");
					tempIndex = null;
				}

				Console.Write("Write the sum ");
				try
				{
					tempRecharge = Convert.ToDouble(Console.ReadLine());
				}
				catch (FormatException)
				{
					Console.WriteLine("wrong data");
					tempRecharge = null;
				}


				if (tempIndex != null && parkingLot.Count > tempIndex)
				{
					if (tempRecharge != null)
					{
						parkingLot[(int)tempIndex].Balance += Convert.ToDouble(tempRecharge);
					}
				}
				else
				{
					Console.WriteLine("index error");
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

		public void DisplayBalance()
		{
			double totalDebt = 0;
			foreach (Car c in parkingLot)
				totalDebt += c.debt;
			Console.WriteLine("\nBalance of Parking Lot is {0} $\nTotal debt in {1} $", balance,totalDebt);
		}

		public void DisplayFreePlaces()
		{
			Console.WriteLine("\nFree places: {0}",Settings.ParkingSpace-parkingLot.Count);
		}
		public void Pay(object obj)
		{
			lock (locker)
			{
				foreach (Car c in parkingLot)
				{
					if (c.Balance > (int)c.carType + c.debt)
					{
						Transaction temp = new Transaction(c);
						transactionLog.Add(temp);
						balance += temp.WithDrawMoney;
					}
					else
					{
						c.debt += Settings.Fine * (int)c.carType;
					}
				}
			}
		}

		public void LogFileOutPut(object obj)
		{
			 List<Transaction> tempList = new List<Transaction>();


			foreach (Transaction t in transactionLog)
			{
				double temp = DateTime.Now.Subtract(t.TransactionStamp).TotalSeconds;
				if (temp < 60)
					tempList.Add(t);
			}
			
			transactionLog = tempList;

			lock (locker)
			{
				FileStream file1 = new FileStream("Transactions.log", FileMode.Append);
				StreamWriter sw = new StreamWriter(file1);

				sw.WriteLine();
				foreach (Transaction t in tempList)
				{
					sw.WriteLine(string.Format("{0}\t{1}\t{2}\n", t.CarID, t.TransactionStamp, t.WithDrawMoney));
				}
				sw.WriteLine();
				sw.WriteLine(DateTime.Now.ToString());
				sw.Close();
			}
		}

		public void DisplayTransactions()
		{
			double sum=0;
			List<Transaction> tempList = new List<Transaction>();
			foreach (Transaction t in transactionLog)
			{
				double temp = DateTime.Now.Subtract(t.TransactionStamp).TotalSeconds;
				if (temp <= 60)
				{
					tempList.Add(t);
					sum += t.WithDrawMoney;
				}								
			}

			transactionLog = tempList;


			Console.WriteLine(DateTime.Now);
			Console.WriteLine();
			foreach (Transaction t in transactionLog)
			{
				t.DisplayTransactionInfo();
			}
			Console.WriteLine("\nTotal money in last minute {0}", sum);
		}
		public void LogFileInPut()
		{
			lock (locker)
			{
				FileStream file1 = new FileStream("Transactions.log", FileMode.Open);
				StreamReader sr = new StreamReader(file1);
				Console.Write(sr.ReadToEnd());
				sr.Close();
			}
		}
	}
}
