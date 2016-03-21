using UnityEngine;
using System.Collections;

public class GunHanddle : MonoBehaviour
{
    public Camera FPScamera;
    public Gun[] Guns;
    public int GunIndex = -1;
    [HideInInspector]
    public Gun CurrentGun;

    bool isFirstFire = false;

    void OnEnable()
    {
        if (Guns.Length < 1)
        {
            Guns = this.gameObject.GetComponentsInChildren<Gun>();
        }
        for (int i = 0; i < Guns.Length; i++)
        {
            if (FPScamera)
                Guns[i].NormalCamera = FPScamera;
            Guns[i].fovTemp = FPScamera.fieldOfView;
            if(Guns[i].positionTemp == Vector3.zero)
            {
                Guns[i].positionTemp = Guns[i].transform.localPosition;
            }
        }
        SwitchGun();
        LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);
    }


    void OnGameFinish(LTEvent evt)
    {
        if (evt.data != null)
        {
            GameRecords record = evt.data as GameRecords;
            if (record != null)
            {
                if (record.FinishType == GameFinishType.Failed)
                {
                    //hide gun
                    for(int i= 0;i<Guns.Length;i++)
                    {
                        Guns[GunIndex].SetActive(false);
                    }
                }
            }
        }
    }

    void Hide(GameObject gameObject, bool show)
    {
        /*Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
    	foreach (Renderer r in renderers) {
        	r.enabled = show;
    	}*/
    }

    public void Zoom()
    {
        if (CurrentGun)
            CurrentGun.Zoom();
    }
    public void ZoomToggle()
    {
        if (CurrentGun)
            CurrentGun.ZoomToggle();
    }

    public void Reload()
    {
        if (CurrentGun)
            CurrentGun.Reload();
    }

    public void ZoomAdjust(int delta)
    {
        if (CurrentGun)
            CurrentGun.ZoomDelta(delta);
    }
    public void OffsetAdjust(Vector2 delta)
    {
        if (CurrentGun)
            CurrentGun.OffsetAdjust(delta);
    }
    public void SwitchGun(int index)
    {
        if (FPScamera.enabled)
        {
            for (int i = 0; i < Guns.Length; i++)
            {
                Hide(Guns[i].gameObject, false);
                Guns[i].SetActive(false);
            }
            if (Guns.Length > 0 && index < Guns.Length && index >= 0)
            {
                GunIndex = index;
                CurrentGun = Guns[GunIndex].gameObject.GetComponent<Gun>();
                Hide(Guns[GunIndex].gameObject, true);
                Guns[GunIndex].SetActive(true);
            }
        }
    }

    public void SwitchGun()
    {
        //  int count = 0;
        int index = GunIndex + 1;
        while (true)
        {
            if (index >= Guns.Length)
                index =index - Guns.Length;
            if (Player.CurrentUser.IsGunActived(Guns[index].id))
            {
                SwitchGun(index);
                break;
            }
            index++;
        }
    }

    public void Shoot()
    {
        if (CurrentGun)
        {
            CurrentGun.Shoot();
            isFirstFire = true;
            if (isFirstFire)
            {
                BehaviorDesigner.Runtime.GlobalVariables.Instance.SetVariableValue("PlayerFired", true);
            }
        }
    }

    public void HoldBreath(int noiseMult)
    {
        if (CurrentGun)
            CurrentGun.FPSmotor.Holdbreath(noiseMult);
    }
}
