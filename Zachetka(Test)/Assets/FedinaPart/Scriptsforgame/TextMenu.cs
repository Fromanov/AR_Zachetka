using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMenu : MonoBehaviour
{
	public Text txt;
    // Start is called before the first frame update
    void Start()
    {

		Vector3 pausePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		txt.rectTransform.position = Camera.main.WorldToScreenPoint(pausePos);
	
    }

    // Update is called once per frame
    void Update()
    {

		Vector3 pausePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		txt.rectTransform.position = Camera.main.WorldToScreenPoint(pausePos);
	}
}
