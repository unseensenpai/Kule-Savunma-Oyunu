using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin üstündeyken zemin rengi.
    private Renderer rend; // Çizilmiþ nesneleri taradýktan sonra tutmak için deðiþken.
    private Color startColor; // Zeminin ilk rengi.
    private GameObject turret; // Daha önce kule oluþturulup oluþturmadýðý onaylama iþlemi için oyun objesi deðiþkeni.
    public Vector3 positionOffset; // Kulelerin zeminden belirlediðimiz yükseklikte oluþmasý için.
    BuildManager buildManager; // Buildmanagerý çaðýr.

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    void OnMouseDown() // Týklama iþlemi yapýlan zeminde
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        GameObject turretToBuild = buildManager.GetTurretToBuild(); // Kule yapýcýdan bu bölgeye hangi kulenin yapýlacaðý bilgisini al ve oyun objesi olarak tut.
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation); // Bu objeyi bu zeminin olduðu yerde oluþtur.
    }

    void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        if (turret != null) // Kule varsa
        {
            Debug.Log("Burada zaten bir kule var."); // Uyarý ver 
            return;
        }
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

    //private void OnMouseExit()
    //{
    //    rend.material.color = startColor; // Mouse zemin üstünden çýktýysa rengi eski haline getir.
    //}
}
