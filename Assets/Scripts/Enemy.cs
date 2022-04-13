using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0]; // Waypointlerden ilkini hedef olarak belirle.
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position; // Waypoint ile d��man aras�ndaki mesafeyi vekt�rel olarak tutuyoruz.
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); // D��man� normalize(birer birim ilerleyecek) �ekilde hareket ettiriyoruz.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) // D��man waypointe 0.2 birimden daha yak�nsa yeni waypointi bulmas�n� sa�l�yoruz.
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1) // Son waypointe gelene kadar
        {
            Destroy(gameObject); // �nceki waypointi yoket.
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; // Yeni waypointi hedef belirle.
    }
}
