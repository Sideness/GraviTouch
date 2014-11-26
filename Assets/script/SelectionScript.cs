﻿using System.IO;
using UnityEngine;

/// <summary>
/// Script de l'écran titre
/// </summary>
public class SelectionScript : MonoBehaviour
{
    void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;
        const int NbLvlLigne = 5;
        int x = 0;

        DirectoryInfo dir = new DirectoryInfo(@"D:\COURS INGESUP\GraviTouch Git\gravitouch\Assets\scene\Niveaux");
        FileInfo[] fichiers = dir.GetFiles("*.unity");

        foreach (FileInfo fichier in fichiers)
        {
                
            x += buttonWidth + 2;
            if (
                GUI.Button(
                // Centré en x, 2/3 en y
                    new Rect(
                    (Screen.width / 2) - (buttonWidth * NbLvlLigne / 2) + x,
                    (2 * Screen.height / 3) - (buttonHeight / 2),
                    buttonWidth,
                    buttonHeight
                ),
                fichier.Name
                )
            )
            {
                // Sur le clic, on démarre le premier niveau
                // "Stage1" est le nom de la première scène que nous avons créés.
                Debug.Log("NameScene : " + fichier.Name.Substring(0, fichier.Name.Length - 6));
                Application.LoadLevel(fichier.Name.Substring(0, fichier.Name.Length - 6));
                    
            }
        }
    }
}
