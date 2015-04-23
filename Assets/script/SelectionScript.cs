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
	private int buttonWidth = (int)(Screen.width * 0.10f);
	private int buttonHeight = (int)(Screen.width * 9 / 16 * 0.20f);
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

    public static float getTargetTime()
    {
        return float.Parse(levels[indexLevel]["targetTime"]);
    }

    public static int getNumberDeath()
    {
        return int.Parse(levels[indexLevel]["deaths"]);
    }

    public static void writeXML(string attribut, string value)
    {
        StreamReader textLevels = new StreamReader(Application.persistentDataPath + "/levels.xml");
        XmlDocument xmlLevels = new XmlDocument();
        xmlLevels.LoadXml(textLevels.ReadToEnd());
        XmlNodeList levelList = xmlLevels.SelectNodes("//levels/level");
        XmlNodeList levelcontent = levelList[indexLevel].ChildNodes;
        foreach (XmlNode levelsItens in levelcontent)
        {
            if (levelsItens.Name == attribut)
            {
                levelsItens.InnerText = value;
            }
        }
        textLevels.Close();
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/levels.xml");

        xmlLevels.Save(writer);
        writer.Close();
        readXML();
    }

    public static void readXML(){

        levels = new List<Dictionary<string, string>>();
        Dictionary<string, string> obj;

        StreamReader textLevels = new StreamReader(Application.persistentDataPath + "/levels.xml");
        XmlDocument xmlLevels = new XmlDocument();
        xmlLevels.LoadXml(textLevels.ReadToEnd());
        textLevels.Close();

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
                if (levelsItens.Name == "targetTime")
                {
                    obj.Add("targetTime", levelsItens.InnerText); // put this in the dictionary.
                }
                if (levelsItens.Name == "deaths")
                {
                    obj.Add("deaths", levelsItens.InnerText); // put this in the dictionary.
                }
            }
            levels.Add(obj);
        }
    }
	
	void Start()
    {
        {
            if (File.Exists(Application.persistentDataPath + "/levels.xml") == false)
            {
                TextAsset textLevelsA = (TextAsset)Resources.Load("Levels");
                XmlDocument xmlLevelsA = new XmlDocument();
                xmlLevelsA.LoadXml(textLevelsA.text);

                StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/levels.xml");

                xmlLevelsA.Save(writer);
                writer.Close();
            }
        }

        if (Camera.current != null)
            Camera.current.backgroundColor = new Color(colorMax, colorMin, colorMin);

        colorMax = ColorMaxInit / 255f;
        colorMin = ColorMinInit / 255f;

        readXML();
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
            bool chronoBool = bool.Parse(chrono);

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

