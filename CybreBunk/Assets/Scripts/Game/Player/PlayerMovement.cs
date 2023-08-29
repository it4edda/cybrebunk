using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float     vms = 5f;
    [SerializeField] float     hms = 5f;
    [SerializeField] Transform graphicalChild;
    float                      horizontalInput;
    float                      verticalInput;
    
    //START GET TAROT DATA MS 
    void Update()
    {
        horizontalInput           = Input.GetAxis("Horizontal");
        verticalInput             = Input.GetAxis("Vertical");

        if (horizontalInput != 0) graphicalChild.localScale = (horizontalInput < 0 ? Vector3.left : Vector3.right) + Vector3.up;
    }
    void FixedUpdate() { transform.position += new Vector3(horizontalInput * hms * Time.deltaTime, verticalInput * vms * Time.deltaTime); }
}
