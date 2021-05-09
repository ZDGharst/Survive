using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Camera camera;
    private GameObject head;

    void Start()
    {
        camera = Camera.main;
        head = GameObject.Find("Head");
    }

    void Update()
    {
        // https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
        Vector3 input = Input.mousePosition;
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(input.x, input.y, camera.transform.position.y));
        mousePosition.y = 0;
        head.transform.LookAt(mousePosition + Vector3.up * head.transform.position.y);
    }
}
