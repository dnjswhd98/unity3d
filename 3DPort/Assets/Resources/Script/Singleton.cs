using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private static Singleton Instance = null;
    public static Singleton GetInstance()
    {
        if (Instance == null)
            Instance = new Singleton();
        return Instance;
    }

    private List<GameObject> EnableList = new List<GameObject>();
    public List<GameObject> GetEnableList
    {
        get
        {
            return EnableList;
        }
    }
    private Stack<GameObject> DisableList = new Stack<GameObject>();
    public Stack<GameObject> GetDisableList
    {
        get
        {
            return DisableList;
        }
    }

    public int DifficultyLv;
}
