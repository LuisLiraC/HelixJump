using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathZoneCount = 1;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject
{
    public Color stageBackgrounColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;

    public List<Level> levels = new List<Level>();
}