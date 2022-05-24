using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 1000;
    public static int prevMoney;

    public static int Lives;
    public int startLives = 10;
    public static int prevLives;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Debug.Log("Oyun baþlangýç parasý :" + Money);
        Lives = startLives;
        Rounds = 0;
    }
}
