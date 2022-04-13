using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret alındı.");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab); // Standart turreti seç.
    }
    public void PurchaseSecondTurret()
    {
        Debug.Log("Misil Turret alındı.");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab); // 2. turreti seç.
    }
    public void PurchaseThirdTurret()
    {
        Debug.Log("Lazer Turret alındı.");
        buildManager.SetTurretToBuild(buildManager.thirdTurretPrefab); // 3. turreti seç
    }
}
