using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin �st�ndeyken zemin rengi.
    public Vector3 positionOffset; // Kulelerin zeminden belirledi�imiz y�kseklikte olu�mas� i�in.
    public Color notEnoughMoneyColor;

    private Renderer rend; // �izilmi� nesneleri tarad�ktan sonra tutmak i�in de�i�ken.
    private Color startColor; // Zeminin ilk rengi.

    [Header("Optional")]
    public GameObject turret; // Daha �nce kule olu�turulup olu�turmad��� onaylama i�lemi i�in oyun objesi de�i�keni.

    BuildManager buildManager; // Buildmanager� �a��r.

    void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() // T�klama i�lemi yap�lan zeminde
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if (turret != null) // Kule varsa
        {
            Debug.Log("Burada zaten bir kule var."); // Uyar� ver 
            return;
        }
        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {
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

    //private void Start()
    //{
    //    rend = GetComponent<Renderer>(); // �izilmi� t�m nodelar� tara.
    //    startColor = rend.material.color; // Ba�lang��taki renklerini tut.
    // }
    //private void OnMouseEnter()
    //{
    //    rend.material.color = hoverColor; // Mouse zemin �st�ndeyse rengi de�i�tir.
    //}

    //private void OnMouseExit()
    //{
    //    rend.material.color = startColor; // Mouse zemin �st�nden ��kt�ysa rengi eski haline getir.
    //}
}
