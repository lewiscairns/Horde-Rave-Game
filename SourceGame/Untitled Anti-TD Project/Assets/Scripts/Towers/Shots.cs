using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public List<GameObject> minionsInRange;
    private float lastShotTime;
    public GameObject shooter;
    public bool checkSplash;
    public int count = 0;
    private TowerStats towerStats;

    void OnMinionDestroy(GameObject minion)
    {
        //remove dead minion from list of in range minions
        minionsInRange.Remove(minion);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        //check object in collider is a minion
        if (other.gameObject.tag.Equals("Minion"))
        {
            //add 1 to the minion count, add the minion to the in range list and listen for its dead delegate
            count = count + 1;
            minionsInRange.Add(other.gameObject);
            MinionDeadDelegate del = other.gameObject.GetComponent<MinionDeadDelegate>();
            del.minionDelegate += OnMinionDestroy;
        }
    }
 
    void OnTriggerExit2D (Collider2D other)
    {
        //check object in collider is a minion
        if (other.gameObject.tag.Equals("Minion"))
        {
            //remove the minion from the count and in range list, and stop listening to its dead delegate
            count = count - 1;
            minionsInRange.Remove(other.gameObject);
            MinionDeadDelegate del = other.gameObject.GetComponent<MinionDeadDelegate>();
            del.minionDelegate -= OnMinionDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        //get the towers shot, starting point and target location
        GameObject shotPrefab = towerStats.shot;
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;

        //make sure the z-axis locations don't cause errors
        startPosition.z = shotPrefab.transform.position.z;
        targetPosition.z = shotPrefab.transform.position.z;

        //spawn the shot object and give it its current location and target location
        GameObject newShot = (GameObject)Instantiate (shotPrefab);
        newShot.transform.position = startPosition;
        ShotStats shotComp = newShot.GetComponent<ShotStats>();
        shotComp.target = target.gameObject;
        shotComp.startPosition = startPosition;
        shotComp.targetPosition = targetPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        //start variables and get the stats of the attached to tower
        minionsInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerStats = shooter.GetComponent<TowerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //reset variables each loop
        GameObject target = null;
        float minimalMinionDistance = float.MaxValue;

        //check if the shot is dealing a splash or not
        if(checkSplash == false) {
            //if not a splash then target the minion closest to the end of the map on the list
            foreach (GameObject minion in minionsInRange)
            {
                float distanceToGoal = minion.GetComponent<MoveMinion>().DistanceToGoal();
                if(distanceToGoal < minimalMinionDistance) {
                    target = minion;
                    minimalMinionDistance = distanceToGoal;
                }
            }
        } else if(count > 0) {
            //ensure there is at least one minion then start sorting the list
            bool checking = true;
            bool change = false;
            while(checking) 
            {
                //for each object in the list, compare distance of each and sort them from closest to furtherest
                for(int i = 0; i < (count - 1); i++)
                {
                    //get the current and next minions on the list location's
                    float distanceToGoal = minionsInRange[i].GetComponent<MoveMinion>().DistanceToGoal();
                    float newDistance = minionsInRange[i+1].GetComponent<MoveMinion>().DistanceToGoal();
                    //change their positions on the list if the one is closer than the other
                    if(distanceToGoal > newDistance) {
                        GameObject holder = minionsInRange[i];
                        minionsInRange[i] = minionsInRange[i+1];
                        minionsInRange[i+1] = holder;
                        change = true;
                    }
                }
                //if a whole loop didn't produce a change, exit the while loop
                if(change == false) {
                    checking = false;
                }
            }
            //target the middle minion by finding the half way of the sortest list
            int pos = count / 2;
            target = minionsInRange[pos];
        }
        if (target != null)
        {
            //if there is a target and enough time has elapsed since last shot, shoot the shot
            if(Time.time - lastShotTime > towerStats.fireRate) {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
        }
    }
}
