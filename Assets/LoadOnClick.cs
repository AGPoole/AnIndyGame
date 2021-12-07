using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public void  LoadScene(int level)
    {
        //IndyHealth.SetLevel(level);
        RestartOnClick.SetLevel(level);
        SceneManager.LoadScene(level);
    }
}
