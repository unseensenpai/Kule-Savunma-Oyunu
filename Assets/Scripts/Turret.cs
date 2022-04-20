using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f; // Turret menzili

    [Header("Use Bullets")]
    public float fireRate = 1f; // Atýþ hýzý
    private float fireCountdown = 0f; // Sýradaki atýþ için sayaç
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float slowRate = 0.2f;

    [Header("Unity Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate; // Turretin dönmesi için referans
    public float turnSpeed = 10f; // Dönüþ hýzý  
    public Transform firePoint;



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); // Turretin menzilini görebilmek için gizmos kullandýk.

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Düþman objesini bul.
        float shortestDistance = Mathf.Infinity; // En kýsa menzildeki düþmaný bulmak için pozitif olarak tüm yönlerdeki aramalarý tutmasý için variable.
        GameObject nearestEnemy = null; // En yakýn düþman için variable.

        foreach (GameObject enemy in enemies) // Her düþman için
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // En yakýn düþmanýn konumunu vektörel olarak tut.
            if (distanceToEnemy < shortestDistance) // Eðer düþmanlar arasýnda bu düþman en yakýn düþmansa
            {

                shortestDistance = distanceToEnemy; // Artýk en yakýn düþmanýn uzaklýðýný atýyoruz.
                nearestEnemy = enemy; // Ve düþmaný en yakýn düþman seçiyoruz.
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) // En yakýn düþman varsa ve range içindeyse (gizmos rangende)
        {
            target = nearestEnemy.transform; // En yakýn düþmaný hedef belirle.
            targetEnemy = nearestEnemy.GetComponent<Enemy>(); // Lazer için düþmaný belirleme.
        }
        else
        {
            target = null; // Eðer yoksa hedefsiz olmasýný saðla.
        }
    }

    private void Update()
    {
        if (target == null) // Eðer düþman yoksa kule sabit.
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                    impactEffect.Stop(); // Eðer lazer sistemi aktif deðilse partikülleri durdur.
                }
            }
            return;
        }
        LockOnTarget();

        if (useLaser) // Lazer kullanýyor mu?
        {
            Laser();
        }
        else
        {
            // Standart ateþ etme yeri
            if (fireCountdown <= 0) // Ateþ etme süresi dolduysa
            {
                Shoot();
                fireCountdown = 1f / fireRate; // Farklý silahlarýn farklý atýþ hýzlarý olacaðý için ateþ etmenin bekleme süresini düzenleyebilmemizi saðlayan kýsým. 
            }

            fireCountdown -= Time.deltaTime; // Ateþ etme bekleme süresini zaman içinde azalt.
        }
    }

    void Laser() // Lazer fonksiyonu çaðrýlýrsa
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); // Düþmana zamanla hasar aldýr.
        targetEnemy.Slow(slowRate); // Düþmaný yavaþlat.

        if (!lineRenderer.enabled) // Linerenderer aktif et.
        {
            lineRenderer.enabled = true;
            impactEffect.Play(); // Lazer aktifse partikülleri etkinleþtir.
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position); // Rakibe kilitlensin.
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * 0.5f; // Efektlerin düþmana temas noktasýnda çýkmasýný istiyoruz.
                                                                                   // O yüzden turretin baktýðý noktadan 0.5 birim öteliyoruz.
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void LockOnTarget()
    {
        // Kulenin hedefe kilitlenmesi bölümü
        Vector3 direction = target.position - transform.position; // Düþman ile kule arasýndaki mesafeyi vektörel olarak tut.
        Quaternion lookRotation = Quaternion.LookRotation(direction); // Kule objesinin bakýþ açýsýný güncelle.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // Lerp sayesinde kulenin dönüþ animasyonu daha normalize. 
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Kule nesnesini y ekseninde döndür.
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Bir mermi üret atýþ baþlangýç noktasýnda ve bunun pozisyonunu tut.
        Bullet bullet = bulletGO.GetComponent<Bullet>(); // Mermi objesinin tüm methodlarýný kullanmak için nesne oluþtur.

        if (bullet != null) // Eðer mermi varsa
        {
            bullet.Seek(target); // Hedef düþman ara
        }
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Kulelerin yeni hedef aramasý metodunu 0. saniyede baþlatýp her 0.5f saniyede tekrar çalýþtýracak.

    }
}
