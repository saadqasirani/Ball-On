using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private PlayerController playercontrollerScript;
    public TextMeshProUGUI gameOver;
    private SpawnManager spawnManagerScript;
    public Button restartButton;
    public Button playButton;
    public TextMeshProUGUI title;

    // Start is called before the first frame update
    void Start()
    {
        playercontrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playButton.onClick.AddListener(playGame);
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameOverof();
        
    }
    public void gameOverof()
    {
        if(playercontrollerScript.gameover==true)
        {
            gameOver.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void playGame()
    {
        playButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        spawnManagerScript.startGame();

    }
   
}
