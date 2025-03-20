using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    //private float rayCastDistance = 300f;
    private float spreadMath;

    private Gun currentGun = null; //Value set by player Controller on weapon switch
    private Controller playerControl = null;

    private Coroutine delayFireC = null;
    private Coroutine autoFireC = null;
    private Coroutine reloadCoroutine = null;

    private int thisGunIndex = 0;

    [Header("Base Gun Property")]
    [SerializeField] private AnimationCurve spreadCurve;


    private void Awake()
    {
        playerControl = GetComponent<Controller>();
    }

    #region Fire Weapon Logic
    public void Shoot()
    {
            playerControl.SetCanFire(false);

            ////if (currentGun.IsAutomatic)
            ////{
            ////    if (autoFireC == null) autoFireC = StartCoroutine(AutomaticFire());
            ////}
            ////else
            ////{
            ////    Trajectory();
            ////}
            
            if (delayFireC == null) delayFireC = StartCoroutine(DelayFire());
    }

    public void StopShooting()
    {
        if (autoFireC != null)
        {
            StopCoroutine(autoFireC);
            autoFireC = null;
        }
    }

    private void Trajectory()
    {
        Vector3 hitPoint = CrosshairAim();
        if (hitPoint != Vector3.zero)
        {
            Vector3 directionToHitPoint = (hitPoint - shootPoint.position).normalized;
            Vector3 shootPointForward = shootPoint.forward;

            if (Vector3.Dot(directionToHitPoint, shootPointForward) < 0)
            {
                Debug.Log("Looking behind the shoot point.");
            }
            else
            {
                if (currentGun.CurrentAmmo > 0 && !playerControl.GetTransitStatus())
                {
                    FireBullet(directionToHitPoint);
                }
            }
        }
        else
        {
            Debug.Log("Shot Missed");
        }
    }

    private void FireBullet(Vector3 direction)
    {
        StopReload();
        playerControl.ChangeSprite(EPlayerMotion.Shoot);
        playerControl.PlayerRecoil();

        ////for (int i = 0; i < currentGun.PelletsPerShot; i++)
        ////{
        ////    Vector3 spreadDirection = BulletSpread(direction);
        ////    SpawnWeaponProjetile(spreadDirection);
        ////}

        ////currentGun.CurrentAmmo--;
        ////InGameUiManager.Instance.SyncAmmo(thisGunIndex, currentGun.CurrentAmmo, currentGun.CurrentMags);
        if (currentGun.CurrentAmmo <= 0) Reload();
    }

    public virtual void SpawnWeaponProjetile(Vector3 directionPostSpread)
    {
        ////ProjectileManager.Instance.SpawnProjectile(currentGun.projectilePrefab, shootPoint.position, directionPostSpread,
        ////    (int)currentGun.Damage, currentGun.Velocity, currentGun.PhysicalType,
        ////    currentGun.ElementType);
    }

    private Vector3 BulletSpread(Vector3 targetPoint)
    {
        float spreadX = Random.Range(-spreadMath, spreadMath);
        float spreadY = Random.Range(-spreadMath, spreadMath);

        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        Vector3 spreadDirection = spreadRotation * targetPoint;

        //Debug.DrawRay(shootPoint.position, spreadDirection * rayCastDistance, Color.magenta, 1f); //True bulet trajectory
        return spreadDirection.normalized;
    }

    public void Reload()
    {
        //Loses remaining ammo in the magazine On Reload
        ////if(currentGun.CurrentMags > 0 && reloadCoroutine == null && !playerControl.GetIsCC() && currentGun.CurrentAmmo < currentGun.MagazineSize && Time.timeScale != 0)
        ////{
        ////    Debug.Log("Reloading...");
        ////    playerControl.ChangeSprite(EPlayerMotion.Cover);
        ////    reloadCoroutine = StartCoroutine(ReloadCoroutine());
        ////    InGameUiManager.Instance.FillReload(currentGun.ReloadTime);
        ////}
        ////else if (currentGun.CurrentMags <= 0)
        ////{
        ////    Debug.Log("No magazines left.");
        ////}
    }

    public void StopReload()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            InGameUiManager.Instance.StopFillReload();
            reloadCoroutine = null;
        }
    }

    public void AimDownSights(bool value)
    {
        //Degub.Log("Aiming Down Sights");
    }

    ////private IEnumerator ReloadCoroutine()
    ////{
    ////    yield return new WaitForSeconds(currentGun.ReloadTime);
    ////    reloadCoroutine = null;
    ////    if (currentGun != null)
    ////    {
    ////        Debug.Log("Reloaded " + currentGun.MagazineSize + " bullets");
    ////        ////currentGun.CurrentMags--;
    ////        ////currentGun.CurrentAmmo = currentGun.MagazineSize;
    ////        InGameUiManager.Instance.SyncAmmo(thisGunIndex, currentGun.CurrentAmmo, currentGun.CurrentMags);
    ////    }
    ////}

    private Vector3 CrosshairAim()
    {
        Ray ray = playerControl.GetCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawLine(playerControl.GetCamera().transform.position, hit.point, Color.red, .5f);//Raycast Cam->Targer
            return hit.point;
        }
        return Vector3.zero;
    }

    private IEnumerator AutomaticFire()
    {
        while (true)
        {
            Trajectory();
            yield return new WaitForSeconds(1f / currentGun.FireRate); // Wait for the cooldown period
        }
    }

    private IEnumerator DelayFire() //Forces non automatic weapons to follow their fire rate
    {
        yield return new WaitForSeconds(1f / currentGun.FireRate);
        playerControl.SetCanFire(true);
        Debug.Log("Can Fire");
        delayFireC = null; // Reset the coroutine reference
    }
    #endregion

    public void SetCurrentGun(Gun gun)
    {
            StopShooting();
            currentGun = gun;
            spreadMath = spreadCurve.Evaluate(currentGun.Accuracy)*25;
    }

    public void SetGunIndex(int index)
    {
        thisGunIndex = index;
    }

    public void AmmoCheck()
    {
          if (currentGun.CurrentAmmo <= 0) Reload();
    }
}
