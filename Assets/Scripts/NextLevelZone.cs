using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevelZone : MonoBehaviour
{
    [Header("Black Screen For Scene Transition")]
    [SerializeField] private float fadeTime;

    //When player advance to the next level play the scene transition and stop all movement
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Movement>().enabled = false;
            StartCoroutine(SceneTransition());
        }
    }

    private IEnumerator SceneTransition()
    {
        float fadeTime = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
