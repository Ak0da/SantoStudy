using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameRules : MonoBehaviour
{
    [SerializeField]
    private ListButton[] ButtonList;

    [SerializeField]
    private TextMeshProUGUI QuestionText;

    [SerializeField]
    private Image RedGlare;

    [SerializeField]
    private GameObject SpeechBubble;
    private TextMeshProUGUI ScoreText;

    private int NumberFalseAnswers = 0;
    private bool[] AnsweredOptions;

    public GameObject CutBambooPrefab;

    private int CorrectButtonIndex;
    private int Score = 0;

    private int LevelLimit = 1500;

    private List<WordEntry> WordEntries;
    private struct WordEntry{
        public string Character {get; private set;}
        public string Romaji {get; private set;}
        public string Meaning {get; private set;}

        public WordEntry(string character, string romaji, string meaning) : this()
        {
            Character = character;
            Romaji = romaji;
            Meaning = meaning;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadWords();
        SpeechBubble.SetActive(false);
        ScoreText = SpeechBubble.GetComponentInChildren<TextMeshProUGUI>();

        for (int i = 0; i < ButtonList.Length; i++)
        {
            ButtonList[i].listIndex = i;
        }

        BeginRound();
    }

    void ReadWords()
    {
        //string path = Application.dataPath + "/WordsList.csv";
        //StreamReader sr = new StreamReader(path);
        //sr.ReadLine();
        //string content = sr.ReadToEnd();
        string path = "WordsList";
        TextAsset textFile = Resources.Load<TextAsset>(path);
        string content = textFile.text;
        string[] lines = content.Split('\n');
        WordEntries = new List<WordEntry>(LevelLimit);

        for (int i = 1; i < lines.Length && i < LevelLimit; i++)
        {
            string[] entries = lines[i].Replace("\"","").Split(';');
            WordEntries.Add(new WordEntry(entries[3], entries[4], entries[5]));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(int index)
    {
        Debug.Log("Clicked " + index);

        if(CorrectButtonIndex == index)
        {
            Debug.Log("Correct answer");
            Score += 1;
            ScoreText.text = ""+Score;
            SpeechBubble.SetActive(true);

            CutBamboo(index);
            BeginRound();
        }
        else if (!AnsweredOptions[index])
        {
            NumberFalseAnswers += 1;
            AnsweredOptions[index] = true;
        }

        RedGlare.color = new Color(1,1,1, ((float)NumberFalseAnswers)/(AnsweredOptions.Length-1) );
    }

    private void CutBamboo(int index)
    {
        //Vector3 PosBamboo = Camera.main.ScreenToWorldPoint(ButtonList[index].transform.position);
        //Instantiate(CutBambooPrefab, PosBamboo, Quaternion.identity);
        Instantiate(CutBambooPrefab, ButtonList[index].transform.position, Quaternion.identity);
    }

    void BeginRound()
    {
        AnsweredOptions = new bool[ButtonList.Length];
        NumberFalseAnswers = 0;
        CorrectButtonIndex = Random.Range(0,ButtonList.Length);
        ShuffleNFirst(WordEntries,ButtonList.Length);

        QuestionText.text = WordEntries[CorrectButtonIndex].Character + "\n" + WordEntries[CorrectButtonIndex].Romaji;
        
        for (int i = 0; i < ButtonList.Length; i++)
        {
            ButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = WordEntries[i].Meaning;
        }
    }

    void ShuffleNFirst<T>(List<T> values, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int indexSwitch = Random.Range(i,values.Count);
            (values[indexSwitch], values[i]) = (values[i], values[indexSwitch]);
        }
    }

    void EndRound()
    {

    }
}
