using UnityEngine;
using TMPro;

public class UICount : MonoBehaviour
{
    public TextMeshProUGUI egg1;
    public TextMeshProUGUI egg2;
    public TextMeshProUGUI egg3;
    public TextMeshProUGUI egg4;
    public TextMeshProUGUI sugar;
    public TextMeshProUGUI alcool;

    public GameObject ground;
    public SpawnByPlayer spawnByPlayer;
    
    void Start()
    {
        spawnByPlayer = ground.GetComponent<SpawnByPlayer>();
    }

    void Update()
    {
        egg1.text = "Vanilli : " + spawnByPlayer.eggOneCount;
        egg2.text = "Entity X : " + spawnByPlayer.eggTwoCount;
        egg3.text = "Holo : " + spawnByPlayer.eggThreeCount;
        egg4.text = "Toxi : " + spawnByPlayer.eggSixCount;
        sugar.text = "Sugar : " + spawnByPlayer.foodCount;
        alcool.text = "Alcool : " + spawnByPlayer.zoneCount;
    }
}
