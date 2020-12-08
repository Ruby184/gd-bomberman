using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityIcon : MonoBehaviour
{
  public Ability ability;
  
  public Image ringImage;

  public Image icon;

  public TMP_Text levelText;

  [Min(0)]
  public float animationDuration = 0.6f;

  private Coroutine animationCoroutine;

  void Start()
  {
    icon.sprite = ability.icon;
    SetLevel(ability.initialLevel);
  }

  public void SetLevel(int level)
  {
    levelText.text = level.ToString();

    if (animationCoroutine != null)
    {
      StopCoroutine(animationCoroutine);
    }

    animationCoroutine = StartCoroutine(AnimateFillAmount(Mathf.Clamp01(level / (float) ability.maximumLevel), 0.6f));
  }
  
  private IEnumerator AnimateFillAmount(float value, float duration)
  {
    float time = 0.0f;

    while (time < duration)
    {
      yield return new WaitForEndOfFrame();
      time += Time.deltaTime;
      ringImage.fillAmount = Mathf.Lerp(ringImage.fillAmount, value, time / duration);
    }

    ringImage.fillAmount = value;
    animationCoroutine = null;
  }
}
