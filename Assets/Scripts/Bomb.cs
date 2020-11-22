using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapTileGridCreator.Core;

public class Bomb : MonoBehaviour
{
  public float timer = 3f;
  public int radius = 5;
  public GameObject explosionPrefab;
  public GameObject firePrefab;
  private Grid3D grid;
  private float remainingTime = 0f;
  private bool exploded = false;
  void Start()
  {
    grid = FindObjectOfType<Grid3D>();
    remainingTime = timer;
  }

  void Update()
  {
    remainingTime -= Time.deltaTime;

    if (remainingTime <= 0 && !exploded)
    {
      Explode();
    }
  }
  void OnTriggerEnter(Collider other)
  {
    if (!exploded && other.CompareTag(firePrefab.tag))
    {
      Explode();
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      GetComponent<Collider>().isTrigger = false;
    }
  }

  private void Explode()
  {
    exploded = true;

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
        if (cell.TryGetComponent(out IDestroyable destroyable) && destroyable.Hit())
        {
          Destroy(cell.gameObject);
          Instantiate(explosionPrefab, position, Quaternion.identity);
        }

        return;
      }

      Instantiate(firePrefab, position, Quaternion.identity);
    }
  }
}
