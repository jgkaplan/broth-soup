using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem
{
    public abstract string Name
    {
        get;
    }
    public abstract string Description
    {
        get;
    }
    public abstract Sprite Icon
    {
        get;
    }

    public abstract void Load();


    public void LevelUp()
    {
        OnLevelUp();
        GameManager.instance.FinishLevelUp();
    }

    /**
     * What stats should this item change on level up?
     */
    public abstract void OnLevelUp();
}
