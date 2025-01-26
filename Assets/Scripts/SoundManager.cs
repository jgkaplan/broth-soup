using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource shootBubble;
    public AudioSource playerHurt;
    public AudioSource enemyHurt;
    public AudioSource xpGet;
    public AudioSource levelUp;
    public AudioSource bubblePop;
    public AudioSource crabScream;

    private SoundPool bubblePool;
    private SoundPool enemyHurtPool;
    private SoundPool xpPool;
    private SoundPool bubblePopPool;

    public static SoundManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        bubblePool = new SoundPool(shootBubble, 5);
        enemyHurtPool = new SoundPool(enemyHurt, 5);
        xpPool = new SoundPool(xpGet, 5);
        bubblePopPool = new SoundPool(bubblePop, 5);
    }

    public void PlayPlayerHurt()
    {
        if (SettingsStuff.misophonia) return;
        playerHurt.Play();
    }

    public void PlayXP()
    {
        if (SettingsStuff.misophonia) return;
        xpPool.Play();
    }

    public void PlayLevelUpFinish()
    {
        if (SettingsStuff.misophonia) return;
        levelUp.Play();
    }

    public void PlayShootBubble()
    {
        if (SettingsStuff.misophonia) return;
        bubblePool.Play();
    }

    public void PlayEnemyHurt()
    {
        if (SettingsStuff.misophonia) return;
        enemyHurtPool.Play();
    }

    public void PlayEnemyDie()
    {
        if (SettingsStuff.misophonia) return;
    }

    public void PlayBubblePop()
    {
        if (SettingsStuff.misophonia) return;
        bubblePopPool.Play();
    }

    public void PlayCrabScream()
    {
        playerHurt.Play();
        crabScream.Play();
    }
}
