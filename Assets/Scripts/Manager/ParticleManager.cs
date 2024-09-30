using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager thisInstance;
    public static ParticleManager Instance => thisInstance;

    private List<ParticleSystemData> permaFXList = new List<ParticleSystemData>(); // Holds VFX that are permanent
    private List<ParticleSystemData> enemyFXList = new List<ParticleSystemData>(); // Holds VFX related to enemies
    private List<ParticleSystemData> playerFXList = new List<ParticleSystemData>(); // Holds VFX related to players

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

    // Use the struct in CreatePSystem
    public void CreatePSystem(ParticleSystem particlePrefab, Vector3 location, EVFXClassification classification)
    {
        // Instantiate the particle system at the desired location
        ParticleSystem newParticleSystem = Instantiate(particlePrefab, location, Quaternion.identity);

        // Create a new struct instance with particle data
        ParticleSystemData particleData = new ParticleSystemData(newParticleSystem, location, classification);

        // Add the particle system to the appropriate list based on its classification
        switch (classification)
        {
            case EVFXClassification.EnemyFX:
                enemyFXList.Add(particleData);
                break;
            case EVFXClassification.PlayerFX:
                playerFXList.Add(particleData);
                break;
            case EVFXClassification.PermaFX:
                permaFXList.Add(particleData);
                break;
        }
    }

    // Method to destroy a particle system, passing the struct
    public void DestroyPSystem(ParticleSystemData particleData)
    {
        if (particleData.particleSystem != null)
        {
            Destroy(particleData.particleSystem.gameObject);
        }

        // Remove from the appropriate list
        switch (particleData.classification)
        {
            case EVFXClassification.EnemyFX:
                enemyFXList.Remove(particleData);
                break;
            case EVFXClassification.PlayerFX:
                playerFXList.Remove(particleData);
                break;
            case EVFXClassification.PermaFX:
                permaFXList.Remove(particleData);
                break;
        }
    }
}

public struct ParticleSystemData
{
    public ParticleSystem particleSystem; // The particle system itself
    public Vector3 location;              // Location of the particle system
    public EVFXClassification classification; // Classification enum (Player, Enemy, etc.)
    // Constructor to initialize all values
    public ParticleSystemData(ParticleSystem system, Vector3 loc, EVFXClassification classType)
    {
        particleSystem = system;
        location = loc;
        classification = classType;
    }
}
