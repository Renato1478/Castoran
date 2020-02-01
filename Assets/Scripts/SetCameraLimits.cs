using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraLimits : MonoBehaviour
{
    [SerializeField] private FrameLimits limits;
    
    private Transform transform;
    private Transform transformPlayer;
    private Camera cam;
    private float offSetX;
    private float offSetY;

    private void Awake ()
    {
        transform = GetComponent<Transform>();
        cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
    }

    private void LateUpdate()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        float boundRight = transform.position.x + width/2;
        float boundTop = transform.position.x + height/2;
        float boundLeft = transform.position.x - width/2;
        float boundBottom = transform.position.x - height/2;

        if ( boundRight > limits.rightLimit ) {
            transform.position = new Vector2(limits.rightLimit - width/2, transform.position.y);
        } else if ( boundLeft < limits.leftLimit ) {
            transform.position = new Vector2(limits.leftLimit + width/2, transform.position.y);
        }
    }
}
