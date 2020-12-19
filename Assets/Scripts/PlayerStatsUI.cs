using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
  public PlayerStats playerStats;

  public Transform abilityIconsGrid;

  public AbilityIcon abilityIconPrefab;

  private Dictionary<Ability, AbilityIcon> abilityIcons = new Dictionary<Ability, AbilityIcon>();

  void Awake()
  {
    foreach (var ability in playerStats.abilityRegistry.GetAbilities())
    {
      abilityIcons.Add(ability.ability, CreateAbilityIcon(ability.ability));
    }
  }

  void OnEnable()
  {
    playerStats.onAbilityLevelChange.AddListener(OnAbilityChange);
  }

  void OnDisable()
  {
    playerStats.onAbilityLevelChange.RemoveListener(OnAbilityChange);
  }

  public void OnAbilityChange(PlayerStats.AbilityLevelChangeArg info)
  {
    if (abilityIcons.TryGetValue(info.ability, out AbilityIcon icon))
    {
      icon.SetLevel(info.level);
    }
  }

  private AbilityIcon CreateAbilityIcon(Ability ability)
  {
    return Instantiate(abilityIconPrefab, abilityIconsGrid).Initialize(ability);
  }
}
