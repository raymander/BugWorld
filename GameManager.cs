using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;
    public GameObject gameOverPanel;
    Stopwatch timer;
    string timerString;
    public Text stopwatchText;
    public GameObject healthBar;

    void Awake()
    {
        timer = new Stopwatch();
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            GameOver();
        }

        System.TimeSpan ts = timer.Elapsed;
        timerString = ts.ToString().Substring(3, 5);
        stopwatchText.text = timerString;
    }

    public void GameOver()
    {
        StartCoroutine("DeathAnimation");
        timer.Stop();     
        healthBar.SetActive(false);

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        gameOverPanel.GetComponent<Animator>().enabled = true;
    }


}
