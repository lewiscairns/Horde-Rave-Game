using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public int level = 0;
    public bool playedLevel = false;
    public float volume = 0f;

    public void AddLevel(int newLevel)
    {
        //change the level to the updated level
        level = newLevel;
    }
    
    public void SetVolume(float newVol)
    {
        //change the volume of the game
        volume = newVol;
    }

    public void HasPlayed(bool answer)
    {
        //update the bool tracking if player has played since leveling up
        playedLevel = answer;
    }

    public void ResetData()
    {
        //reset all game data
        level = 0;
        playedLevel = false;
    }
}
