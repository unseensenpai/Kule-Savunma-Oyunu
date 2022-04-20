using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0]; // Waypointlerden ilkini hedef olarak belirle.
    }
    private void Update()
    {
        Vector3 direction = target.position - transform.position; // Waypoint ile düþman arasýndaki mesafeyi vektörel olarak tutuyoruz.
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World); // Düþmaný normalize(birer birim ilerleyecek) þekilde hareket ettiriyoruz.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) // Düþman waypointe 0.2 birimden daha yakýnsa yeni waypointi bulmasýný saðlýyoruz.
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed; // Rangeden çýkan düþmanlarýn hýzýný sýfýrla.
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) // Son waypointe gelene kadar
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; // Yeni waypointi hedef belirle.
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject); // Önceki waypointi yoket.
    }
}
