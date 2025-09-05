using UnityEngine;

public class CollisionDataRetriever : MonoBehaviour
{
    public bool OnGround { get; private set;}
    public bool OnWall { get; private set;}
    public Vector3 ContactNormal { get; private set; } 

    private void OnCollisionEnter(Collision collision) 
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision) 
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit(Collision collision) 
    {
        OnGround = false;
        OnWall = false;
    }

    public void EvaluateCollision(Collision collision) 
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactNormal = collision.GetContact(i).normal;
            OnGround = ContactNormal.y >= 0.9f;
            OnWall = Mathf.Abs(ContactNormal.x) >= 0.9f || Mathf.Abs(ContactNormal.z) >= 0.9f; 
        }
    }
}