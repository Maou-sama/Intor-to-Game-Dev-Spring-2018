using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {

    [Header("Fade Texture and Speed")]
    [SerializeField] private Texture2D fadeOutTexture;
    [SerializeField] private float fadeSpeed;

    //Values to draw the fade texture
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = 1;

	private void OnGUI()
    {
        //Change the alpha value using the direction (in or out) and speed then clamp it to either 0 or 1
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        //Draw the fade texture on the whole screen on top of everything else
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade (int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginFade(-1);
    }
}
