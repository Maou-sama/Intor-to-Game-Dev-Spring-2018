using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadButton : MonoBehaviour
{

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
