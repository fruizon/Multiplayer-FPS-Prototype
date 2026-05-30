using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip equipSound;

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootSound);
        Debug.Log("Shoot sound");
    }

    public void PlayReload()
    {
        audioSource.PlayOneShot(reloadSound);
    }

    public void PlayEquip()
    {
        audioSource.PlayOneShot(equipSound);
    }
}