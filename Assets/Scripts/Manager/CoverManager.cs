using UnityEngine;
public enum ECoverDirection
{
    Left,
    Right,
}

public class CoverManager : MonoBehaviour
{
    [SerializeField] private Cover[] covers;
    [SerializeField] public int currentCoverIndex = 1; // starts in the middle 0-1-2
    private int oldCoverIndex;

    private Vector3[] path = new Vector3[4];
    private ECoverDirection coverDirection;

    private static CoverManager thisInstance;

    public static CoverManager Instance => thisInstance;

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

        oldCoverIndex = currentCoverIndex;
    }

    private void Start()
    {
        PassiveCoverCheck();
    }

    public Vector3 GetCoverCenter()
    {
        if (currentCoverIndex < 0 || currentCoverIndex > covers.Length - 1)
        {
            Debug.Log("Desired cover index " + currentCoverIndex + " is out of bounds");
            currentCoverIndex = Random.Range(0, covers.Length);
        }
        PassiveCoverCheck();
        return covers[currentCoverIndex].centerC.transform.position;
    }

    public Vector3[] GetCoverPathPoint()
    {
        path[0] = covers[oldCoverIndex].centerC.transform.position;

        switch (coverDirection)
        {
            case ECoverDirection.Left:
                path[1] = covers[oldCoverIndex].leftC.transform.position;
                path[2] = covers[currentCoverIndex].rightC.transform.position;
                path[3] = covers[currentCoverIndex].centerC.transform.position;
                break;

            case ECoverDirection.Right:
                path[1] = covers[oldCoverIndex].rightC.transform.position;
                path[2] = covers[currentCoverIndex].leftC.transform.position;
                path[3] = covers[currentCoverIndex].centerC.transform.position;
                break;

            default:
                break;
        }
        return path;
    }

    public bool CheckForCover(ECoverDirection dir, bool active)
    {
        oldCoverIndex = currentCoverIndex;
        switch (dir)
        {
            case ECoverDirection.Left:
                if (currentCoverIndex > 0 && active)
                {
                    currentCoverIndex--;
                    coverDirection = ECoverDirection.Left;
                    return true;
                }
                else if (currentCoverIndex > 0 && !active)
                {
                    return true;
                }
                return false;

            case ECoverDirection.Right:
                if (currentCoverIndex < covers.Length - 1 && active)
                {
                    currentCoverIndex++;
                    coverDirection = ECoverDirection.Right;
                    return true;
                }
                else if (currentCoverIndex < covers.Length - 1 && !active)
                {
                    return true;
                }
                return false;

            default:
                break;
        }
        // Return false if the direction is not recognized or if the index can't be changed
        return false;
    }

    public void PassiveCoverCheck() //Used to check where player has cover for Ui purposes
    {
        if (CheckForCover(ECoverDirection.Left, false)) InGameUiManager.Instance.SetCoverArrows(ECoverDirection.Left, true);
        else InGameUiManager.Instance.SetCoverArrows(ECoverDirection.Left, false);

        if (CheckForCover(ECoverDirection.Right, false)) InGameUiManager.Instance.SetCoverArrows(ECoverDirection.Right, true);
        else InGameUiManager.Instance.SetCoverArrows(ECoverDirection.Right, false);
    }
}
