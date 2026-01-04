using UnityEngine;

public class OnHungerFull : MonoBehaviour
{
    public float hungerBar;
    public float maxHungerBar;

    public GameObject ownSpecies;
    
    void Update()
    {
        if (hungerBar > maxHungerBar)
        {
            hungerBar = 0;
            
            Vector3 rdPos = UnityEngine.Random.insideUnitCircle;
            rdPos = new Vector3(rdPos.x * 4, 0, rdPos.z * 4);
            int rdPercentage = UnityEngine.Random.Range(0, 100);

            Instantiate(ownSpecies, transform.position + rdPos, transform.rotation);
        }
    }
}
