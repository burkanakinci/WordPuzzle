#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WordCreator))]
public class WordCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WordCreator m_WordCreator = (WordCreator)target;

        if (GUILayout.Button("CreateLevel"))
        {
            m_WordCreator.CreateFolders();
        }
    }

}

#endif


