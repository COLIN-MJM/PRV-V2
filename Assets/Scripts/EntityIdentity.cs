using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

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
    public float rotationSpeed;
    public float refreshPathRate;
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
    public float fovAngle;
    public float interactingRadius;
    public float reproducingRadius;
    
    private void OnDrawGizmos()
    {
        // Faudra l'enlever c'est pour éviter dde l'appeller à chaque DrawGizmo dans un autre script
        Handles.color = new Color(0,1,0,0.5f);
        Handles.DrawSolidArc(transform.position, transform.up, Quaternion.AngleAxis(-fovAngle/2f, transform.up) * transform.forward, fovAngle, fovRadius);
        Handles.color = new Color(0,1,0,0.5f);
        fovRadius = Handles.ScaleValueHandle(fovRadius, transform.position + transform.forward * fovRadius, transform.rotation, 3, Handles.SphereHandleCap, 1);
    }
}
