using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stolen from Milestones for class
[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    // Update is called once per frame
    void Update()
    {
        //var test = Input.GetButton("Menu Button");
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Menu"))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        var panel = this.transform.Find("UI");
        if (panel != null)
        {
            if (panel.gameObject.activeSelf)
            {
                UnpauseGame(panel);
            }
            else
            {
                PauseGame(panel);
            }
        }
    }

    public void PauseGame(Transform panel)
    {
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
        Time.timeScale = 0f;
    }

    public void UnpauseGame(Transform panel)
    {
        ControlsBackButtonPressed();
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart");
        var player = GameObject.Find("Hero");
        var animator = player.GetComponent<Animator>();

        var holdItem = player.GetComponent<HoldItem>();
        var heroController = player.GetComponent<HeroController>();
        if (holdItem != null && holdItem.heldItem != null && heroController != null)
        {
            heroController.DropItem(holdItem);
        }
        Time.timeScale = 1f;
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
