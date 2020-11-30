using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityRegistry", menuName = "ScriptableObjects/AbilityRegistry")]
public class AbilityRegistry : ScriptableObject
{
  [Serializable]
  public struct AbilitySettings {
    public Ability ability;

    [Range(0f, 1f)]
    public float probabilityMultiplier;
  }

  [SerializeField]
  private List<AbilitySettings> abilities;

  private Dictionary<Ability.AbilityType, AbilitySettings> registry = new Dictionary<Ability.AbilityType, AbilitySettings>(Ability.TypesCount);

  void OnEnable()
  {
    Initialize();
  }

  private void Initialize()
  {
    for (int i = 0; i < abilities.Count; i++)
    {
      var item = abilities[i];
      
      if (registry.ContainsKey(item.ability.type)) {
        Debug.LogWarning($"AbilityRegistry: Skipping duplicate ability settings for {item.ability.type} at index {i}.");
      } else {
        registry.Add(item.ability.type, item);
      }
    }

    if (registry.Count != Ability.TypesCount)
    {
      foreach (Ability.AbilityType type in Enum.GetValues(typeof(Ability.AbilityType)))
      {
        if (!registry.ContainsKey(type)) {
          Debug.LogWarning($"AbilityRegistry: Missing ability for type {type}.");
        }
      }
    }
  }

  public Ability GetAbility(Ability.AbilityType type)
  {
    if (registry.TryGetValue(type, out AbilitySettings item)) {
      return item.ability;
    }

    return null;
  }
}
