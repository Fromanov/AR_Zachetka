// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class FloatCubes : MonoBehaviour {
	// User Inputs
	public float degreesPerSecond = 15.0f;
	public float amplitude = 0.5f;
	public float frequency = 1f;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

	private Vector3 startSize;

	// Use this for initialization
	void Start () {
		startSize = transform.localScale;
		degreesPerSecond = Random.Range(4,25);
		amplitude = Random.Range(0.3f,1f);
		frequency = Random.Range(0.1f,1f);

		// Store the starting position & rotation of the object
		posOffset = transform.position;
	}

	// Update is called once per frame
	void Update () {
		// Spin object around Y-Axis
		transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

		// Float up/down with a Sin()
		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
	}

	void CubeHide()
	{
		StartCoroutine("CubeScale");
	}
	void OnEnable()
	{
		if(startSize != Vector3.zero)
		{
			transform.localScale = startSize;
		}
	}
	IEnumerator CubeScale()
	{
		while(transform.localScale.x > 0)
		{
			transform.localScale += new Vector3(-0.01f,-0.01f,-0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		transform.localScale = new Vector3(0,0,0);

	}
}