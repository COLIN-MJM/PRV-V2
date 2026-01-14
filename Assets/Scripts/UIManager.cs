using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gm;
    public InputReader inputReader;
    
    public List<GameObject> buttons;
    private int lastlyClickedButton = 0;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        inputReader = gm.GetComponent<InputReader>();
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponent<UIActions>().isSelected)
            {
                if (lastlyClickedButton == i)
                {
                    ChangeSelectionByWheel(i);
                }
                else
                {
                    buttons[lastlyClickedButton].GetComponent<UIActions>().isSelected = false;
                    lastlyClickedButton = i;
                }
            }
        }
    }

    private void ChangeSelectionByWheel(int i)
    {
        int newSelect = i + inputReader.Mousewheel;
        if (newSelect > buttons.Count - 1)
        {
            newSelect = 0;
        }
        else if (newSelect < 0)
        {
            newSelect = buttons.Count - 1;
        }
        buttons[newSelect].GetComponent<UIActions>().isSelected = true;
    }
}
