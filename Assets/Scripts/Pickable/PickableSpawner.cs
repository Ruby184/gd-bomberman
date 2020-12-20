using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpawner : MonoBehaviour
{
  public AbilityRegistry abilityRegistry;

  public Pickable pickablePrefab;

  [Range(0.0f, 1.0f)]
  public float probability = 0.5f;

  private bool isShuttingDown = false;

  void OnApplicationQuit()
  {
    isShuttingDown = true;
  }

  void OnDestroy()
  {
    if (!isShuttingDown)
    {
      float rnd = Random.Range(0.0f, 1.0f);

      if (rnd <= probability)
      {
        Spawn(abilityRegistry.GetRandomAbility());
      }
    }
  }

  private void Spawn(PickableContract pickable)
  {
    var go = Instantiate(pickablePrefab, transform.position, Quaternion.identity);
    go.pickable = pickable;
  }
}
