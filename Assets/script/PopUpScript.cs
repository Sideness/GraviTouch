using UnityEngine;
using System.Collections;

public class PopUpScript : MonoBehaviour {

	public delegate void actionA();
	public delegate void actionB();
	public delegate void actionX();
	public delegate void actionY();
	private actionA myActionA = null;
	private actionB myActionB = null;
	private actionX myActionX = null;
	private actionY myActionY = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){
		
	}

	//Bind l'action au delegate
	public void Bind(actionA _actionA, actionB _actionB, actionX _actionX, actionY _actionY){
		myActionA = _actionA;
		myActionB = _actionB;
		myActionX = _actionX;
		myActionY = _actionY;
	}


	public void display(string labelA = "", string labelB = "", string labelX = "", string labelY = "",
	                    string labelMessage = ""){
		//Chargement de la texture des boutons Xbox
		string texture = @"Assets\Texture\XboxControllerButton.png";
		Texture2D inputTexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
		
		//On vérifie si on joue avec un Joystick
		if (Input.GetJoystickNames().Length > 0){
			//GUI.DrawTexture(new Rect (Screen.width / 2 - 250/2, Screen.height*0.75f - 250/2, 250, 250), inputTexture, ScaleMode.StretchToFill, true, 30.0f);

		}else{
			float buttonWidth = 100, buttonHeight = 50;
			GUI.Label (new Rect (Screen.width * 0.10f, Screen.height * 0.20f, buttonWidth, buttonHeight), labelMessage);
			if (labelA != ""){
				if (GUI.Button (new Rect (Screen.width * 0.10f, Screen.height * 0.40f, buttonWidth, buttonHeight), labelA)) {
					if (myActionA == null){
						throw new UnityException("ButtonA action is not binded");
					}
					myActionA();
				}
			}
			if (labelX != ""){
				if (GUI.Button (new Rect (Screen.width * 0.10f, Screen.height * 0.50f, buttonWidth, buttonHeight), labelX)) {
					if (myActionX == null){
						throw new UnityException("ButtonX action is not binded");
					}
					myActionX();
				}
			}
			if (labelB != ""){
				if (GUI.Button (new Rect (Screen.width * 0.10f, Screen.height * 0.60f, buttonWidth, buttonHeight), labelB)) {
					if (myActionB == null){
						throw new UnityException("ButtonB action is not binded");
					}
					myActionB();
				}
			}
			if (labelY != ""){
				if (GUI.Button (new Rect (Screen.width * 0.10f, Screen.height * 0.70f, buttonWidth, buttonHeight), labelY)) {
					if (myActionY == null){
						throw new UnityException("ButtonY action is not binded");
					}
					myActionY();
				}
			}
		}
	}
}
