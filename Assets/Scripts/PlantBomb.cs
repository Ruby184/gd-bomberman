using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MapTileGridCreator.Core;

[RequireComponent(typeof(PlayerStats))]
public class PlantBomb : MonoBehaviour
{
  public Bomb bombPrefab;

  private Grid3D grid;

  private PlayerStats playerStats;

  private bool isPlanting = false;

  private float elapsedTime = 0f;

  public int bombsCount => playerStats.GetAbilityValueInt(Ability.AbilityType.BombsCount);

  public int throwDistance => playerStats.GetAbilityValueInt(Ability.AbilityType.BombThrowing);

  public float plantingTime => playerStats.GetAbilityValue(Ability.AbilityType.BombPlantingSpeed);

  public void OnPlantBomb(InputAction.CallbackContext ctx)
  {
    if (ctx.started)
    {
      isPlanting = true;
    }

    if (ctx.canceled && isPlanting)
    {
      ResetPlanting();
    }
	}

  void Awake()
	{
		playerStats = GetComponent<PlayerStats>();
	}
  
  void Start()
  {
    grid = FindObjectOfType<Grid3D>();
  }

  void Update()
  {
    if (isPlanting)
    {
      elapsedTime += Time.deltaTime;

      if (elapsedTime >= plantingTime)
      {
        PlantTheBomb();
        ResetPlanting();
      }
    }
  }
  private void ResetPlanting()
  {
    isPlanting = false;
    elapsedTime = 0f;
  }

  private void PlantTheBomb()
  {
    Vector3 position = grid.TransformPositionToGridPosition(transform.position);
    position.y += grid.SizeCell / 2;
    Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
    bomb.player = playerStats;
  }

  private bool CanPlantBomb()
  {
    return playerStats.GetPlantedBombsCount() < bombsCount;
  }
}
