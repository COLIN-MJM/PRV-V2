// using System;
// using UnityEngine;
//
// public class FoodEating : MonoBehaviour
// {
//     public EntityIdentity entityID;
//     public EntityFOV entityFOV;
//     public Collider[] foodInRange;
//     public GameObject child;
//
//     private void Start()
//     {
//         entityID = GetComponent<EntityIdentity>();
//         entityFOV = GetComponent<EntityFOV>();
//         InvokeRepeating(nameof(EatingCheck), 0f, 0.2f);
//     }
//
//     public void EatingCheck()
//     {
//         foodInRange = Physics.OverlapSphere(transform.position, entityID.interactingRadius);
//         
//         foreach (var entity in foodInRange)
//         {
//             if (entity.CompareTag("Food"))
//             { 
//                 entityFOV.foodWithinFOV.Remove(entity.gameObject);
//                 Instantiate(child, entity.transform.position, Quaternion.identity);
//                 Destroy(entity.gameObject);
//             }
//         }
//     }
// }
//
