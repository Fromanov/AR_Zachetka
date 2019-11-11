using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{

	public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void Active()
	{
		obj.SetActive(true);
	}

	public void Back()
	{
		obj.SetActive(false);
	}


}
