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

			//DO STUFF !!! 

		}
	}
}
