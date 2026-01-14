using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> buttons;
    private int lastlyClickedButton = 0;

    private void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponent<UIActions>().isSelected)
            {
                if (lastlyClickedButton != i)
                {
                    buttons[lastlyClickedButton].GetComponent<UIActions>().isSelected = false;
                    lastlyClickedButton = i;
                }
            }
        }
    }
}
