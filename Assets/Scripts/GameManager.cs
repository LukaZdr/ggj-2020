using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Tooltip("Is this threshold is surpassed, the game is lost")]
    public float maxLeakage = 1000;

    [Header("Do not set these!")]
    public float leakage;
    public int nailedHoles = 0;
    public int coalBurned = 0;
    public bool lost = false;

    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    //=============== Informationen aus der Welt sammeln
    public void Leaking(float value)
    {
        if (lost) return;

        leakage += value;

        if (leakage > maxLeakage)
        {
            GameOver();
        }
    }

    public void PlacedBolt()
    {
        if (lost) return;

        nailedHoles++;
    }

    public void BurnedCoal()
    {
        if (lost) return;

        coalBurned++;
    }

    public void GameOver()
    {
        lost = true;
        Debug.Log("You've lost!");
    }
}
