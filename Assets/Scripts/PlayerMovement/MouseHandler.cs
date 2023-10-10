using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    // horizontal rotation speed
    [SerializeField] private float horizontalSpeed = 1f;
    // vertical rotation speed
     [SerializeField] private float verticalSpeed = 1f;
     [SerializeField] private float xRotation = 0.0f;
     [SerializeField] private float yRotation = 0.0f;
     [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;
 
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
 
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
