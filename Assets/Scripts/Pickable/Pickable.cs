using System;
using UnityEngine;
using UnityEngine.Events;

// Component adds functionality for picking up pickable gameobject
// and applying the associated pickable scriptable object to player
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
  [HideInInspector]
  public PickableContract pickable;

  public PickupEvent onPickup;

  void Start()
  {
    // instantiate pickable prefab as child of this gameobject
    Instantiate(pickable.GetPrefab(), transform);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      TryPickup(other.gameObject);
    }
  }

  // try to apply pickable on player which is trying to collect pickable
  // if apply is successful, emit unity event and destroy gameobject
  private void TryPickup(GameObject player)
  {
    if (pickable.Apply(player))
    {
      onPickup?.Invoke(player);
      Destroy(gameObject);
    }
  }

  [Serializable]
  public class PickupEvent : UnityEvent<GameObject> {};
}
