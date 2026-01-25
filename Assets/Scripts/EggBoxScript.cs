using System;
using Unity.VisualScripting;
using UnityEngine;

public class EggBoxScript : MonoBehaviour
{
    public GameObject ground;
    private SpawnByPlayer eggCounter;

    public int eggNumeralType;
    private int eggCount;
    private float caca;
    private Transform box;
    private MeshRenderer egg1;
    private MeshRenderer egg2;
    
    void Start()
    {
        eggCounter = ground.GetComponent<SpawnByPlayer>();
        
        box = transform.GetChild(0);
        egg1 = box.GetChild(1).GetComponent<MeshRenderer>();
        egg2 = box.GetChild(2).GetComponent<MeshRenderer>();
        
        // Debug.Log(box + " " + egg1 + " " + egg2);
    }

    
    void Update()
    {
        switch (eggNumeralType)
        {
            case 1:
                eggCount = eggCounter.eggOneCount;
                HideEggChild();
                break;
            
            case 2:
                eggCount = eggCounter.eggTwoCount;
                HideEggChild();
                break;

            case 3:
                eggCount = eggCounter.eggThreeCount;
                HideEggChild();
                break;

            case 4:
                eggCount = eggCounter.eggSixCount;
                HideEggChild();
                break;
        }
    }

    void HideEggChild()
    {
        if (eggCount >= 2)
        {
            egg1.enabled = true;
            egg2.enabled = true;
        }

        else if (eggCount == 1)
        {
            egg1.enabled = true;
            egg2.enabled = false;
        }
        
        else if (eggCount == 0)
        {
            egg1.enabled = false;
            egg2.enabled = false;
        }
    }
}
