using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data File/Level Data")]
public class LevelData : ScriptableObject
{
    [Range(10,250)]
    public int groundX, groundZ;
    public Vector3 playerPosition;
    public Vector3 goalPosition;
    public Vector3[] obstaclePositions;

    public bool useNoise = false;

    // Noise Settings.
    [Range(1, 8)]
    public int numLayers = 1;

    public float strength = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistance = 0.5f;
    public float minValue;

    public Vector3 center;

}
