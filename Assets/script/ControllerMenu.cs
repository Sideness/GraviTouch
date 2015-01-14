﻿using UnityEngine;
using System.Collections;

public class ControllerMenu : MonoBehaviour {

	public ControllerMenu(string buttonA, string buttonB, string buttonY, string buttonX){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Draw () {
		
	}

	void OnGUI(){
		string texture = @"Assets\Texture\XboxControllerButton.png";
		Texture2D inputTexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));

		if (displayGUI) {

			if (Input.GetJoystickNames.length > 0){
				GUI.DrawTexture(new Rect (Screen.width / 2 - 250/2, Screen.height*0.75f - 250/2, 250, 250), inputTexture, ScaleMode.StretchToFill, true, 30.0f);
			}else{
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 50, 100, 50), "Niveau Terminé !");
				if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 25, 100, 50), "Continuer")) {
					SelectionScript.nextLevel();
				}
				if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.width / 2 - 25, 100, 50), "Menu")) {
					Application.LoadLevel("menu");
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.width / 2 - 25, 100, 50), "Quitter")) {
					Application.Quit();
				}
			}


		}
	}
}
