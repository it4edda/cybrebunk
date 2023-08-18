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
    void FixedUpdate()
    {
        if (!isShaking) followVector = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, followVector, speed * Time.deltaTime);
        //add mouse position thing for better vision
    }
    public void CameraShake(float duration) => StartCoroutine(Cam(duration, 1));
    public void CameraShake(float duration, float magnitude) => StartCoroutine(Cam(duration, magnitude)); IEnumerator Cam(float duration, float magnitude)
    {
        float elapsedTime = 0f;
        float startSpeed  = speed;
        isShaking =  true;
        speed     *= 10;
        while (elapsedTime < duration)
        {
            float xOffset = /*followVector.x +*/ Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = /*followVector.y +*/ Random.Range(-0.5f, 0.5f) * magnitude;
            Debug.Log(xOffset);
            followVector += new Vector3(xOffset,           yOffset,           0);
            elapsedTime  += Time.deltaTime;
        }
        yield return new WaitForSeconds(duration);
        isShaking = false;
        speed        = startSpeed;
        yield return null;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(followVector, 1);
    }
}
