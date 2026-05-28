using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator anim;
    private Weapon weapon;

    void Start()
    {
        anim = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();
    }

    public void PlayEquip()
    {
        Play("Equip");
    }

    public void PlayReload()
    {
        Play("Reload");
    }

    void Play(string animName)
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        anim.enabled = true;
        anim.Play(animName, 0, 0f);
    }

    // Animation Event
    public void OnAnimationFinished()
    {
        anim.enabled = false;

        weapon.SetReady();
    }
}