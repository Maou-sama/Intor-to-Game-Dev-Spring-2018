using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    SceneManager.LoadScene("MainMenu");
                    Destroy(gameObject);
                }
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("Load Prev Level");
                    LoadPreviousScene();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Load Next Level");
                    LoadNextScene();
                }
            }
        }
    }

    void LoadNextScene()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LoadPreviousScene()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
