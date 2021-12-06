using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool gameIsOver;
    // internal static object instance;

    public bool ReadGameIsOver()
    {
        return gameIsOver;
    }

    public void SetGameIsOver()
    {
        gameIsOver = true;
    }
}