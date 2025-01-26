using UnityEngine;
using UnityEngine.UI;

public class Big : BaseItem
{
    public override string Name => "Slow and Steady";

    public override string Description => "Extra health and slower shots for bigger impact";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/SlowAndSteady");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.maxHp += 2;
        PlayerController.instance.hp += 2;
        PlayerController.instance.projVelMult *= 0.8f;
        PlayerController.instance.projSize *= 1.5f;
        PlayerController.instance.cd *= 1.15f;
    }
}