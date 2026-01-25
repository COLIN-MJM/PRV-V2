using UnityEngine;

public class SugarLevel : MonoBehaviour
{
    private SpawnByPlayer spawnByPlayer;
    private Vector3 initialPos;
    private Vector3 velocity = Vector3.up;
    
    void Start()
    {
        spawnByPlayer = GameObject.FindGameObjectWithTag("Ground").GetComponent<SpawnByPlayer>();
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        int currentFoodCount = spawnByPlayer.foodCount;
        int maxFoodCount = spawnByPlayer.maxFoodCount;

        Vector3 targetPos = new Vector3(initialPos.x, (initialPos.y + currentFoodCount), initialPos.z);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref velocity,5f * Time.deltaTime);

    }
}
