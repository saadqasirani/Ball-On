using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    private float spawnRange = 9.0f;
    public int countenemy;
    public int wavecount = 0;
    private PlayerController playerControllerScript;
    public GameObject Prefab;
    public GameObject[] BigEnemy;
    public GameObject[] superprefab;
    public TextMeshProUGUI level;
    public TextMeshProUGUI scoretext;
    public int score;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        int count = Random.Range(0, superprefab.Length);
        countenemy = FindObjectsOfType<Enemy>().Length;
        if (playerControllerScript.gameover == false)//if game is not over it will constantly generate more eneimies
        {
            if (countenemy == 0)
            {
                wavecount++;
                spawnEnemies(wavecount);
                Instantiate(superprefab[count], generateSpawnPostion(), superprefab[count].transform.rotation);
                levels(0);
                score = wavecount * 5;
                updateScore(0);
            }
        }
    }
    private Vector3 generateSpawnPostion()//it will generate different spawn postions
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 mainPostion = new Vector3(spawnPosX, 0.88f, spawnPosZ);
        return mainPostion;
    }
    private void spawnEnemies(int spawnEnemy)//we use for loop to spawn as many enemies as we move forward in the game
    {
        int count = Random.Range(0, superprefab.Length);
        int spawnrange = Random.Range(0, BigEnemy.Length);
        for (int i = 0; i < spawnEnemy; i++)
        {
            Instantiate(Prefab, generateSpawnPostion(), Prefab.transform.rotation);
        }
        if (spawnEnemy > 5)
        {
            Instantiate(BigEnemy[spawnrange], generateSpawnPostion(), BigEnemy[spawnrange].transform.rotation);
        }
        if (spawnEnemy > 6 && spawnEnemy<8)
        {
            Instantiate(BigEnemy[spawnrange], generateSpawnPostion(), BigEnemy[spawnrange].transform.rotation);

        }
        else if(spawnEnemy>8)
                {
            Instantiate(BigEnemy[spawnrange], generateSpawnPostion(), BigEnemy[spawnrange].transform.rotation);
            Instantiate(BigEnemy[spawnrange], generateSpawnPostion(), BigEnemy[spawnrange].transform.rotation);
            Instantiate(BigEnemy[spawnrange], generateSpawnPostion(), BigEnemy[spawnrange].transform.rotation);
           
        }
    }
    private void levels(int addwaves)
    {
        wavecount += addwaves;
        level.text = "Level:" + wavecount;
       
    }
    public void updateScore(int addscore)
    {
        score += addscore;
        scoretext.text = "Score:" + score;
    }
    public void startGame()
    {
        spawnEnemies(wavecount);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();// i have used playercontroller script to get know when the game is over
        levels(0);
        updateScore(0);
        scoretext.gameObject.SetActive(true);
        level.gameObject.SetActive(true);
    }
}