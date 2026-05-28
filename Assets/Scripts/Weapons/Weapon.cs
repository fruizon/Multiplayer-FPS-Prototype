using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Stats")]
    public float range = 100f;
    public int damage = 20;


    [Header("Fire Rate")]
    public float fireRate = 0.09f;
    private float nextFireTime;

    [Header("Spread")]
    public float spread = 0.02f;

    [Header("Recoil")]
    public float recoilKick = 0.15f;
    public float recoilSmooth = 8f;
    private float currentRecoil;
    private float recoilVelocity;
    private Vector3 weaponStartPos;

    [Header("Camera Recoil")]
    public Transform cameraRecoilPivot;

    public float cameraKick = 2f;
    public float cameraReturnSpeed = 8f;
    public float cameraSnappiness = 12f;

    private float currentCameraRecoil;
    private float targetCameraRecoil;
    private float sprayAmount;

    [Header("Impact Effects")]
    public GameObject metalImpact;
    public GameObject woodImpact;
    public GameObject stoneImpact;

    [Header("Bullet Holes")]
    public GameObject metalHole;
    public GameObject woodHole;
    public GameObject stoneHole;


    private enum WeaponState
    {
        Ready,
        Reloading,
        Equiping
    }
    public bool CanSwitch()
    {
        return state == WeaponState.Ready;
    }
    private WeaponState state = WeaponState.Ready;

    private Animator anim;



    void Start()
    {
        weaponStartPos = transform.localPosition;
    }
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        Equip();
    }

    void OnDisable()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Reload();
        HandleShoot();
        HandleRecoil();
        HandleCameraRecoil();

        if (!Input.GetMouseButton(0))
        {
            sprayAmount = Mathf.Lerp(
                sprayAmount,
                0f,
                Time.deltaTime * 5f
            );
        }
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

    void HandleRecoil()
    {
        currentRecoil = Mathf.SmoothDamp(
            currentRecoil,
            0f,
            ref recoilVelocity,
            1f / recoilSmooth
        );

        transform.localPosition =
            weaponStartPos + new Vector3(0f, 0f, -currentRecoil);
    }

    void HandleCameraRecoil()
    {
        targetCameraRecoil = Mathf.Lerp(
            targetCameraRecoil,
            0f,
            Time.deltaTime * cameraReturnSpeed
        );

        currentCameraRecoil = Mathf.Lerp(
            currentCameraRecoil,
            targetCameraRecoil,
            Time.deltaTime * cameraSnappiness
        );

        cameraRecoilPivot.localRotation =
            Quaternion.Euler(-currentCameraRecoil, 0f, 0f);
    }

    void Shoot()
    {
        AddRecoil();

        Vector3 direction = cam.transform.forward;

        direction += cam.transform.right * Random.Range(-spread, spread);
        direction += cam.transform.up * Random.Range(-spread, spread);

        Ray ray = new Ray(cam.transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            SpawnImpact(hit);
            SpawnBulletHole(hit);

            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }

    void AddRecoil()
    {
        currentRecoil += recoilKick;

        targetCameraRecoil += cameraKick;
        sprayAmount += 0.15f;
        sprayAmount = Mathf.Clamp(sprayAmount, 0f, 3f);

        targetCameraRecoil += cameraKick + sprayAmount;
    }

    void SpawnImpact(RaycastHit hit)
    {
        GameObject effect = null;

        int layer = hit.collider.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Metal"))
            effect = metalImpact;
        else if (layer == LayerMask.NameToLayer("Wood"))
            effect = woodImpact;
        else if (layer == LayerMask.NameToLayer("Stone"))
            effect = stoneImpact;

        if (effect != null)
        {
            GameObject impact = Instantiate(
                effect,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            Destroy(impact, 2f);
        }
    }

    void SpawnBulletHole(RaycastHit hit)
    {
        GameObject holePrefab = null;

        int layer = hit.collider.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Metal"))
            holePrefab = metalHole;
        else if (layer == LayerMask.NameToLayer("Wood"))
            holePrefab = woodHole;
        else if (layer == LayerMask.NameToLayer("Stone"))
            holePrefab = stoneHole;

        if (holePrefab != null)
        {
            Quaternion rotation =
                Quaternion.LookRotation(-hit.normal);

            GameObject hole = Instantiate(
                holePrefab,
                hit.point + hit.normal * 0.001f,
                rotation
            );

            Destroy(hole, 20f);
        }
    }


    void SetReady()
    {
        state = WeaponState.Ready;

        anim.enabled = false; // ключевой момент
    }

    public void Reload()
    {
        if (state != WeaponState.Ready) return;

        state = WeaponState.Reloading;

        anim.enabled = true;
        anim.Play("Reload", 0, 0f);
    }

    public void Equip()
    {
        state = WeaponState.Equiping;

        anim.enabled = true;
        anim.Play("Equip", 0, 0f);
    }

    public void OnAnimFinished()
    {
        SetReady();
    }
}