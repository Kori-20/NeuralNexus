using NUnit.Framework.Internal;
using UnityEngine;

public class FloatingNumbers : MonoBehaviour
{
    private int damageFloat;
    [SerializeField] private Animation motion;

    private void Start()
    {
        transform.localPosition += new Vector3(Random.Range(-2, 2), Random.Range(3, 6), -10);
        motion.clip.legacy = true;
        Destroy(gameObject, 0.3f);
    }
}
