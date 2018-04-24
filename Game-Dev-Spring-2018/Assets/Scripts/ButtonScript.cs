using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}