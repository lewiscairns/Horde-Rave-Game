using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewWave : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public int[] typeID;
    public int[] pointList;
    public int pointLimit;
    public int currentPoints;
    public TextMeshProUGUI title;

    public GameObject imp;
    public GameObject wizard;
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        //set current points to 0 and access the points text at the top
        currentPoints = 0;
        title = GameObject.Find("PointsText").GetComponent<TextMeshProUGUI>();
        int level = game.GetComponent<GameManager>().CheckLevel();
        if(level >= 1) {
            imp.SetActive(true);
        } else {
            imp.SetActive(false);
        }
        if(level >= 2) {
            wizard.SetActive(true);
        } else {
            wizard.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //keep the points text up-to-date with what the user has changed
        title.GetComponent<TextMeshProUGUI>().text = "Max: " + pointLimit + " Used: " + currentPoints;
    }
}
