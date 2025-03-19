using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Controller : MonoBehaviour
{
    [Header("MotionState")]
    [SerializeField] private Sprite covereringSprite;
    [SerializeField] private Sprite shootingSprite;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Cursor")]
    [SerializeField, Range(0.1f, 2f)] private float cursorSpeed = 1f;
    [SerializeField] private bool cursorVisible = true;
    Vector3 initialMousePosition = Vector3.zero;

    [Header("Crosshair")]
    [SerializeField] private Image crosshair;
    private Color crosshairColor;

    [Header("Camera")]
    [SerializeField] public Camera cam;
    private PostProcessVolume blurVolume;

    [Header("FakeRecoil")]
    private float recoilDuration = 0.01f;
    private float recoilMagnitude = 0.1f;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Fire")]
    [SerializeField] private bool canFire = true;
    [SerializeField] private bool isCC = false;

    [Header("Switch Gun")]
    private bool canSwitchGun = true;
    private float gunSwitchCooldown = 0.25f;

    [Header("References")]
    [SerializeField] private MissionGear missionGear;
    [SerializeField] private GunBehaviour gunBehave;
    [SerializeField] private int currentWeaponSlot = 0;

    [Header("CoverSystem")]
    private float coverT = 0;
    [SerializeField] private float coverSpeed = 5f;
    private bool inTransit = false;

    private void OnEnable()
    {
        playerInput.enabled = true;
    }

    private void OnDisable()
    {
        playerInput.enabled = false;
    }

    private void Start()
    {
        InputSetup();
        transform.position = CoverManager.Instance.GetCoverCenter();

        if (missionGear.GetGunInSlot(currentWeaponSlot)){
            gunBehave.SetCurrentGun(missionGear.GetGunInSlot(currentWeaponSlot));
            InGameUiManager.Instance.AnimateGunSlots(false, currentWeaponSlot);
            InGameUiManager.Instance.SyncAmmo(currentWeaponSlot,
                missionGear.GetGunInSlot(currentWeaponSlot).currentAmmo,
                missionGear.GetGunInSlot(currentWeaponSlot).currentMags);
        }
        else Debug.LogWarning("No guns equipped");


        crosshairColor = crosshair.color;

        Cursor.visible = cursorVisible;
        Cursor.lockState = CursorLockMode.Confined;
        initialMousePosition = new Vector3(Screen.width / 2, Screen.height / 2, -5);
        crosshair.transform.position = initialMousePosition;

        blurVolume = cam.gameObject.GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (IsCursorWithinScreen())
        {
            if (Time.timeScale == 0) MoveCursor(1);
            else MoveCursor(cursorSpeed);
        }
        else { StopShooting(); }
    }

    private void MoveCursor(float mouseSpeed)
    {
        Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
        Vector3 sensitivityAdjustedDelta = mouseDelta * mouseSpeed;
        Vector3 newCrosshairPosition = crosshair.transform.position + sensitivityAdjustedDelta;

        newCrosshairPosition.x = Mathf.Clamp(newCrosshairPosition.x, 0, Screen.width);
        newCrosshairPosition.y = Mathf.Clamp(newCrosshairPosition.y, 0, Screen.height);

        crosshair.transform.position = newCrosshairPosition;
        initialMousePosition = Input.mousePosition;
    }

    private void MouseCursorAlign()
    {
        crosshair.transform.position = Input.mousePosition;
    }

    private void MouseCursorColor(bool toCursor)
    {
        if (toCursor) crosshair.color = crosshairColor;
        else crosshair.color = new Color(1.0f - crosshairColor.r, 1.0f - crosshairColor.g, 1.0f - crosshairColor.b, crosshairColor.a);
    }

    private bool IsCursorWithinScreen()
    {
        return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
               Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;
    }

    #region Input Actions

    private void InputSetup()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Exit"].performed += ctx => ExitMenu();
        playerInput.actions["Pause"].performed += ctx => ExitMenu();

        playerInput.actions["Shoot"].started += ctx => StartShooting();
        playerInput.actions["Shoot"].canceled += ctx => StopShooting();
        playerInput.actions["Reload"].performed += ctx => gunBehave.Reload();

        playerInput.actions["GoToLeftCover"].performed += ctx => SwitchCover(ECoverDirection.Left);
        playerInput.actions["GoToRIghtCover"].performed += ctx => SwitchCover(ECoverDirection.Right);

        playerInput.actions["SwitchWeapon1"].performed += ctx => SwitchWeapon(0);
        playerInput.actions["SwitchWeapon2"].performed += ctx => SwitchWeapon(1);
        playerInput.actions["SwitchWeapon3"].performed += ctx => SwitchWeapon(2);

        playerInput.actions["QAbility"].started += ctx => CastAbility(EAbilityKey.Q);
        playerInput.actions["WAbility"].started += ctx => CastAbility(EAbilityKey.W);
        playerInput.actions["EAbility"].started += ctx => CastAbility(EAbilityKey.E);


        playerInput.actions["QAbility"].canceled += ctx => CancelAbilityCast(EAbilityKey.Q);
        playerInput.actions["WAbility"].canceled += ctx => CancelAbilityCast(EAbilityKey.W);
        playerInput.actions["EAbility"].canceled += ctx => CancelAbilityCast(EAbilityKey.E);

        playerInput.actions["ADS"].started += ctx => gunBehave.AimDownSights(true);
        playerInput.actions["ADS"].canceled += ctx => gunBehave.AimDownSights(false);
    }

    public void StartShooting()
    {
        if (IsCursorWithinScreen() && Time.timeScale != 0 && gunBehave != null && canFire && !isCC && missionGear.GetGunInSlot(currentWeaponSlot))
        {
            gunBehave.Shoot();
        }
    }

    private void StopShooting()
    {
        gunBehave.StopShooting();
        ChangeSprite(EPlayerMotion.Cover);
    }

    private void SwitchCover(ECoverDirection value)
    {
        if (!inTransit && Time.timeScale != 0 && CoverManager.Instance.CheckForCover(value, true))
        {
            ChangeSprite(EPlayerMotion.Cover);
            CoverManager.Instance.GetCoverPathPoint();
            StartCoroutine(MoveAlongPath(CoverManager.Instance.GetCoverPathPoint()));
        }
    }

    private IEnumerator MoveAlongPath(Vector3[] path)
    { 
        inTransit = true;
        CoverManager.Instance.PassiveCoverCheck();//Ui update

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = path[i];
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float startTime = Time.time;

            while (Vector3.Distance(transform.position, endPosition) > 0.1f)
            {
                float distCovered = (Time.time - startTime) * coverSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                yield return null;
            }
        }
        transform.position = path[path.Length - 1];

        inTransit = false;
        Debug.Log("Reached destination");
    }

    private void SwitchWeapon(int weaponSlot)
    {
        if (missionGear.GetGunInSlot(weaponSlot) != null && Time.timeScale != 0 && canSwitchGun)
        {
            if (weaponSlot != currentWeaponSlot)
            {
                ChangeSprite(EPlayerMotion.Cover);
                StartCoroutine(GunSwitchCooldown());

                //Reverse the animation of the current weapon slot
                InGameUiManager.Instance.AnimateGunSlots(true, currentWeaponSlot);

                currentWeaponSlot = weaponSlot;
                gunBehave.StopReload();
                gunBehave.SetCurrentGun(missionGear.GetGunInSlot(currentWeaponSlot));
                gunBehave.SetGunIndex(weaponSlot);

                //Animate the new weapon slot
                InGameUiManager.Instance.AnimateGunSlots(false, currentWeaponSlot);
                InGameUiManager.Instance.SyncAmmo(currentWeaponSlot,
                missionGear.GetGunInSlot(currentWeaponSlot).currentAmmo,
                missionGear.GetGunInSlot(currentWeaponSlot).currentMags);

                gunBehave.AmmoCheck();
                //Debug.Log("Switch#0" + currentWeaponSlot + "##" + missionGear.GetGunInSlot(currentWeaponSlot).name); return;
            }
            Debug.Log("Already in slot " + weaponSlot); return;
        }
        Debug.LogWarning("No gun assigned to weapon slot " + weaponSlot);
    }

    public void PlayerRecoil()
    {
        //Gets called when shooting, slightly shakes the player sprite to give the illusion of recoil, shake strength is based on weapon type & accuracy
        StartCoroutine(ApplyRecoilShake());
    }

    private IEnumerator ApplyRecoilShake()
    {
        Vector3 originalPosition = playerSprite.transform.localPosition; // Store the original position of the player sprite
        float elapsed = 0.0f;

        while (elapsed < recoilDuration)
        {
            // Use UnityEngine.Random to generate the shake offset
            float xOffset = UnityEngine.Random.Range(-1f, 1f) * recoilMagnitude;
            float yOffset = UnityEngine.Random.Range(-1f, 1f) * recoilMagnitude;

            // Apply the shake based on the original position
            playerSprite.transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsed += Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        // Ensure the player sprite returns exactly to its original position
        playerSprite.transform.localPosition = originalPosition;
    }

    private IEnumerator GunSwitchCooldown()
    {
        canSwitchGun = false;
        float elapsed = 0.0f;

        while (elapsed < gunSwitchCooldown)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        canSwitchGun = true;
    }

    public void ExitMenu()
    {
        if (MenuUiManager.Instance != null && InGameUiManager.Instance == null) MenuUiManager.Instance.CloseMenu();
        else PauseGame();
    }

    public void PauseGame()
    {
        if (blurVolume != null)
        {
            if (InGameUiManager.Instance.PauseGame())
            {
                blurVolume.enabled = true;
                MouseCursorAlign();
                MouseCursorColor(false);
            }
            else
            {
                blurVolume.enabled = false;
                MouseCursorColor(true);
            }
        }
        else Debug.LogWarning("Post Process Volume not found");
    }

    #endregion

    public void ChangeSprite(EPlayerMotion motion)
    {
        switch (motion)
        {
            case EPlayerMotion.Cover:
                playerSprite.sprite = covereringSprite;
                break;

            case EPlayerMotion.Shoot:
                playerSprite.sprite = shootingSprite;
                break;

            default:
                break;
        }
    }

    private void CastAbility(EAbilityKey key)
    {
        switch (key)
        {
            case EAbilityKey.Q:
                Debug.Log("Ability Q");
                break;

            case EAbilityKey.W:
                Debug.Log("Ability W");
                break;

            case EAbilityKey.E:
                Debug.Log("Ability E");
                break;

            default:
                break;
        }
    }

    private void CancelAbilityCast(EAbilityKey key)
    {
        switch (key)
        {
            case EAbilityKey.Q:
                break;

            case EAbilityKey.W:
                break;

            case EAbilityKey.E:
                break;

            default:
                break;
        }
    }

    #region Getters & Setters
    public void SetCanFire(bool value) { canFire = value; }
    public bool GetCanFire() { return canFire; }
    public void SetIsCC(bool value) { isCC = value; }
    public bool GetIsCC() { return isCC; }
    public Camera GetCamera() { return cam; }

    public bool GetTransitStatus() { return inTransit; }
    #endregion
}


