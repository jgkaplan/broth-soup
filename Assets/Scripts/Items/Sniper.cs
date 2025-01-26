using UnityEngine;
using UnityEngine.UI;

public class Sniper : BaseItem
{
    public override string Name => "Sniper";

    public override string Description => "Smaller number of stronger and more precise bubbles.";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Sniper");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.spread *= 0.2f;
        PlayerController.instance.projVelMult *= 1.5f;
        PlayerController.instance.projCt = Mathf.Max(1, Mathf.CeilToInt(0.8f * PlayerController.instance.projCt));
        PlayerController.instance.cd *= 0.7f;
        PlayerController.instance.projDmg *= 1.8f;
    }
}
