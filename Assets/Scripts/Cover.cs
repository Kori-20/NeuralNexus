using UnityEngine;
public class Cover : MonoBehaviour
{
    [Header("Cover Points")]
    [SerializeField] public Transform leftC;
    [SerializeField] public Transform centerC;
    [SerializeField] public Transform rightC;

    [Header("Offsets")]
    [SerializeField] private float yOs = 1f;
    [SerializeField] private float zOs = -0.5f;
    [SerializeField] private float xOs = 2f;
    private float halfLength;
    private void OnValidate()
    {
        if (centerC != null)
        {
            halfLength = centerC.localScale.x / 2;
            centerC.localPosition = new Vector3(0f, yOs, zOs);
        }
        if (leftC != null) leftC.localPosition = new Vector3(-halfLength - xOs, yOs, zOs);
        if (rightC != null) rightC.localPosition = new Vector3(halfLength + xOs, yOs, zOs);
    }
}

