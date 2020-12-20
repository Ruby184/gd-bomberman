using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GasTimerDisplay : MonoBehaviour
{
  public PoisonGasShrink shinkManager;

  public TMP_Text timeText;

  void Update()
  {
    timeText.text = ToDisplayTime(shinkManager.GetRemainingTime());
  }

  string ToDisplayTime(float timeToDisplay)
  {
    float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
    float seconds = Mathf.FloorToInt(timeToDisplay % 60);

    return string.Format($"{minutes:00}:{seconds:00}");
  }
}
