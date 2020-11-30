using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : ScriptableObject, PickableContract
{
  public abstract GameObject GetPrefab();
  public abstract bool Apply(GameObject player);
}
