using UnityEngine;
using UnityEngine.UI;

public class Stream : BaseItem
{
    public override string Name => "Stream";

    public override string Description => "Many smaller weaker bubbles";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Stream");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.projCt *= 2;
        PlayerController.instance.projSize *= 0.7f;
        PlayerController.instance.projDmg *= 0.6f;
        PlayerController.instance.spread += 1f;
    }
}
