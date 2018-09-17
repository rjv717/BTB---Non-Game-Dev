using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsiderSript : MonoBehaviour {

    public LevelData levelData;

    // Use this for initialization
    void Start () {
        LevelManager.StartLevel(levelData);
        LevelManager.OnLevelEnd += AnnounceGameOver;
	}

    void AnnounceGameOver(bool completed)
    {
        if (completed)
        {
            Debug.Log("Level Completed!");
        } else
        {
            Debug.Log("Level Over.");
        }
    }
}
