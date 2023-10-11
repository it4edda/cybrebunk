using System;
using System.Collections;

using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float     speed;
    Vector3                    followVector;
    bool                       isShaking = false;
    void LateUpdate()
    {
        if (!isShaking) followVector = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, followVector, speed * Time.deltaTime);
        //add mouse position thing for better vision
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(followVector, 1);
    }
}
