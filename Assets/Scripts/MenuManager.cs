using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void LaunchGame()
    {
        Debug.Log("Launching game...");
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        // Quit the game
        Application.Quit();
    }
}
