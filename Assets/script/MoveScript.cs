using UnityEngine;
using System.Collections;
using System.Timers;

public class MoveScript : MonoBehaviour {

	// Use this for initialization

	public float posMin ;
	public float posMax ;
	public float speed ;
	private float currentSpeed;

 
	void Start () {
		currentSpeed = speed;
	
	}
	
	// Update is called once per frame
	void Update () {

			if (transform.position.y < posMin) {
				currentSpeed = speed;
						
			}
			else if (transform.position.y > posMax)
			{
				currentSpeed = speed*-1;
			}




	}

	void FixedUpdate()
	{

		transform.Translate (new Vector3 (currentSpeed * Time.deltaTime,0.0f, 0.0f));
	}
	
}
