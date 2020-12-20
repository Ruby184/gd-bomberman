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

  public int bombsCount => playerStats.GetAbilityValueInt(Ability.AbilityType.BombsCount);

  public int explosionRadius => playerStats.GetAbilityValueInt(Ability.AbilityType.BombExplosionRadius);

  public int throwDistance => playerStats.GetAbilityValueInt(Ability.AbilityType.BombThrowing);

  public float plantingTime => playerStats.GetAbilityValue(Ability.AbilityType.BombPlantingSpeed);

  public void OnPlantBomb(InputAction.CallbackContext ctx)
  {
    Debug.Log($"Performed: {ctx.performed}, started: {ctx.started}, canceled: {ctx.canceled}, duration: {ctx.duration}");
    Debug.Log($"Starttime: {ctx.startTime}, time: {ctx.time}, control: {ctx.control}");
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
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      Vector3 position = grid.TransformPositionToGridPosition(transform.position);
      // Debug.Log($"Position: {transform.position}, {position}");
      position.y += grid.SizeCell / 2;
      Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
      bomb.radius = explosionRadius;
    }
  }
}
