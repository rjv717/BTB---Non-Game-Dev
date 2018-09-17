using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySessionManager : MonoBehaviour
{

    public static PlaySessionManager ins;
    public LevelCatalog catalog;
    //[SerializeField]
    int currentLevel = 0;
    //[SerializeField]
    int furthestLevel = 0;
    public bool mostRecentEndSuccess = false;
    public bool gameComplete = false;

    public int CurrentLevel
    {
        set
        {
            currentLevel = value;
        }
    }

    public int FurthestLevel
    {
        get
        {
            return furthestLevel;
        }
        set
        {
            furthestLevel = value;
        }
    }

    void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (ins != this)
        {
            Destroy(gameObject);
        }
        furthestLevel = SaveManager.LoadGameData();
        SceneManager.sceneLoaded += OnSceneLoad;
        LevelManager.OnLevelEnd += HandleLevelEnd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            SaveManager.ClearSaves();
        }
    }

    void OnSceneLoad(Scene s, LoadSceneMode lsm)
    {
        if (!IsSingleton()) return;

        if (s.name == "Level")
        {
            LevelManager.StartLevel(catalog.GetLevel(currentLevel));
            PersistentAudioPlayer.ins.ChangeMusic(1);
        } else
        {
            PersistentAudioPlayer.ins.ChangeMusic(0);
        }
    }

    void HandleLevelEnd(bool levelCompleted)
    {
        if (!IsSingleton()) return;

        mostRecentEndSuccess = levelCompleted;

        if (levelCompleted)
        {
            currentLevel++;
            if ((currentLevel) >= catalog.Length)
            {
                currentLevel = 0;
                gameComplete = true;
            } else if (currentLevel > furthestLevel)
            {
                furthestLevel = currentLevel;
                SaveManager.SaveGameData(furthestLevel);
            }
        }
        SceneManager.LoadScene("Level End");
    }

    bool IsSingleton()
    {
        if (ins == this)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
