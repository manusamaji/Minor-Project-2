using System;
using UnityEngine;

public class PhysicsControl : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Ground")]
    [SerializeField] private float groundRayDistance;
    [SerializeField] private Transform leftGroundPoint;
    [SerializeField] private Transform rightGroundPoint;
    [SerializeField] private LayerMask whatToDetect;
    public bool grounded;
    private RaycastHit2D hitInfoLeft;
    private RaycastHit2D hitInfoRight;

    [Header("Wall")]
    [SerializeField] private float wallRayDistance;
    [SerializeField] private Transform wallCheckPointUpper;
    [SerializeField] private Transform wallCheckPointLower;
    public bool wallDetected;
    private RaycastHit2D hitInfoWallUpper;
    private RaycastHit2D hitInfoWallLower;
    internal int horizontalInput;

    [Header("Colliders")]
    [SerializeField] private Collider2D standColl;
    [SerializeField] private Collider2D crouchColl;



    private float gravityValue;
    void Start()
    {
        gravityValue = rb.gravityScale;
    }
    private bool CheckWall()
    {
        hitInfoWallUpper = Physics2D.Raycast(wallCheckPointUpper.position, transform.right, wallRayDistance, whatToDetect);
        hitInfoWallLower = Physics2D.Raycast(wallCheckPointLower.position, transform.right, wallRayDistance, whatToDetect);
        Debug.DrawRay(wallCheckPointUpper.position,new Vector3(wallRayDistance,0,0),Color.red);
        Debug.DrawRay(wallCheckPointLower.position, new Vector3(wallRayDistance, 0, 0), Color.red);

        if (hitInfoWallUpper || hitInfoWallLower)
            return true;
        return false;
    }


    private bool CheckGround()
    {
        hitInfoLeft=Physics2D.Raycast(leftGroundPoint.position,Vector2.down,groundRayDistance,whatToDetect);
        hitInfoRight=Physics2D.Raycast(rightGroundPoint.position,Vector2.down,groundRayDistance, whatToDetect);

        Debug.DrawRay(leftGroundPoint.position, new Vector3(0, -groundRayDistance, 0), Color.red);
        Debug.DrawRay (rightGroundPoint.position, new Vector3(0, -groundRayDistance,0), Color.red);


        if(hitInfoLeft || hitInfoRight )
            return true;
        return false;
    }
  

    // Update is called once per frame
    void Update()
    {
        grounded = CheckGround();
        if(CheckGround())
        {
            Debug.Log("touching ground");
        }
    }
    public void DisableGravity()
    {
        rb.gravityScale = 0;
    }
    public void EnableGravity()
    {
        rb.gravityScale=gravityValue;
    }
    public void ResetVelocity()
    {
        rb.linearVelocity=Vector2.zero;
    }

    public void StandColliders()
    {
        standColl.enabled = true;
        crouchColl.enabled = false;
    }
    public void CrouchColliders()
    {
        standColl.enabled=false;
        crouchColl.enabled=true;
    }
    private void FixedUpdate()
    {
        grounded=CheckGround();
        wallDetected = CheckWall();
    }
}
