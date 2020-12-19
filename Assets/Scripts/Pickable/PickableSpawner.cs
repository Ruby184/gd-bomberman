using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpawner : MonoBehaviour
{
  public AbilityRegistry abilityRegistry;

  public Pickable pickablePrefab;

  private bool isShuttingDown = false;

  void OnApplicationQuit()
  {
    isShuttingDown = true;
  }

  void OnDestroy()
  {
    if (!isShuttingDown)
    {
      Spawn(abilityRegistry.GetRandomAbility());
    }
  }

  private void Spawn(PickableContract pickable)
  {
    var go = Instantiate(pickablePrefab, transform.position, Quaternion.identity);
    go.pickable = pickable;
  }
}
