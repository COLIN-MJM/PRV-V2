using System;
using TMPro;
using UnityEngine;

public class LanguageShifter : MonoBehaviour
{
    [SerializeField] private string[] textVariants;
    [SerializeField] private SOManager soManager;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        ChangeLanguage(Language.English);
    }

    private void Update()
    {
        ChangeLanguage(soManager.languageHolder.chosenLanguage);
    }

    private void ChangeLanguage(Language language)
    {
        text.text = textVariants[(int)language];
    }
}
