using UnityEngine;

/// <summary>
/// Script de l'écran titre
/// </summary>
public class MenuScript : MonoBehaviour
{
    void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;

        // Affiche un bouton pour sélectionner un niveau
        if (
          GUI.Button(
            // Centré en x, 2/3 en y
            new Rect(
              Screen.width / 2 - (buttonWidth / 2),
              (2 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Select level !"
          )
        )
        {
            // Sur le clic, on lance l'écran de sélection de niveaux
            // "selection" est le nom de la scène de sélection de niveau
            Application.LoadLevel("selection");
        }
    }
}
