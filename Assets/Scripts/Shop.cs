using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret alındı.");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab); // Standart turreti seç.
    }
    public void PurchaseSecondTurret()
    {
        Debug.Log("Misil Turret alındı.");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab); // Standart turreti seç.
    }
    public void PurchaseThirdTurret()
    {
        Debug.Log("Lazer Turret alındı.");
    }
}
