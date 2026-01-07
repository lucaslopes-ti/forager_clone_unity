using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IslandPrefabDataBase : MonoBehaviour
{

    public ResourceLoot[] resourceLV1;
    public ResourceLoot[] resourceLV2;
    public List<GameObject> resourceIsland = new List<GameObject>();


    public void CreateResourceIsland()
    {
        resourceIsland.Clear();

        if (resourceLV1.Length > 0)
        {
            foreach (ResourceLoot l in resourceLV1)
            {
                for (int i = 0; i < l.amount; i++)
                {
                    resourceIsland.Add(l.resource);
                }
            }
        }

        if (CoreGame._instance.gameManager.playerLevel >= 2)
        {
            if (resourceLV2.Length > 0)
            {
                
                foreach (ResourceLoot l in resourceLV2)
                {
                    for (int i = 0; i < l.amount; i++)
                    {
                        resourceIsland.Add(l.resource);
                    }
                }
            }
        }
    }
    
}
