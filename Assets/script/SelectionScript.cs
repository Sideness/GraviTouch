using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;

using System.Xml;

/// <summary>
/// Script de l'écran titre
/// </summary>
public class SelectionScript : MonoBehaviour
{
	protected static int indexLevel = 0;
	private int buttonWidth = 135;
	private int buttonHeight = 145;
	private int NbLvlLigne = 5;
	private static bool canMove = true;
	private static Timer aTimer;
	private bool mouseOnLevel = false;
	public	float ColorMaxInit = 200;
	public float ColorMinInit = 30;
    
    private static List<Dictionary<string, string>> levels;
	private static float colorMax;
	private static float colorMin;
	private static int colorState = 0;
	private static float colorStep = 0.01f;
	
	private Color color = new Color(colorMax,colorMin,colorMin);
	
	public static void nextLevel(){
        indexLevel++;
		
        Application.LoadLevel(levels[indexLevel]["title"]);
	}

	public static bool isLastLevel(){
        return indexLevel == levels.Count - 1;
	}
	
	public static void resetLevel(){

        Application.LoadLevel(levels[indexLevel]["title"]);
	}
	
	void Start()
	{
		
		
	    if (Camera.current != null)
		    Camera.current.backgroundColor = new Color(colorMax,colorMin,colorMin);

        levels = new List<Dictionary<string, string>>();
        Dictionary<string, string> obj;

        colorMax = ColorMaxInit / 255f;
        colorMin = ColorMinInit / 255f;

        TextAsset textLevels = (TextAsset)Resources.Load("Levels");
        XmlDocument xmlLevels = new XmlDocument();
        xmlLevels.LoadXml(textLevels.text);

        XmlNodeList levelsList = xmlLevels.GetElementsByTagName("level"); // array of the level nodes.

        foreach (XmlNode levelInfo in levelsList)
        {
            XmlNodeList levelcontent = levelInfo.ChildNodes;
            obj = new Dictionary<string, string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.

            foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
            {
                if (levelsItens.Name == "title")
                {
                    obj.Add("title", levelsItens.InnerText); // put this in the dictionary.
                }
                if (levelsItens.Name == "score")
                {
                    obj.Add("score", levelsItens.InnerText); // put this in the dictionary.
                }
                if (levelsItens.Name == "time")
                {
                    obj.Add("time", levelsItens.InnerText); // put this in the dictionary.
                }
            }
            levels.Add(obj);
        }
	}
	
	void OnGUI()
	{
		int x = 0;
		int y = 0;

        mouseOnLevel = false;
        int cptLevel = 0;
        foreach (Dictionary<string, string> level in levels)
        {
            x += buttonWidth + 20;
            if (cptLevel % NbLvlLigne == 0)
            {
                y += buttonHeight + 20;
                x = 0;
            }
            Rect rect = new Rect(
                (Screen.width * 0.10f) - (buttonWidth / 2) + x,
                (Screen.height * 0.10f) - (buttonHeight / 2) + y,
                buttonWidth,
                buttonHeight
                );

            if (rect.Contains(Event.current.mousePosition))
            {
                indexLevel = cptLevel;
                mouseOnLevel = true;
            }

            string nameLevel;
            level.TryGetValue("title", out nameLevel);

            string nbStar;
            level.TryGetValue("score", out nbStar);
            int nbStarInt = int.Parse(nbStar);

            string chrono;
            level.TryGetValue("time", out chrono);
            bool chronoBool = bool.TryParse(chrono, out chronoBool);

            new SelectLevelItem( nameLevel, nbStarInt , chronoBool ).display(rect, indexLevel==cptLevel);

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
            Application.LoadLevel(levels[indexLevel]["title"]);
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

