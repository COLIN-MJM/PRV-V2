using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoice : MonoBehaviour
{
    public InputReader inputReader;
    public SpawnByPlayer spawnByPlayer;
    public List<GameObject> choices;
    public GameObject currentChoice;
    private Text text;
    
    void Awake()
    {
        // Load the Arial font from the Unity Resources folder.
        Font font;
        font = (Font)Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        text = textGO.GetComponent<Text>();
        text.font = font;
        text.text = " ";
        text.fontSize = 48;
        text.alignment = TextAnchor.LowerLeft;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(-450, 575, 0);
        rectTransform.sizeDelta = new Vector2(1000, 200);
    }

    private void Start()
    {
        inputReader = GetComponent<InputReader>();
        spawnByPlayer = GameObject.FindGameObjectWithTag("Ground").GetComponent<SpawnByPlayer>();
        currentChoice = choices[0];
        spawnByPlayer.objectToSpawn = currentChoice;
        text.text = $"Current Spawn if Right Click: {currentChoice.name}";
    }

    private void Update()
    {
        if (inputReader.NumChoice >= 0)
        {
            currentChoice = choices[inputReader.NumChoice];
            spawnByPlayer.objectToSpawn = currentChoice;
            text.text = $"Current Spawn if Right Click: {currentChoice.name}";
        }
    }
}
