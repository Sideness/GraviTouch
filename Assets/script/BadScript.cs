using UnityEngine;
using System.Collections;

public class BadScript : MonoBehaviour {
	private PopUpScript myDeathPopUp;
	private bool displayGUI = false;
	// Use this for initialization
	void Start () {
		myDeathPopUp = new PopUpScript();
		myDeathPopUp.Bind(delegate(){SelectionScript.resetLevel();}, //boutonA
		delegate(){Application.Quit();}, //boutonB
		delegate(){Application.LoadLevel("menu");},
		null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ResetLevel(){
		displayGUI = true;
	}
	
	void OnGUI(){
		if (displayGUI) {
			myDeathPopUp.display("Recommencer", "Quitter", "Menu", "", "Game Over");
		}

	}
}
