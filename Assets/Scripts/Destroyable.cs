using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
  bool Hit();
}
public class Destroyable : MonoBehaviour, IDestroyable
{
  public bool Hit()
  {
    return true;
  }
}
