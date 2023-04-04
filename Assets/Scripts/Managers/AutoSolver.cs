using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System.Linq;
using System;

public class AutoSolver : CustomBehaviour
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public async void StartCheckWord(string _targetWord)
    {
        TempClickableTarget = null;
        await GenerateWords(_targetWord);
    }

    private char[] m_LettersArray;
    private int m_MaxLength;
    private int m_MinLength = 2;
    private async Task GenerateWords(string letters)
    {
        m_LettersArray = letters.ToCharArray();
        m_MaxLength = (m_LettersArray.Length < 11) ? (m_LettersArray.Length) : (11);
        for (int length = m_MaxLength; length >= m_MinLength; length--)
        {
            await GenerateWordsHelper(m_LettersArray, "", length);
            if ((length == m_MinLength) && (TempClickableTarget == null))
            {
                GameManager.Instance.OnSuccess();
            }
        }
    }

    public string TempClickableTarget { get; private set; }
    private string m_NewWord;
    private char[] m_NewLetters;
    private async Task GenerateWordsHelper(char[] letters, string currentWord, int length)
    {
        if (currentWord.Length == length)
        {
            string result = await CheckWord(currentWord);
            if (result != null)
            {
                TempClickableTarget = result;
                GameManager.Instance.LevelManager.WordManager.SpawnEmptyLetters();
                return;
            }
        }
        for (int i = 0; i < letters.Length; i++)
        {
            m_NewWord = currentWord + letters[i];

            m_NewLetters = new char[letters.Length - 1];
            System.Array.Copy(letters, 0, m_NewLetters, 0, i);
            System.Array.Copy(letters, i + 1, m_NewLetters, i, letters.Length - i - 1);
            if (TempClickableTarget == null)
            {
                await GenerateWordsHelper(m_NewLetters, m_NewWord, length);
            }
            else
            {
                break;
            }
        }
    }
    public TextAsset m_TargetTextAsset;
    public List<string> m_TargetLetters;
    AsyncOperationHandle<TextAsset> handle;

    private int m_LowerBond;
    private int m_UpperBound;
    private int m_Middle;
    int m_CompareResult;
    public async Task<string> CheckWord(string word, Action _onCompleteSuccess = null, Action _onCompleteFailed = null)
    {
        handle = Addressables.LoadAssetAsync<TextAsset>("Assets/Resources_moved/Texts/" + word[0] + "/" + word[0] + "_" + word.Length + ".txt");
        await handle.Task;
        m_TargetTextAsset = handle.Result;
        if (m_TargetTextAsset != null)
        {
            m_TargetLetters = m_TargetTextAsset.text.ToLower().Split("\n").ToList();
            m_UpperBound = m_TargetLetters.Count - 1;
            m_LowerBond = 0;

            m_Middle = (m_LowerBond + m_UpperBound) / 2;

            while (m_LowerBond <= m_UpperBound)
            {
                m_Middle = (m_LowerBond + m_UpperBound) / 2;
                m_CompareResult = word.ToLower().Trim().CompareTo(m_TargetLetters[m_Middle].ToLower().Trim());
                if (m_CompareResult == 0)
                {
                    Debug.Log("Var" + "   :   " + word);
                    _onCompleteSuccess?.Invoke();
                    return word;
                }
                else if (m_CompareResult > 0)
                {
                    m_LowerBond = m_Middle + 1;
                }
                else
                {
                    m_UpperBound = m_Middle - 1;
                }
            }

            _onCompleteFailed?.Invoke();
            return null;
        }
        _onCompleteFailed?.Invoke();
        return null;
    }
}
