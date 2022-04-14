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
        Debug.Log("Standard Turret se�ildi.");
        buildManager.SelectTurretToBuild(standardTurret); // Standart turreti se�.
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Misil Turret se�ildi.");
        buildManager.SelectTurretToBuild(missileLauncher); // misil turreti se�.
    }
    public void SelectLaserBeamer()
    {
        Debug.Log("Lazer Turret se�ildi.");
        buildManager.SelectTurretToBuild(laserBeamer); // 3. turreti se�
    }
}
