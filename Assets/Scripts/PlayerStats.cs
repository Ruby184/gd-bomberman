using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
  public AbilityLevelChangeEvent onAbilityLevelChange;

  public AbilityRegistry abilityRegistry;

  // This allows assigning ability levels in editor
  // we then create a dictionary from that to speed up lookups
  [Serializable]
  public struct AbilityLevel {
    public Ability.AbilityType type;

    [Min(0)]
    public int level;
  }

  [SerializeField]
  private List<AbilityLevel> abilityLevels;

  private Dictionary<Ability.AbilityType, int> abilities = new Dictionary<Ability.AbilityType, int>(Ability.TypesCount);

  private int bombsPlanted = 0;

  void Awake()
  {
    for (int i = 0; i < abilityLevels.Count; i++)
    {
      var item = abilityLevels[i];

      if (abilities.ContainsKey(item.type)) {
        Debug.LogWarning($"PlayerStats: Skipping duplicate ability level for {item.type} at index {i}.");
      } else {
        abilities.Add(item.type, item.level);
      }
    }
  }

  void Start()
  {
    foreach (var entry in abilities)
    {
      onAbilityLevelChange?.Invoke(new AbilityLevelChangeArg(gameObject, abilityRegistry.GetAbility(entry.Key), entry.Value));
    }
  }

  public int GetPlantedBombsCount()
  {
    return bombsPlanted;
  }

  public void OnBombPlanted()
  {
    bombsPlanted++;
  }

  public void OnBombExploded()
  {
    bombsPlanted--;
  }

  public int GetAbilityLevel(Ability.AbilityType type)
  {
    if (abilities.TryGetValue(type, out int currentLevel))
    {
      return currentLevel;
    }
    else
    {
      return abilityRegistry.GetAbility(type).initialLevel;
    }
  }

  public float GetAbilityValue(Ability.AbilityType type)
  {
    return abilityRegistry.GetAbility(type).ValueAtLevel(GetAbilityLevel(type));
  }

  public int GetAbilityValueInt(Ability.AbilityType type)
  {
    return Mathf.FloorToInt(GetAbilityValue(type));
  }

  public bool IncreaseAbilityLevel(Ability.AbilityType type)
  {
    Ability ability = abilityRegistry.GetAbility(type);
    int currentLevel;
    
    if (!abilities.TryGetValue(type, out currentLevel))
    {
      currentLevel = ability.initialLevel;
    }

    if (currentLevel < ability.maximumLevel) {
      abilities[type] = currentLevel + 1;
      onAbilityLevelChange?.Invoke(new AbilityLevelChangeArg(gameObject, ability, abilities[type]));
      return true;
    }

    return false;
  }

  public readonly struct AbilityLevelChangeArg
  {
    public GameObject player { get; }

    public Ability ability { get; }

    public int level { get; }

    public AbilityLevelChangeArg(GameObject player, Ability ability, int level)
    {
      this.player = player;
      this.ability = ability;
      this.level = level;
    }
  }

  [Serializable]
  public class AbilityLevelChangeEvent : UnityEvent<AbilityLevelChangeArg> {};
}
