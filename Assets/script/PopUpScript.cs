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
	private float slidingPanel = Screen.width * 0.3f;
	private float slidingPosition;
	private float slidingSpeed = 8;

	public PopUpScript(){
		slidingPosition = slidingPanel;
	}

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
		//string texture = @"Assets\Texture\XboxControllerButton.png";
		//Texture2D inputTexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
		float buttonWidth = Screen.width * 0.20f, buttonHeight = Screen.height * 0.15f;


		Texture2D textureBox = new Texture2D(1, 1);
		textureBox.SetPixel(0,0,new Color(0, 0, 0, 0.8f));
		textureBox.Apply();
		GUI.skin.box.normal.background = textureBox;
		GUI.Box(new Rect(0, 0, slidingPanel - slidingPosition, Screen.height), GUIContent.none);

		if (slidingPosition > 0) {
			slidingPosition -= slidingSpeed;
			if (slidingPosition < 0)
				slidingPosition = 0;
		}
			

		float buttonPositionX = slidingPanel / 2 - buttonWidth / 2 - slidingPosition;

		GUIStyle centeredStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.fontSize = (int)(Screen.width * 0.04f);
		centeredStyle.normal.textColor = Color.white;
		
		GUI.Label (new Rect (0 - slidingPosition, Screen.height * 0.10f, slidingPanel, buttonHeight * 2), labelMessage, centeredStyle);

        centeredStyle = new GUIStyle(GUI.skin.GetStyle("Button"));
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        centeredStyle.fontSize = (int)(Screen.width * 0.02f);
        centeredStyle.normal.textColor = Color.white;

		//On vérifie si on joue avec un Joystick
        //if (Input.GetJoystickNames().Length > 0){
        //    //GUI.DrawTexture(new Rect (Screen.width / 2 - 250/2, Screen.height*0.75f - 250/2, 250, 250), inputTexture, ScaleMode.StretchToFill, true, 30.0f);

        //}else{


			//GUI.Label(new Rect(0, 0, Screen.width * 0.3f, Screen.height), new Color());


			if (labelA != ""){
                if (GUI.Button(new Rect(buttonPositionX, Screen.height * 0.25f, buttonWidth, buttonHeight), labelA, centeredStyle))
                {
					if (myActionA == null){
						throw new UnityException("ButtonA action is not binded");
					}
					myActionA();
				}
			}
			if (labelB != ""){
                if (GUI.Button(new Rect(buttonPositionX, Screen.height * 0.45f, buttonWidth, buttonHeight), labelB, centeredStyle))
                {
					if (myActionB == null){
						throw new UnityException("ButtonB action is not binded");
					}
					myActionB();
				}
			}
			if (labelX != ""){
                if (GUI.Button(new Rect(buttonPositionX, Screen.height * 0.65f, buttonWidth, buttonHeight), labelX, centeredStyle))
                {
					if (myActionX == null){
						throw new UnityException("ButtonX action is not binded");
					}
					myActionX();
				}
			}
			if (labelY != ""){
                if (GUI.Button(new Rect(buttonPositionX, Screen.height * 0.85f, buttonWidth, buttonHeight), labelY, centeredStyle))
                {
					if (myActionY == null){
						throw new UnityException("ButtonY action is not binded");
					}
					myActionY();
				}
            //}
		}
	}
}
