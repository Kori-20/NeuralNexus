using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUiManager : MonoBehaviour
{
    private static InGameUiManager thisInstance;
    public static InGameUiManager Instance => thisInstance;

    [Header("In Game Gun Menu")]
    [SerializeField] private TextMeshProUGUI ammoAmmount;
    [SerializeField] private TextMeshProUGUI magAmmount;

    [SerializeField] private GameObject weaponsUI;
    [SerializeField] private Image[] slotBckgList;
    [SerializeField] private Image[] slotGunList;

    [SerializeField] private GameObject rightCoverArrow;
    [SerializeField] private GameObject leftCoverArrow;

    [SerializeField] private Animator[] slotAnimators;
    [SerializeField] private Slider reloadProgress;
    private Coroutine reloadFillCoroutine = null;

    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (reloadProgress == null) Debug.LogWarning("Reload Progress Bar not found");
    }

    private void OnValidate()
    {

    }

    private void Start()
    {

    }

    public void SyncAmmo(int gunIndex, int ammo, int mags)
    {
        ammoAmmount.text = ammo.ToString();
        magAmmount.text = mags.ToString();
        //Debug.Log("Synced Gun: " + gunIndex + " Ammo: " + ammo + " Mags: " + mags);   
    }

    public void SyncGunIcons(int id, Sprite img, Color rarityColor)
    {
        if (slotBckgList[id])
        {
            slotBckgList[id].color = rarityColor;
        }
        else Debug.LogWarning("Slot Background not found");

        if (slotGunList[id])
        {
            slotGunList[id].enabled = true;
            slotGunList[id].sprite = img;
        }
        else Debug.LogWarning("Slot Gun not found");
    }

    public void FillReload(float duration)
    {
        reloadFillCoroutine = StartCoroutine(FillReloadCoroutine(duration));
    }

    public void StopFillReload()
    {
        StopCoroutine(reloadFillCoroutine);
        reloadFillCoroutine = null;
        reloadProgress.value = 0;
    }

    private IEnumerator FillReloadCoroutine(float duration)
    {
        float elapsedTime = 0f;
        float startValue = reloadProgress.value;

        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, reloadProgress.maxValue, elapsedTime / duration);
            reloadProgress.value = newValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the bar is filled
        reloadProgress.value = reloadProgress.maxValue;
        reloadFillCoroutine = null;
        //Reset the bar for the next reload
        reloadProgress.value = 0;
    }

    public void AnimateGunSlots(bool reverse, int slotIndex)
    {
        string format = "null";
        if (!reverse) format = "GSlot_0" + (slotIndex + 1) + "_Open";
        else format = "GSlot_0" + (slotIndex + 1) + "_Close";
        Debug.Log("Playing: " + format);
        slotAnimators[slotIndex].Play(format);
    }

    public void SetCoverArrows(ECoverDirection direction, bool value)
    {
        switch (direction)
        {
            case ECoverDirection.Left:
                leftCoverArrow.SetActive(value);
                break;
            case ECoverDirection.Right:
                rightCoverArrow.SetActive(value);
                break;
        }
    }

    public bool PauseGame()
    {
        if (pauseMenu != null && pauseMenu.activeSelf)
        {
            GameManager.Instance.ResumeTimeDefault();
            pauseMenu.SetActive(false);
            return false;
        }
        else
        {
            GameManager.Instance.PauseTime();
            pauseMenu.SetActive(true);
            return true;
        }
    }
}
