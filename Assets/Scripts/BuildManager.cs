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
            Debug.Log("Kule inþa etmek için yeterli para bulunmuyor!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Kule inþa edildi! Kalan para:  " + PlayerStats.Money);
    }


    private void Awake()
    {
        if (instance != null) // Kule oluþturma penceresi önceden açýldýysa 
        {
            Debug.LogError("Birden fazla kule yapýcý çaðýramazsýn.");
            return;
        }
        instance = this;     // Her node için oluþturulan kule üretme nesnesinin bu sýnýftan üretildiðini belirtme.   
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

}



//public void Start()
//{
//    turretToBuild = standartTurretPrefab; // Yapýlacak kulenin standart kule tipinin prefabý olmasýný saðla.
//}

