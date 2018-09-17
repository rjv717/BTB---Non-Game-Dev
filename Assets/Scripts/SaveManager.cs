using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager {

    static readonly string path = "/Saves/";
    static readonly string filename = "savegame";
    static readonly string extension = ".dat";

	public static void SaveGameData(int value)
    {
        //PlayerPrefs.SetInt("FurthestLevel", PlaySessionManager.ins.FurthestLevel);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + path + filename + extension);
        bf.Serialize(file, value);
        file.Close();
    }

    public static void SaveSetting (string label, float value)
    {
        PlayerPrefs.SetFloat(label, value);
    }

    public static int LoadGameData()
    {
        //PlaySessionManager.ins.FurthestLevel = PlayerPrefs.GetInt("FurthestLevel");
        if (File.Exists(Application.dataPath + path + filename + extension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + path + filename + extension, FileMode.Open);
            int value = (int)bf.Deserialize(file);
            file.Close();
            return value;
        }
        else
        {
            return 0;
        }
    }

    public static float LoadSetting(string label)
    {
        return PlayerPrefs.GetFloat(label);
    }

    public static void ClearSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Saves Cleared.");
    }
}
