using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour {

    public LevelSelectButton buttonPreFab;
    public Transform buttonContainer;

    private void Start()
    {
        for (int i = 0; i < PlaySessionManager.ins.catalog.Length; i++)
        {
            LevelSelectButton newButton = Instantiate(buttonPreFab, buttonContainer);
            newButton.Initialize(this, i); 
        }
    }

    public void BackButtonPressed()
    {
        SceneLoader.LoadScene(SceneName.MenuMain);
    }

    public void LevelButtonPressed(int levelIndex)
    {
        // Set the PlaySessionManager.
        PlaySessionManager.ins.CurrentLevel = levelIndex;
        // Change Level
        SceneLoader.LoadScene(SceneName.Level);
    }
}
