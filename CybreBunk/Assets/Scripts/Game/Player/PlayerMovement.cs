using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform graphicalChild;
    [SerializeField] bool canMove = true;
    
    Vector2               moveSpeed;
    Vector2               moveInput;
    PlayerStats           playerStats;
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        moveSpeed   = Vector2.one * playerStats.MovementSpeed;
    }

    //START GET TAROT DATA MS 
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!canMove) return;
        transform.position += new Vector3(moveInput.x * moveSpeed.x * Time.deltaTime, moveInput.y * moveSpeed.y * Time.deltaTime);
        if (moveInput.x != 0) graphicalChild.localScale = (moveInput.x < 0 ? Vector3.left : Vector3.right) + Vector3.up;
    }

    public Vector2 MoveSpeed
    {
        get => moveSpeed; 
        set => moveSpeed = value;
    }

}
