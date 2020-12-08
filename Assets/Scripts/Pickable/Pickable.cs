using System;
using UnityEngine;
using UnityEngine.Events;

// Component adds functionality for picking up pickable gameobject
// and applying the associated pickable scriptable object to player
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
  // This is assigned when gameobject prefab is instantiated by PickableSpawner
  [HideInInspector]
  public PickableContract pickable;

  public PickupEvent onPickup;

  void Start()
  {
    // Instantiate pickable prefab as child of this gameobject
    Instantiate(pickable.GetPrefab(), transform);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      TryPickup(other.gameObject);
    }
  }

  // Try to apply pickable on player which is trying to collect pickable
  // if apply is successful, emit unity event and destroy gameobject
  private void TryPickup(GameObject player)
  {
    if (pickable.Apply(player))
    {
      onPickup?.Invoke(new PickupEventArg(player, pickable));
      Destroy(gameObject);
    }
  }

  public readonly struct PickupEventArg
  {
    public GameObject player { get; }

    public PickableContract pickable { get; }

    public PickupEventArg(GameObject player, PickableContract pickable)
    {
      this.player = player;
      this.pickable = pickable;
    }
  }

  [Serializable]
  public class PickupEvent : UnityEvent<PickupEventArg> {};
}
