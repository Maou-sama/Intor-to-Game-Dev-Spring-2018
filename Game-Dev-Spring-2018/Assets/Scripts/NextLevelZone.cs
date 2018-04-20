using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevelZone : MonoBehaviour
{

    public GameObject blackScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            blackScreen.GetComponent<Animator>().SetBool("LoadNextScene", true);
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Movement>().enabled = false;
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
