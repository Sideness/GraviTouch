using UnityEngine;
using System.Collections;
using System.Timers;

public class MoveScript : MonoBehaviour {

	// Use this for initialization

	public float posMin ;
	public float posMax ;
	public float speed ;

 
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

			if ((transform.position.y < posMin || transform.position.y > posMax)) {
						speed *= -1;
						
				}



	}

	void FixedUpdate()
	{

		transform.Translate (new Vector3 (speed * Time.deltaTime,0.0f, 0.0f));
	}
	
}
