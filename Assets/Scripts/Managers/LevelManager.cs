using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : CustomBehaviour
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public void StartLevel(LevelData _levelData)
    {
        Debug.Log(_levelData.title +" : " +_levelData.LevelNumber);
    }

}

