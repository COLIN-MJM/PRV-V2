using System;
using TMPro;
using UnityEngine;

public class PlayerKillCount : MonoBehaviour
{
    [SerializeField] private TMP_Text killCountText;
    public int killCount = 0;

    private void Update()
    {
        killCountText.text = killCount.ToString();
    }
}
