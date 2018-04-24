using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevelZone : MonoBehaviour
{
    [Header("Black Screen For Scene Transition")]
    [SerializeField] private GameObject blackScreen;

    //When player advance to the next level play the scene transition animation and stop all movement
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            blackScreen.GetComponent<Animator>().SetBool("LoadNextScene", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Movement>().enabled = false;
            StartCoroutine(SceneTransition());
        }
    }

    private IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
