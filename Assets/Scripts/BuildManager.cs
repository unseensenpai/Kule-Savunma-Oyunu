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
//    turretToBuild = standartTurretPrefab; // Yap�lacak kulenin standart kule tipinin prefab� olmas�n� sa�la.
//}

