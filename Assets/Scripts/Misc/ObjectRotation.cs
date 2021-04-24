using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    // Instead of exposing 3 unrelated vars, use the Vector3 type
    //public float xAngle, yAngle, zAngle;
    public Vector3 rotationVelocity;
    
    // No need for rotation speed AND yAngle, as they represent almost the same information.
    // Let's combine that into rotationVelocity instead.
    //public float rotationSpeed;

    // The gameObject has a RigidBody. Keep a reference for common usage.
    public Rigidbody rigidBody = null;

    private void Start()
    {
        // Allow failure if not all refs are properly linked. Added a log to keep track of them.
        if(rigidBody == null)
        {
            Debug.LogWarning($"ObjectRotation :: Start : Missing rigidbody reference in the object {gameObject.name}. Please repair.");
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Never move or rotate during the Update phase because it invalidates the physics
    // simulation and may need to reprocess parts of the simulation, which is bad.
    // The physics engine has its own phase named "FixedUpdate" for modifying rigidbodies.
    void FixedUpdate()
    {
        // Notice the new Time.fixedDeltaTime :)
        Quaternion deltaRotation = Quaternion.Euler(rotationVelocity * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }
}
