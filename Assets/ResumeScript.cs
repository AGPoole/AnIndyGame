using UnityEngine;
using System.Collections;

public class ResumeScript : MonoBehaviour {

	public void Resume()
    {
        PauseScript.pauseScript.TogglePause();
    } 
}
