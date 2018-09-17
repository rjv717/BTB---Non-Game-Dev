using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public delegate void GoalReachedHandler(bool levelCompleted);
    public static event GoalReachedHandler OnGoalReached;

    private void OnTriggerEnter(Collider other)
    {
        // Announce that Goal has been reached.
        if (OnGoalReached != null)
        {
            OnGoalReached(true);
        }
    }
}
