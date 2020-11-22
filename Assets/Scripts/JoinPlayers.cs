using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
  public GameObject playerPrefab;

  void Start()
  {
    var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
    var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
  }
}
