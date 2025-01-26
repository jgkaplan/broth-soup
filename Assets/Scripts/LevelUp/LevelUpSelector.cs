using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpSelector : MonoBehaviour
{
    [SerializeField]
    private LevelUpButton[] options;
    public TextMeshProUGUI levelUpText;
    public List<string> levelUpLines = new List<string> {
        "Level Up!",
        "Recipe for Success!",
        "Cooking up a Storm!",
        "Moi Bien!",
        "I'm butter and you're spice!",
        "More chefs in the kitchen!"
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenSelector(BaseItem[] items)
    {
        levelUpText.text = levelUpLines[Random.Range(0, levelUpLines.Count)];
        for (int i = 0; i < 3; i++)
        {
            options[i].AssignBaseItem(items[i]);
        }
    }
}
