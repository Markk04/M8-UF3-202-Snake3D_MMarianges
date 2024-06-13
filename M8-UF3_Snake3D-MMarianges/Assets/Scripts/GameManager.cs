using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int Score;
    private int BScore;
    [SerializeField] private TMP_Text ScoreTxt;
    [SerializeField] private GameObject gameOverImg;
    [SerializeField] private GameObject MainMenuBtn;
    [SerializeField] private GameObject RestartBtn;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject _stickL;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        ScoreTxt.text = "Score: " + Score;
        gameOver = false;
        BScore = PlayerPrefs.GetInt("bestScore");
        gameOverImg.SetActive(false);
        MainMenuBtn.SetActive(false);
        RestartBtn.SetActive(false);
        bool savedGyroscopeEnabled = PlayerPrefs.GetInt("GyroscopeEnabled", 0) == 1;
        Debug.Log("Gyroscope Enabled: " + savedGyroscopeEnabled.ToString());

        if (savedGyroscopeEnabled)
        {
            _stickL.SetActive(false);
            Player.GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            _stickL.SetActive(true);
            Player.GetComponent<PlayerController>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addScore()
    {
        Score++;
        ScoreTxt.text = "Score: " + Score;
    }

    public void setUpGameOver()
    {
        gameOver = true;
        gameOverImg.SetActive(true);
        MainMenuBtn.SetActive(true);
        RestartBtn.SetActive(true);

        if (Score > BScore)
        {
            PlayerPrefs.SetInt("bestScore", Score);
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
