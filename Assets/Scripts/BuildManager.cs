using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject turretToBuild;
    public GameObject standartTurretPrefab;
    public GameObject anotherTurretPrefab;

    private void Awake()
    {
        if (instance != null) // Kule oluþturma penceresi hiç açýlmadýysa.
        {
            Debug.LogError("Birden fazla kule yapýcý çaðýramazsýn.");
            return;
        }
        instance = this;     // Her node için oluþturulan kule üretme nesnesinin bu sýnýftan üretildiðini belirtme.   
    }

    
    private void Start()
    {
        turretToBuild = standartTurretPrefab; // Yapýlacak kulenin standart kule tipinin prefabý olmasýný saðla.
    }
    
    public GameObject GetTurretToBuild() // Oluþturma iþlemi için hangi prefabý kullanacaðý bilgisini çaðrýldýðý sýnýfa gönder.
    {
        return turretToBuild;
    }
    public void SetTurretToBuild(GameObject turret) // Hangi turreti üreteceðimizi seçtiðimiz method
    {
        turretToBuild = turret;
    }

}
