using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    public bool CanBuild 
    { 
        get 
        { 
            return turretToBuild != null; 
        } 
    }

    public bool HasMoney
    {
        get
        {
            return PlayerStats.Money >= turretToBuild.cost;
        }
    }


    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Kule in�a etmek i�in yeterli para bulunmuyor!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Kule in�a edildi! Kalan para:  " + PlayerStats.Money);
    }


    private void Awake()
    {
        if (instance != null) // Kule olu�turma penceresi �nceden a��ld�ysa 
        {
            Debug.LogError("Birden fazla kule yap�c� �a��ramazs�n.");
            return;
        }
        instance = this;     // Her node i�in olu�turulan kule �retme nesnesinin bu s�n�ftan �retildi�ini belirtme.   
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

}



//public void Start()
//{
//    turretToBuild = standartTurretPrefab; // Yap�lacak kulenin standart kule tipinin prefab� olmas�n� sa�la.
//}

