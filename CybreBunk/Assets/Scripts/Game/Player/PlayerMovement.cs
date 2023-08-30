using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2   moveSpeed;
    [SerializeField] Transform graphicalChild;
    Vector2                    moveInput;
    
    //START GET TAROT DATA MS 
    void Update()
    {
        moveInput       = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput.x != 0) graphicalChild.localScale = (moveInput.x < 0 ? Vector3.left : Vector3.right) + Vector3.up;
    }
    void FixedUpdate() { transform.position += new Vector3(moveInput.x * moveSpeed.x * Time.deltaTime, moveInput.y * moveSpeed.y * Time.deltaTime); }
}
