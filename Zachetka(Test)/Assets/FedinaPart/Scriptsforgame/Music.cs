using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
	public AudioSource source;
	public AudioSource click;
	// Start is called before the first frame update
	void Start()
    {
		source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Click()
	{
		click.Play();
	}




}
