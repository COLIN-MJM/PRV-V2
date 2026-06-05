using UnityEngine;

public class SOManager : MonoBehaviour
{
    public LanguageHolder languageHolder;

    public void ChangeLanguage(Language language)
    {
        languageHolder.chosenLanguage = language;
    }
}
