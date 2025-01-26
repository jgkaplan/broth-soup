using UnityEngine;
using UnityEngine.UI;

public class Nothing : BaseItem
{
    public override string Name => "Nothing";

    public override string Description => "This upgrade does nothing. Unless...";

    protected Sprite _icon;
    public override Sprite Icon => _icon;

    public override void Load()
    {
        _icon = Resources.Load<Sprite>("ItemCards/Nothing");
    }

    public override void OnLevelUp() { }
}
