using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour
{
    private float smoothValue;

    private void Start()
    {
        smoothValue = Input.GetAxis("Mouse ScrollWheel");
    }

    private void Update()
    {
        smoothValue = Input.GetAxis("Mouse ScrollWheel");
    }

    public float HorizontalMove
    { 
        get
        {
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                return 1f;
            }
            else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
    
    public float VerticalMove
    {
        get
        {
            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            {
                return 1f;
            }
            else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }

    public int Mousewheel
    {
        get
        {
            if (smoothValue < 0)
            {
                return -1;
            }
            else if (smoothValue > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public Vector3 MiddleClick
    {
        get
        {
            if (Mouse.current.middleButton.isPressed)
            {
                return Input.mousePosition;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    public int NumChoice
    {
    //     get
    //     {
    //         if (Keyboard.current.numpad0Key.isPressed)
    //         {
    //             return 0;
    //         }
    //         else if (Keyboard.current.numpad1Key.isPressed)
    //         {
    //             return 1;
    //         }
    //         else if (Keyboard.current.numpad2Key.isPressed)
    //         {
    //             return 2;
    //         }
    //         else if (Keyboard.current.numpad3Key.isPressed)
    //         {
    //             return 3;
    //         }
    //         else if (Keyboard.current.numpad4Key.isPressed)
    //         {
    //             return 4;
    //         }
    //         else if (Keyboard.current.numpad5Key.isPressed)
    //         {
    //             return 5;
    //         }
    //         else if (Keyboard.current.numpad6Key.isPressed)
    //         {
    //             return 6;
    //         }
    //         else if (Keyboard.current.numpad7Key.isPressed)
    //         {
    //             return 7;
    //         }
    //         else if (Keyboard.current.numpad8Key.isPressed)
    //         {
    //             return 8;
    //         }
    //         else if (Keyboard.current.numpad9Key.isPressed)
    //         {
    //             return 9;
    //         }
    //         else
    //         {
    //             return -1;
    //         }
    //     }
    // }
    
    get
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                return 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                return 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                return 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                return 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                return 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                return 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                return 6;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                return 7;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                return 8;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                return 9;
            }
            else
            {
                return -1;
            }
        }
    }
}
