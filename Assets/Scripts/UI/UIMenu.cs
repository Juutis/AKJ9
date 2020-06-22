using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    private GameObject startGameButton;
    private GameObject restartGameButton;
    private GameObject continueGameButton;
    private GameObject exitGameButton;
    private GameObject tutorialButton;
    private Text txtMessage;
    private bool initialized = false;

    public void Show(string message, bool showContinue, bool showStart, bool showRestart, bool showExit, bool showTutorial)
    {
        container.SetActive(true);
        if (!initialized) {
            txtMessage = this.FindChildObject("message").GetComponent<Text>();
            tutorialButton = this.FindChildObject("TutorialButton").gameObject;
            startGameButton = this.FindChildObject("StartGameButton").gameObject;
            restartGameButton = this.FindChildObject("RestartButton").gameObject;
            continueGameButton = this.FindChildObject("ContinueButton").gameObject;
            exitGameButton = this.FindChildObject("ExitButton").gameObject;
            initialized = true;
        }
        txtMessage.text = message;
        continueGameButton.SetActive(showContinue);
        tutorialButton.SetActive(showTutorial);
        startGameButton.SetActive(showStart);
        restartGameButton.SetActive(showRestart);
        exitGameButton.SetActive(showExit);
    }

    public void Hide()
    {
        container.SetActive(false);
        UIManager.main.MenuWasClosed();
    }
    public void StartGame()
    {
        Debug.Log("Start");
        Hide();
    }

    public void StartTutorial()
    {
        Debug.Log("Tutorial");
        Hide();
        TutorialManager.main.StartTutorial();
    }
    public void Continue()
    {
        Debug.Log("Continue");
        Hide();
    }
    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Hide();
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
        Hide();
    }
}
