using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeTexture : MonoBehaviour, IDestroyable
{
  public Texture[] textures;
  private Renderer rend;
  private int hits = 0;

  void Awake()
  {
    rend = GetComponent<Renderer>();
  }

  public bool Hit()
  {
    if (++hits < textures.Length)
    {
      rend.material.mainTexture = textures[hits];
      return false;
    }

    return true;
  }
}
