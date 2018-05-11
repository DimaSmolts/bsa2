using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSA_2_2
{
	class Car
	{
		public enum EnumCarType { Motorcycle = 1, Bus = 2, PassengerCar = 3, Truck = 5}

		private Guid carID;
		public readonly EnumCarType carType;
		private double balance;
		public double debt; //борг


		public Car(int type, double balance)
		{
			carID = Guid.NewGuid();
			switch (type)
			{
				case 1:
					carType = EnumCarType.Motorcycle;
					break;
				case 2:
					carType = EnumCarType.Bus;
					break;
				case 3:
					carType = EnumCarType.PassengerCar;
					break;
				case 4:
					carType = EnumCarType.Truck;
					break;
			}
			this.balance = balance;
			debt = 0;	
		}

		public void DisplayCarInfo()
		{
			Console.Write("{0}\t{1,16}\t{2,8}\t{3,8}", carID, carType, balance, debt);			
		}

		public Guid CarID
		{
			get { return carID; }
		}
		public double Balance
		{
			get { return balance; }
			set
			{
				if (value < 0)
					Console.WriteLine("Error, you cann`t set balance<0");
				else
					balance = value; 
			}
		}

	}
}
