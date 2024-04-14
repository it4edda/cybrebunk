using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float     speed;
    [SerializeField] float     zoomSpeed;
    [SerializeField] bool      isStationary;
    [SerializeField] float     shortAspect;
    [SerializeField] float     tallAspect;
    Vector3                    followVector;
    bool                       isShaking = false;
    Camera                     cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (!isShaking && !isStationary) followVector = new Vector3(target.position.x, target.position.y, -10);
        transform.position   = Vector3.Lerp(transform.position, followVector, speed                                * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, isStationary ? tallAspect : shortAspect, zoomSpeed * Time.deltaTime);
    }
    public void CameraShake(float duration) => StartCoroutine(Cam(duration, 0.3f));
    public void CameraShake(float duration, float magnitude) => StartCoroutine(Cam(duration, magnitude)); IEnumerator Cam(float duration, float magnitude)
    {
        float elapsedTime = 0f;
        float startSpeed  = speed;
        isShaking =  true;
        speed     *= 10;

        Vector3 originalFollowVector = followVector;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            followVector = originalFollowVector + new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        followVector = originalFollowVector;
        isShaking = false;
        speed     = startSpeed;
    }
    public void SetStationary()
    {
        isStationary = true;
        followVector = Vector3.forward * -10;

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(followVector, 1);
    }
}
