using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin �st�ndeyken zemin rengi.
    private Renderer rend; // �izilmi� nesneleri tarad�ktan sonra tutmak i�in de�i�ken.
    private Color startColor; // Zeminin ilk rengi.
    private GameObject turret; // Daha �nce kule olu�turulup olu�turmad��� onaylama i�lemi i�in oyun objesi de�i�keni.
    public Vector3 positionOffset; // Kulelerin zeminden belirledi�imiz y�kseklikte olu�mas� i�in.
    BuildManager buildManager; // Buildmanager� �a��r.

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    void OnMouseDown() // T�klama i�lemi yap�lan zeminde
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        GameObject turretToBuild = buildManager.GetTurretToBuild(); // Kule yap�c�dan bu b�lgeye hangi kulenin yap�laca�� bilgisini al ve oyun objesi olarak tut.
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation); // Bu objeyi bu zeminin oldu�u yerde olu�tur.
    }

    void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        if (turret != null) // Kule varsa
        {
            Debug.Log("Burada zaten bir kule var."); // Uyar� ver 
            return;
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
