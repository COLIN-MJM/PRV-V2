using UnityEngine;

public class EntityCount : MonoBehaviour
{
    public int entityCount;
    
    public int vanilliCount;
    public int enXCount;
    public int holoCount;
    public int toxiCount;


    
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
    }




}
