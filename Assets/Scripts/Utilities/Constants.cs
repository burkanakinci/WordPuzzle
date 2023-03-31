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
public struct DatabaseTables
{
    public const string USER = "Users";
    public const string TEAM = "Teams";
    public const string MESSAGE = "Messages";
}
public struct UIAnimationStates
{
    public const string CLICKED_BADGE = "ClickedBadge";
}

public struct PooledObjectTags
{
    public const string MATCHABLE = "Matchable";
    public const string BLAST_PARTICLE = "BlastParticle";
}

#endregion

#region Enums
public enum ObjectsLayer
{
    Default = 0,
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
}

public enum HudPanelAreas
{
}

public enum ActiveParents
{

}

public enum ListOperations
{
    Adding,
    Substraction,
}

public enum PlayerStates
{
    MainState = 0,
    GamePlayState = 1,
    SuccessState = 2,
    FailedState = 3,
}
#endregion
