using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;    
    private float health;
    public float startHealth = 100;
    public int worth = 50;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;


    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !isDead)
        {
            Die();
            health = 100;
        }
    }

    void Die()
    {
        isDead = true;
        PlayerStats.Money += worth;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }    
    public void Slow(float rate)
    {

        speed = startSpeed * (1f - rate);
    }
}
