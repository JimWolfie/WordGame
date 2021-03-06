﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordList : MonoBehaviour
{
    private static WordList S;

    [Header("Set in Inspector")]

    public TextAsset wordListText;

    public int numToParseBeforeYield = 10000;

    public int wordLengthMin = 3;

    public int wordLengthMax = 7;

    [Header("Set Dynamically")]

    public int currLine = 0;

    public int totalLines;

    public int longWordCount;

    public int wordCount;

    //Private fields

    private string[] lines;

    private List<string> longWords;

    private List<string> words;

    void Awake()
    {
        S = this;
    }

    public void Init()
    {
        lines = wordListText.text.Split('\n');

        totalLines = lines.Length;

        StartCoroutine(ParseLines());
    }

    static public void INIT()
    {
        S.Init();
    }

    //All coroutines have IEnumerator as their return type.

    public IEnumerator ParseLines()
    {
        string word;

        //Init the list to hold the longest words and all valid words

        longWords = new List<string>();

        words = new List<string>();

        for (currLine = 0; currLine < totalLines; currLine++)
        {
            word = lines[currLine];

            //If the word is as long as wordLengthMax

            if (word.Length == wordLengthMax)
            {
                longWords.Add(word); //…then store it in longWords
            }

            //If it's between wordLengthMin and wordLengthMax in length…

            if (word.Length >= wordLengthMin && word.Length <= wordLengthMax)
            {
                words.Add(word); //…then add it to the list of all valid words
            }

            //Determine whether the coroutine should yield

            if (currLine % numToParseBeforeYield == 0)
            {
                //Count the words in each list to show parsing process

                longWordCount = longWords.Count;

                wordCount = words.Count;

                // This yields execution until the next frame

                yield return null; 

                //The yield will cause the execution of this method to wait
                //here while other code executes and then continue from this
                //point into the next iteration of the for loop.
            }
        }

        longWordCount = longWords.Count;

        wordCount = words.Count;

        //Send a message to this gameObject to let it know the parse is done

        gameObject.SendMessage("WordListParseComplete");
    }

    //These methods allow other classes to access the private List<string>s
    static public List<string> GET_WORDS()
    {
        return (S.words);
    }

    static public string GET_WORD(int ndx)
    {
        return (S.words[ndx]);
    }

    static public List<string> GET_LONG_WORDS()
    {
        return (S.longWords);
    }

    static public string GET_LONG_WORD(int ndx)
    {
        return (S.longWords[ndx]);
    }

    static public int WORD_COUNT
    {
        get { return S.wordCount; }
    }

    static public int LONG_WORD_COUNT
    {
        get { return S.longWordCount; }
    }

    static public int NUM_TO_PARSE_BEFORE_YIELD
    {
        get { return S.numToParseBeforeYield; }
    }

    static public int WORD_LENGTH_MIN
    {
        get { return S.wordLengthMin; }
    }

    static public int WORD_LENGTH_MAX
    {
        get { return S.wordLengthMax; }
    }
}
