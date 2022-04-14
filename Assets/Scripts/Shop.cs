using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret seçildi.");
        buildManager.SelectTurretToBuild(standardTurret); // Standart turreti seç.
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Misil Turret seçildi.");
        buildManager.SelectTurretToBuild(missileLauncher); // misil turreti seç.
    }
    public void SelectLaserBeamer()
    {
        Debug.Log("Lazer Turret seçildi.");
        buildManager.SelectTurretToBuild(laserBeamer); // 3. turreti seç
    }
}
