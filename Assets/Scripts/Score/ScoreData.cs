using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    private static ScoreData instance = null;
    private static readonly object padlock = new object();

    ScoreData()
    {
    }

    public static ScoreData Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ScoreData();
                }
                return instance;
            }
        }
    }

    public int levelScore;
}
