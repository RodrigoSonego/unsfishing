using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
    
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject helpMenuMinigames;






    public void PlayGame()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void HelpMovementEnd()
    {
        helpMenu.SetActive(false);
        helpMenuMinigames.SetActive(true);
    }

    public void HelpEnd()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
}
