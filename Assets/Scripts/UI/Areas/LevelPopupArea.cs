using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopupArea : BaseArea<MainMenuPanel>
{
    [SerializeField] private LevelPopup m_LevelPopupPrefab;
    [SerializeField] private Transform m_LevelPopupParent;
    private int m_LevelJsonFilesLength;
    private LevelPopup m_TempSpawnedLevelPopup;
    public override void Initialize(MainMenuPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);

        m_LevelJsonFilesLength = Resources.LoadAll<TextAsset>(Constants.LEVELS_DIRECTORY).Length;

        string _tempLevelFilePath;
        LevelData _tempLevelData;
        TextAsset _levelTextFile;

        for (int _levelCount = 1; _levelCount <= m_LevelJsonFilesLength; _levelCount++)
        {
            m_TempSpawnedLevelPopup = Instantiate(
                (m_LevelPopupPrefab),
                (Vector3.zero),
                (Quaternion.identity),
                (m_LevelPopupParent)
            );

            _tempLevelFilePath = Constants.LEVEL_PATH + _levelCount;
            _levelTextFile = Resources.Load<TextAsset>(_tempLevelFilePath);
            _tempLevelData = JsonUtility.FromJson<LevelData>(_levelTextFile.text);

            m_TempSpawnedLevelPopup.Initialize(this);
            m_TempSpawnedLevelPopup.SetLevelPopupData((_levelCount), (_tempLevelData));
        }
    }
}
