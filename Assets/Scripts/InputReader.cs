using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour
{
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

    public float Zoom
    {
        get
        {
            if (Mouse.current.scroll.ReadValue().y < 0f)
            {
                return 1f;
            }
            else if (Mouse.current.scroll.ReadValue().y > 0f)
            {
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }

    public int NumChoice
    {
        get
        {
            if (Keyboard.current.numpad0Key.isPressed)
            {
                return 0;
            }
            else if (Keyboard.current.numpad1Key.isPressed)
            {
                return 1;
            }
            else if (Keyboard.current.numpad2Key.isPressed)
            {
                return 2;
            }
            else if (Keyboard.current.numpad3Key.isPressed)
            {
                return 3;
            }
            else if (Keyboard.current.numpad4Key.isPressed)
            {
                return 4;
            }
            else if (Keyboard.current.numpad5Key.isPressed)
            {
                return 5;
            }
            else if (Keyboard.current.numpad6Key.isPressed)
            {
                return 6;
            }
            else if (Keyboard.current.numpad7Key.isPressed)
            {
                return 7;
            }
            else if (Keyboard.current.numpad8Key.isPressed)
            {
                return 8;
            }
            else if (Keyboard.current.numpad9Key.isPressed)
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
