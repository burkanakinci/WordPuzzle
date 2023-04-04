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
        GameManager.Instance.InputManager.SetInputCanClickable(false);
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
                GameManager.Instance.InputManager.SetInputCanClickable(true);
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

    private string[] m_TargetLetters;
    private TextAsset m_TextAsset;
    private int m_LetterIndex;
    private AssetReference m_AssetPath;
    public async Task<string> CheckWord(string word, Action _onCompleteSuccess = null, Action _onCompleteFailed = null)
    {
        m_AssetPath = new AssetReference($"Assets/Resources_moved/Texts/{word[0]}/{word[0]}_{word.Length}.txt");
        m_TextAsset = await m_AssetPath.LoadAssetAsync<TextAsset>().Task;
        m_TargetLetters = m_TextAsset.text.ToLower().Split("\r\n");
        m_LetterIndex = Array.BinarySearch(m_TargetLetters, word.ToLower());

        if (m_LetterIndex >= 0)
        {
            _onCompleteSuccess?.Invoke();
            return word;
        }
        else
        {
            _onCompleteFailed?.Invoke();
            return null;
        }
    }

}
