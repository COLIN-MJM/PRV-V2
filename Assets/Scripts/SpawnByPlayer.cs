using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnByPlayer : MonoBehaviour, IPointerClickHandler
{
    [Header("Input Detection")]
    public GameObject gm;
    public InputReader inputReader;
    
    [Header("Choices Related")]
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
        gm = GameObject.FindGameObjectWithTag("GM");
        inputReader = gm.GetComponent<InputReader>();
        
        currentChoice = choices[0];
        text.text = $"Current Spawn if Right Click: {currentChoice.name}";
    }
    
    private void Update()
    {
        if (inputReader.NumChoice >= 0)
        {
            currentChoice = choices[inputReader.NumChoice];
            text.text = $"Current Spawn if Right Click: {currentChoice.name}";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left)
        {
            // Instantiate(currentChoice, new Vector3(eventData.pointerPressRaycast.worldPosition.x, 0f, eventData.pointerPressRaycast.worldPosition.z), Quaternion.identity);
            Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
        }
    }
}
