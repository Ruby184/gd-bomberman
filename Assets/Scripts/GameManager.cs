using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
  public delegate void GameOverEvent(int playerWon);

  public event GameOverEvent onGameOver;

  public bool isPaused = false;

  public void PlayerDied(GameObject player)
  {
    var p = player.GetComponent<PlayerInput>();
    onGameOver?.Invoke(p.playerIndex == 0 ? 2 : 1);
    PauseGame();
  }

  public void RestartGame()
  {
    SceneManager.LoadScene("Game");
    ResumeGame();
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void PauseGame()
  {
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void ResumeGame()
  {
    isPaused = false;
    Time.timeScale = 1f;
  }
}
