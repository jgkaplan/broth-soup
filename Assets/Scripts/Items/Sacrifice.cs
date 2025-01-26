using UnityEngine;
using UnityEngine.UI;

public class Sacrifice : BaseItem
{
    public override string Name => "Sacrifice";

    public override string Description => "Sacrifice your health for significant damage boost";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Sacrifice");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.maxHp -= 4;
        PlayerController.instance.projDmg *= 4f;
    }
}
