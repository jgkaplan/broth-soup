using UnityEngine;
using UnityEngine.UI;

public class Fast : BaseItem
{
    public override string Name => "Faster and Faster";

    public override string Description => "Make your bubbles, and consequently yourself doubly fast";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Fast");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.projVelMult *= 2f;
        PlayerController.instance.cd *= 1.1f;
    }
}
