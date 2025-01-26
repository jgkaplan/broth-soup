using UnityEngine;

public class BurstEnemy : BasicFollowerEnemy
{
    private bool hasSplit = false;
    public GameObject meatspike;

    public override void InitEnemy(int id=-1)
    {
        base.InitEnemy(id=-1);

        hasSplit = false;
    }

    protected override void RotAndDie()
    {
        if (hasSplit)
        {
            base.RotAndDie();
        }
        else
        {
            Debug.Log("That which is eternal can never die");
            for (int i = 0; i < 2; i ++)
            {
                GameObject newPuffer = EnemyMgr.Instance.GetEnemyFromPool(WaveMgr.Instance.GetBurstEnemyPool());
                newPuffer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newPuffer.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f)*2f,Random.Range(-1f, 1f)*2f,0f);
                newPuffer.GetComponent<BurstEnemy>().InitEnemy();
                newPuffer.GetComponent<BurstEnemy>().SetHasSplit(true);
                newPuffer.SetActive(true);
            }

            hp = basehp;
            SetHasSplit(true);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            baseScale = transform.localScale;

            for (int i = 0; i < 6; i ++)
            {
                Instantiate(meatspike, transform.position, Quaternion.Euler(0f, 0f, i*(360f/6f)));
            }
        }
    }

    public void SetHasSplit(bool b)
    {
        hasSplit = b;
    }
}
