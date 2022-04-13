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
        Vector3 direction = target.position - transform.position; // Waypoint ile düþman arasýndaki mesafeyi vektörel olarak tutuyoruz.
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); // Düþmaný normalize(birer birim ilerleyecek) þekilde hareket ettiriyoruz.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) // Düþman waypointe 0.2 birimden daha yakýnsa yeni waypointi bulmasýný saðlýyoruz.
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1) // Son waypointe gelene kadar
        {
            Destroy(gameObject); // Önceki waypointi yoket.
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; // Yeni waypointi hedef belirle.
    }
}
