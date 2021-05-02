using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private int speed = 20;

    private float horizontalInput;
    private float verticalInput;
    
    private int horizontalBounds = 120;
    private int verticalBounds = 200;

    void Start()
    {
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if(transform.position.x < -horizontalBounds)
        {
            transform.position = new Vector3(-horizontalBounds, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > horizontalBounds)
        {
            transform.position = new Vector3(horizontalBounds, transform.position.y, transform.position.z);
        }

        if(transform.position.z < -verticalBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -verticalBounds);
        }
        else if(transform.position.z > verticalBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, verticalBounds);
        }
    }
}
