using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
