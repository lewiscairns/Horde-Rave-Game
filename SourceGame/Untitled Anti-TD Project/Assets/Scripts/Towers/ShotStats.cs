using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotStats : MonoBehaviour
{
    public int speed;
    public float damage;
    public float splashDamage;
    public bool slow;
    public bool splash;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public List<GameObject> minionsInRange;
    private float distance;
    private float shotTime;

    void OnTriggerEnter2D (Collider2D other)
    {
        //add each minion that enters the collider to the list of in range minions
        if (other.gameObject.tag.Equals("Minion"))
        {
            minionsInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        //remove each minion that leaves the collider from the list of minions in range
        if (other.gameObject.tag.Equals("Minion"))
        {
            minionsInRange.Remove(other.gameObject);
        }
    }
 
    void Start() 
    {
        //sete current time, distance to the target
        shotTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
    }
 
    // Update is called once per frame
    void Update() 
    {
        //set the time since the shot and use that to decide where the shot should be in its progress
        float timeInterval = Time.time - shotTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        //check if the targets position has been reached and the target exists
        if (gameObject.transform.position.Equals(targetPosition)) {
            if (target != null) {
                //get the targets healthbar and apply damage to it
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                if (healthBar.currentHealth <= 0) {
                    //if the targets health is 0 or less destroy the target
                    Destroy(target);
                }
                
                if(slow) {
                    //if the shot slows, apply call the minions slow method
                    foreach (GameObject minion in minionsInRange)
                    {
                        minion.GetComponent<MoveMinion>().IsSlow();
                    }
                }

                if(splash) {
                    //if the shot applies splash damage, damage all minions in range when target hit
                    foreach (GameObject minion in minionsInRange)
                    {
                        healthBar = minion.GetComponentInChildren<HealthBar>();
                        healthBar.currentHealth -= Mathf.Max(splashDamage, 0);
                    }
                }
            }
        //remove game object from map
        Destroy(gameObject);
        }
    }
}