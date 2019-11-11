using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opisanie : MonoBehaviour
{
	private int a = 0;
	public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if(a == 1)
		{
			obj.SetActive(true);
		}
		else
		{
			obj.SetActive(false);
		}


	}


	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.3f);
		a++;
	}

	public void Back()
	{
		a = 0;
	}


}
