using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport destination;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float exitOffset = 1f;
    [SerializeField] private float cooldownSeconds = 0.2f;

    private Collider portalCollider;
    private Rigidbody portalRigidbody;

    private void Awake()
    {
        portalCollider = GetComponent<Collider>();
        portalCollider.isTrigger = true;

        portalRigidbody = GetComponent<Rigidbody>();
        portalRigidbody.isKinematic = true;
        portalRigidbody.useGravity = false;
        portalRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destination == null)
        {
            return;
        }

        Transform traveler = ResolveTraveler(other);

        if (!IsValidTraveler(other, traveler))
        {
            return;
        }

        TeleportStamp stamp = traveler.GetComponent<TeleportStamp>();
        if (stamp == null)
        {
            stamp = traveler.gameObject.AddComponent<TeleportStamp>();
        }

        if (!stamp.CanTeleport(cooldownSeconds))
        {
            return;
        }

        traveler.position = destination.transform.position + destination.transform.forward * exitOffset;
        stamp.MarkTeleported();
    }

    private Transform ResolveTraveler(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            return other.attachedRigidbody.transform;
        }

        CharacterController characterController = other.GetComponentInParent<CharacterController>();
        if (characterController != null)
        {
            return characterController.transform;
        }

        return other.transform.root;
    }

    private bool IsValidTraveler(Collider other, Transform traveler)
    {
        if (string.IsNullOrEmpty(targetTag))
        {
            return true;
        }

        if (other.CompareTag(targetTag) || traveler.CompareTag(targetTag))
        {
            return true;
        }

        if (targetTag == "Player" && traveler.GetComponentInChildren<PlayerMovement>() != null)
        {
            return true;
        }

        return false;
    }
}

public class TeleportStamp : MonoBehaviour
{
    private float lastTeleportTime = -1000f;

    public bool CanTeleport(float cooldown)
    {
        return Time.time - lastTeleportTime >= cooldown;
    }

    public void MarkTeleported()
    {
        lastTeleportTime = Time.time;
    }
}
