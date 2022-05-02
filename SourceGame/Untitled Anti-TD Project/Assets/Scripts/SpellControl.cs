using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellControl : MonoBehaviour
{
    public Texture2D freezeCursor;
    public Texture2D healCursor;
    public Texture2D maxCursor;
    public Texture2D removeCursor;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public int spellType = 0;
    public bool frozenOnCooldown = false;
    public bool healOnCooldown = false;
    public bool maxOnCooldown = false;
    public bool removeOnCooldown = false;
    public GameObject frozenTimer;
    public GameObject healTimer;
    public GameObject maxTimer;
    public GameObject removeTimer;
    public GameObject[] objects;

    public void FreezeSpell()
    {
        //check if spell is on cooldown and if not allow the user to use by changing active type and cursor texture
        if (frozenOnCooldown == false)
        {
            Cursor.SetCursor(freezeCursor, hotSpot, curMode);
            spellType = 1;
        }
    }

    public void HealingSpell()
    {
        //check if spell is on cooldown and if not allow the user to use by changing active type and cursor texture
        if (healOnCooldown == false)
        {
            Cursor.SetCursor(healCursor, hotSpot, curMode);
            spellType = 2;
        }
    }

    public void MaxSpell()
    {
        //check if spell is on cooldown and if not allow the user to use by changing active type and cursor texture
        if (maxOnCooldown == false)
        {
            Cursor.SetCursor(maxCursor, hotSpot, curMode);
            spellType = 3;
        }
    }

    public void RemoveSpell()
    {
        //check if spell is on cooldown and if not allow the user to use by changing active type and cursor texture
        if (removeOnCooldown == false)
        {
            Cursor.SetCursor(removeCursor, hotSpot, curMode);
            spellType = 4;
        }
    }

    public void MouseReset()
    {
        //set the mouse back to the normal texture
        Cursor.SetCursor(null, hotSpot, curMode);
    }

    public void WaveReset()
    {
        //reset the spell cooldowns
        frozenOnCooldown = false;
        healOnCooldown = false;
        maxOnCooldown = false;
        removeOnCooldown =false;
        spellType = 0;
        //set all the towers on the map to active
        foreach (GameObject obj in objects) {
            obj.SetActive(true);
        }
    }

    public void FirstWave()
    {
        //find all the towers in the map
        objects = GameObject.FindGameObjectsWithTag("Tower");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if user right clicks then remove spell from active and reset cursor texture
        if(Input.GetMouseButtonDown(1)){
            Cursor.SetCursor(null, hotSpot, curMode);
            spellType = 0;
        }

        //check if each spell is on cooldown, keep timer active if so or remove if not
        if(frozenOnCooldown == true){
            frozenTimer.SetActive(true);
        } else {
            frozenTimer.SetActive(false);
        }

        if(healOnCooldown == true){
            healTimer.SetActive(true);
        } else {
            healTimer.SetActive(false);
        }

        if(maxOnCooldown == true){
            maxTimer.SetActive(true);
        } else {
            maxTimer.SetActive(false);
        }

        if(removeOnCooldown == true){
            removeTimer.SetActive(true);
        } else {
            removeTimer.SetActive(false);
        }
    }
}
