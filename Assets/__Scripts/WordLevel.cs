﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]           // allows WordLevels to be edited in inspector
public class Wordlevel {

    public int levelNum;
    public int longWordIndex;
    public string word;

    // a Dictionary<,> of all letters in word
    public Dictionary<char, int> charDict;

    // all words that can be spelled with the letters in charDict
    public List<string> subWords;

    // Static function that counts instances of chars in a string, returning Dictionary<char,int> with said info
    public static Dictionary<char,int> MakeCharDict(string w) {
        Dictionary<char, int> dict = new Dictionary<char, int>();
        char c;
        for(int i = 0; i < w.Length; i++) {
            c = w[i];
            if(dict.ContainsKey(c)) {
                dict[c]++;
            } else {
                dict.Add(c, 1);
            }
        }
        return dict;
    }

    // This static method checks whether word can be spelled with the chars in level.charDict
    public static bool CheckWordInLevel(string str, Wordlevel level) {
        Dictionary<char, int> counts = new Dictionary<char, int>();
        for(int i = 0; i < str.Length; i++) {
            char c = str[i];
            
            // if charDict contains char c
            if(level.charDict.ContainsKey(c)) {
                // if counts doesn't already have char c as a key
                if(!counts.ContainsKey(c)) {
                    // then add a new key with a value of 1
                    counts.Add(c, 1);
                } else {
                    // otherwise, add 1 to current value
                    counts[c]++;
                }

                // if there are more instances of char c than in level.charDict[c]
                if(counts[c] > level.charDict[c]) {
                    // then return false
                    return false;
                }

            } else {
                // this code running means char c is not in level.word, so return false
                return false;
            }
        }
        // if everything got here without breaking and returning false, everything worked. yay!
        return true;
    }
}
