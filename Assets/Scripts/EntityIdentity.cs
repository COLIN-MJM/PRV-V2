using UnityEngine;
using UnityEngine.Serialization;

public class EntityIdentity : MonoBehaviour
{
    //Status
    public Species species;
    public Gender gender;
    public Season matingSeason;
    public State state = State.Idle;
    
    //Strengths & Weaknesses
    public Species[] strengthAgainst;
    public Species[] weaknessAgainst;
    public Species[] fightingUpperHandAgainst;
    public Species[] fightingLowerHandAgainst;
    
    //Movement Variables
    public float nativeSpeed;
    public float speedModifierWhenChasing;
    public float speedModifierWhenFleeing;
    public float speedModifierWhenFatigued;
    
    //Time Variables
    public float enduranceWhenChasing;
    public float enduranceWhenFleeing;
    public float recoveryTime;
    public float reproductionCooldown;
    
    //Spawn Variables
    public int spawningNumber = 1;
    public int percentageSpawningVariance = 5;
    public int numberOfAllowedChildren = 0;

    //Action Radii
    public float fovRadius;
    [FormerlySerializedAs("fightingRadius")] public float interactingRadius;
    public float reproducingRadius;
    
    public float refreshPathRate;
}
