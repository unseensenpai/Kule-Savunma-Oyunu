using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    //public Transform enemyPrefab; // D��man objesinin nesnesi
    public Wave[] waves;
    public float timeBetweenWaves = 5f; // dalgalar aras�ndaki zaman
    public float countdown = 2f; // ilk dalgay� olu�turma s�resi
    private int waveIndex = 0; // kac tane dusman objesinin spawn olaca��n� tutan saya�
    public Transform spawnPoint; // Ba�lang�� noktas�n�n nesnesi
    public Text waveCountdownText; // Ekrana yazd�rabilmek i�in text
    public static int EnemiesAlive = 0; // D��manlar �ld���nde s�radaki wavei �a��rmak i�in sabit tutulan de�i�kenimiz. 
    public WaveSpawnCanvas WaveSpawnImage;
    public string returnToMenu = "MainMenu";

    private void Update() 
    {
        if (EnemiesAlive>0) // D��man varsa
        {
            WaveSpawnImage.FadeOut();
            return; // Hi�bir �ey yapmadan geri d�n.
        }
        if (EnemiesAlive <= 0)
        {
            WaveSpawnImage.FadeIn();
        }
        if (countdown <= 0f) // E�er do�ma s�resi 0�n alt�na d��t�yse
        {

            StartCoroutine(SpawnWave()); // Spawn wave komutunu �al��t�r ama asenkron olarak.
            countdown = timeBetweenWaves; // S�radaki dalga i�in countdownu 0dan 5 saniyeye ��kar�yoruz.
            return;
        }
        countdown -= Time.deltaTime; // Saya� zaman i�inde azalmal�.
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);        
        //waveCountdownText.text = ("SIRADAK� DALGA ���N " + (Mathf.Floor(countdown).ToString()) + " SAN�YE");
    }

    IEnumerator SpawnWave()
    {
        // IEnumerator fonksiyonu kodu belli bir sure aral���nda �al��t�rmak i�in kullan�l�yor
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        Debug.Log("Wave Incoming!");
        for (int i = 0; i < wave.amount; i++)              // 6 WAVE - 1. slow 2.normal 3.normal 4.h�zl� 5.h�zl� 6.boss  -- WAVE AMOUNT = 20 , WAVES = 0-5 - WAVEINDEX - 6 , WAVE LENGHT 6
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate); // Wave rateine g�re bekle.
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
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); // Prefab� spawn noktas�nda olu�turuyoruz.
        EnemiesAlive++;
    }

}
