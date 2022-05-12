using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin üstündeyken zemin rengi.
    public Vector3 positionOffset; // Kulelerin zeminden belirlediðimiz yükseklikte oluþmasý için.
    public Color notEnoughMoneyColor;

    private Renderer rend; // Çizilmiþ nesneleri taradýktan sonra tutmak için deðiþken.
    private Color startColor; // Zeminin ilk rengi.

    [HideInInspector]
    public GameObject turret; // Daha önce kule oluþturulup oluþturmadýðý onaylama iþlemi için oyun objesi deðiþkeni.
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager; // Buildmanagerý çaðýr.

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>(); // Çizilmiþ tüm nodelarý tara.
        startColor = rend.material.color; // Baþlangýçtaki renklerini tut.
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() // Týklama iþlemi yapýlan zeminde
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
            Debug.Log("Kule inþa etmek için yeterli para bulunmuyor!");
            return;
        }
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        Debug.Log("Kule inþa edildi!");
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Yükseltmek için yeterli para bulunmuyor!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Eski kuleyi sil
        Destroy(turret);

        // Yeni yükseltilmiþ kuleyi ekle
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
        Debug.Log("Kule yükseltildi!");
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
        rend.material.color = hoverColor; // Mouse zemin üstündeyse rengi deðiþtir.
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
        rend.material.color = startColor; // Mouse zemin üstünden çýktýysa rengi eski haline getir.
    }


    //private void Start()
    //{
    //    rend = GetComponent<Renderer>(); // Çizilmiþ tüm nodelarý tara.
    //    startColor = rend.material.color; // Baþlangýçtaki renklerini tut.
    // }
    //private void OnMouseEnter()
    //{
    //    rend.material.color = hoverColor; // Mouse zemin üstündeyse rengi deðiþtir.
    //}

    //GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
    //Destroy(effect, 5f);

}
