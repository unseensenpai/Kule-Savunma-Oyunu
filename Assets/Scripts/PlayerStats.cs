using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney;

    private void Start()
    {
        Money = startMoney;
        Debug.Log("Oyun baþlangýç parasý :" + Money);
    }
}
