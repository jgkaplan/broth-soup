using UnityEngine;
using UnityEngine.UI;

public class MaxHealth : BaseItem
{
    public override string Name => "Max Health";

    public override string Description => "Increase your max health by 2";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/MaxHealth");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.maxHp += 2;
        PlayerController.instance.hp += 2;
    }
}
