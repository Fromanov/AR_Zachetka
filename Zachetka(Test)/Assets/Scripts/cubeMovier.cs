using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovier : MonoBehaviour
{	
	public float rangeX = 2f;	
	public float speed = 3f;	
	public float direction = 1f;
	
	Vector3 initialPosition;

	
	void Start()
	{		
		initialPosition = transform.position;
	}
	
	void Update()
    {		
		float movementX = direction * speed * Time.deltaTime;		
		float newX = transform.position.x + movementX;
		
		if (Mathf.Abs(newX - initialPosition.y) > rangeX)
		{			
			direction *= -1;
		}		
		else
		{			
			transform.Translate(new Vector3(movementX, 0, 0));
		}
	}
}
