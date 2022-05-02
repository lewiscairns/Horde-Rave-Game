using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDeadDelegate : MonoBehaviour
{
    public delegate void MinionDelegate (GameObject minion);
    public MinionDelegate minionDelegate;

    void OnDestroy() {
        //make sure the delegate has a game object and then call it to have all listeners know the object is dead
        if (minionDelegate != null) {
            minionDelegate(gameObject);
        }
    }
}
