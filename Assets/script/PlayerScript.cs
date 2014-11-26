using UnityEngine;
using System.Timers;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - La vitesse de déplacement
	/// </summary>
	public Vector2 speed = new Vector2(50, 50);
	private static int rotationIndex = -1;
	public static float gravity = 9.89f;

	public static int selectedGravity = 0;
	// 2 - Stockage du mouvement
	private Vector2 movement;
	private bool RightOrLeft; // Right = 0 , Left = 1;
	private float rotation = 0;
	private bool changeGravity = false;
	private Vector2[] gravityDirections = new []{new Vector2(0f, -gravity), //Normal
											new Vector2(gravity, 0f),//Gauche 											
											new Vector2(0f, gravity),//Haut
											new Vector2(-gravity, 0f)};//Droite


	void Update()
	{
		// 3 - Récupérer les informations du clavier/manette
		float inputX = Input.GetAxis("Horizontal");


		// 4 - Calcul du mouvement
		movement = new Vector2(
			speed.x * inputX,
			0);
		if (selectedGravity == 1) {
						float tmpX = movement.x;
						movement.x = movement.y;
						movement.y = tmpX;
				} else if (selectedGravity == 2)
						movement *= -1;
				else if (selectedGravity == 3)
				{
					float tmpX = movement.x;
					movement.x = -movement.y;
					movement.y = -tmpX;
				}



		if (Input.GetButtonDown ("RotateRight")) {
						changeGravity = true;
						RightOrLeft = false;
						
				} else	if (Input.GetButtonDown ("RotateLeft")) {
						changeGravity = true;
						RightOrLeft = true;
				}




	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log(collider.gameObject.GetType().ToString());
		if (collider.gameObject.GetComponent<BadScript> ()) {
			selectedGravity = 1;
						Rotate(true);
						Destroy (gameObject);
						Application.LoadLevel (Application.loadedLevel);
				}
		else if (collider.gameObject.GetComponent<EndDoorScript> ()) {
			collider.gameObject.GetComponent<EndDoorScript> ().EndLevel();
		}
	}
	void FixedUpdate()
	{
		// 5 - Déplacement
		rigidbody2D.velocity = movement;
		if(Camera.current != null)
			Camera.current.transform.position = new Vector3(rigidbody2D.position.x, rigidbody2D.position.y,-10f);



		if (changeGravity)
		{
			Rotate(RightOrLeft);
			rotationIndex = 0;
			changeGravity = false;
		}
		if (Camera.current != null) {
			var target = Quaternion.Euler (0, 0, selectedGravity * 90);
			Camera.current.transform.rotation = Quaternion.Slerp (Camera.current.transform.rotation, target,
			                                                      Time.deltaTime * 5);
//			if(rotationIndex <=)
//				Camera.current.orthographicSize += 0.1f;
//			else
//						Camera.current.orthographicSize -= 0.1f;
				}

	}


	private void Rotate(bool RightOrLeft)
	{
		int direction = ( RightOrLeft ? -1 : 1);
		selectedGravity += direction;


		if (selectedGravity > 3)
						selectedGravity = 0;
				else if (selectedGravity < 0)
						selectedGravity = 3;

		Physics2D.gravity = gravityDirections [selectedGravity];

	}
}