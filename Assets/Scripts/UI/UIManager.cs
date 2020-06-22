using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager main;
    void Awake()
    {
        main = this;
        worldSpaceCanvas = this.FindChildObject("WorldSpaceCanvas").transform;
        uiCanvas = this.FindChildObject("UICanvas").transform;
        hudScore = GetComponentInChildren<HUDScore>();
    }
    private Transform uiCanvas;
    private Transform worldSpaceCanvas;
    private HUDScore hudScore;
    private UIMenu uiMenu;
    public Transform WorldSpaceCanvas { get { return worldSpaceCanvas; } }

    public Transform UICanvas {get {return uiCanvas;}}

    private bool startGameMenuOpen = false;
    private bool pauseMenuOpen = false;
    private bool endMenuOpen = false;
    private bool gameOverMenuOpen = false;

    void Start() {
        uiMenu = GetComponentInChildren<UIMenu>();
        ShowGameStart();
    }

    public void MenuWasClosed() {
        startGameMenuOpen = false;
        pauseMenuOpen = false;
        endMenuOpen = false;
        gameOverMenuOpen = false;
        Unpause();
    }

    private void Pause() {
        Time.timeScale = 0f;
    }
    private void Unpause() {
        Time.timeScale = 1f;
    }

    public void ShowPauseMenu() {
        pauseMenuOpen = true;
        uiMenu.Show("Game paused.", true, false, true, true, false);
        Pause();
    }

    public void ShowGameOver() {
        gameOverMenuOpen = true;
        uiMenu.Show("Game over! Your score was: {0}".Format(ScoreManager.main.GetScore()), false, false, true, true, false);
        Pause();
    }

    public void ShowTheEnd() {
        endMenuOpen = true;
        uiMenu.Show("The end. Your score was: {0}".Format(ScoreManager.main.GetScore()), false, false, true, true, false);
        Pause();
    }

    public void ShowGameStart() {
        startGameMenuOpen = true;
        uiMenu.Show("Welcome!", false, true, false, false, true);
        Pause();
    }

    public HitPointBar GetHitPointBar(float hp)
    {
        HitPointBar hpBar = Prefabs.Instantiate<HitPointBar>();
        hpBar.Initialize(hp);
        hpBar.transform.SetParent(worldSpaceCanvas);
        return hpBar;
    }

    public void UpdateMultiplier (float multiplier) {
        hudScore.UpdateMultiplier(multiplier);
    }
    public void UpdateScore (int addition, int score) {
        hudScore.UpdateScore(addition, score);
    }

    void Update() {
        if (!startGameMenuOpen && !endMenuOpen && !gameOverMenuOpen) {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) {
                ShowPauseMenu();
            }
        }
    }
}
