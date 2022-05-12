using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    //public Transform enemyPrefab; // Düþman objesinin nesnesi
    public Wave[] waves;
    public float timeBetweenWaves = 5f; // dalgalar arasýndaki zaman
    public float countdown = 2f; // ilk dalgayý oluþturma süresi
    private int waveIndex = 0; // kac tane dusman objesinin spawn olacaðýný tutan sayaç
    public Transform spawnPoint; // Baþlangýç noktasýnýn nesnesi
    public Text waveCountdownText; // Ekrana yazdýrabilmek için text
    public static int EnemiesAlive = 0; // Düþmanlar öldüðünde sýradaki wavei çaðýrmak için sabit tutulan deðiþkenimiz. 
    public WaveSpawnCanvas WaveSpawnImage;
    public string returnToMenu = "MainMenu";

    private void Update() 
    {
        if (EnemiesAlive>0) // Düþman varsa
        {
            WaveSpawnImage.FadeOut();
            return; // Hiçbir þey yapmadan geri dön.
        }
        if (EnemiesAlive <= 0)
        {
            WaveSpawnImage.FadeIn();
        }
        if (countdown <= 0f) // Eðer doðma süresi 0ýn altýna düþtüyse
        {

            StartCoroutine(SpawnWave()); // Spawn wave komutunu çalýþtýr ama asenkron olarak.
            countdown = timeBetweenWaves; // Sýradaki dalga için countdownu 0dan 5 saniyeye çýkarýyoruz.
            return;
        }
        countdown -= Time.deltaTime; // Sayaç zaman içinde azalmalý.
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);        
        //waveCountdownText.text = ("SIRADAKÝ DALGA ÝÇÝN " + (Mathf.Floor(countdown).ToString()) + " SANÝYE");
    }

    IEnumerator SpawnWave()
    {
        // IEnumerator fonksiyonu kodu belli bir sure aralýðýnda çalýþtýrmak için kullanýlýyor
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        Debug.Log("Wave Incoming!");
        for (int i = 0; i < wave.amount; i++)              // 6 WAVE - 1. slow 2.normal 3.normal 4.hýzlý 5.hýzlý 6.boss  -- WAVE AMOUNT = 20 , WAVES = 0-5 - WAVEINDEX - 6 , WAVE LENGHT 6
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate); // Wave rateine göre bekle.
        }
        waveIndex++;

        if (waveIndex == waves.Length+1)
        {
            Debug.Log(" LEVEL 1 TAMAMLANDI!");
            this.enabled = false;
            GameManagers.GameIsOver = true;
            GameManagers gm = new GameManagers();
            gm.EndGame();
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); // Prefabý spawn noktasýnda oluþturuyoruz.
        EnemiesAlive++;
    }

}
