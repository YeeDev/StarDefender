using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] Transform[] pathArray = null;

    int coordinateToMove = 0;
    float f;
    
    void Update()
    {
        Vector3 positionWithHeight = pathArray[coordinateToMove].position;
        positionWithHeight.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, positionWithHeight, speed);

        if ((transform.position - positionWithHeight).sqrMagnitude <= 0)
        {
            coordinateToMove++;
            f = 0;
        }

        transform.LookAt(positionWithHeight, Vector3.up);
    }
}
