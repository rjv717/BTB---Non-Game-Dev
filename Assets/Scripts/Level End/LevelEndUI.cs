using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour {

    public Text message;
    public Button restartButton;
    public Text rButtonLabel;

    void Start()
    {
        if (PlaySessionManager.ins.gameComplete)
        {
            message.text = "You Win!";
            restartButton.gameObject.SetActive(false);
            PlaySessionManager.ins.gameComplete = false;
        } else {
            bool success = PlaySessionManager.ins.mostRecentEndSuccess;
            message.text = success ? "Level Completed!" : "Level Failed!";
            rButtonLabel.text = success ? "Next Level" : "Try Again";
        }
    }

    public void RestartButtonPressed()
    {
        SceneLoader.LoadScene(SceneName.Level);
    }

    public void MenuButtonPressed()
    {
        SceneLoader.LoadScene(SceneName.MenuMain);
    }
}
