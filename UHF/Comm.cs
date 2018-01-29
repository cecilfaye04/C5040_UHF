
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;

namespace UHF
{
	public class Comm
	{
		/// <summary>
		/// 检测是不是数字
		/// </summary>
		/// <param name="strNumber"></param>
		/// <returns></returns>
		public static bool IsNumber(string strNumber)
		{
			Regex regex = new Regex(@"^\d+(\.\d)?$");
			return regex.IsMatch(strNumber);

		}
		/// <summary>
		/// is hex 判断是否全是十六进制数
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		public static bool isHex(string msg)
		{

			if (msg.Length % 2 != 0 && msg.Length != 1)
			{
				return false;
			}
			else if (Regex.IsMatch(msg, "^[0-9A-Fa-f]+$"))
			{
				return true;
			}
			else
				return false;
		}
	}
}

