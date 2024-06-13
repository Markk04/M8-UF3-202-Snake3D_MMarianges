using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_Text bestScore;
    public Toggle gyroscopeToggle;
    private bool _isGyroscopeEnabled;

    // Asigna estos botones en el Inspector de Unity
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        bestScore.text = "Best Score: " + PlayerPrefs.GetInt("bestScore", 0); // Default to 0 if bestScore is not set
        _isGyroscopeEnabled = gyroscopeToggle.isOn;
        gyroscopeToggle.onValueChanged.AddListener(delegate {
            OnToggleValueChanged(gyroscopeToggle);
        });
        Debug.Log($"Gyroscope is {(_isGyroscopeEnabled ? "enabled" : "disabled")}");

        // Asigna las funciones a los botones
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("GyroscopeEnabled", _isGyroscopeEnabled ? 1 : 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void OnToggleValueChanged(Toggle change)
    {
        _isGyroscopeEnabled = gyroscopeToggle.isOn;
        Debug.Log($"Gyroscope is {(_isGyroscopeEnabled ? "enabled" : "disabled")}");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
