using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string SceneToLoad = "Scene1";

    // OnClick function for "Play" button
    public void StartGame()
    {
        Debug.Log("Start the game!");
        SceneManager.LoadScene(SceneToLoad);
    }

    // OnClick function for "Quit" button
    public void QuitGame()
    {
        Debug.Log("Game should be closing now.");
        Application.Quit();
    }
}
