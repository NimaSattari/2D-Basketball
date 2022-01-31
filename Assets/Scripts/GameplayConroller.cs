using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayConroller : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
