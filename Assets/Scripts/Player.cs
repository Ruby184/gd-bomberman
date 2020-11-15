using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Tilemap tilemap;

    private float movementSpeed = 2f;

    private Rigidbody2D rigidBody;
    private Vector2 inputVector;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = Vector2.ClampMagnitude(inputVector, 1) * movementSpeed;

        // Vector3Int gridPosition = tilemap.WorldToCell(transform.position);
        // Vector3 worldPosition = tilemap.CellToWorld(gridPosition + change) * movementSpeed * Time.deltaTime;
        Vector2 newPosition = rigidBody.position + movementVector * Time.fixedDeltaTime;

        // transform.position = worldPosition;
        rigidBody.MovePosition(newPosition);
    }
}
