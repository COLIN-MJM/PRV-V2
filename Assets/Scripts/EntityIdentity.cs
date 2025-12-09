using UnityEngine;
using UnityEngine.Serialization;

public class EntityIdentity : MonoBehaviour
{
    [Header("Status")]
    public Species species;
    public Season matingSeason;
    public State state = State.Idle;
    
    [Header("Strengths & Weaknesses")]
    public Species[] strengthAgainst;
    public Species[] weaknessAgainst;
    public Species[] fightingUpperHandAgainst;
    public Species[] fightingLowerHandAgainst;
    
    [Header("Movement Variables")]
    public float nativeSpeed;
    public float speedModifierWhenChasing;
    public float speedModifierWhenFleeing;
    public float speedModifierWhenFatigued;
    
    [Header("Time Variables")]
    public float enduranceWhenChasing;
    public float enduranceWhenFleeing;
    public float recoveryTime;
    public float reproductionCooldown;

    [Header("Action Radius")]
    public float fovRadius;
    [FormerlySerializedAs("fightingRadius")] public float interactingRadius;
    public float reproducingRadius;
    
    public float refreshPathRate;
}
