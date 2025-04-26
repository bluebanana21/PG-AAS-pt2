
using System.Diagnostics;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused =false;
    public GameObject pauseMenuUi;
    public GameObject player;
    public GameObject saveManager;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerInteract>().enabled = true;
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerInteract>().enabled = false;
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
       
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }
}
