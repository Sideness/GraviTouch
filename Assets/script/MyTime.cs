using UnityEngine;
using System.Collections;

public class MyTime : MonoBehaviour {
    public float timer = 0.0f;
    private float timeNeeded;
    private bool running = false;
    public GUIStyle style;

    public void Start()
    {
        timeNeeded = SelectionScript.getTargetTime();
        running = true;
    }

    public bool endTimer()
    {
        running = false;
        return timer <= timeNeeded;
    }

    public MyTime()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            timer += Time.deltaTime;
        }
	}

    public void OnGUI()
    {
        style = new GUIStyle(GUI.skin.GetStyle("Box"));
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = (int)(Screen.width * 0.03f);
        style.normal.textColor = Color.white;

        if (timer > timeNeeded)
        {
            style.normal.textColor = Color.red;
        }

        GUI.Box(new Rect(Screen.width * 0.40f, 0, Screen.width * 0.20f, Screen.height * 0.10f),
                         "Temps : " + timer.ToString("00.00"), style);
    }
}