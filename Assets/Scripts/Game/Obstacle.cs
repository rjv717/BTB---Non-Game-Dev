using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public delegate void ObstacleImpactHandler(bool levelCompleted);
    public static event ObstacleImpactHandler OnCollision;

    private void OnCollisionEnter (Collision obj)
    {
        // Announce that Goal has been reached.
        if (OnCollision != null)
        {
            OnCollision(false);
        }
    }
}
