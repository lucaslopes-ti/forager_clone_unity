using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class CheckRecipeIsReady : MonoBehaviour
{
    public Button button;
    public Craft recipe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    public void CheckRecipe()
    {
        // Compara os Craft diretamente, não os itens
        RecipeIsReady r = CoreGame._instance.gameManager.recipes.First(x => x.recipe == recipe);
        button.interactable = r.isReady;
    }
}
