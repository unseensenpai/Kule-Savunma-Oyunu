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
        Debug.Log("Standard Turret al�nd�.");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab); // Standart turreti se�.
    }
    public void PurchaseSecondTurret()
    {
        Debug.Log("Misil Turret al�nd�.");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab); // 2. turreti se�.
    }
    public void PurchaseThirdTurret()
    {
        Debug.Log("Lazer Turret al�nd�.");
        buildManager.SetTurretToBuild(buildManager.thirdTurretPrefab); // 3. turreti se�
    }
}
