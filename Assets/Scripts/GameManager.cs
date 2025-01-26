using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [Serializable]
    public class TimeEvents
    {
        public float timeToHappen;
        public UnityEvent e;
    }
    private readonly BaseItem[] allItems = {
        //new Cooldown(),
        //new MaxHealth(),
        //new Nothing(),
        new Big(),
        new Burst(),
        new Cannon(),
        new Charged(),
        new Fast(),
        new MaxHealth(),
        new Nothing(),
        new Penetrate(),
        new Quick(),
        new Sacrifice(),
        new Sniper(),
        new Stream(),

    };

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private LevelUpSelector levelUpPanel;

    [SerializeField]
    private RectTransform xpBar;
    private float max_progress_width;

    [SerializeField]
    private Image[] hpBar = new Image[6];
    public Sprite[] fullHeartImgs = new Sprite[6];
    public Sprite[] halfHeartImgs = new Sprite[6];

    public Sprite[] frothSprites = new Sprite[4];
    private int frothTimer = 1;
    private int frothCt = 0;
    public Image frothVignette;

    [Header("Events")]
    public TimeEvents[] events;
    private float gameTimer = 0f;
    private int eventPointer = 0;

    private int xp = 0;
    private int level = 1;
    private int xp_needed_for_level = 5;
    enum GameState
    {
        Playing,
        LevelingUp
    }

    private GameState state = GameState.Playing;

    public static GameManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        gameTimer = 0;
        eventPointer = 0;
        xp = 0;
        level = 1;
        state = GameState.Playing;
        max_progress_width = xpBar.sizeDelta[0];
        xpBar.sizeDelta = new(0, 10);
        Array.Sort(events, (a, b) => a.timeToHappen.CompareTo(b.timeToHappen));
        foreach (BaseItem i in allItems)
        {
            i.Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                UpdatePlaying();
                break;
            default:
                Time.timeScale = 0f;
                break;
        }
    }
    // float xpTimer = 0;
    void UpdatePlaying()
    {
        // xpTimer += Time.deltaTime;
        // if (xpTimer >= 0.5f)
        // {
        //     CollectXP(1);
        //     xpTimer = 0;
        // }
        if (eventPointer >= events.Length) return;
        gameTimer += Time.deltaTime;
        timerText.text = $"{TimeSpan.FromSeconds(gameTimer):m\\:ss}";
        while (eventPointer < events.Length && events[eventPointer].timeToHappen <= gameTimer)
        {
            events[eventPointer].e.Invoke();
            eventPointer++;
        }

        frothTimer++;
        if (frothTimer % 480 == 0)
        {
            frothVignette.sprite = frothSprites[frothCt];
            frothTimer = 0;
            frothCt = (frothCt + 1) % 4;
        }
    }

    public bool IsPlaying()
    {
        return state == GameState.Playing;
    }

    public void Win()
    {
        SceneManager.LoadScene("You Win");
    }

    public void Lose()
    {
        SceneManager.LoadScene("You Died");
    }

    public void CollectXP(int amount)
    {
        SoundManager.instance.PlayXP();
        xp += amount;
        xpBar.sizeDelta = new(Mathf.Min(1, (float)xp / xp_needed_for_level) * max_progress_width, 10);
        if (xp >= xp_needed_for_level)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        state = GameState.LevelingUp;
        level += 1;

        BaseItem[] levelUpItems = new BaseItem[3];
        for (int i = 0; i < 3; i++)
        {
            int r = UnityEngine.Random.Range(i, allItems.Length);
            levelUpItems[i] = allItems[r];
            (allItems[r], allItems[i]) = (allItems[i], allItems[r]);
        }


        levelUpPanel.gameObject.SetActive(true);
        levelUpPanel.OpenSelector(levelUpItems);
    }

    public void FinishLevelUp()
    {
        state = GameState.Playing;
        levelUpPanel.gameObject.SetActive(false);
        xp = Mathf.Max(0, xp - xp_needed_for_level);
        xp_needed_for_level += 10;
        xpBar.sizeDelta = new(Mathf.Min(1, (float)xp / xp_needed_for_level) * max_progress_width, 10);
        SoundManager.instance.PlayLevelUpFinish();
        if (xp >= xp_needed_for_level)
        {
            LevelUp();
        }
    }

    public void PlayerHPChange(int hp)
    {
        foreach (Image heart in hpBar)
        {
            heart.gameObject.SetActive(false);
        }
        for (int i = 0; i < hpBar.Length * 2; i++)
        {
            if (i < hp)
            {
                if (i % 2 == 0)
                {
                    hpBar[i / 2].sprite = halfHeartImgs[i / 2];
                    hpBar[i / 2].gameObject.SetActive(true);
                }
                else
                {
                    hpBar[i / 2].sprite = fullHeartImgs[i / 2];
                }
            }
        }
    }
}
