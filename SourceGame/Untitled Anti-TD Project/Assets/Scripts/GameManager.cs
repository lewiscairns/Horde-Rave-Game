using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LeadMinion leadMinion;
    public MainMenuController controller;
    PlayerSave gameData = new PlayerSave();

    public GameObject configureSystem;
    public GameObject gameMap;
    public GameObject failImage;
    public GameObject winImage;
    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;
    public GameObject spell4;
    public GameObject volumeSlider;
    public GameObject music;

    public GameObject freezeConfirm;
    public GameObject maxConfirm;
    public GameObject removeConfirm;

    public int mapHealth;
    public int fullHealth;
    public bool started;
    public int levelIfComplete;
    public bool first;

    public void CloseFreeze()
    {
        //close spell instruction screen
        freezeConfirm.SetActive(false);
    }

    public void CloseMax()
    {
        //close spell instruction screen
        maxConfirm.SetActive(false);
    }

    public void CloseRemove()
    {
        //close spell instruction screen
        removeConfirm.SetActive(false);
    }

    public void OpenMapOverview()
    {
        //open the map overview scene
        controller.startGame();
    }

    public void VolumeSlider()
    {
        //get the current value of the slider and set that as the volume of the music
        gameData.SetVolume(volumeSlider.GetComponent<Slider>().value);
        music.GetComponent<AudioSource>().volume = gameData.volume;
        //save the players volume settings
        SaveControl.instance.SaveGame(gameData);
    }

    public void GMNewGame()
    {
        //reset the save data of the game
        gameData.ResetData();
        SaveControl.instance.SaveGame(gameData);
    }

    public void GMSaveGame()
    {
        //save the game data currently stored in the game data instance
        SaveControl.instance.SaveGame(gameData);
    }

    public int CheckLevel()
    {
        //give the script calling the players level
        return gameData.level;
    }

    public void BeginMap()
    {
        //activate all unlocked spells and start the game map
        spell1.SetActive(true);
        spell2.SetActive(true);
        //if statements to check whether the player has the spells unlocked or not
        if(gameData.level >= 2) {
            spell3.SetActive(true);
        } else {
            spell3.SetActive(false);
        }
        if(gameData.level >= 3) {
            spell4.SetActive(true);
        } else {
            spell4.SetActive(false);
        }
        configureSystem.SetActive(false);
        gameMap.SetActive(true);
        //tell the spell manager whether its the first wave and reset all its cooldowns
        GameObject spellManager = GameObject.Find("SpellManager");
        if(!first) {
            spellManager.GetComponent<SpellControl>().FirstWave();
        }
        spellManager.GetComponent<SpellControl>().WaveReset();
        //note the first wave has been played
        first = true;
        //get the wave script and begin the wave then wait to start before doing game checks by calling wait game
        GameObject newPath = GameObject.Find("NewPath");
        leadMinion = newPath.GetComponent<LeadMinion>();
        leadMinion.StartWave();
        StartCoroutine(WaitGame());
    }

    IEnumerator WaitGame()
    {
        //wait 2 seconds then allow the manager to check variables
        yield return new WaitForSeconds(2);
        started = true;
    }

    public void RoundFail()
    {
        StartCoroutine(RoundFailScreen());
    }

    IEnumerator RoundFailScreen()
    {
        //turn off the spells, show the fail screen the re-open the configure screen
        spell1.SetActive(false);
        spell2.SetActive(false);
        spell3.SetActive(false);
        spell4.SetActive(false);
        failImage.SetActive(true);
        yield return new WaitForSeconds(1);
        failImage.SetActive(false);
        configureSystem.SetActive(true);
        GameObject spellManager = GameObject.Find("SpellManager");
        spellManager.GetComponent<SpellControl>().MouseReset();
    }

    public void RoundWin()
    {
        //remove all minions, set the players level higher as required, save the game data if so and start win screen
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag ("Minion");
        for(var i = 0 ; i < gameObjects2.Length ; i ++)
        {
            Destroy(gameObjects2[i]);
        }
        if(levelIfComplete > gameData.level){
            gameData.AddLevel(levelIfComplete);
            gameData.HasPlayed(false);
            SaveControl.instance.SaveGame(gameData);
        }
        SaveControl.instance.SaveGame(gameData);
        StartCoroutine(RoundWinScreen());
    }

    IEnumerator RoundWinScreen()
    {
        //turn off the spells and load the map overview scene
        spell1.SetActive(false);
        spell2.SetActive(false);
        spell3.SetActive(false);
        spell4.SetActive(false);
        winImage.SetActive(true);
        yield return new WaitForSeconds(2);
        controller.startGame();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //load the player save data and make sure it has values
        started = false;
        gameData = SaveControl.instance.LoadGame();
        if(gameData == null) {
            gameData = new PlayerSave();
            SaveControl.instance.SaveGame(gameData);
        }
        //make sure the map health is full, get the scene controller and set the game volume
        mapHealth = fullHealth;
        controller = configureSystem.GetComponent<MainMenuController>();
        music.GetComponent<AudioSource>().volume = gameData.volume;

        //check if player has played since unlocking new spells and check the instructions exist to spawn in
        if(!gameData.playedLevel && removeConfirm) {
            //check the players level to see what instructions should be played
            if(gameData.level == 0){
                freezeConfirm.SetActive(true);
            } else if(gameData.level == 2) {
                maxConfirm.SetActive(true);
            } else if(gameData.level == 3) {
                removeConfirm.SetActive(true);
            }
            gameData.playedLevel = true;
            SaveControl.instance.SaveGame(gameData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the checks have started check if the player has won or lost
        if(started){
            if(mapHealth == 0) {
                //check if the map has run out health and call win method if so
                RoundWin();
                mapHealth = fullHealth;
                started = false;
            }
            if(GameObject.FindGameObjectsWithTag("Minion").Length == 0) {
                //check if the player has lost all minions and call fail method if so
                RoundFail();
                mapHealth = fullHealth;
                started= false;
            }
        }

        //set the value of the volume slider to the current volume of the game
        if(volumeSlider != null) {
            volumeSlider.GetComponent<Slider>().value = gameData.volume;
        }
    }
}
