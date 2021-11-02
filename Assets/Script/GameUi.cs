using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameObject mainUi;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject darkness;
    [SerializeField] private GameObject scoreobject;
    [SerializeField] private GameObject StartUi;
    [SerializeField] private GameObject clear;
    [SerializeField] private Text bestscoreDisplay;
    [SerializeField] private Pause pause;
    [SerializeField] private GameObject deathUi;
    [SerializeField] private PlayerStatManager playstat;
    [SerializeField] private Text scoreDisplay;
    public float score;
    public float changescore;
    public bool Gclear = false;
    public bool start = false;

    void Start()
    {
        changescore = PlayerPrefs.GetFloat("score");
        bestscoreDisplay.text = "best Score : " + changescore;
        Time.timeScale = 0;

    }
    void Update()
    {
        if (playstat.Stats.Health <= 0 )
        {
            mainUi.SetActive(false);
            deathUi.SetActive(true);
            darkness.SetActive(true);
            scoreobject.SetActive(true);
            if(score > changescore)
            {
                PlayerPrefs.SetFloat("score", score);
            }
            changescore = PlayerPrefs.GetFloat("score");
            scoreDisplay.text = "score : " + changescore;
            Time.timeScale = 0;
            return;
        }

        if (Gclear)
        {
            mainUi.SetActive(false);
            clear.SetActive(true);
            darkness.SetActive(true);
            scoreobject.SetActive(true);
            if (score > changescore)
            {
                PlayerPrefs.SetFloat("score", score);
            }
            changescore = PlayerPrefs.GetFloat("score");
            scoreDisplay.text = "score : " + changescore;
            scoreDisplay.text = "score : " + score;
            Time.timeScale = 0;
            return;
        }

        if (pause.Paused)
        {
            mainUi.SetActive(false);
            pauseScreen.SetActive(true);
            darkness.SetActive(true);
        }
        else if(!pause.Paused && start )
        {
            mainUi.SetActive(true);
            pauseScreen.SetActive(false);
            darkness.SetActive(false);
        }
    }
    public void AddScore(int s)
    {
        score += s;
    }

    public void GameClear()
    {
        Gclear = true;
    }

    public void ReloadScene()
    { 
        SceneManager.LoadScene("SampleScene");
    }

    public void StartScene()
    {
        mainUi.SetActive(true);
        StartUi.SetActive(false);
        start = true;
        Time.timeScale = 1;
    }
}
