using UnityEngine;

public class AlcoolStock : MonoBehaviour
{
    public GameObject ground;
    private SpawnByPlayer shotCounter;
    private int shotCount;

    private Transform alcool;
    public MeshRenderer[] shots;
    
    void Start()
    {
        shotCounter = ground.GetComponent<SpawnByPlayer>();
        alcool = transform.GetChild(0);
    }
    
    void Update()
    {
        shotCount = shotCounter.zoneCount;
        
        switch (shotCount)
        {
            case 0:
                shots[0].enabled = false;
                shots[1].enabled = false;
                shots[2].enabled = false;
                shots[3].enabled = false;
                shots[4].enabled = false;
                break;
            
            case 1:
                shots[0].enabled = true;
                shots[1].enabled = false;
                shots[2].enabled = false;
                shots[3].enabled = false;
                shots[4].enabled = false;
                break;
            
            case 2:
                shots[0].enabled = true;
                shots[1].enabled = true;
                shots[2].enabled = false;
                shots[3].enabled = false;
                shots[4].enabled = false;
                break;
            case 3:
                shots[0].enabled = true;
                shots[1].enabled = true;
                shots[2].enabled = true;
                shots[3].enabled = false;
                shots[4].enabled = false;
                break;
            
            case 4:
                shots[0].enabled = true;
                shots[1].enabled = true;
                shots[2].enabled = true;
                shots[3].enabled = true;
                shots[4].enabled = false;
                break;
            
            case 5:
                shots[0].enabled = true;
                shots[1].enabled = true;
                shots[2].enabled = true;
                shots[3].enabled = true;
                shots[4].enabled = true;
                break;
        }
    }
}
