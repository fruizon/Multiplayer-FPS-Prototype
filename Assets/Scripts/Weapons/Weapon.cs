using Photon.Pun;
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

    [Header("Ammo")]
    public int magazineSize = 30;
    public int currentAmmo = 30;
    public int reserveAmmo = 90;





    public WeaponAudio audioManager;

    private PhotonView photonView;



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

        audioManager?.PlayEquip();
        weaponAnimator?.PlayEquip();
    }
    void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
    }

    void Awake()
    {
        if (audioManager == null)
            audioManager = GetComponent<WeaponAudio>();
    }

    void Update()
    {
        // if (!photonView.IsMine) return;


        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        HandleShoot();
    }

    void HandleShoot()
    {
        if (state != WeaponState.Ready)
            return;

        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        audioManager.PlayShoot();


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
            {
                PhotonView targetPV = health.GetComponent<PhotonView>();

                if (targetPV != null)
                {
                    targetPV.RPC("TakeDamageRPC", RpcTarget.All, damage);
                }
            }
        }

        vfx?.PlayMuzzle();
    }

    public void Reload()
    {
        if (state != WeaponState.Ready)
            return;

        if (currentAmmo >= magazineSize)
            return;

        if (reserveAmmo <= 0)
            return;

        state = WeaponState.Reloading;
        audioManager?.PlayReload();
        weaponAnimator.PlayReload();
    }

    public void FinishReload()
    {
        int neededAmmo = magazineSize - currentAmmo;

        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        weaponAnimator.anim.enabled = false;

        SetReady();
    }

    public void SetReady()
    {
        state = WeaponState.Ready;
    }
}