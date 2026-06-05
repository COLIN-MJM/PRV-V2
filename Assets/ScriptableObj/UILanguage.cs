using System;
using TMPro;
using UnityEngine;

public class UILanguage : MonoBehaviour
{
    [SerializeField] private Language language;
    [SerializeField] private SOManager soManager;
    [SerializeField] private TMP_Text text;

    private void Update()
    {
        if (soManager.languageHolder.chosenLanguage != language)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.yellow;
        }
    }

    public void ChangeLanguage()
    {
        soManager.ChangeLanguage(language);
    }
}
