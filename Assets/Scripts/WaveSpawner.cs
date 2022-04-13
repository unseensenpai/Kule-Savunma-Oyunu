using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; // Düþman objesinin nesnesi
    public float timeBetweenWaves = 5f; // dalgalar arasýndaki zaman
    public float countdown = 2f; // ilk dalgayý oluþturma süresi
    private int waveIndex = 0; // kac tane dusman objesinin spawn olacaðýný tutan sayaç
    public Transform spawnPoint; // Baþlangýç noktasýnýn nesnesi
    public Text waveCountdownText; // Ekrana yazdýrabilmek için text


    private void Update() 
    {
        if(countdown <= 0f) // Eðer doðma süresi 0ýn altýna düþtüyse
        {
            StartCoroutine(SpawnWave()); // Spawn wave komutunu çalýþtýr ama asenkron olarak.
            countdown = timeBetweenWaves; // Sýradaki dalga için countdownu 0dan 5 saniyeye çýkarýyoruz.
        }
        countdown -= Time.deltaTime; // Sayaç zaman içinde azalmalý.
        waveCountdownText.text = Mathf.Round(countdown).ToString(); // Sayacý ekrana yazdýrmak için tutuyoruz.
    }

    IEnumerator SpawnWave()
    {
        // IEnumerator fonksiyonu kodu belli bir sure aralýðýnda çalýþtýrmak için kullanýlýyor
        waveIndex++;
        Debug.Log("Wave Incoming!");
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // IEnumeratordaki methodun 0.5 saniyede bir kez return etmesini saðlýyoruz.
        }        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Prefabý spawn noktasýnda oluþturuyoruz.
    }

}
