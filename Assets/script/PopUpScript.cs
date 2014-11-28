using UnityEngine;
using System.Collections;

public class PopUpScript : MonoBehaviour {

	private static string message = "";
	private static bool displayGUI = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static void PopUpMessage(string _message){
		message = _message;
		displayGUI = true;
	}
	
	void OnGUI(){
		if (displayGUI) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 50, 100, 50), message);
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.width / 2 - 25, 100, 50), "Continuer")) {
				Object.Destroy(this);
			}
		}
	}
}
