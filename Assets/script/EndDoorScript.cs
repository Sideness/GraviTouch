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
			PopUpScript mySuccessPopUp = new PopUpScript();
			mySuccessPopUp.Bind(delegate(){SelectionScript.nextLevel();}, //boutonA
			delegate(){Application.Quit();}, //boutonB
			delegate(){Application.LoadLevel("menu");},
			null);
			
			mySuccessPopUp.display("Niveau suivant", "Quitter", "Menu", "", "Niveau terminé");
		}
	}
}
