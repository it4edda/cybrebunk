using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2   moveSpeed;
    [SerializeField] Transform graphicalChild;
    [SerializeField] bool      canMove = true;
    Vector2                    moveInput;
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
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
        get => moveInput; 
        set => moveInput = value;
    }

}
