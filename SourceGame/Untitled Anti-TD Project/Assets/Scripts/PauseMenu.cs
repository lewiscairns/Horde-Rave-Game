using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;

    public void Resume()
    {
        //set the game back to normal speed, turn off the menu and show the state of the menu using paused
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void Pause()
    {
        //load the pause menu, set the game speed to 0 and show the state of the menu using paused
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        //set the game back to normal speed and change the scene back to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player presses esc and if so turn the menu on or off depending on the state of paused
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                Resume();
            } else {
                Pause();
            }
        }
    }
}
