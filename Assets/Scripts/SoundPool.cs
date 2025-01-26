using UnityEngine;

public class SoundPool
{
    private AudioSource[] sounds;

    private int curIndex = 0;

    public SoundPool(AudioSource source, int count = 5)
    {
        sounds = new AudioSource[count];
        sounds[0] = source;
        for (int i = 1; i < count; i++)
        {
            sounds[i] = AudioSource.Instantiate(source, SoundManager.instance.transform);
        }
        curIndex = 0;
    }

    public void Play()
    {
        if (sounds[curIndex].isPlaying)
        {
            sounds[curIndex].Stop();
        }
        sounds[curIndex].Play();
        curIndex = (curIndex + 1) % sounds.Length;
    }
}
