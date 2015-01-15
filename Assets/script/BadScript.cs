using UnityEngine;
using System.Collections;

public class BadScript : MonoBehaviour {
	
	private bool displayGUI = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ResetLevel(){
		displayGUI = true;
	}
	
	void OnGUI(){
		if (displayGUI) {
<<<<<<< HEAD
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 50, 100, 50), "Vous etes mort !");
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 25, 100, 50), "Recommencer")) {
				SelectionScript.resetLevel();
			}
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.width / 2 - 25, 100, 50), "Menu")) {
				Application.LoadLevel("menu");
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.width / 2 - 25, 100, 50), "Quitter")) {
				Application.Quit();
			}
=======
			new ControllerMenu().display();
>>>>>>> b60d0d5fccc87d3945fa247b1ed56074303ac8c3
		}

	}
}
