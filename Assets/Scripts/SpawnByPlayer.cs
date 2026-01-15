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
    
    public int choiceIndex = 0;
    
    private Text text;

    [Header("Outils disponible, max dispo et timer")]
    public int foodCount;
    public int maxFoodCount = 5;
    public float foodTimer;
    public float maxFoodTimer = 5f;
    
    public float zoneCount;
    public float maxZoneCount = 5;
    public float zoneTimer;
    public float maxZoneTimer = 5f;

    public int maxEggCount = 3;
    
    public int eggOneCount;
    
    public int eggTwoCount;
    
    public int eggThreeCount;
    
    public int eggSixCount;


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

        foodCount = maxFoodCount;
        
        currentChoice = choices[0];
        text.text = $"Current Spawn if Right Click: {currentChoice.name}";
    }
    
    private void Update()
    {
        currentChoice = choices[choiceIndex];
        text.text = $"Current Spawn if Right Click: {currentChoice.name}";
        
        OnTimerGetFood();
        OnTimerGetKillZone();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Instantiate(currentChoice, new Vector3(eventData.pointerPressRaycast.worldPosition.x, 0f, eventData.pointerPressRaycast.worldPosition.z), Quaternion.identity);
            if (currentChoice.name == "Food" && foodCount > 0)
            {
                foodCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Alcool" && zoneCount > 0)
            {
                zoneCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs species1" && eggOneCount > 0)
            {
                eggOneCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs species2" && eggTwoCount > 0)
            {
                eggTwoCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs species3" && eggThreeCount > 0)
            {
                eggThreeCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs species6" && eggSixCount > 0)
            {
                eggSixCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Scarecrow")
            {
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
        }
    }

    public void OnTimerGetFood()
    {
        if (foodCount < maxFoodCount)
        {
            foodTimer += Time.deltaTime;
        }

        if (foodTimer >= maxFoodTimer)
        {
            foodTimer = 0;
            foodCount++;
        }

        if (foodCount >= maxFoodCount)
        {
            foodCount = maxFoodCount;
            foodTimer = 0;
        }
        
    }
    
    public void OnTimerGetKillZone()
    {
        if (zoneCount < maxZoneCount)
        {
            zoneTimer += Time.deltaTime;
        }

        if (zoneTimer >= maxZoneTimer)
        {
            zoneTimer = 0;
            zoneCount++;
        }

        if (zoneCount >= maxZoneCount)
        {
            zoneCount = maxZoneCount;
            zoneTimer = 0;
        }
        
    }   
}
