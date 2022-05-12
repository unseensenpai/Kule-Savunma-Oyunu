using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f; // Merminin hýzýný sabitle.
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public int damage = 50;
    Enemy enemyObject = new Enemy();


    public void Seek(Transform _target) // Merminin yeni hedef aramasý için method.
    {
        target = _target; // Yeni düþmaný, merminin gideceði hedef yap.
    }

    private void Update()
    {
        if (target == null) // Eðer hedef düþman kalmadýysa
        {
            Destroy(gameObject); // Mermiyi yoket.
            return;
        }

        Vector3 direction = target.position - transform.position; // Düþman ile mermi arasýndaki mesafeyi vektörel olarak tut.

        float distanceThisFrame = speed * Time.deltaTime; // Merminin zaman içerisinde sabit hýzla her framede hareket edebilmesi için deðiþkene ata.

        if (direction.magnitude <= distanceThisFrame) // Eðer merminin hýzý, vektör olarak tutulan düþmanýn uzaklýðýndan daha hýzlýysa
        {
            HitTarget(); // Düþmana vurma methodunu çalýþtýr.

            if (enemyObject.startHealth <= 1)
            {
            }
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World); // Eðer deðilse mermiyi hareket ettirmeye devam et.
        transform.LookAt(target);
    }

    void HitTarget() // Düþmana vurma methodu
    {

        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Merminin patlama efektini, merminin olduðu pozisyonda bir oyun objesine çevirip çalýþtýr.
        Destroy(effectInstance, 5f); // Bu efekti 2 saniye sonra kaldýr. }

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject); // Mermiyi yoket.
        //Debug.Log("Bir þeylere vurduk.");
        //Destroy(gameObject); // Mermiyi yoket.

    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
