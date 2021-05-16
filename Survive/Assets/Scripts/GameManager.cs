using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D crosshair;
    public static float timeElapsed = 0.0f;
    public static int tombstonesDestroyed = 0;
    public static int zombiesKilled = 0;
    public static bool gameOver = false;

    void Start()
    {
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);

        GameManager.UpdateMusicVolume();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public static void ResetValues()
    {
        timeElapsed = 0.0f;
        tombstonesDestroyed = 0;
        zombiesKilled = 0;
        gameOver = false;
    }

    public static void UpdateMusicVolume()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
}
