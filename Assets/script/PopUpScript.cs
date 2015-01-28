using UnityEngine;
using System.Collections;

public class PopUpScript : MonoBehaviour {

	public delegate void actionA();
	public delegate void actionB();
	public delegate void actionX();
	public delegate void actionY();
	private static string message = "";
	private static bool displayGUI = false;
	private static ArrayList labelButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void display(actionA _actionA, actionB _actionB, actionX _actionX, actionY _actionY){
		//Chargement de la texture des boutons Xbox
		string texture = @"Assets\Texture\XboxControllerButton.png";
		Texture2D inputTexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
		
		//On vérifie si on joue avec un Joystick
		if (Input.GetJoystickNames().Length > 0){
			//GUI.DrawTexture(new Rect (Screen.width / 2 - 250/2, Screen.height*0.75f - 250/2, 250, 250), inputTexture, ScaleMode.StretchToFill, true, 30.0f);

		}else{
			GUI.Label (new Rect (Screen.width * 0.50f - 50, Screen.height * 0.60f, 100, 50), "Niveau Terminé !");
			if (GUI.Button (new Rect (Screen.width * 0.50f - 155, Screen.height * 0.70f, 100, 50), "Continuer")) {
				//SelectionScript.nextLevel();
				_actionA();
			}
			if (GUI.Button (new Rect (Screen.width * 0.50f - 50, Screen.height * 0.70f, 100, 50), "Menu")) {
				//Application.LoadLevel("menu");
				_actionX();
			}
			if (GUI.Button (new Rect (Screen.width * 0.50f + 55, Screen.height * 0.70f, 100, 50), "Quitter")) {
				//Application.Quit();
				_actionB();
			}
		}
	}
}
