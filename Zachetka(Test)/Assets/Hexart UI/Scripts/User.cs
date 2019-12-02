using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
	//public string avatar {get { return avatar; } set { avatar = value; }  }

	public string Name;

	public string Email;

	public string Id;

	public int Hours;

	public int Coin;

	public int Discount;

	public string Phone_number;

	public string Password;

	public bool YChecked;

	public string Categories;

	public string Commentary;

	public string Spent;





	public User(string name, string email, string id, int hours, int coin, int discount, string phone_number, string password) : this(name, email, id, hours, coin, discount, phone_number)
	{
		this.Password = password;
	}

	public User(string name, string email, string id, int hours, int coin, int discount, string phone) : this(name, email, id, hours, coin, discount)
	{
		this.Phone_number = phone;
	}

	public User(string name, string email, string id, int hours, int coin, int discount)
	{
		this.Name = name;
		this.Email = email;
		this.Id = id;
		this.Hours = hours;
		this.Coin = coin;
		this.Discount = discount;
	}

	public User(string email, string id, int hours, int coin)
	{
		this.Email = email;
		this.Id = id;
		this.Hours = hours;
		this.Coin = coin;
	}

	public User(string email, int hours, int coin)
	{
		this.Email = email;
		this.Hours = hours;
		this.Coin = coin;
	}

	public User(string email, int hours, int coin, string name)
	{
		this.Email = email;
		this.Hours = hours;
		this.Coin = coin;
		this.Name = name;
	}

	public User(string id, string phone_number) : this(id)
	{
		this.Phone_number = phone_number;
	}


	public User()
	{

	}

	public User(string email)
	{

		this.Email = email;

	}

	public override string ToString()
	{
		return "User: " + Name + " Email " + Email + "  Coin " + Coin + "  Hours " + Hours + " Id " + Id;
	}
}