using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f; // Merminin h�z�n� sabitle.
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public int damage = 50;
    Enemy enemyObject = new Enemy();


    public void Seek(Transform _target) // Merminin yeni hedef aramas� i�in method.
    {
        target = _target; // Yeni d��man�, merminin gidece�i hedef yap.
    }

    private void Update()
    {
        if (target == null) // E�er hedef d��man kalmad�ysa
        {
            Destroy(gameObject); // Mermiyi yoket.
            return;
        }

        Vector3 direction = target.position - transform.position; // D��man ile mermi aras�ndaki mesafeyi vekt�rel olarak tut.

        float distanceThisFrame = speed * Time.deltaTime; // Merminin zaman i�erisinde sabit h�zla her framede hareket edebilmesi i�in de�i�kene ata.

        if (direction.magnitude <= distanceThisFrame) // E�er merminin h�z�, vekt�r olarak tutulan d��man�n uzakl���ndan daha h�zl�ysa
        {
            HitTarget(); // D��mana vurma methodunu �al��t�r.

            if (enemyObject.startHealth <= 1)
            {
            }
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World); // E�er de�ilse mermiyi hareket ettirmeye devam et.
        transform.LookAt(target);
    }

    void HitTarget() // D��mana vurma methodu
    {

        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Merminin patlama efektini, merminin oldu�u pozisyonda bir oyun objesine �evirip �al��t�r.
        Destroy(effectInstance, 5f); // Bu efekti 2 saniye sonra kald�r. }

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject); // Mermiyi yoket.
        //Debug.Log("Bir �eylere vurduk.");
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
