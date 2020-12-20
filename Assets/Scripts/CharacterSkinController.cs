using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is just sligtly edited original script for Jammo Character.
// All credits goes to Mix and Jam.
// https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456 
public class CharacterSkinController : MonoBehaviour
{
  public Texture2D[] albedoList;
  
  [ColorUsage(true, true)]
  public Color[] eyeColors;
  
  public enum EyePosition { normal, happy, angry, dead}
  public EyePosition eyeState;

  private Animator animator;
  private Renderer[] characterRenderers;

  void Awake()
  {
    animator = GetComponent<Animator>();
    characterRenderers = GetComponentsInChildren<Renderer>();
  }

  /*
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      //ChangeMaterialSettings(0);
      ChangeEyeOffset(EyePosition.normal);
      ChangeAnimatorIdle("normal");
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      //ChangeMaterialSettings(1);
      ChangeEyeOffset(EyePosition.angry);
      ChangeAnimatorIdle("angry");
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      //ChangeMaterialSettings(2);
      ChangeEyeOffset(EyePosition.happy);
      ChangeAnimatorIdle("happy");
    }
    if (Input.GetKeyDown(KeyCode.Alpha4))
    {
      //ChangeMaterialSettings(3);
      ChangeEyeOffset(EyePosition.dead);
      ChangeAnimatorIdle("dead");
    }
  }
  */

  public void ChangeAnimatorIdle(string trigger)
  {
    animator.SetTrigger(trigger);
  }

  public void ChangeMaterialSettings(int index)
  {
    for (int i = 0; i < characterRenderers.Length; i++)
    {
      var mat = characterRenderers[i].material;
      
      if (characterRenderers[i].transform.CompareTag("PlayerEyes"))
      {
        mat.SetColor("_EmissionColor", eyeColors[index]);
      } 
      else
      {
        mat.SetTexture("_MainTex", albedoList[index]);
      }
    }
  }

  public void ChangeEyeOffset(EyePosition pos)
  {
    Vector2 offset = Vector2.zero;

    switch (pos)
    {
      case EyePosition.normal:
        offset = new Vector2(0, 0);
        break;
      case EyePosition.happy:
        offset = new Vector2(.33f, 0);
        break;
      case EyePosition.angry:
        offset = new Vector2(.66f, 0);
        break;
      case EyePosition.dead:
        offset = new Vector2(.33f, .66f);
        break;
      default:
        break;
    }

    for (int i = 0; i < characterRenderers.Length; i++)
    {
      if (characterRenderers[i].transform.CompareTag("PlayerEyes"))
      {
        characterRenderers[i].material.SetTextureOffset("_MainTex", offset);
      }
    }
  }
}
