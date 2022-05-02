using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadMinion : MonoBehaviour
{
    public NewWave wave;
    public GameObject[] waypoints;

    public GameObject dwarf;
    public GameObject golem;
    public GameObject goblin;
    public GameObject imp;
    public GameObject wizard;

    public bool dwarfSyn;
    public bool goblinSyn;
    public bool golemPass;
    public bool impPass;
    public bool goblinPass;
    public float wizardPass = 0f;

    public void GenerateDwarf() 
    {
        //spawn dwarf prefab and provide it waypoints and increase speed if wizard(s) are on the map
        GameObject newMinion = (GameObject)Instantiate(dwarf, waypoints[0].transform.position, Quaternion.identity);
        MoveMinion newMove = newMinion.GetComponent<MoveMinion>();
        newMove.waypoints = waypoints;
        newMove.speed = newMove.speed + wizardPass;

        //get minions healthbar and add health if the golem passive or dwarf synergy is active
        HealthBar minionHealth = newMinion.GetComponentInChildren<HealthBar>();
        if(golemPass) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
            golemPass = false;
        }
        if(dwarfSyn) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
        }
    }

    public void GenerateGolem() 
    {
        //spawn dwarf prefab and provide it waypoints and increase speed if wizard(s) are on the map
        GameObject newMinion = (GameObject)Instantiate(golem, waypoints[0].transform.position, Quaternion.identity);
        MoveMinion newMove = newMinion.GetComponent<MoveMinion>();
        newMove.waypoints = waypoints;
        newMove.speed = newMove.speed + wizardPass;
    }

    public void GenerateGoblin()
    {
        //spawn dwarf prefab and provide it waypoints and increase speed if wizard(s) are on the map
        GameObject newMinion = (GameObject)Instantiate(goblin, waypoints[0].transform.position, Quaternion.identity);
        MoveMinion newMove = newMinion.GetComponent<MoveMinion>();
        newMove.waypoints = waypoints;
        newMove.speed = newMove.speed + wizardPass;

        //get minions healthbar and add health if the golem passive or goblin synergy is active
        HealthBar minionHealth = newMinion.GetComponentInChildren<HealthBar>();
        if(golemPass) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
            golemPass = false;
        }
        if(goblinSyn) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
        }
    }

    public void GenerateImp()
    {
        //spawn dwarf prefab and provide it waypoints and increase speed if wizard(s) are on the map
        GameObject newMinion = (GameObject)Instantiate(imp, waypoints[0].transform.position, Quaternion.identity);
        MoveMinion newMove = newMinion.GetComponent<MoveMinion>();
        newMove.waypoints = waypoints;
        newMove.speed = newMove.speed +wizardPass;

        //get minions healthbar and add health if the golem passive or imp passive is active
        HealthBar minionHealth = newMinion.GetComponentInChildren<HealthBar>();
        if(golemPass) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
            golemPass = false;
        }
        if (impPass) {
            minionHealth.maxHealth = minionHealth.maxHealth + 75;
            minionHealth.currentHealth = minionHealth.currentHealth + 75;
        }
    }

    public void GenerateWizard()
    {
        //spawn dwarf prefab and provide it waypoints and increase speed if wizard(s) are on the map
        GameObject newMinion = (GameObject)Instantiate(wizard, waypoints[0].transform.position, Quaternion.identity);
        MoveMinion newMove = newMinion.GetComponent<MoveMinion>();
        newMove.waypoints = waypoints;
        newMove.speed = newMove.speed + wizardPass;

        //get minions healthbar and add health if the golem passive is active
        HealthBar minionHealth = newMinion.GetComponentInChildren<HealthBar>();
        if(golemPass) {
            minionHealth.maxHealth = minionHealth.maxHealth + 50;
            minionHealth.currentHealth = minionHealth.currentHealth + 50;
            golemPass = false;
        }
    }

    public void StartWave()
    {
        StartCoroutine(SleepWave());
    }

    IEnumerator SleepWave()
    {
        int dwarfCount = 0;
        int goblinCount = 0;
        //go through the whole wave
        for (int i = 0; i < wave.slots.Length; i++) {
            //check if 3 dwarfs are in a row
            if(wave.typeID[i] == 13){
                dwarfCount = dwarfCount + 1;
                if(dwarfCount >= 3) {
                    dwarfSyn = true;
                }
            } else if(wave.typeID[i] != 1) {
                //reset check if minion isn't a dwarf
                dwarfCount = 0;
            }

            //check if the first 2 in the wave are goblins
            if(wave.typeID[i] == 3 && i <= 1){
                goblinCount = goblinCount + 1;
                if(goblinCount == 2) {
                    goblinSyn = true;
                }
            } else if(wave.typeID[i] != 3) {
                //reset count if one of the first minions aren't a goblin
                goblinCount = 0;
            }

            //count the number of wizards
            if(wave.typeID[i] == 5) {
                wizardPass = wizardPass + 1f;
            }
        }
        
        //change the wizard passive number to match what the increase of speed should be
        wizardPass = wizardPass * 0.03f;

        //for the whole wave spawn in the selected minion and wait 1 second before spawning the next
        for (int i = 0; i < wave.slots.Length; i++) {
            if(wave.typeID[i] == 1){
                GenerateDwarf();
                yield return new WaitForSeconds(1);
            } else if(wave.typeID[i] == 2) {
                GenerateGolem();
                yield return new WaitForSeconds(1);
                golemPass = true;
            } else if(wave.typeID[i] == 3) {
                GenerateGoblin();
                yield return new WaitForSeconds(1);
            } else if(wave.typeID[i] == 4) {
                GenerateImp();
                yield return new WaitForSeconds(1);
            } else if(wave.typeID[i] == 5) {
                GenerateWizard();
                yield return new WaitForSeconds(1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the wave script
        GameObject newWave = GameObject.Find("ConfigureSystem");
        wave = newWave.GetComponent<NewWave>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

