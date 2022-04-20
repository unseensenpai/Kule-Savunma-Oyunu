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
        Vector3 direction = target.position - transform.position; // Waypoint ile d��man aras�ndaki mesafeyi vekt�rel olarak tutuyoruz.
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World); // D��man� normalize(birer birim ilerleyecek) �ekilde hareket ettiriyoruz.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) // D��man waypointe 0.2 birimden daha yak�nsa yeni waypointi bulmas�n� sa�l�yoruz.
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed; // Rangeden ��kan d��manlar�n h�z�n� s�f�rla.
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
        Destroy(gameObject); // �nceki waypointi yoket.
    }
}
