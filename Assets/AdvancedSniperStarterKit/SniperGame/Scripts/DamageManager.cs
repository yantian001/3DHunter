using UnityEngine;

public class DamageManager : MonoBehaviour
{

    public GameObject[] deadbody;
    public AudioClip[] hitsound;
    public int hp = 100;
    public int Score = 10;
    private float distancedamage;
    private bool isDied = false;
    public bool isEnemy = true;

    void Start()
    {

    }

    void Update()
    {
        if (hp <= 0 && !isDied)
        {
            Dead(Random.Range(0, deadbody.Length));
            isDied = true;
        }
    }

    public void ApplyDamage(int damage, Vector3 velosity, float distance)
    {
        if (hp <= 0)
        {
            return;
        }
        distancedamage = distance;
        hp -= damage;
        //Debug.Log(damage);
    }

    public void ApplyDamage(int damage, Vector3 velosity, float distance, int suffix)
    {
        if (hp <= 0)
        {
            return;
        }
        distancedamage = distance;
        hp -= damage;
        if (hp <= 0)
        {
            Dead(suffix);
        }

    }

    public void ApplyDamage(int damage, Vector3 velosity, float distance, int suffix, HitPosition hitPos)
    {
        if (hp <= 0)
        {
            return;
        }
        distancedamage = distance;
        hp -= damage;
        if (hp <= 0)
        {
            Dead(suffix, hitPos);
        }
    }

    public void Dead(int suffix, HitPosition hitPos)
    {
        // throw new NotImplementedException();
        if (isEnemy)
        {
            if (deadbody.Length > 0 && suffix >= 0 && suffix < deadbody.Length)
            {
                // this Object has removed by Dead and replaced with Ragdoll. the ObjectLookAt will null and ActionCamera will stop following and looking.
                // so we have to update ObjectLookAt to this Ragdoll replacement. then ActionCamera to continue fucusing on it.
                GameObject deadReplace = (GameObject)Instantiate(deadbody[suffix], this.transform.position, this.transform.rotation);
                // copy all of transforms to dead object replaced
                CopyTransformsRecurse(this.transform, deadReplace);
                // destroy dead object replaced after 5 sec
                Destroy(deadReplace, 5);
                // destry this game object.
                Destroy(this.gameObject, 1);
                this.gameObject.SetActive(false);

            }
            AfterDead(suffix, hitPos);
        }
        else
        {
            LeanTween.rotateZ(transform.root.gameObject, 90, 0.5f);
        }
    }

    public void AfterDead(int suffix, HitPosition pos = HitPosition.NONE)
    {
      
        EnemyDeadInfo edi = new EnemyDeadInfo();
        edi.score = Score;
        edi.transform = this.transform;
        edi.headShot = suffix == 2;
        edi.hitPos = pos;
        edi.animal = this.GetComponent<Animal>();
        LeanTween.dispatchEvent((int)Events.ENEMYDIE, edi);
    }


    public void Dead(int suffix)
    {
        if (isEnemy)
        {
            if (deadbody.Length > 0 && suffix >= 0 && suffix < deadbody.Length)
            {
                // this Object has removed by Dead and replaced with Ragdoll. the ObjectLookAt will null and ActionCamera will stop following and looking.
                // so we have to update ObjectLookAt to this Ragdoll replacement. then ActionCamera to continue fucusing on it.
                GameObject deadReplace = (GameObject)Instantiate(deadbody[suffix], this.transform.position, this.transform.rotation);
                // copy all of transforms to dead object replaced
                CopyTransformsRecurse(this.transform, deadReplace);
                // destroy dead object replaced after 5 sec
                Destroy(deadReplace, 5);
                // destry this game object.
                Destroy(this.gameObject, 1);
                this.gameObject.SetActive(false);

            }
            AfterDead(suffix);
        }
        else
        {
            LeanTween.rotateZ(transform.root.gameObject, 90, 0.5f);
        }
    }

    // Copy all transforms to Ragdoll object
    public void CopyTransformsRecurse(Transform src, GameObject dst)
    {
        dst.transform.position = src.position;
        dst.transform.rotation = src.rotation;
        foreach (Transform child in dst.transform)
        {
            var curSrc = src.Find(child.name);
            if (curSrc)
            {
                CopyTransformsRecurse(curSrc, child.gameObject);
            }
        }
    }

}
