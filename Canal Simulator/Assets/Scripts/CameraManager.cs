using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;
    public int currentCamera = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        Keyboard keyboard = Keyboard.current;

        if (gamepad == null) return;

        if(gamepad.triangleButton.wasPressedThisFrame)
        {
            cameras[currentCamera].Priority -= 1;
            if(currentCamera == cameras.Count - 1)
            {
                currentCamera = 0;
            }
            else
            {
                currentCamera += 1;
            }
            cameras[currentCamera].Priority += 1;
            Debug.Log(cameras[currentCamera].name);
        }

        cameras[currentCamera].transform.rotation = new Quaternion(cameras[currentCamera].transform.rotation.x, cameras[currentCamera].transform.rotation.y, 0, cameras[currentCamera].transform.rotation.w);
    }
}
