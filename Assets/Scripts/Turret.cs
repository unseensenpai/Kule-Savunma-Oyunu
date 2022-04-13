using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]

    public float range = 15f; // Turret menzili
    public float fireRate = 1f; // At�� h�z�
    private float fireCountdown = 0f; // S�radaki at�� i�in saya�

    [Header("Unity Fields")]

    public string enemyTag = "Enemy";
    public Transform partToRotate; // Turretin d�nmesi i�in referans
    public float turnSpeed = 10f; // D�n�� h�z�
    public GameObject bulletPrefab;
    public Transform firePoint;
    

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); // Turretin menzilini g�rebilmek i�in gizmos kulland�k.

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // D��man objesini bul.
        float shortestDistance = Mathf.Infinity; // En k�sa menzildeki d��man� bulmak i�in pozitif olarak t�m y�nlerdeki aramalar� tutmas� i�in variable.
        GameObject nearestEnemy = null; // En yak�n d��man i�in variable.

        foreach (GameObject enemy in enemies) // Her d��man i�in
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // En yak�n d��man�n konumunu vekt�rel olarak tut.
            if(distanceToEnemy < shortestDistance) // E�er d��manlar aras�nda bu d��man en yak�n d��mansa
            {
               
                shortestDistance = distanceToEnemy; // Art�k en yak�n d��man�n uzakl���n� at�yoruz.
                nearestEnemy = enemy; // Ve d��man� en yak�n d��man se�iyoruz.
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) // En yak�n d��man varsa ve range i�indeyse (gizmos rangende)
        {
            target = nearestEnemy.transform; // En yak�n d��man� hedef belirle.
        }
        else
        {
            target = null; // E�er yoksa hedefsiz olmas�n� sa�la.
        }
    }

    private void Update()
    {
        if(target == null) // E�er d��man yoksa kule sabit.
        {
            return;
        }

        // Kulenin hedefe kilitlenmesi b�l�m�
        Vector3 direction = target.position - transform.position; // D��man ile kule aras�ndaki mesafeyi vekt�rel olarak tut.
        Quaternion lookRotation = Quaternion.LookRotation(direction); // Kule objesinin bak�� a��s�n� g�ncelle.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 
        // Lerp sayesinde kulenin d�n�� animasyonu daha normalize. 
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Kule nesnesini y ekseninde d�nd�r.

        // Ate� etme b�l�m�
        if(fireCountdown <= 0) // Ate� etme s�resi dolduysa
        {
            Shoot();
            fireCountdown = 1f / fireRate; // Farkl� silahlar�n farkl� at�� h�zlar� olaca�� i�in ate� etmenin bekleme s�resini d�zenleyebilmemizi sa�layan k�s�m. 
        }

        fireCountdown -= Time.deltaTime; // Ate� etme bekleme s�resini zaman i�inde azalt.
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Bir mermi �ret at�� ba�lang�� noktas�nda ve bunun pozisyonunu tut.
        Bullet bullet = bulletGO.GetComponent<Bullet>(); // Mermi objesinin t�m methodlar�n� kullanmak i�in nesne olu�tur.

        if(bullet != null) // E�er mermi varsa
        {
            bullet.Seek(target); // Hedef d��man ara
        }
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Kulelerin yeni hedef aramas� metodunu 0. saniyede ba�lat�p her 0.5f saniyede tekrar �al��t�racak.

    }
}
