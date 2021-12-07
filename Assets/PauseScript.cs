using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public static PauseScript pauseScript;

    public GameObject pauseMenu;

    void Start()
    {
        if(pauseScript == null)
        {
            pauseScript = this;
        }
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

	// Update is called once per frame
	public void TogglePause() {

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
        
	
}
