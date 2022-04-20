using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor; // Mouse zemin üstündeyken zemin rengi.
    public Vector3 positionOffset; // Kulelerin zeminden belirlediðimiz yükseklikte oluþmasý için.
    public Color notEnoughMoneyColor;

    private Renderer rend; // Çizilmiþ nesneleri taradýktan sonra tutmak için deðiþken.
    private Color startColor; // Zeminin ilk rengi.

    [Header("Optional")]
    public GameObject turret; // Daha önce kule oluþturulup oluþturmadýðý onaylama iþlemi için oyun objesi deðiþkeni.

    BuildManager buildManager; // Buildmanagerý çaðýr.

    void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() // Týklama iþlemi yapýlan zeminde
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if (turret != null) // Kule varsa
        {
            Debug.Log("Burada zaten bir kule var."); // Uyarý ver 
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
