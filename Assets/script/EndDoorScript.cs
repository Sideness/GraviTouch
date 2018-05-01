using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndDoorScript : MonoBehaviour {

	private bool displayGUI = false;
	private PopUpScript mySuccessPopUp;
	// Use this for initialization
	void Start () {
		if (SelectionScript.isLastLevel()){
			mySuccessPopUp = new PopUpScript ();
			mySuccessPopUp.Bind (delegate() {
                SceneManager.LoadScene("selection");
			}, //boutonA
			delegate() {
				Application.Quit ();
			}, //boutonB
			null,
			null);
		} else {
			mySuccessPopUp = new PopUpScript ();
			mySuccessPopUp.Bind (delegate() {
				SelectionScript.nextLevel();
			}, //boutonA
			delegate() {
				Application.Quit ();
			}, //boutonB
			delegate() {
				SceneManager.LoadScene("selection");
			},
			null);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndLevel(){
		displayGUI = true;
        MyTime time = GameObject.Find("Main Camera").GetComponent<MyTime>();
        if (time.endTimer())
        {
            time.endTimer();
        }
	}

	void OnGUI(){
		if (displayGUI) {
			if (SelectionScript.isLastLevel()){
				mySuccessPopUp.display("Choix du niveau", "Quitter", "", "", "Niveau et jeu terminés");
			}else{
				mySuccessPopUp.display("Niveau suivant", "Quitter", "Choix du niveau", "", "Niveau terminé");
			}
		}
	}
}
