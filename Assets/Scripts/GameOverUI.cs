using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
  public GameObject gameOverPanel;

  public TMP_Text wonText;

  void OnEnable()
  {
    GameManager.Instance.onGameOver += OnGameOver;
  }

  void OnDisable()
  {
    GameManager.Instance.onGameOver -= OnGameOver;
  }

  private void OnGameOver(int playerWon)
  {
    wonText.text = $"Player {playerWon} won!";
    gameOverPanel.SetActive(true);
  }

  public void PlayAgain()
  {
    gameOverPanel.SetActive(false);
    GameManager.Instance.RestartGame();
  }

  public void QuitGame()
  {
    GameManager.Instance.QuitGame();
  }
}
