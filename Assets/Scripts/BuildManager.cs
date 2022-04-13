using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject turretToBuild;
    public GameObject standartTurretPrefab;
    public GameObject anotherTurretPrefab;
    public GameObject thirdTurretPrefab;

    private void Awake()
    {
        if (instance != null) // Kule olu�turma penceresi �nceden a��ld�ysa 
        {
            Debug.LogError("Birden fazla kule yap�c� �a��ramazs�n.");
            return;
        }
        instance = this;     // Her node i�in olu�turulan kule �retme nesnesinin bu s�n�ftan �retildi�ini belirtme.   
    }   
   
    public GameObject GetTurretToBuild() // Olu�turma i�lemi i�in hangi prefab� kullanaca�� bilgisini �a�r�ld��� s�n�fa g�nder.
    {
        return turretToBuild;
    }
    public void SetTurretToBuild(GameObject turret) // Hangi turreti �retece�imizi se�ti�imiz method
    {
        turretToBuild = turret;
    }

}



//public void Start()
//{
//    turretToBuild = standartTurretPrefab; // Yap�lacak kulenin standart kule tipinin prefab� olmas�n� sa�la.
//}

