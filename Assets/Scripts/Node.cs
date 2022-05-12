using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin �st�ndeyken zemin rengi.
    public Vector3 positionOffset; // Kulelerin zeminden belirledi�imiz y�kseklikte olu�mas� i�in.
    public Color notEnoughMoneyColor;

    private Renderer rend; // �izilmi� nesneleri tarad�ktan sonra tutmak i�in de�i�ken.
    private Color startColor; // Zeminin ilk rengi.

    [HideInInspector]
    public GameObject turret; // Daha �nce kule olu�turulup olu�turmad��� onaylama i�lemi i�in oyun objesi de�i�keni.
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager; // Buildmanager� �a��r.

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>(); // �izilmi� t�m nodelar� tara.
        startColor = rend.material.color; // Ba�lang��taki renklerini tut.
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() // T�klama i�lemi yap�lan zeminde
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null) // Kule varsa
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Kule in�a etmek i�in yeterli para bulunmuyor!");
            return;
        }
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        Debug.Log("Kule in�a edildi!");
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Y�kseltmek i�in yeterli para bulunmuyor!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Eski kuleyi sil
        Destroy(turret);

        // Yeni y�kseltilmi� kuleyi ekle
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
        Debug.Log("Kule y�kseltildi!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        //Spawn effect eklenecek.
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor; // Mouse zemin �st�ndeyse rengi de�i�tir.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor; // Mouse zemin �st�nden ��kt�ysa rengi eski haline getir.
    }


    //private void Start()
    //{
    //    rend = GetComponent<Renderer>(); // �izilmi� t�m nodelar� tara.
    //    startColor = rend.material.color; // Ba�lang��taki renklerini tut.
    // }
    //private void OnMouseEnter()
    //{
    //    rend.material.color = hoverColor; // Mouse zemin �st�ndeyse rengi de�i�tir.
    //}

    //GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
    //Destroy(effect, 5f);

}
