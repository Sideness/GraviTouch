using UnityEngine;
using System.Collections;

public class MyDeath : MonoBehaviour {

    public int numberDeath = 0;
    public GUIStyle style;

	// Use this for initialization
	void Start () {
        numberDeath = SelectionScript.getNumberDeath();
	}
	
    public void AddDeath()
    {
        ++numberDeath;
        SelectionScript.writeXML("deaths", numberDeath+"");
    }

    public void OnGUI()
    {
        style = new GUIStyle(GUI.skin.GetStyle("Box"));
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = (int)(Screen.width * 0.03f);
        style.normal.textColor = Color.white;

        GUI.Box(new Rect(Screen.width * 0.05f, 0, Screen.width * 0.20f, Screen.height * 0.10f),
                         "Morts : " + numberDeath.ToString(), style);
    }
}
