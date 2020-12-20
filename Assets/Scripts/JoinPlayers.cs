using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
  [Serializable]
  public struct PlayerSettings
  {
    public string controlScheme;

    [Min(0)]
    public int skinIndex;

    public Vector3 spawnPoint;
  }

  [SerializeField]
  private List<PlayerSettings> players;

  public GameObject playerPrefab;

  void Start()
  {
    foreach (PlayerSettings settings in players)
    {
      JoinPlayer(settings);
    }
  }

  private PlayerInput JoinPlayer(PlayerSettings settings)
  {
    var player = PlayerInput.Instantiate(playerPrefab, controlScheme: settings.controlScheme, pairWithDevice: Keyboard.current);
    var skin = player.GetComponent<CharacterSkinController>();
    skin.ChangeMaterialSettings(settings.skinIndex);
    player.transform.position = settings.spawnPoint;
    
    return player;
  }
}
