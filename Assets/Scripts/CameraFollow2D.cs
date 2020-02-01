using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform playerTransform;

    public float offsetX;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // // called every frame
    // void Update()
    // {

    // }

    // // called every fixed frame rate
    // void FixedUpdate() 
    // {

    // }

    // caled after update and fixed update 
    void LateUpdate()
    {

        // we store current camera's position in variable temp
        Vector3 temp = transform.position;
        
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;
        temp.y += offsetY;
        temp.x += offsetX;

        transform.position = temp;

    }
}
