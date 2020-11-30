using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupRegistry", menuName = "ScriptableObjects/Powerup/Registry")]
public class PowerupRegistry : ScriptableObject
{
  [Serializable]
  public struct PowerupSettings {
    public Powerup powerup;
  }

  [SerializeField]
  private List<PowerupSettings> powerups;

  private Dictionary<Type, PowerupSettings> registry = new Dictionary<Type, PowerupSettings>();

  void OnEnable()
  {
    Initialize();
  }

  private void Initialize()
  {
    for (int i = 0; i < powerups.Count; i++)
    {
      var item = powerups[i];
      Type type = item.powerup.GetType();

      if (registry.ContainsKey(type)) {
        Debug.LogWarning($"PowerupRegistry: Skipping duplicate powerup settings for {type.Name} at index {i}.");
      } else {
        registry.Add(type, item);
      }
    }
  }

  public T GetPowerup<T>() where T: Powerup
  {
    Type type = typeof(T);

    if (registry.TryGetValue(type, out PowerupSettings item)) {
      return item.powerup as T;
    }

    return CreateInstance<T>();
  }
}
