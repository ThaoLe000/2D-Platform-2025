using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause Game")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        //SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if(status == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.1f);
    }
    #endregion

    #region MenuGame

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
}
