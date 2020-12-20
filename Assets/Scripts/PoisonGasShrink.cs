using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapTileGridCreator.Core;

[RequireComponent(typeof(Grid3D))]
public class PoisonGasShrink : MonoBehaviour
{
  public GameObject poisonGasPrefab;

  public int size = 35;

  public float time = 30f;

  private int cycle = 0;

  private float timeRemaining = 0;

  private Grid3D grid;

  void Awake()
  {
    grid = GetComponent<Grid3D>();
  }

  void Start()
  {
    timeRemaining = time;
  }

  void Update()
  {
    timeRemaining -= Time.deltaTime;

    if (timeRemaining < 0)
    {
      NextCycle();
      timeRemaining = time;
    }
  }

  public float GetRemainingTime()
  {
    return Mathf.Max(timeRemaining, 0f);
  }

  private void NextCycle()
  {
    if (cycle < size) {
      SpawnGasAtDistance(size - ++cycle);
    }
  }

  private void SpawnGasAtDistance(int distance)
  {
    foreach (Vector3Int axis in grid.GetConnexAxes())
    {
      // Skip top and down axes
      if (axis.y != 0)
      {
        continue;
      }

      SpawnGasOnAxisAtDistance(axis, distance);
    }
  }

  private void SpawnGasOnAxisAtDistance(Vector3Int axis, int distance)
  {
    Vector3 distanceVector = Vector3.Cross(axis, Vector3Int.up) * distance;
    
    for (int i = -distance; i <= distance; i++)
    {
      Vector3 position = grid.TransformPositionToGridPosition(axis * i + distanceVector);
      Instantiate(poisonGasPrefab, position, Quaternion.identity);
    }
  }
}
