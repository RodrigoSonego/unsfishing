using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour 
{

    float currentTime = 0f;
    float startingTime = 5f;

    [SerializeField] TMP_Text countdownText;
    [SerializeField] private string levelName;
    [SerializeField] private GameObject GameOverScreen;





    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void Retry()
    {
        GameOverScreen.SetActive(false);
        SceneManager.LoadScene(levelName);
    }

   public void PlayGame()
    {
       Debug.Log("cu");
    }
}