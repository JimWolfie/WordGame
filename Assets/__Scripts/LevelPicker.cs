using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPicker : MonoBehaviour
{
    static private System.Random rng;

    [Header("Set in Inspector")]

    public int randomSeed = 12345;

    void Awake()
    {
        rng = new System.Random(randomSeed);
    }

    static public int Next(int max = -1)
    {
        //Returns the next number from rng between 0 and max-1.
        //If -1 is passed in, max is ignored

        if(max == -1)
        {
            return rng.Next();
        }

        else
        {
            return rng.Next(max);
        }
    }
}
