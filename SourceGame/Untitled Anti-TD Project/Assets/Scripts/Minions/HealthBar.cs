using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    private float xScale;
    public SpellControl spellControl;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void MouseIsUsed()
    {
        //check the spell type, reset the cursor and spell type, then run the corresponding spell method
        if (spellControl.spellType == 3) {
            Cursor.SetCursor(null, hotSpot, curMode);
            spellControl.spellType = 0;
            StartCoroutine(MaxHealthSpell());
        } else if(spellControl.spellType == 2) {
            Cursor.SetCursor(null, hotSpot, curMode);
            spellControl.spellType = 0;
            StartCoroutine(HealSpell());
        } 
    }

    IEnumerator HealSpell()
    {
        //set current health to max health and put the heal spell on a cooldown
        currentHealth = maxHealth;
        spellControl.healOnCooldown = true;
        yield return new WaitForSeconds(10);
        spellControl.healOnCooldown = false;
    }

    IEnumerator MaxHealthSpell()
    {
        //set current health to be invincible then revert back to normal health, and put spell on cooldown
        float prevMaxHealth = maxHealth;
        maxHealth = 10000;
        currentHealth = maxHealth;
        spellControl.maxOnCooldown = true;
        yield return new WaitForSeconds(2);
        maxHealth = prevMaxHealth;
        currentHealth = maxHealth;
        yield return new WaitForSeconds(25);
        spellControl.maxOnCooldown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the healthbar scale and get the spell control manager
        xScale = 1;
        GameObject spellManager = GameObject.Find("SpellManager");
        spellControl = spellManager.GetComponent<SpellControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the sprite of the healthbar to match the % of current health left
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * xScale;
        gameObject.transform.localScale = tmpScale;
    }
}
