using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class IslandManager : MonoBehaviour
{
    private IslandPrefabDataBase dataBase;
    public IslandSlotGrid[] slot;
    public int initialResource;
    public int maxResource;

    void Start()
    {
        dataBase = GetComponent<IslandPrefabDataBase>();
        dataBase.CreateResourceIsland();
        slot = GetComponentsInChildren<IslandSlotGrid>();
        if (initialResource > 0 && dataBase.resourceIsland.Count > 0)
        {
            for (int i = 0; i < initialResource; i++)
            {
                NewResource();
            }
        }

        StartCoroutine(SpawnResource());
    }

    void NewResource()
    {
        int idSlot = UnityEngine.Random.Range(0, slot.Length);
        IslandSlotGrid s = slot[idSlot];

        if (s.isBusy == false)
        {
            if (CoreGame._instance.gameManager.PlayerDistance(s.transform.position) == true)
            {
                int idResource = UnityEngine.Random.Range(0, dataBase.resourceIsland.Count);
                GameObject resource = Instantiate(dataBase.resourceIsland[idResource]);
                resource.GetComponent<Mine>().SetSlot(s);
                s.Busy(true);
            }
            else
            {
                NewResource();
            }
        }
        else if (s.isBusy == true)
        {
            NewResource();
        }

    }

    IEnumerator SpawnResource()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoreGame._instance.gameManager.timeToSpawnResource);
            int count = slot.Where(s => s.isBusy == true).Count();
            if (count < maxResource)
            {
                NewResource();
            }
        }
    }

    public void CraftMode()
    {
        StopCoroutine(SpawnResource());
        foreach (IslandSlotGrid s in slot)
        {
            if (s.isBusy == false)
            {
                s.ShowBorder(true);
            }
        }
    }

    public void GameplayMode()
    {
        foreach (IslandSlotGrid s in slot)
        {
            s.ShowBorder(false);
        }

        StartCoroutine(SpawnResource());
    }

}
