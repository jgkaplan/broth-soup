using UnityEngine;
using UnityEngine.UI;

public class Burst : BaseItem
{
    public override string Name => "Burst";

    public override string Description => "Many short distance bubbles in big spread";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Burst");
    }

    public override void OnLevelUp()
    {
        PlayerController.instance.projCt = Mathf.CeilToInt(1.5f * PlayerController.instance.projCt);
        PlayerController.instance.projSize *= 0.9f;
        PlayerController.instance.projDmg *= 0.8f;
        PlayerController.instance.spread += 3f;
    }
}
