using UnityEngine;
using UnityEngine.UI;

public class Cannon : BaseItem
{
    public override string Name => "Cannon";

    public override string Description => "Big and impactful bubbles";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Cannon");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.cd *= 1.25f;
        PlayerController.instance.projSize *= 2f;
        PlayerController.instance.projDmg *= 1.5f;
        PlayerController.instance.spread += 1.5f;
    }
}
