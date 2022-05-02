using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfigureMinion : MonoBehaviour
{
    public NewWave wave;
    public int minionID;
    public GameObject selectedMinion;
    public int minionPoints;

    // Start is called before the first frame update
    void Start()
    {
        //find the config object to get its NewWave script
        GameObject newWave = GameObject.Find("ConfigureSystem");
        wave = newWave.GetComponent<NewWave>();
    }

    public void AddMinion()
    {
        //check each slot in the wave
        for (int i = 0; i < wave.slots.Length; i++)
        {
            //if their is an avaiable slot in the wave and adding this unit wouldn't bring the points above max, add its details to the wave
            if (wave.isFull[i] == false && (wave.currentPoints + minionPoints) <= wave.pointLimit) {
                //reserve the slot, add the minion details then add its image to the players UI
                wave.isFull[i] = true;
                wave.typeID[i] = minionID;
                wave.currentPoints = wave.currentPoints + minionPoints;
                Instantiate(selectedMinion, wave.slots[i].transform, false);
                wave.pointList[i] = minionPoints;
                break;
            }
        }
    }
}
