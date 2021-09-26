using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject zombie;
    public GameObject blackScreen;
    public void StartGame()
    {
        Debug.Log("Start");

        var zombieAnimator = zombie.GetComponent<Animator>();
        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("playerDetected", true);
        }

        StartCoroutine(LoadGame());
        if (blackScreen != null)
        {
            var fadeComponent = blackScreen.GetComponent<Fade>();
            if (fadeComponent != null)
            {
                fadeComponent.fadeAfterSeconds = 3;
                fadeComponent.TriggerFadeIn();
            }
        }
        
        Time.timeScale = 1f;
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("Dungeon Delvers");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void ControlsButtonPressed()
    {
        if (controlsMenu)
        {
            controlsMenu.SetActive(true);
        }
        if (mainMenu)
        {
            mainMenu.SetActive(false);
        }
    }
    public void ControlsBackButtonPressed()
    {
        if (controlsMenu)
        {
            controlsMenu.SetActive(false);
        }
        if (mainMenu)
        {
            mainMenu.SetActive(true);
        }
    }

    public void someLog()
    {
        Debug.Log("SOMETHING");
    }
}
