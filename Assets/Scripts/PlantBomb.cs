﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MapTileGridCreator.Core;

public class PlantBomb : MonoBehaviour
{
  public Bomb bombPrefab;

  private Grid3D grid;

  void Start()
  {
    grid = FindObjectOfType<Grid3D>();
  }

  void Update()
  {
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      Vector3 position = grid.TransformPositionToGridPosition(transform.position);
      Debug.Log($"Position: {transform.position}, {position}");
      position.y += grid.SizeCell / 2;
      Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
    }
  }
}