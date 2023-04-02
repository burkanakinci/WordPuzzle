using UnityEngine;
using System.Collections.Generic;

#region Structs
public class PlayerData
{
    public int PlayerLevel;
    public Dictionary<int, int> HighScores;
}

[System.Serializable]
public class LevelData
{
    public string title;
    public TileData[] tiles;
    public int LevelNumber;
}

[System.Serializable]
public struct TileData
{
    public int id;
    public Vector3 position;
    public string character;
    public int[] children;
}
#endregion

#region Constants
public struct Constants
{
    public const string PLAYER_DATA = "PlayerSavedData";
    public const string LEVELS_DIRECTORY = "Levels/";
    public const string LEVEL_PATH = "Levels/level_";
}

public struct PooledObjectTags
{
    public const string LETTER = "Letter";
    public const string EMPTY_LETTER = "EmptyLetter";
}

#endregion

#region Enums
public enum ObjectsLayer
{
    Default = 0,
    Letter = 6,
}

public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    OnSuccess = 2,
    OnFailed = 3
}

public enum MainMenuAreas
{
    MainArea = 0,
    LevelPopupArea = 1,
}

public enum HudPanelAreas
{
    HudArea=0,
}

public enum ActiveParents
{
    ActiveLetterParent = 0,
    ActiveEmptyLetterParent=1,
}

public enum ListOperations
{
    Adding,
    Substraction,
}

public enum PlayerStates
{
    MainState = 0,
    GameplayState = 1,
    SuccessState = 2,
    FailedState = 3,
}
#endregion
