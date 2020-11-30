using UnityEngine;

// Generic interface used by pickable abilities and powerups
public interface PickableContract
{
  GameObject GetPrefab();
  bool Apply(GameObject player);
}
