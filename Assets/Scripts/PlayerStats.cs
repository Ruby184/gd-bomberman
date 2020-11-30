using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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

  void Awake() {
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

  public int GetAbilityLevel(Ability.AbilityType type) {
    if (abilities.TryGetValue(type, out int currentLevel))
    {
      return currentLevel;
    }
    else
    {
      return abilityRegistry.GetAbility(type).initialLevel;
    }
  }

  public float GetAbilityValue(Ability.AbilityType type) {
    return abilityRegistry.GetAbility(type).ValueAtLevel(GetAbilityLevel(type));
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
      return true;
    }

    return false;
  }
}
