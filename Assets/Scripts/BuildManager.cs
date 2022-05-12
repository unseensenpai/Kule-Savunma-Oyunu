using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;
    public GameObject buildEffect;
    public GameObject sellEffect;
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
        selectedNode = null;
        nodeUI.Hide();
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}



//public void Start()
//{
//    turretToBuild = standartTurretPrefab; // Yapýlacak kulenin standart kule tipinin prefabý olmasýný saðla.
//}

