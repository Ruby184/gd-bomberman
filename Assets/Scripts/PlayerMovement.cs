using UnityEngine;
using UnityEngine.InputSystem;

// TODO: backwards movement is buggy, debug and fix it.
// Script responsible for reacting to controls for movement.
// Inspired by original script for Jammo Character.
// https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456 
[RequireComponent(typeof(CharacterController), typeof(Animator))]
[RequireComponent(typeof(PlayerInput), typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
	private CharacterController controller;
	private Animator animator;
	private Camera cam;
	private PlayerStats playerStats;
	private Vector2 movement;
	private Vector3 velocity;

	public float rotationSpeed = 0.001f;
	public float allowPlayerRotation = 0.1f;
	public bool isGrounded;

	[Header("Animation Smoothing")]

	[Range(0, 1f)]
	public float StartAnimTime = 0.3f;
	[Range(0, 1f)]
	public float StopAnimTime = 0.15f;

	public float movementSpeed => playerStats.GetAbilityValue(Ability.AbilityType.MovementSpeed);

	public void OnMovement(InputAction.CallbackContext ctx)
	{
		movement = ctx.ReadValue<Vector2>();
	}

	void Awake()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		cam = GetComponent<PlayerInput>().camera;
		playerStats = GetComponent<PlayerStats>();
	}

	void Update()
	{
		ApplyGravity();
		InputMagnitude();
  }

	private void ApplyGravity()
	{
		isGrounded = controller.isGrounded;
		
		if (isGrounded && velocity.y < 0) {
			velocity.y = 0f;
		}

		velocity += Physics.gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
	}

	private void InputMagnitude ()
	{
		float speed = movement.sqrMagnitude;

		if (speed > allowPlayerRotation) {
			animator.SetFloat("Blend", speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation();
		} else {
			animator.SetFloat("Blend", speed, StopAnimTime, Time.deltaTime);
		}
	}
	/*
    void PlayerMoveAndRotation() {
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
	}
	*/

  private void PlayerMoveAndRotation()
	{
		var localMovementDirection = new Vector3(movement.x, 0f, movement.y).normalized;

		if (localMovementDirection.magnitude > 0.1f) {
			transform.rotation = Quaternion.Slerp(transform.rotation, CalculateTargetRotation(localMovementDirection), rotationSpeed * Time.deltaTime);
			controller.Move(transform.TransformDirection(localMovementDirection) * movementSpeed * Time.deltaTime);
		}
	}

	private Quaternion CalculateTargetRotation(Vector3 localMovementDirection)
	{
		var flatForward = cam.transform.forward;
		flatForward.y = 0f;
		flatForward.Normalize();

		var cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
		cameraToInputOffset.eulerAngles = new Vector3(0f, cameraToInputOffset.eulerAngles.y, 0f);

		return Quaternion.LookRotation(cameraToInputOffset * flatForward);
	}
}
