﻿using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TheStackGame : MonoBehaviour
{
	int stack_uzunlugu;
	Color renk;

	float count = 0.07f;
	const float max_Deger = 0.51f;
	const float buyukluk = 1f;
	Vector2 stack_boyut = new Vector2(0.5f, 0.5f);
	float hiz_degeri = 0.01f;
	float hiz;
	GameObject[] go_stack;
	int stack_index;
	bool x_ekseninde_hareket;
	Vector3 eski_stack_pos;
	float hassasiyet;
	bool stack_alindi = false;
	private int a = 0;
	bool dead = false;
	public int counter;
	public Text text;
	public Color[] cols;
	public GameObject obj;
	public GameObject objtwo;
	public int max_value;
	public int b = 0;
	public GameObject ButtonPause;
	public GameObject ButtonContinue;
	public Texture Zachetka;
	public Texture Ground;
	public Texture Grass;
	public Texture brick;
	public Texture Rock;
	public Music mus;
	public GameObject clicker;
	public Ray ray;
	public RaycastHit hit;
	public GameObject Cont;
	public Text txt;
	public GameObject panelfail;
	// Start is called before the first frame update
	void Start()
	{
		PlayerPrefs.SetInt("pause", 0);
	
		counter = 0;
		hiz = hiz_degeri;
		stack_uzunlugu = transform.childCount;
		go_stack = new GameObject[stack_uzunlugu];
		for (int i = 0; i < stack_uzunlugu; i++)
		{
			go_stack[i] = transform.GetChild(i).gameObject;
			if (PlayerPrefs.GetInt("Textur") == 0)
			{
				go_stack[i].GetComponent<Renderer>().material.color = cols[Random.Range(0, 7)];
			}
			if (PlayerPrefs.GetInt("Textur") == 1)
			{
				go_stack[i].GetComponent<Renderer>().material.color = Color.white;
				go_stack[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Zachetka);

			}
			if (PlayerPrefs.GetInt("Textur") == 2)
			{
				go_stack[i].GetComponent<Renderer>().material.color = Color.white;
				go_stack[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Ground);
			}
			if (PlayerPrefs.GetInt("Textur") == 3)
			{
				go_stack[i].GetComponent<Renderer>().material.color = Color.white;
				go_stack[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Grass);
			}
			if (PlayerPrefs.GetInt("Textur") == 4)
			{
				go_stack[i].GetComponent<Renderer>().material.color = Color.white;
				go_stack[i].GetComponent<Renderer>().material.SetTexture("_MainTex", brick);
			}
			if (PlayerPrefs.GetInt("Textur") == 5)
			{
				go_stack[i].GetComponent<Renderer>().material.color = Color.white;
				go_stack[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Rock);
			}








		}
		stack_index = stack_uzunlugu - 1;

	}

	// Update is called once per frame
	void Update()
	{
		if (!dead)
		{
			if (Input.GetMouseButtonDown(0) &&	(PlayerPrefs.GetInt("pause") == 0))
			{
				a++;
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.gameObject.GetComponent<TextMenu>())
					{
						Paus();
					}
					else
					{
						if (Stack_Kontrol())
						{
							mus.Click();
							Stack_Al_Koy();
							count += 0.07f;
							counter++;
							max_value += counter;
							if (b != 2)
							{
								b++;
							}
							if (a > 7)
								transform.position -= new Vector3(0, 0.07f, 0);

						}
						else
						{
							if (b == 1)
							{
								PlayerPrefs.SetInt("max", max_value);
							}
							Bitir();

							if (counter >= PlayerPrefs.GetInt("max"))
							{
								PlayerPrefs.SetInt("max", counter);
							}

						}
					}					
				}
				else if(PlayerPrefs.GetInt("pause") == 0)
				{
					
					if (Stack_Kontrol())
					{
						mus.Click();
						Stack_Al_Koy();
						count += 0.07f;
						counter++;
						max_value += counter;
						if (b != 2)
						{
							b++;
						}
						if (a > 7)
							transform.position -= new Vector3(0, 0.07f, 0);
					}
					else
					{
						if (b == 1)
						{
							PlayerPrefs.SetInt("max", max_value);
						}
						Bitir();
						Debug.Log("Fail");
						panelfail.SetActive(true);
						if (counter >= PlayerPrefs.GetInt("max"))
						{
							PlayerPrefs.SetInt("max", counter);
						}

					}

				}
					



			}
			Hareketlendir();

			//transform.position = Vector3.Lerp(transform.position, new Vector3(0, 4, 0), 0.1f);
		}
		text.text = counter.ToString();
	}

	void Stack_Al_Koy()
	{
		eski_stack_pos = go_stack[stack_index].transform.localPosition;
		if (stack_index <= 0)
		{
			stack_index = stack_uzunlugu;
		}
		stack_alindi = false;
		stack_index--;
		x_ekseninde_hareket = !x_ekseninde_hareket;
		go_stack[stack_index].transform.localScale = new Vector3(stack_boyut.x, 0.07f, stack_boyut.y);
		if (PlayerPrefs.GetInt("Textur") == 0)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = cols[Random.Range(0, 7)];
		}
		if (PlayerPrefs.GetInt("Textur") == 1)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = Color.white;
			go_stack[stack_index].GetComponent<Renderer>().material.SetTexture("_MainTex", Zachetka);

		}
		if (PlayerPrefs.GetInt("Textur") == 2)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = Color.white;
			go_stack[stack_index].GetComponent<Renderer>().material.SetTexture("_MainTex", Ground);
		}
		if (PlayerPrefs.GetInt("Textur") == 3)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = Color.white;
			go_stack[stack_index].GetComponent<Renderer>().material.SetTexture("_MainTex", Grass);
		}
		if (PlayerPrefs.GetInt("Textur") == 4)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = Color.white;
			go_stack[stack_index].GetComponent<Renderer>().material.SetTexture("_MainTex", brick);
		}
		if (PlayerPrefs.GetInt("Textur") == 5)
		{
			go_stack[stack_index].GetComponent<Renderer>().material.color = Color.white;
			go_stack[stack_index].GetComponent<Renderer>().material.SetTexture("_MainTex", Rock);
		}
	}

	void Hareketlendir()
	{
		if (x_ekseninde_hareket)
		{
			if (!stack_alindi)
			{
				go_stack[stack_index].transform.localPosition = new Vector3(-0.51f, count, hassasiyet);
				stack_alindi = true;
			}
			if (go_stack[stack_index].transform.localPosition.x > max_Deger)
			{
				hiz = hiz_degeri * -1;
			}
			else if (go_stack[stack_index].transform.localPosition.x < -max_Deger)
			{
				hiz = hiz_degeri;
			}
			go_stack[stack_index].transform.localPosition += new Vector3(hiz, 0, 0);
		}
		else
		{
			if (!stack_alindi)
			{
				go_stack[stack_index].transform.localPosition = new Vector3(hassasiyet, count, -0.51f);
				stack_alindi = true;
			}
			if (go_stack[stack_index].transform.localPosition.z > max_Deger)
			{
				hiz = hiz_degeri * -1;
			}
			else if (go_stack[stack_index].transform.localPosition.z < -max_Deger)
			{
				hiz = hiz_degeri;
			}
			go_stack[stack_index].transform.localPosition += new Vector3(0, 0, hiz);
		}
	}

	bool Stack_Kontrol()
	{
		if (x_ekseninde_hareket)
		{
			float fark = eski_stack_pos.x - go_stack[stack_index].transform.localPosition.x;
			stack_boyut.x -= Mathf.Abs(fark);
			if (stack_boyut.x < 0)
			{
				return false;
			}
			go_stack[stack_index].transform.localScale = new Vector3(stack_boyut.x, 0.07f, stack_boyut.y);
			float mid = go_stack[stack_index].transform.localPosition.x / 2 + eski_stack_pos.x / 2;
			go_stack[stack_index].transform.localPosition = new Vector3(mid, count, eski_stack_pos.z);
			hassasiyet = go_stack[stack_index].transform.localPosition.x;
		}
		else
		{
			float fark = eski_stack_pos.z - go_stack[stack_index].transform.localPosition.z;
			stack_boyut.y -= Mathf.Abs(fark);
			if (stack_boyut.y < 0)
			{
				return false;
			}
			go_stack[stack_index].transform.localScale = new Vector3(stack_boyut.x, 0.07f, stack_boyut.y);
			float mid = go_stack[stack_index].transform.localPosition.z / 2 + eski_stack_pos.z / 2;
			go_stack[stack_index].transform.localPosition = new Vector3(eski_stack_pos.x, count, mid);
			hassasiyet = go_stack[stack_index].transform.localPosition.z;
		}
		return true;
	}

	void Bitir()
	{
		dead = true;
		go_stack[stack_index].AddComponent<Rigidbody>();
		panelfail.SetActive(true);
		ButtonPause.SetActive(false);
		txt.enabled = false;
	}

	private bool IsMouseOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	public void Retry()
	{
		SceneManager.LoadScene(1);
	}

	public void Menu()
	{
		SceneManager.LoadScene(0);
	}

	public void Continue()
	{
		objtwo.SetActive(false);
		Cont.SetActive(false);
		PlayerPrefs.SetInt("pause", 0);
		txt.enabled = true;
	}

	public void Paus()
	{
		PlayerPrefs.SetInt("pause", 1);
		objtwo.SetActive(true);
		Cont.SetActive(true);
		txt.enabled = false;
	}



}