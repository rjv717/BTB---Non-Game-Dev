using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data File/Level Catalog")]
public class LevelCatalog : ScriptableObject {

    [SerializeField]
    LevelData[] levels;

    private int lastIndex = 0;

    public int Length { get { return (levels.Length); } }

    public LevelData GetLevel(int index)
    {
        if (index < levels.Length && index >= 0)
        {
            lastIndex = index;
            return levels[index];
        } else
        {
            return null;
        }
    }

    public int GetIndexOf (LevelData data)
    {
        if (levels[lastIndex].Equals(data))
        {
            return lastIndex;
        } else
        {
            return System.Array.IndexOf(levels, data);
        }
    }
}
