using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
    [Header("Muzzle")]
    public ParticleSystem muzzleFlash;

    [Header("Impacts")]
    public GameObject metalImpact;
    public GameObject woodImpact;
    public GameObject stoneImpact;

    [Header("Bullet holes")]
    public GameObject metalHole;
    public GameObject woodHole;
    public GameObject stoneHole;

    public void PlayMuzzle()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();
    }

    public void SpawnImpact(RaycastHit hit)
    {
        GameObject prefab = GetImpact(hit);

        if (prefab == null) return;

        Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
    }

    public void SpawnBulletHole(RaycastHit hit)
    {
        GameObject prefab = GetHole(hit);

        if (prefab == null) return;

        Instantiate(
            prefab,
            hit.point + hit.normal * 0.001f,
            Quaternion.LookRotation(-hit.normal)
        );
    }

    GameObject GetImpact(RaycastHit hit)
    {
        int layer = hit.collider.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Metal"))
            return metalImpact;

        if (layer == LayerMask.NameToLayer("Wood"))
            return woodImpact;

        if (layer == LayerMask.NameToLayer("Stone"))
            return stoneImpact;

        return null;
    }

    GameObject GetHole(RaycastHit hit)
    {
        int layer = hit.collider.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Metal"))
            return metalHole;

        if (layer == LayerMask.NameToLayer("Wood"))
            return woodHole;

        if (layer == LayerMask.NameToLayer("Stone"))
            return stoneHole;

        return null;
    }
}