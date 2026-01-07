using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IslandPrefabDataBase : MonoBehaviour
{

    public ResourceLoot[] resourceLV1;
    public ResourceLoot[] resourceLV2;
    public ResourceLoot[] resourceLV3;
    public ResourceLoot[] resourceLV4;
    public ResourceLoot[] resourceLV5;

    public int[] LevelToUpgrade;
    public List<GameObject> resourceIsland = new List<GameObject>();


    public void CreateResourceIsland()
    {
        resourceIsland.Clear();

        for (int i = 0; i < LevelToUpgrade.Length; i++)
        {
            if(CoreGame._instance.gameManager.playerLevel >= LevelToUpgrade[i])
            {
                switch(i)
                {
                    case 0:
                        ResourceLevel(resourceLV1);
                        break;
                    case 1:
                        ResourceLevel(resourceLV2);
                        break;
                    case 2:
                        ResourceLevel(resourceLV3);
                        break;
                    case 3:
                        ResourceLevel(resourceLV4);
                        break;
                    case 4:
                        ResourceLevel(resourceLV5);
                        break;
                }
            }
        }
    }

    private void ResourceLevel(ResourceLoot[] res){
        if (res != null && res.Length > 0)
        {
            foreach (ResourceLoot l in res)
            {
                if (l.resource != null)
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
