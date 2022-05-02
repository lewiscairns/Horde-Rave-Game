using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpell : MonoBehaviour
{
    public SpellControl spellControl;
    public GameObject thisTower;
    public GameObject normalTower;
    public GameObject frozenTower;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public TowerStats towerStats;

    IEnumerator FreezeTower()
    {
        //keep track of the firerate then set it so the tower won't fire while froze
        float prevFireRate = towerStats.fireRate;
        towerStats.fireRate = 100f;
        normalTower.SetActive(false);
        frozenTower.SetActive(true);
        spellControl.frozenOnCooldown = true;

        yield return new WaitForSeconds(5);
        //set the tower back to normal settings
        frozenTower.SetActive(false);
        normalTower.SetActive(true);
        towerStats.fireRate = prevFireRate;

        yield return new WaitForSeconds(2);
        //take spell off cooldown
        spellControl.frozenOnCooldown = false;
    }

    void OnMouseOver()
    {
        //check the spell type, reset the cursor and spell type, then run the corresponding spell method
        if (Input.GetMouseButtonDown(0)) {
            if (spellControl.spellType == 1) {
                Cursor.SetCursor(null, hotSpot, curMode);
                spellControl.spellType = 0;
                StartCoroutine(FreezeTower());
            } else if(spellControl.spellType == 4) {
                Cursor.SetCursor(null, hotSpot, curMode);
                spellControl.spellType = 0;
                spellControl.removeOnCooldown = true;
                thisTower.SetActive(false);
            } 
        }
    }

    void ComeBack()
    {
        thisTower.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the game manager and stats of the tower currently attached to
        GameObject spellManager = GameObject.Find("SpellManager");
        spellControl = spellManager.GetComponent<SpellControl>();
        towerStats = this.gameObject.GetComponentInChildren<TowerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
