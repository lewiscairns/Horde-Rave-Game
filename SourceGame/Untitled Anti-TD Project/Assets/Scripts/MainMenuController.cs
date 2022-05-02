using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject confirmScreen;

    public void startGame() {
        //change scene to map overview
        SceneManager.LoadScene(sceneName: "MapOverview");
    }

    public void NewGame()
    {
        //change menu view
        mainMenu.SetActive(false);
        confirmScreen.SetActive(true);
    }

    public void settings() {
        //change menu view
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void back() {
        //change menu view
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void exitGame() {
        //quit the game
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

