using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Drag the OTHER teleporter object into this slot in the Inspector.")]
    public Transform destinationTeleporter;

    [Tooltip("Offset to prevent the player from getting stuck inside the destination object.")]
    public Vector3 spawnOffset = new Vector3(0f, 1f, 0f);

    [Tooltip("Time in seconds before the player can teleport again (prevents infinite loops).")]
    public float cooldown = 1.5f;

    private float nextTeleportTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextTeleportTime) return;

        if (destinationTeleporter == null)
        {
            Debug.LogWarning($"Teleporter on {gameObject.name} is missing a destination!");
            return;
        }

        if (other.CompareTag("Player"))
        {
            // Set cooldown on destination to avoid immediate back-and-forth
            Teleporter destinationScript = destinationTeleporter.GetComponent<Teleporter>();
            if (destinationScript != null)
            {
                destinationScript.SetCooldown(Time.time + cooldown);
            }

            // Also lock this teleporter briefly so the player can't retrigger while inside
            SetCooldown(Time.time + cooldown);

            Vector3 targetPosition = destinationTeleporter.position + spawnOffset;

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = targetPosition;
                cc.enabled = true;
            }
            else
            {
                other.transform.position = targetPosition;
            }

            Debug.Log($"Teleported {other.name} to {destinationTeleporter.name}");
        }
    }

    public void SetCooldown(float lockUntilTime)
    {
        nextTeleportTime = lockUntilTime;
    }
}
