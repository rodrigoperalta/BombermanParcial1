using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
   
    public Transform target;
    public float distance;
    public float height;
    private Transform cameraTransform;    
    public float sensHorizontal = 10.0f; //Camera movement Sensibilty
    public float sensVertical = 10.0f; //Camera movement Sensibilty
    public float rotationX = 0;
    public float minimunVert = -45.0f; //Limits camera Movement
    public float maximumVert = 45.0f; //Limits camera Movement



    void Start () {
        if (Player.Get().IsFPSActive() == false)
        {
            cameraTransform = transform;
        }
            
	}
   

    private void LateUpdate()
    {
        if (Player.Get().IsFPSActive() == true) //Switches to FPS camera
        {
                transform.position = Player.Get().transform.position;                
                rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                rotationX = Mathf.Clamp(rotationX, minimunVert, maximumVert); //Limits the camera movement
                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
        if (Player.Get().IsFPSActive() == false) //Switch to Third Person Camera
        {
            cameraTransform.position = new Vector3(target.position.x - distance, target.position.y + height, target.position.z);
            cameraTransform.LookAt(target);
        }
        
    }

   

}
