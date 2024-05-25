using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystm : MonoBehaviour
{
    [SerializeField] ParticleSystem ChargeEffect;
    [SerializeField] ParticleSystem ChargeEffect2;
    [SerializeField] ParticleSystem ShootEffect;
    [SerializeField] Transform ArrowPos;
    [SerializeField] GameObject arrow;
    [SerializeField] float yPos;
    private bool chargeEffectActivated;
    private bool shootEffectActivated;

    private void Start()
    {
        chargeEffectActivated = false;
        shootEffectActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.instance.isHoldingAttack == true && chargeEffectActivated == false)
        {
            ChargeEffect.Play();
            ChargeEffect2.Play();
            chargeEffectActivated = true;
            if(shootEffectActivated == true)
            {
                ShootEffect.Stop();
                shootEffectActivated = false;
            }
        }
        else if (InputManager.instance.isHoldingAttack == false && shootEffectActivated == false && InputManager.instance.canAttackAgain == false)
        {
            ChargeEffect.Stop();
            ChargeEffect2.Stop();
            chargeEffectActivated = false;
            GameObject arrowPrefab = Instantiate(arrow, ArrowPos.position, Quaternion.identity);
            arrowPrefab.transform.rotation = this.transform.rotation;
            ShootEffect.Play();
            shootEffectActivated = true;
            this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        }
    }

    IEnumerator resetRotation()
    {
        yield return new WaitForSeconds(0.6f);
        
    }
}
