using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Debug.Log("Oyun ba�lang�� paras� :" + Money);
        Lives = startLives;

        Rounds = 0;
    }
}
