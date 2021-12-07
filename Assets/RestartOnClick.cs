using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartOnClick : MonoBehaviour {
    static int level;

    public static void SetLevel(int lev)
    {
        level = lev;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(level);
    }
}
