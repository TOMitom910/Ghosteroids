using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCamera : MonoBehaviour
{
    
    private Rigidbody2D objectRb;
    private Vector2 objectVelocity;
    
    [SerializeField]
    private Camera mainCamera;
    
    [SerializeField]
    private ShipController shipController;

    private void OnTriggerExit2D(Collider2D objectCollider)
    {
        objectRb = objectCollider.GetComponent<Rigidbody2D>();
        objectVelocity = objectRb.velocity;
        
        if ((objectRb.position.x > 8.5f && objectRb.position.y > 4.5f) ||
            (objectRb.position.x > 8.5f && objectRb.position.y < -4.5f) ||
            (objectRb.position.x < -8.5f && objectRb.position.y > 4.5f) ||
            (objectRb.position.x < -8.5f && objectRb.position.y < -4.5f))
        {
            objectRb.position = new Vector2(-objectRb.position.x, -objectRb.position.y);
        }
        else
        {
            objectRb.position = objectRb.position.y > 5 || objectRb.position.y < -5
                ? objectRb.position = new Vector2(objectRb.position.x, -objectRb.position.y)
                : objectRb.position = new Vector2(-objectRb.position.x, objectRb.position.y);
        }
    }
}
