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

	private bool isCntrolable = true;
    private Rect mobileRightRect;
    private Rect mobileLeftRect;
    private bool isAlive = true;

	// 2 - Stockage du mouvement
	private Vector2 movement;
	private bool RightOrLeft; // Right = 0 , Left = 1;
	private float rotation = 0;
	private bool changeGravity = false;
    private Vector2 touchPoint = new Vector2();

    public PlayerScript(){
        mobileLeftRect = new Rect(
                0, 0,
                Screen.width * 0.5f,
                Screen.height);
        mobileRightRect = new Rect(
                Screen.width * 0.5f, 0,
                Screen.width * 0.5f,
                Screen.height);
    }


	void Start(){

	}


	void Update()
	{
		if (isCntrolable) {
						// 3 - Récupérer les informations du clavier/manette
						float inputX = Input.GetAxis ("Horizontal");


						// 4 - Calcul du mouvement
						movement = new Vector2 (
			speed.x * inputX,
			0);

			movement = CameraScript.applyGravity (movement);


			if (Input.GetButtonDown ("RotateRight")) {
					changeGravity = true;
					RightOrLeft = false;
						
			} else	if (Input.GetButtonDown ("RotateLeft")) {
					changeGravity = true;
					RightOrLeft = true;
			}

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPoint = invertAxisTouch(Input.GetTouch(0).position);
                //if (Input.GetTouch(0).deltaPosition )
                if (mobileLeftRect.Contains(touchPoint))
                {
                    changeGravity = true;
                    RightOrLeft = true;
                }
                else if (mobileRightRect.Contains(touchPoint) && CameraScript.freeCameraRect.Contains(touchPoint) == false)
                {
                    changeGravity = true;
                    RightOrLeft = false;
                }
            }
	    }
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
        if (isAlive)
        {
            if (collider.gameObject.GetComponent<BadScript>())
            {
                MyTime time = GameObject.Find("Main Camera").GetComponent<MyTime>();
                if (SelectionScript.getTargetTime() >= time.timer)
                {
                    SelectionScript.writeXML("time", "true");
                }
                time.endTimer();
                MyDeath death = GameObject.Find("Main Camera").GetComponent<MyDeath>();
                death.AddDeath();
                isAlive = false;
                collider.gameObject.GetComponent<BadScript>().ResetLevel();
                Object.Destroy(this);
                return;

            }
            else if (collider.gameObject.GetComponent<EndDoorScript>())
            {
                collider.gameObject.GetComponent<EndDoorScript>().EndLevel();
                Object.Destroy(this);
            }
        }
	}

	public void setControlable(bool isControlable)
	{
		isCntrolable = isControlable;
	}

	void FixedUpdate()
	{
		// 5 - Déplacement
		if (isCntrolable)
		{
						GetComponent<Rigidbody2D>().velocity = movement;
				if (Camera.current != null)
			{
						Camera.current.transform.position = new Vector3 (GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, -10f);
						Camera.current.orthographicSize = CameraScript.defaultZoom;
			}



				if (changeGravity) {
						Rotate (RightOrLeft);
						rotationIndex = 0;
						changeGravity = false;
				}
		}
				if (Camera.current != null) {
						var target = Quaternion.Euler (0, 0, CameraScript.selectedGravity * 90);
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
		CameraScript.selectedGravity += direction;


		if (CameraScript.selectedGravity > 3)
			CameraScript.selectedGravity = 0;
		else if (CameraScript.selectedGravity < 0)
			CameraScript.selectedGravity = 3;

		Physics2D.gravity = CameraScript.gravityDirections [CameraScript.selectedGravity];

	}

    public static Vector2 invertAxisTouch(Vector2 vect)
    {
        Vector2 vectTmp = new Vector2(vect.x, vect.y);
        vectTmp.y = Screen.height - vectTmp.y;

        return vectTmp;
    }

}