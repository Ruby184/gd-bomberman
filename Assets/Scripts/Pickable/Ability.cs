using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ability is a scriptable object which has one of ability types
// and associated setup for levels and values for given ability.
// It is also pickable so it increases level of the associated player ability after picking it up.
[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject, PickableContract
{
  // This enum specifies different types of abilities for player
  // Extending is easy as we just need to add another enum item
  // and create scriptable object where we select added type.
  // Then we just add created object to AbilityRegistry.
  public enum AbilityType {
    BombsCount,
    BombExplosionRadius,
    MovementSpeed,
    BombThrowing,
    BombPlantingSpeed
  }

  // This static property just maintains count of ability types enum.
  public static readonly int TypesCount = Enum.GetNames(typeof(AbilityType)).Length;

  [Tooltip("Title of the ability shown in the UI.")]
  public string title;

  [Tooltip("Ability type which is used in code to maintain and manipulate level info in PlayerStats component.")]
  public AbilityType type;

  [Header("Visual Settings")]

  [Tooltip("Icon which identifies the ability in the UI.")]
  public Sprite icon;

  [Tooltip("Prefab with 3D representation of the ability which is spawned in the game.")]
  public GameObject prefab;

  [Header("Level Settings")]

  [Tooltip("Initial level for ability, can be zero or one.")]
  [Range(0, 1)]
  public int initialLevel = 1;

  [Tooltip("How many times can this ability be upgraded from initial level.")]
  [Range(1, 10)]
  public int upgradesCount = 5;

  [Header("Value Settings")]

  [Tooltip("Initial value for ability when it has not been upgraded yet.")]
  [Min(0)]
  public float initialValue = 0f;

  [Tooltip("Maximum value for ability after all possible upgrades.")]
  [Min(0)]
  public float maximumValue = 1.0f;

  // Maximum level after all possible upgrades applied
  public int maximumLevel => initialLevel + upgradesCount;

  // Amount of value increase per one level.
  public float amountPerLevel => (maximumValue - initialValue) / upgradesCount;

  public float ValueAtLevel(int level)
  {
    return initialValue + (level - initialLevel) * amountPerLevel;
  }

  // Method called after ability was picked up by player.
  // We try to increase the ability level using PlayerStats component.
  // If player has the maximum level of ability it will return false.
  public bool Apply(GameObject player)
  {
    return player.GetComponent<PlayerStats>().IncreaseAbilityLevel(this.type);
  }

  // Method used by PickableSpawner to get prefab of spawned ability 
  public GameObject GetPrefab()
  {
    return prefab;
  }
}
