using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnByPlayer : MonoBehaviour, IPointerClickHandler
{
    [Header("Input Detection")]
    public GameObject gm;
    public InputReader inputReader;
    private EntityCount entityCount;
    public StudioEventEmitter eventEmitterWrong;
    public StudioEventEmitter eventEmitterRecharge;
    
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
    
    public int zoneCount;
    public int maxZoneCount = 5;
    public float zoneTimer;
    public float maxZoneTimer = 5f;

    public int maxEggCount = 3;
    
    public int eggOneCount;
    private bool eggOneBool = true;
    
    public int eggTwoCount;
    private bool eggTwoBool = true;
    
    
    public int eggThreeCount;
    private bool eggThreeBool = true;
    
    
    public int eggSixCount;
    private bool eggSixBool = true;
    

    private int maxEntityCount = 50;
    private int midEntityCount = 25;
    private int lowEntityCount = 15;
    private int minEntityCount = 7;
    public bool debugText;

    void Awake()
    {
        // Load the Arial font from the Unity Resources folder.
        if (debugText)
        {
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
        

        
    }
    
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        inputReader = gm.GetComponent<InputReader>();
        entityCount = gm.GetComponent<EntityCount>();

        foodCount = maxFoodCount;
        
        currentChoice = choices[0];
        if (debugText)
        {
            text.text = $"Current Spawn if Left Click: {currentChoice.name}";
        }
    }
    
    private void Update()
    {
        currentChoice = choices[choiceIndex];

        if (debugText)
        {
            text.text = $"Current Spawn if Left Click: {currentChoice.name}";
        }
        
        OnTimerGetFood();
        OnTimerGetKillZone();
        ToolUpdater();


        if (entityCount.vanilliCount <= 0 && eggOneBool)
        {
            eggOneBool = false;
            eggOneCount = maxEggCount;
            eventEmitterRecharge.SetParameter("RechargeState", 3);
        }
        else if (entityCount.vanilliCount > 0)
        {
            eggOneBool = true;
            eventEmitterRecharge.SetParameter("RechargeState", 0);
        }
        
        if (entityCount.enXCount <= 0 && eggTwoBool)
        {
            eggTwoBool = false;
            eggTwoCount = maxEggCount;
            eventEmitterRecharge.SetParameter("RechargeState", 3);
        }
        else if (entityCount.enXCount > 0)
        {
            eggTwoBool = true;
            eventEmitterRecharge.SetParameter("RechargeState", 0);
        }
        
        if (entityCount.holoCount <= 0 && eggThreeBool)
        {
            eggThreeBool = false;
            eggThreeCount = maxEggCount;
            eventEmitterRecharge.SetParameter("RechargeState", 3);
        }
        else if (entityCount.holoCount > 0)
        {
            eggThreeBool = true;
            eventEmitterRecharge.SetParameter("RechargeState", 0);
        }
        
        if (entityCount.toxiCount <= 0 && eggSixBool)
        {
            eggSixBool = false;
            eggSixCount = maxEggCount;
            eventEmitterRecharge.SetParameter("RechargeState", 3);
        }
        else if (entityCount.toxiCount > 0)
        {
            eggSixBool = true;
            eventEmitterRecharge.SetParameter("RechargeState", 0);
        }


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
            else if (currentChoice.name == "Eggs Species1" && eggOneCount > 0)
            {
                eggOneCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs Species2" && eggTwoCount > 0)
            {
                eggTwoCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs Species3" && eggThreeCount > 0)
            {
                eggThreeCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Eggs Species6" && eggSixCount > 0)
            {
                eggSixCount--;
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else if (currentChoice.name == "Scarecrow")
            {
                Instantiate(currentChoice, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
            }
            else
            {
                eventEmitterWrong.Play();
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
            eventEmitterRecharge.SetParameter("RechargeState", 1);
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
            eventEmitterRecharge.SetParameter("RechargeState", 2);
        }

        if (zoneCount >= maxZoneCount)
        {
            zoneCount = maxZoneCount;
            zoneTimer = 0;
        }
        
    }

    public void ToolUpdater()
    {
        if (entityCount.entityCount > maxEntityCount)
        {
            maxFoodCount = 0;
            maxZoneCount = 5;
            maxZoneTimer = 1.5f;
        }
        else if (entityCount.entityCount < maxEntityCount && entityCount.entityCount > midEntityCount)
        {
            maxFoodCount = 3;
            maxFoodTimer = 4f;
            maxZoneCount = 3;
            maxZoneTimer = 2.5f;
        }
        else if (entityCount.entityCount < midEntityCount && entityCount.entityCount > lowEntityCount)
        {
            maxFoodCount = 4;
            maxFoodTimer = 3f;
            maxZoneCount = 2;
            maxZoneTimer = 4f;
        }
        else if (entityCount.entityCount < lowEntityCount)
        {
            maxFoodCount = 5;
            maxFoodTimer = 2;
            maxZoneCount = 2;
            maxZoneTimer = 5f;
        }
        else if (entityCount.entityCount < minEntityCount)
        {
            maxFoodCount = 5;
            maxFoodTimer = 1;
            maxZoneCount = 0;
        }
    }
    
}
