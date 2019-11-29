using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
	public int cc;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
		cc = PlayerPrefs.GetInt("max");
		text.text = cc.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
