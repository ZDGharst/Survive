using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private int speed = 20;

    private float horizontalInput;
    private float verticalInput;
    private float zoomInput;

    private int horizontalBounds = 80;
    private int verticalBounds = 100;
    private int zoomInBound = 7;
    private int zoomOutBound = 30;

    private float normalizePanning;

    void Start()
    {
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        zoomInput = Input.GetAxis("Mouse ScrollWheel") * 7;
        normalizePanning = transform.position.y / 15;

        /* This may seem weird, but it's needed for the z portion of the translate
         * when zooming in/out to be prevented if already at max bounds. */
        if((transform.position.y == zoomInBound && zoomInput > 0) ||
           (transform.position.y == zoomOutBound && zoomInput < 0))
        {
            zoomInput = 0;
        }

        Vector3 movement = new Vector3(horizontalInput, -zoomInput, verticalInput + zoomInput / 1.5f);
        transform.Translate(movement * speed * normalizePanning * Time.deltaTime, Space.World);

        if(Input.GetKeyDown("space"))
        {
            ResetCamera();
        }

        EnforceBounds();
    }

    void EnforceBounds()
    {
        if(transform.position.x < -horizontalBounds)
        {
            transform.position = new Vector3(-horizontalBounds, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > horizontalBounds)
        {
            transform.position = new Vector3(horizontalBounds, transform.position.y, transform.position.z);
        }

        if(transform.position.y < zoomInBound)
        {
            transform.position = new Vector3(transform.position.x, zoomInBound, transform.position.z);
        }
        else if(transform.position.y > zoomOutBound)
        {
            transform.position = new Vector3(transform.position.x, zoomOutBound, transform.position.z);
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

    void ResetCamera()
    {
        transform.position = new Vector3(0, 15, -7);
    }
}
