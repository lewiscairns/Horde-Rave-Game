using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapOverlayManager : MonoBehaviour
{
    public GameObject howTo;
    public GameObject tutorial;
    public GameObject lava;
    public GameObject mountain;
    public GameObject keep;
    public GameObject manager;
    public GameManager gameManager;

    public void LoadHowTo()
    {
        //load the map the player selected
        SceneManager.LoadScene(sceneName: "HowToPlay");
    }

    public void LoadTutorial()
    {
        //load the map the player selected
        SceneManager.LoadScene(sceneName: "TutorialMap");
    }

    public void LoadLava()
    {
        //load the map the player selected
        SceneManager.LoadScene(sceneName: "LavaMap");
    }

    public void LoadMountain()
    {
        //load the map the player selected
        SceneManager.LoadScene(sceneName: "MountainMap");
    }

    public void LoadKeep()
    {
        //load the map the player selected
        SceneManager.LoadScene(sceneName: "BouldersMap");
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the game manager
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //check the player level and load the available maps for the corresponding level
        int level = gameManager.CheckLevel();
        if(level == 1){
            lava.SetActive(true);
        } else if(level == 2){
            lava.SetActive(true);
            mountain.SetActive(true);
        } else if(level >= 3){
            lava.SetActive(true);
            mountain.SetActive(true);
            keep.SetActive(true);
        }
    }
}
