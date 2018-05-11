using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSA_2_2
{
	public static class Settings
	{
		private static int timeOut;
		private static Dictionary<int, string> priceList;
		private static int parkingSpace;
		private static double fine;

		static Settings()
		{
			// initializing

			timeOut = 3;

			priceList = new Dictionary<int, string>();
			priceList.Add(1, "motorcycle");
			priceList.Add(2, "bus");
			priceList.Add(3, "passenger car");
			priceList.Add(5, "truck");			

			parkingSpace = 100;

			fine = 2.5;
		}

		public static int TimeOut
		{
			get
			{
				return timeOut;
			}
		}
		public static Dictionary<int, string> PriceList
		{
			get
			{
				return priceList;
			}
		}
		public static int ParkingSpace
		{
			get
			{
				return parkingSpace;
			}
		}
		public static double Fine
		{
			get
			{
				return fine;
			}
		}
	}
}
