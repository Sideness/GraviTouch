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
			PopUpScript myDeathPopUp = new PopUpScript();
			myDeathPopUp.Bind(delegate(){SelectionScript.resetLevel();}, //boutonA
								delegate(){Application.Quit();}, //boutonB
								delegate(){Application.LoadLevel("menu");},
								null);

			myDeathPopUp.display("Recommencer", "Quitter", "Menu", "", "Vous etes mort");
		}

	}
}
