using UnityEngine;
using UnityEngine.UI;

public class Quick : BaseItem
{
    public override string Name => "Quick Recharge";

    public override string Description => "Recharges your bubble launcher faster!";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/QuickReload");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.cd *= 0.6f;
    }
}
