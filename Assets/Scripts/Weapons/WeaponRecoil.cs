using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("Refs")]
    public Transform weaponTransform;
    public Transform cameraRecoilPivot;

    [Header("Weapon recoil")]
    public float recoilKick = 0.15f;
    public float recoilSmooth = 8f;

    private float currentRecoil;
    private float recoilVelocity;
    private Vector3 startPos;

    [Header("Camera recoil")]
    public float cameraKick = 2f;
    public float cameraReturnSpeed = 8f;
    public float cameraSnappiness = 12f;

    private float currentCameraRecoil;
    private float targetCameraRecoil;

    private float sprayAmount;

    void Start()
    {
        if (weaponTransform != null) startPos = weaponTransform.localPosition;
    }

    void Update()
    {
        HandleWeaponRecoil();
        HandleCameraRecoil();
    }

    public void AddRecoil()
    {
        currentRecoil += recoilKick;

        targetCameraRecoil += cameraKick;

        sprayAmount += 0.15f;
        sprayAmount = Mathf.Clamp(sprayAmount, 0f, 3f);

        targetCameraRecoil += cameraKick + sprayAmount;
    }

    void HandleWeaponRecoil()
    {
        currentRecoil = Mathf.SmoothDamp(
            currentRecoil,
            0f,
            ref recoilVelocity,
            1f / recoilSmooth
        );

        if (weaponTransform != null)
        {
            weaponTransform.localPosition =
                startPos + new Vector3(0f, 0f, -currentRecoil);
        }
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

        if (cameraRecoilPivot != null)
        {
            cameraRecoilPivot.localRotation =
                Quaternion.Euler(-currentCameraRecoil, 0f, 0f);
        }
    }
}