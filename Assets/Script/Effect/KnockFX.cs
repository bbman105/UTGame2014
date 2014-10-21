using UnityEngine;
using System.Collections;

public class KnockFX : MonoBehaviour
{

    public bool knockEffect;
    public GameObject knockFXPrefab;		// Prefab of knock effect.

    private float effectDist;


    void Awake()
    {
        knockEffect = true;
        effectDist = 0.35f;
        this.enabled = false;
    }


    public void OnKnockFX()
    {

        if (knockEffect)
        {
            this.enabled = true;
            // Create a quaternion with a random rotation in the z-axis.
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            Status status = (Status)transform.parent.GetComponent("Status");
            Vector3 effectPos = new Vector3(transform.position.x + (effectDist * status.GetMovDirection()), transform.position.y, transform.position.z);
            // Instantiate the knockFX when knock the enemy target with the random rotation.
            Instantiate(knockFXPrefab, effectPos, randomRotation);
            this.enabled = false;
        }
    }
}
