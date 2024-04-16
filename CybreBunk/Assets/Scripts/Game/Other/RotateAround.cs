using UnityEngine;

public class RotateAround : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    public GameObject target;
    [SerializeField] float degreesPerSec;

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.transform.position, Vector3.forward, degreesPerSec * Time.deltaTime);
    }
}
