using UnityEngine;
using UnityEngine.SceneManagement;

public class BadScript : MonoBehaviour {
	private PopUpScript myDeathPopUp;
	private bool displayGUI = false;
	// Use this for initialization
	void Start () {
		myDeathPopUp = new PopUpScript();
		myDeathPopUp.Bind(delegate(){SelectionScript.resetLevel();}, //boutonA
		delegate(){Application.Quit();}, //boutonB
		delegate(){SceneManager.LoadScene("selection");},
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
			myDeathPopUp.display("Recommencer", "Quitter", "Choix du niveau", "", "Game Over");
		}
	}
}
