using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSlots : MonoBehaviour
{
    private NewWave wave;
    public int slotNum;
    public int slotPoints;
    
    //when remove button is pressed, remove the minion image and points spent
    public void DropMinion() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
            wave.currentPoints = wave.currentPoints - slotPoints;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //find the config object to get its NewWave script
        GameObject newWave = GameObject.Find("ConfigureSystem");
        wave = newWave.GetComponent<NewWave>();
        slotPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if the minion is dropped from the slot, set all wave variables to default
        if(transform.childCount <= 0){
            wave.isFull[slotNum] = false;
            wave.typeID[slotNum] = 0;
            wave.currentPoints = wave.currentPoints - wave.pointList[slotNum];
            wave.pointList[slotNum] = 0;
        }
    }
}
