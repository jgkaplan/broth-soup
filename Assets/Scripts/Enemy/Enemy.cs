using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int id;
    public float basehp;
    protected float hp;
    public float speed;
    public GameObject xp;
    public int dmg;

    public Vector3 baseScale;

    public Sprite[] sprites = new Sprite[2];
    public Sprite[] deathSprites;

    protected bool isDead = false;

    public virtual void InitEnemy(int id = -1)
    {
        if (id >= 0) { this.id = id; }
        hp = basehp;

        baseScale = transform.localScale;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        SoundManager.instance.PlayEnemyHurt();
        if (hp <= 0f)
        {
            RotAndDie();
        }
    }

    protected virtual void RotAndDie()
    {
        // Debug.Log(id.ToString() + " Killed!");
        XP newXp = Instantiate(xp, transform.position, Quaternion.identity).GetComponent<XP>();
        newXp.RandomizeSprite();
        isDead = true;
        StartCoroutine(ExplodeAndDieAndStayDead());
        SoundManager.instance.PlayEnemyDie();
    }

    // It's an improv reference
    IEnumerator ExplodeAndDieAndStayDead()
    {
        GetComponent<SpriteRenderer>().sprite = deathSprites[0];
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = deathSprites[1];
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        EnemyMgr.Instance.ReturnEnemyToPool(gameObject);
    }

    public int GetDamage()
    {
        return dmg;
    }
}
