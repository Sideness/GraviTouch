using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private bool freeCamera = false;
	public Vector2 speedFreeCamera = new Vector2(50, 50);
	private Vector2 movement = new Vector2();
	public float smoothTime = 0.3F;
	public float zoomSpeed = 1.0F;
	public static float defaultZoom = 9.0F;
	private float currentZoom = 0f;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown ("FreeCamera")) {
			Debug.Log ("FreeCamera");
			freeCamera = freeCamera?false:true ;
			GameObject.Find("Perso").GetComponent<PlayerScript>().setControlable(!freeCamera);
			
		}



		if (freeCamera)
		{

			
			if (Input.GetButtonDown ("CameraZoomPlus")) 
			{
				currentZoom = zoomSpeed;
				Debug.Log ("ZoomPlus = " + currentZoom);
			}
			else if (Input.GetButtonDown ("CameraZoomMinus")) 
			{
					currentZoom = zoomSpeed * -1;
				Debug.Log ("ZoomMinus = " + currentZoom);
			}
		
		
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		movement = new Vector2(speedFreeCamera.x * inputX, speedFreeCamera.y* inputY);
		

		}
	}

	void FixedUpdate()
	{
		if (freeCamera && Camera.current != null) {
			Vector3 targetPosition = new Vector3(Camera.current.transform.position.x + movement.x,
			                                                           Camera.current.transform.position.y + movement.y,
			                                                           Camera.current.transform.position.z);

			Camera.current.transform.position =  Vector3.SmoothDamp(transform.position,targetPosition,ref velocity, smoothTime);
			Camera.current.orthographicSize += currentZoom;
			currentZoom=0;
				}
		
	}
}
