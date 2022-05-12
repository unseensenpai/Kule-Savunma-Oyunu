using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost; // kule satýn alma miktarý 
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public int GetSellAmount()
    {
        return cost / 2;
    }


}
