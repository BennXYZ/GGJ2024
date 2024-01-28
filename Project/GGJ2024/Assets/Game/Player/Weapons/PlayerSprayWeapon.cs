using System;
using System.Collections;
using UnityEngine;


public class PlayerSprayWeapon : PlayerAmmunitionWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Mouse0;

    public override bool CanFire => Ammunition >= 9;

    [SerializeField]
    GameObject gasArea;
    [SerializeField]
    ParticleSystem particleEffect;

    [SerializeField]
    AudioSource flowerAudio;

    Coroutine currentCoroutine;

    private void Start()
    {
        currentCoroutine = StartCoroutine(RegenAmmunition());
    }

    public override void FireWeapon()
    {
        gasArea.SetActive(true);
        particleEffect.Play();
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(UseUpAmmunition());
        flowerAudio.volume = 0.3f;
    }

    public override void EndUsingWeapon()
    {
        if (!gasArea.activeSelf)
            return;
            gasArea.SetActive(false);
        particleEffect.Stop();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(RegenAmmunition());
        flowerAudio.volume = 0;
    }

    IEnumerator UseUpAmmunition()
    {
        while(ammunition > 0)
        {
            yield return new WaitForSeconds(0.3f);
            ammunition--;
        }
        if (ammunition == 0)
            EndUsingWeapon();
    }

    IEnumerator RegenAmmunition()
    {
        while (ammunition < 9)
        {
            yield return new WaitForSeconds(1);
            ammunition++;
        }
    }
}
