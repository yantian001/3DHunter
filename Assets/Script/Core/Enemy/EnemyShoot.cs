using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyShoot : MonoBehaviour
{

    public GameObject bullet;

    public Transform targetTranform;

    public Transform firePosition;

    public float spread;

    public GameObject muzzleEffect;

    public AudioClip shootAudio;

    public void Start()
    {
        targetTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //public void Fire()
    //{
    //    if(bullet && targetTranform)
    //    {
    //        var b = (GameObject)GameObject.Instantiate(bullet, firePosition.position, Quaternion.LookRotation(targetTranform.position));
    //        //b.transform.SetParent(transform);
    //        // b.
    //        // b.GetComponent<Rigidbody>().velocity = b.transform.TransformDirection(firePosition.position - targetTranform.position);
    //        b.GetComponent<AS_Bullet>().source = transform;
    //        b.transform.forward =(targetTranform.position- firePosition.position + new Vector3(Random.Range(-spread,spread), Random.Range(-spread, spread), Random.Range(-spread, spread))).normalized;
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="spr"></param>
    //public void Fire(float spr)
    //{
    //    if (bullet && targetTranform)
    //    {
    //        var b = (GameObject)GameObject.Instantiate(bullet, firePosition.position, Quaternion.LookRotation(targetTranform.position));
    //        //b.transform.SetParent(transform);
    //        // b.
    //        // b.GetComponent<Rigidbody>().velocity = b.transform.TransformDirection(firePosition.position - targetTranform.position);
    //        b.GetComponent<AS_Bullet>().source = transform;
    //        b.transform.forward = (targetTranform.position - firePosition.position + new Vector3(Random.Range(-spr, spr), Random.Range(-spr, spr), Random.Range(-spr, spr))).normalized;
    //    }
    //}

    public void Fire(SharedGenericVariable[] vals)
    {
        if (bullet && targetTranform)
        {
            var b = (GameObject)GameObject.Instantiate(bullet, firePosition.position, Quaternion.LookRotation(targetTranform.position));
            //b.transform.SetParent(transform);
            // b.
            // b.GetComponent<Rigidbody>().velocity = b.transform.TransformDirection(firePosition.position - targetTranform.position);
            var asbullet = b.GetComponent<AS_Bullet>();
            asbullet.source = firePosition.root.transform;
            asbullet.Damage = Mathf.RoundToInt( ConvertUtil.ToFloat( vals[1].Value.value.GetValue()));
            float spr = ConvertUtil.ToFloat( vals[0].Value.value.GetValue());
            b.transform.forward = (targetTranform.position - firePosition.position + new Vector3(Random.Range(-spr, spr), Random.Range(-spr, spr), Random.Range(-spr, spr))).normalized;
        }

        if(muzzleEffect && firePosition)
        {
            var muzzle = (GameObject)GameObject.Instantiate(muzzleEffect, firePosition.position, firePosition.rotation);
            muzzle.transform.SetParent(firePosition);
            muzzle.transform.localScale = new Vector3(10, 10, 10);
            Destroy(muzzle, 0.1f);
        }

        if(shootAudio)
        {
            LeanAudio.play(shootAudio, transform.position);
        }
    }
}
