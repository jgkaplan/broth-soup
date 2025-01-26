using UnityEngine;
using UnityEngine.Audio;

public class SettingsStuff : MonoBehaviour
{
    public static bool misophonia = false;
    public static bool invertControls = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToggleSounds(bool on)
    {
        misophonia = on;
    }

    public void ToggleControls(bool on)
    {
        invertControls = on;
    }
}
