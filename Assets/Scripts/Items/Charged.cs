using UnityEngine;
using UnityEngine.UI;

public class Charged : BaseItem
{
    public override string Name => "Charged bubbles";

    public override string Description => "Longer reloads for more impactful projectiles";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Charged");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.projVelMult *= 1.6f;
        PlayerController.instance.projDmg *= 1.3f;
        PlayerController.instance.cd *= 1.3f;
    }
}
