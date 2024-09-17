using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager thisInstance;
    public static ParticleManager Instance => thisInstance;

    private List<ParticleSystem> effectsList = new List<ParticleSystem>(); //Holds all active visual effects

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

    public void CreatePSystem(ParticleSystem particle, Vector3 location)
    {
        //Used to create a new particle system at a given location
    }
}
