using System.IO;
using UnityEngine;
using System.Collections;
using System.Timers;

using System.IO;
using UnityEngine;

/// <summary>
/// Script de l'écran titre
/// </summary>
public class SelectionScript : MonoBehaviour
{
	protected static int indexLevel = 0;
	private int buttonWidth = 135;
	private int buttonHeight = 145;
	private int NbLvlLigne = 5;
	private int selectedAddedSize= 14;
	private static bool canMove = true;
	private static Timer aTimer;
	private bool mouseOnLevel = false;
	public	float ColorMaxInit = 200;
	public float ColorMinInit = 30;
	
	private static float colorMax;
	private static float colorMin;
	private static int colorState = 0;
	private static float colorStep = 0.01f;
	FileInfo[] fichiers;
	
	private Color color = new Color(colorMax,colorMin,colorMin);
	
	public static void nextLevel(){
		indexLevel++;
		
		DirectoryInfo dir = new DirectoryInfo(@".\assets\scene\Niveaux");
		FileInfo[] fichiers = dir.GetFiles("*.unity");
		
		Application.LoadLevel(fichiers[indexLevel].Name.Substring(0, fichiers[indexLevel].Name.Length - 6));
	}
	
	public static void resetLevel(){
		
		DirectoryInfo dir = new DirectoryInfo(@".\assets\scene\Niveaux");
		FileInfo[] fichiers = dir.GetFiles("*.unity");
		
		Application.LoadLevel(fichiers[indexLevel].Name.Substring(0, fichiers[indexLevel].Name.Length - 6));
	}
	
	public SelectionScript()
	{
		colorMax = ColorMaxInit/255f;
		colorMin = ColorMinInit/255f;
	}
	
	void Start()
	{
		
		
		if (Camera.current != null)
			Camera.current.backgroundColor = new Color(colorMax,colorMin,colorMin);
		
		
		
	}
	
	void OnGUI()
	{
		int x = 0;
		int y = 0;
		
		DirectoryInfo dir = new DirectoryInfo(@".\assets\scene\Niveaux");
		fichiers = dir.GetFiles("*.unity");
		mouseOnLevel = false;
		int cptLevel = 0;
		foreach (FileInfo fichier in fichiers)
		{
			x += buttonWidth + 20;
			if (cptLevel % NbLvlLigne == 0)
			{
				y += buttonHeight + 20;
				x = 0;
			}
			Rect rect = new Rect(
				(Screen.width *0.10f) - (buttonWidth / 2) + x,
				(Screen.height *0.10f) - (buttonHeight / 2) + y,
				buttonWidth,
				buttonHeight
				);
			
			if (rect.Contains (Event.current.mousePosition))
			{
				indexLevel = cptLevel;
				mouseOnLevel = true;
			}
			
			
			new SelectLevelItem(fichier.Name.Remove (fichier.Name.IndexOf (".")), cptLevel % 4,cptLevel % 2 == 0).display(rect,cptLevel == indexLevel);
			
			cptLevel++;
			
			
		}
		
		
	}
	
	void FixedUpdate()
	{
		if (Camera.current != null)
			Camera.current.backgroundColor = color;
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && mouseOnLevel)
		{
			Application.LoadLevel(fichiers[indexLevel].Name.Remove(fichiers[indexLevel].Name.IndexOf (".")));
		}
		
		if (Camera.current != null) {
			color = Camera.current.backgroundColor;
			
			switch (colorState) {
				
			case 0:
				if (color.b < colorMax)
					color.b += colorStep;
				else
					colorState = 1;
				break;
			case 1:
				if (color.r > colorMin)
					color.r -= colorStep;
				else
					colorState = 2;
				break;
			case 2:
				if (color.g < colorMax)
					color.g += colorStep;
				else
					colorState = 3;
				break;
			case 3:
				if (color.b > colorMin)
					color.b -= colorStep;
				else
					colorState = 4;
				break;
			case 4:
				if (color.r < colorMax)
					color.r += colorStep;
				else
					colorState = 5;
				break;
			case 5:
				if (color.g > colorMin)
					color.g -= colorStep;
				else
				{
					colorState = 0;
					color = new Color(colorMax,colorMin,colorMin);
				}
				break;
			}
			
			
		}
		
		if (canMove) {
			float inputX = Input.GetAxis ("Horizontal");
			
			canMove = false;
			if (inputX > 0) 
				indexLevel ++;
			else if (inputX < 0)
				indexLevel--;
			else
			{
				canMove= true;
				return;
			}
			
			
			aTimer = new Timer (200);
			
			
			aTimer.Elapsed += new ElapsedEventHandler (OnTimedEvent);
			aTimer.Enabled = true;
			
			
		}
		
	}
	
	private static void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		canMove = true;
		aTimer.Enabled = false;
	}
	
}

