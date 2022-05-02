using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMinion : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints;
    public GameManager gameManager;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    public HealthBar healthBar;
    public GameObject healthManager;
    public GameObject sprite;
    public Animator anim;

    public float DistanceToGoal()
    {
        //set the distance to zero use the objects position and position of the next waypoint to find the distance
        float distance = 0;
        distance += Vector2.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);
        //for each waypoint left to go past, add the distance between to the distance total
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints [i].transform.position;
            Vector3 endPosition = waypoints [i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

    public void IsSlow()
    {
        //start the MovingSlow method
        StartCoroutine(MovingSlow());
    }

    IEnumerator MovingSlow()
    {
        //for 2 seconds reduce the speed of the minion then revert back
        float prevSpeed = speed;
        speed = 3f;
        yield return new WaitForSeconds(2);
        speed = prevSpeed;
    }

    void OnMouseOver()
    {
        //detect if minion has been clicked
        if (Input.GetMouseButtonDown(0)) {
            healthBar.MouseIsUsed();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the current time and get the game manager and healthbar scripts
        lastWaypointSwitchTime = Time.time;
        GameObject manager = GameObject.Find("Manager");
        gameManager = manager.GetComponent<GameManager>();
        healthBar = healthManager.GetComponent<HealthBar>();
        anim = sprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the position of the next waypoint and use MoveTowards to send the minion towards it
        Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, endPosition, speed * Time.deltaTime);

        //set the direction for the animation system so the sprite moves the correct way
        anim.SetTrigger("spawn");     
        anim.SetFloat("moveX", (waypoints [currentWaypoint + 1].transform.position.x - gameObject.transform.position.x));
        anim.SetFloat("moveY", (waypoints [currentWaypoint + 1].transform.position.y - gameObject.transform.position.y));
        
        //when the minion is finished at current waypoint, get the next one or if finished delete object and reduce map HP
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
            }
            else
            {
                gameManager.mapHealth = gameManager.mapHealth - 1;
                Destroy(gameObject);
            }
        }
    }
}
