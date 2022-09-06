using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
