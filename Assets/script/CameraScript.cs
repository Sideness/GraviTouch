using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


    private bool freeCamera = false;
	public Vector2 speedFreeCamera = new Vector2(50, 50);
	private Vector2 movement = new Vector2();
	public float smoothTime = 0.3F;
	public float zoomSpeed = 1.0F;
	public static float gravity = 9.89f;
	public static float defaultZoom = 9.0F;
	private float currentZoom = 0f;
	private Vector3 velocity = Vector3.zero;
	public static Vector2[] gravityDirections = new []{new Vector2(0f, -gravity), //Normal
		new Vector2(gravity, 0f),//Gauche 											
		new Vector2(0f, gravity),//Haut
		new Vector2(-gravity, 0f)};//Droite
	public static int selectedGravity = 0;

	// Use this for initialization
	void Start () {
		selectedGravity = 0;
		Physics2D.gravity = gravityDirections [selectedGravity];
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown ("FreeCamera")) {
			Debug.Log ("FreeCamera");
			freeCamera = freeCamera?false:true ;
			PlayerScript player = GameObject.Find("Perso").GetComponent<PlayerScript>();
				if (player != null)
					player.setControlable(!freeCamera);
			
		}



		if (freeCamera)
		{

			
			if (Input.GetButtonDown ("CameraZoomPlus")) 
			{
				currentZoom = zoomSpeed* -1;

			}
			else if (Input.GetButtonDown ("CameraZoomMinus")) 
			{
					currentZoom = zoomSpeed ;

			}
		
		
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		movement = new Vector2(speedFreeCamera.x * inputX, speedFreeCamera.y* inputY);
			movement=applyGravity (movement);

		}
	}

	void FixedUpdate()
	{



		if (Camera.current != null && GameObject.Find("Directional light") != null) {
			GameObject.Find("Directional light").GetComponent<Light>().transform.position = Camera.current.transform.position;
						if (freeCamera) {
								Vector3 targetPosition = new Vector3 (Camera.current.transform.position.x + movement.x,
			                                                           Camera.current.transform.position.y + movement.y,
			                                                           Camera.current.transform.position.z);

								Camera.current.transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);

								Camera.current.orthographicSize += currentZoom;
								currentZoom = 0;
						}
				}
		
	}

	public static Vector2 applyGravity(Vector2 vector)
	{
		if (CameraScript.selectedGravity == 1) {
			float tmpX = vector.x;
			vector.x = vector.y;
			vector.y = tmpX;
		} else if (CameraScript.selectedGravity == 2)
			vector *= -1;
		else if (CameraScript.selectedGravity == 3) {
			float tmpX = vector.x;
			vector.x = -vector.y;
			vector.y = -tmpX;
		}
		return vector;
		}

}
