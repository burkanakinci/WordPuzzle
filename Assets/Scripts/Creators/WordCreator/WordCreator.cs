#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

using System.Linq;

public class WordCreator : MonoBehaviour
{
    [SerializeField] private string m_Alphabet;
    [SerializeField] private TextAsset m_AllWordDefaultTextAsset;
    private string m_SavePath;

    private List<string> m_Lines = new List<string>();
    public void CreateFolders()
    {
        m_Lines = m_AllWordDefaultTextAsset.text.ToUpper().Split("\n").ToList();

        for (int _alphabetCount = 0; _alphabetCount < m_Alphabet.Length; _alphabetCount++)
        {
            AssetDatabase.CreateFolder("Assets/Resources/Texts", m_Alphabet[_alphabetCount].ToString());
            for (int _wordLength = 2; _wordLength < 12; _wordLength++)
            {
                m_SavePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Texts/" + m_Alphabet[_alphabetCount] + "/" + m_Alphabet[_alphabetCount] + "_" + _wordLength + ".txt");

                List<string> _inLine = m_Lines.FindAll(_word => _word.Trim().Length == _wordLength);
                if (!File.Exists(m_SavePath))
                {
                    File.WriteAllText(m_SavePath, "");
                }
                _inLine.ForEach(_line =>
                {
                    if ((_line.Length > 0))
                    {
                        if (_line[0] == m_Alphabet[_alphabetCount])
                        {
                            File.AppendAllText(m_SavePath, _line + "\n");
                        }
                    }
                });
            }
        }
    }
}

#endif