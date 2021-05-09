using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Camera camera;
    private GameObject head;

    private bool leftBarrel = true;
    private float barrelXPosLeft = -0.19f;
    private float barrelXPosRight = 0.25f;
    private float barrelYPos = 2.44f;
    private float barrelZPos = 1.60f;

    public GameObject missilePrefab;

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

        if(Input.GetMouseButtonDown(0))
        {
            float barrelXPos = barrelXPosRight;
            if(leftBarrel)
            {
                barrelXPos = barrelXPosLeft;

            }
            leftBarrel = !leftBarrel;

            float headDirection = head.transform.rotation.eulerAngles.y + 90;
            Vector3 headPosition = head.transform.position;
            Vector3 barrel = head.transform.rotation * new Vector3(barrelXPos, barrelYPos, barrelZPos);
            Quaternion missileRotation = Quaternion.Euler(0.0f, headDirection, 90.0f);

            Instantiate(missilePrefab, barrel, missileRotation);
        }
    }
}
