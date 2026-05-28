using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public WeaponRecoil recoil;
    public WeaponVFX vfx;
    public WeaponAnimator weaponAnimator;

    [Header("Stats")]
    public float range = 100f;
    public int damage = 20;

    [Header("Fire Rate")]
    public float fireRate = 0.09f;
    private float nextFireTime;

    [Header("Spread")]
    public float spread = 0.02f;

    private enum WeaponState
    {
        Ready,
        Reloading,
        Equiping
    }

    private WeaponState state = WeaponState.Ready;

    public bool CanSwitch()
    {
        return state == WeaponState.Ready;
    }

    void OnEnable()
    {
        state = WeaponState.Equiping;
        nextFireTime = 0f;

        weaponAnimator?.PlayEquip();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        HandleShoot();
    }

    void HandleShoot()
    {
        if (state != WeaponState.Ready) return;
        if (!Input.GetMouseButton(0)) return;

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        recoil?.AddRecoil();

        Vector3 direction = cam.transform.forward;
        direction += cam.transform.right * Random.Range(-spread, spread);
        direction += cam.transform.up * Random.Range(-spread, spread);

        Ray ray = new Ray(cam.transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            vfx?.SpawnImpact(hit);
            vfx?.SpawnBulletHole(hit);

            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);
        }

        vfx?.PlayMuzzle();
    }

    public void Reload()
    {
        if (state != WeaponState.Ready)
            return;

        state = WeaponState.Reloading;

        weaponAnimator?.PlayReload();
    }

    public void SetReady()
    {
        state = WeaponState.Ready;
    }
}