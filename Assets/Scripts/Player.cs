using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
  public float maxInGasDuration = 1.5f;

  private Coroutine dieCoroutine;

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Fire"))
    {
      GameManager.Instance.PlayerDied(gameObject);
    }
    else if (other.CompareTag("PoisonGas") && dieCoroutine == null)
    {
      dieCoroutine = StartCoroutine(DieAfter(maxInGasDuration));
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("PoisonGas") && dieCoroutine != null)
    {
      StopCoroutine(dieCoroutine);
    }
  }

  private IEnumerator DieAfter(float duration)
  {
    float time = 0.0f;

    while (time < duration)
    {
      time += Time.deltaTime;
      yield return null;
    }

    GameManager.Instance.PlayerDied(gameObject);
    dieCoroutine = null;
  }
}
