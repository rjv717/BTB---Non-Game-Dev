using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    LevelSelectUI ui;
    int levelIndex;
    bool isEnabled = true;

    public void Initialize(LevelSelectUI ui, int levelIndex)
    {
        this.ui = ui;
        this.levelIndex = levelIndex;
        GetComponentInChildren<Text>().text = "Level " + levelIndex;
        if (levelIndex > PlaySessionManager.ins.FurthestLevel)
        {
            // gameObject.SetActive(false);
            isEnabled = false;
            GetComponentInChildren<Text>().enabled = false;
        }
    }
    
    public void OnPress()
    {
        if (isEnabled)
        {
            ui.LevelButtonPressed(levelIndex);
        }
    }
}
