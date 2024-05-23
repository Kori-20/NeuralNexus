using UnityEngine;

public class MenuUiManager : MonoBehaviour
{
    private static MenuUiManager thisInstance;
    public static MenuUiManager Instance => thisInstance;

    [Header("Active Menu")]
    [SerializeField] private GameObject[] activeMenuGroup;

    [Header("All Menu")]
    [SerializeField] private GameObject QuitConfirm;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Inventory;

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
    }


    public void CloseMenu()
    {
        if (activeMenuGroup.Length <= 0)
        {
            QuitConfirm.SetActive(true);
            activeMenuGroup = new GameObject[] { QuitConfirm };
        }
        else
        {
            int highestIndex = 0;
            for (int i = 1; i < activeMenuGroup.Length; i++)
            {
                if (activeMenuGroup[i].transform.GetSiblingIndex() > activeMenuGroup[highestIndex].transform.GetSiblingIndex())
                {
                    highestIndex = i;
                }
            }
            activeMenuGroup[highestIndex].SetActive(false); // Close the last opened menu
        }
        Debug.Log("Closed Menu Called");
    }

}
