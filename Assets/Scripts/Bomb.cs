﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapTileGridCreator.Core;

[RequireComponent(typeof(Collider))]
public class Bomb : MonoBehaviour
{
  public float timer = 3f;
  public GameObject explosionPrefab;
  public ParticleSystem firePrefab;
  private Grid3D grid;
  private Coroutine timerCoroutine;

  [HideInInspector]
  public PlayerStats player;

  private int radius = 5;

  private int playerColliderCount = 0;

  void Start()
  {
    grid = FindObjectOfType<Grid3D>();
    radius = player.GetAbilityValueInt(Ability.AbilityType.BombExplosionRadius);
    player.OnBombPlanted();
    timerCoroutine = StartCoroutine(StartTimer());
  }

  private IEnumerator StartTimer()
  {
    float remaining = timer;

    while (remaining > 0)
    {
      remaining -= Time.deltaTime;
      yield return null;
    }

    Explode();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerColliderCount++;
    }
    else if (other.CompareTag(firePrefab.tag))
    {
      StopCoroutine(timerCoroutine);
      Explode();
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (--playerColliderCount <= 0)
      {
        GetComponent<Collider>().isTrigger = false;
      }
    }
  }

  private void Explode()
  {
    Vector3 position = transform.position;

    Instantiate(explosionPrefab, position, Quaternion.identity);
    Instantiate(firePrefab, position, Quaternion.identity);

    Vector3Int index = grid.GetIndexByPosition(ref position);

    foreach (Vector3Int axis in grid.GetConnexAxes())
    {
      // Skip top and down axes
      if (axis.y != 0)
      {
        continue;
      }

      FireOnAxis(index, axis);
    }

    player.OnBombExploded();
    Destroy(gameObject);
  }

  private void FireOnAxis(Vector3Int index, Vector3Int axis)
  {
    for (int i = 1; i <= radius; i++)
    {
      Vector3Int newIndex = index + axis * i;
      Cell cell = grid.TryGetCellByIndex(ref newIndex);
      Vector3 position = grid.GetPositionCell(newIndex);
      position.y -= grid.SizeCell / 2;

      if (cell != null)
      {
        if (cell.TryGetComponent(out Destroyable destroyable) && destroyable.Hit())
        {
          Destroy(cell.gameObject);
          Instantiate(explosionPrefab, position, Quaternion.identity);
          Instantiate(firePrefab, position, Quaternion.identity);
        }

        return;
      }

      Instantiate(firePrefab, position, Quaternion.identity);
    }
  }
}
