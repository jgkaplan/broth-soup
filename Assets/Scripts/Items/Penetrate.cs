using UnityEngine;
using UnityEngine.UI;

public class Penetrate : BaseItem
{
    public override string Name => "Reinforced Bubbles";

    public override string Description => "Stronger bubbles penetrate enemy but deal less damage";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Sniper");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.projPen += 1;
        PlayerController.instance.projDmg *= 0.9f;
    }
}
