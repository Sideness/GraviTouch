using UnityEngine;
using System.Collections;

public class EndDoorScript : MonoBehaviour {

	private bool displayGUI = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndLevel(){
		displayGUI = true;
	}

	void OnGUI(){
		if (displayGUI) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 50, 100, 50), "Niveau Terminé !");
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 25, 100, 50), "Continuer")) {
				SelectionScript.nextLevel();
			}
		}
	}
}
