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
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 50, 100, 50), "Vous etes mort !");
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 25, 100, 50), "Recommencer")) {
				SelectionScript.resetLevel();
			}
		}
	}
}
