using UnityEngine;
using System.Collections;

public class SelectLevelItem : MonoBehaviour {
	private Texture2D aTexture;
	private Rect myRect;
	private string levelName;
	private bool show;
	private float selectedAddedSize = 14;

	public SelectLevelItem(string levelName, int nbStar, bool Chrono)
	{	
		this.levelName = levelName;
		char chronoChar = '0';
		if (Chrono)
			chronoChar = '1';

		aTexture = Resources.Load ("Texture/Menu/" + nbStar + "_" + chronoChar) as Texture2D;
		show = false;
	}

	public void display(Rect rect,bool selected)
	{
			if (!aTexture) {
				Debug.LogError ("Assign a Texture in the inspector.");
				return;
			}



		if (!selected)
		GUI.DrawTexture(rect, aTexture, ScaleMode.StretchToFill, true, 10.0f);
		else 
			GUI.DrawTexture(new Rect(
				rect.x - selectedAddedSize/2,
				rect.y - selectedAddedSize/2,
				rect.width + selectedAddedSize,
				rect.height + selectedAddedSize),
			aTexture,ScaleMode.StretchToFill, true, 10.0f);


		GUIStyle  centeredStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		centeredStyle.alignment = TextAnchor.UpperCenter;
		if (selected)
			centeredStyle.fontSize = (int)(Screen.width * 0.015f);
		else
            centeredStyle.fontSize = (int)(Screen.width * 0.011f);

		GUI.Label (new Rect (rect.width/2-50+rect.x, rect.height/2-25+rect.y, 100, 50), levelName, centeredStyle);

	}
}
