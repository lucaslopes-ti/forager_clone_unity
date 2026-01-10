using UnityEngine;

[CreateAssetMenu(fileName = "new Craft", menuName = "ScriptableObject/craft", order = 2)]

public class Craft : ScriptableObject
{
    public Recipe[] recipes;
    public GameObject produce;
    public int amount;
    public float timeToProduce;
}