using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f; // Merminin h�z�n� sabitle.
    public GameObject impactEffect;

    public void Seek(Transform _target) // Merminin yeni hedef aramas� i�in method.
    {
        target = _target; // Yeni d��man�, merminin gidece�i hedef yap.
    }

    private void Update()
    {
        if(target == null) // E�er hedef d��man kalmad�ysa
        {
            Destroy(gameObject); // Mermiyi yoket.
            return;
        }

        Vector3 direction = target.position - transform.position; // D��man ile mermi aras�ndaki mesafeyi vekt�rel olarak tut.

        float distanceThisFrame = speed * Time.deltaTime; // Merminin zaman i�erisinde sabit h�zla her framede hareket edebilmesi i�in de�i�kene ata.

        if(direction.magnitude <= distanceThisFrame) // E�er merminin h�z�, vekt�r olarak tutulan d��man�n uzakl���ndan daha h�zl�ysa
        {
            HitTarget(); // D��mana vurma methodunu �al��t�r.
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World); // E�er de�ilse mermiyi hareket ettirmeye devam et.
    }

    void HitTarget() // D��mana vurma methodu
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); 
        // Merminin patlama efektini, merminin oldu�u pozisyonda bir oyun objesine �evirip �al��t�r.

        Destroy(effectInstance, 2f); // Bu efekti 2 saniye sonra kald�r.
        //Debug.Log("Bir �eylere vurduk.");
        Destroy(target.gameObject); // D��man� yoket.
        Destroy(gameObject); // Mermiyi yoket.
    }
}
