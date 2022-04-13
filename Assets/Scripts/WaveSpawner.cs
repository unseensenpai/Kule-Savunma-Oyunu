using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; // D��man objesinin nesnesi
    public float timeBetweenWaves = 5f; // dalgalar aras�ndaki zaman
    public float countdown = 2f; // ilk dalgay� olu�turma s�resi
    private int waveIndex = 0; // kac tane dusman objesinin spawn olaca��n� tutan saya�
    public Transform spawnPoint; // Ba�lang�� noktas�n�n nesnesi
    public Text waveCountdownText; // Ekrana yazd�rabilmek i�in text


    private void Update() 
    {
        if(countdown <= 0f) // E�er do�ma s�resi 0�n alt�na d��t�yse
        {
            StartCoroutine(SpawnWave()); // Spawn wave komutunu �al��t�r ama asenkron olarak.
            countdown = timeBetweenWaves; // S�radaki dalga i�in countdownu 0dan 5 saniyeye ��kar�yoruz.
        }
        countdown -= Time.deltaTime; // Saya� zaman i�inde azalmal�.
        waveCountdownText.text = Mathf.Round(countdown).ToString(); // Sayac� ekrana yazd�rmak i�in tutuyoruz.
    }

    IEnumerator SpawnWave()
    {
        // IEnumerator fonksiyonu kodu belli bir sure aral���nda �al��t�rmak i�in kullan�l�yor
        waveIndex++;
        Debug.Log("Wave Incoming!");
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // IEnumeratordaki methodun 0.5 saniyede bir kez return etmesini sa�l�yoruz.
        }        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Prefab� spawn noktas�nda olu�turuyoruz.
    }

}
