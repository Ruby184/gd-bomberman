using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Destroyable : MonoBehaviour
{
  [Min(1)]
  public int hitsToDestroy = 1;

  [ColorUsage(false)]
  public Color colorEnd = Color.black;

  private int hits = 0;

  private Renderer rend;

  private Color colorStart;

  void Awake()
  {
    rend = GetComponent<Renderer>();
    colorStart = rend.material.color;
  }

  public bool Hit()
  {
    if (++hits < hitsToDestroy)
    {
      float lerp = hits / (float) (hitsToDestroy - 1);
      rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
      return false;
    }

    return true;
  }
}
