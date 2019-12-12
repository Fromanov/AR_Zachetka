using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

namespace Apxfly.Verificator
{
	/// <summary>
	/// Tests an E-Mail address.
	/// </summary>
	public static class Verificator
	{
		
		public static string IsValidPhoneNumber(string phone_number)
		{
			StringBuilder result = new StringBuilder();
			int length = phone_number.Length;
			char[] p = phone_number.ToCharArray();
			if (length == 11)
			{
				result.Append("+7");
				for (int i = 1; i < p.Length; i++)
				{

					if (Char.IsNumber(p[i]))
						result.Append(p[i]);
					else
					{
						return null;
					}
				}
			}
			else if (length == 12)
			{
				result.Append("+7");
				for (int i = 2; i < p.Length; i++)
				{

					if (Char.IsNumber(p[i]))
						result.Append(p[i]);
					else
					{
						return null;
					}
				}
			}
			else
			{
				return null;
			}
			return result.ToString();
		}
	}
}
