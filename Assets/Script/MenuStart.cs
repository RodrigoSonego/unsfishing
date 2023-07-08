using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
    
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject MenuPrincipal;
    [SerializeField] private GameObject Menuhelp;
    [SerializeField] private GameObject MenuhelpMini;






    public void PlayGame()
    {
        MenuPrincipal.SetActive(false);
        Menuhelp.SetActive(true);
    }

    public void HelpMovementEnd()
    {
        Menuhelp.SetActive(false);
        MenuhelpMini.SetActive(true);
    }

    public void HelpEnd()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
}
