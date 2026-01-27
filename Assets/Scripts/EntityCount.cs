using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityCount : MonoBehaviour
{
    public int entityCount;
    
    public int vanilliCount;
    public int dyingVanilliCount;
    public int enXCount;
    public int dyingEnXCount;
    public int holoCount;
    public int dyingHoloCount;
    public int toxiCount;
    public int dyingToxiCount;
    
    [SerializeField] private TMP_Text vanilliCountText;
    [SerializeField] private TMP_Text vanilliDeathText;
    [SerializeField] private TMP_Text enXCountText;
    [SerializeField] private TMP_Text enXDeathText;
    [SerializeField] private TMP_Text holoCountText;
    [SerializeField] private TMP_Text holoDeathText;
    [SerializeField] private TMP_Text toxiCountText;
    [SerializeField] private TMP_Text toxiDeathText;

    // LES +1 ON START SONT FAIT DANS LE SCRIPT STATECHECKER
    // LES -1 SONT FAIT DANS LES SCRIPTS APPROPRIE POUR CHAQUE MORT POSSIBLE, PAR EXEMPLE : LE SCRIPT INTERACT
    
    public void EntityCountMod(int value)
    {
        entityCount += value;
    }
    
    public void VanilliCountMod(int value)
    {
        vanilliCount += value;
    }

    public void EnXCountMod(int value)
    {
        enXCount += value;
    }
    
    public void HoloCountMod(int value)
    {
        holoCount += value;
    }

    public void ToxiCountMod(int value)
    {
        toxiCount += value;
    }

    public void Update()
    {
        if (entityCount < 0)
        {
            entityCount = 0;
        }
        
        if (vanilliCount < 0)
        {
            vanilliCount = 0;
        }
        
        if (enXCount < 0)
        {
            enXCount = 0;
        }
        
        if (holoCount < 0)
        {
            holoCount = 0;
        }
        
        if (toxiCount < 0)
        {
            toxiCount = 0;
        }
        
        vanilliCountText.text = vanilliCount.ToString();
        enXCountText.text = enXCount.ToString();
        holoCountText.text = holoCount.ToString();
        toxiCountText.text = toxiCount.ToString();
        vanilliDeathText.text = dyingVanilliCount.ToString();
        enXDeathText.text = dyingEnXCount.ToString();
        holoDeathText.text = dyingHoloCount.ToString();
        toxiDeathText.text = dyingToxiCount.ToString();
    }

    public void CountUpdate(EntityIdentity objectEntityID, int value)
    {
        
        EntityCountMod(value);
        
        if (objectEntityID.species == Species.S1)
        {
            VanilliCountMod(value);
        }
        else if (objectEntityID.species == Species.S2)
        {
            EnXCountMod(value);
        } 
        else if (objectEntityID.species == Species.S3)
        {
            HoloCountMod(value);
        }
        else if (objectEntityID.species == Species.S6)
        {
            ToxiCountMod(value);
        }

        if (value < 0)
        {
            if (objectEntityID.species == Species.S1)
            {
                dyingVanilliCount -= value;
            }
            else if (objectEntityID.species == Species.S2)
            {
                dyingEnXCount -= value;
            } 
            else if (objectEntityID.species == Species.S3)
            {
                dyingHoloCount -= value;
            }
            else if (objectEntityID.species == Species.S6)
            {
                dyingToxiCount -= value;
            }
        }
    }




}
