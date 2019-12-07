using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player : Character
{
    [SerializeField] private LayerMask terrainLayer;
    public int goldAmount = 0;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            Jump();
        }

        Vector3 horizontalMovement = Vector2.right * Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        transform.position += horizontalMovement;

        CheckCollision();
        base.Update();

        //Debug.Log("Am I Jumping?:" + jumping);
        if (Dead)
        {
            SceneManager.LoadScene(0);
        }
    }
    
    void CheckCollision()
    {
        Vector2 newPosition = transform.position;
        float widthOffset = (GetComponent<BoxCollider2D>().size.x / 2);

        #region VERTICAL CHECKS
        float ceilingRayDistance = .5f;
        float floorRayDistance = .125f;

        RaycastHit2D[] floorRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (Vector3.up * .5f), Vector2.down, ceilingRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset*.9f), Vector2.down, ceilingRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset*.9f), Vector2.down, ceilingRayDistance, terrainLayer)
        };

        float verticalOffset = 1.75f;
        RaycastHit2D[] ceilingRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset), Vector2.up, floorRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset*.9f), Vector2.up, floorRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset*.9f), Vector2.up, floorRayDistance, terrainLayer)
        };
        #region Debug Vertical Collisions
        //Show Floor Collision Checks
        Debug.DrawLine(transform.position + (Vector3.up * .5f), transform.position + (Vector3.up * .5f) + (Vector3.down * ceilingRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset * .9f) + (Vector3.down * ceilingRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset * .9f) + (Vector3.down * ceilingRayDistance), Color.red);
        //Show Ceiling Collision Checks
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset), transform.position + (Vector3.up * verticalOffset) + (Vector3.up * ceilingRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset * .9f) + (Vector3.up * ceilingRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset * .9f) + (Vector3.up * ceilingRayDistance), Color.red);

        #endregion

        RaycastHit2D hit = floorRaycasts.FirstOrDefault(ray => ray.collider != null);
        if (hit && currentJumpSpeed <= 0)
        {
            newPosition = new Vector2(transform.position.x, hit.point.y);
            transform.position = newPosition;

            jumping = false;
            currentJumpSpeed = 0;
        }
        else if (floorRaycasts.All(ray => ray.collider == null))
        {
            if (!jumping)
                jumping = true;
        }
        hit = ceilingRaycasts.FirstOrDefault(ray => ray.collider != null);
        if (hit)
        {
            if (currentJumpSpeed > 0)
            {
                currentJumpSpeed = 0;
            }
        }

        #endregion

        #region HORIZONTAL CHECK
        float horizontalDirection = Input.GetAxisRaw("Horizontal");

        bool isMoving = horizontalDirection != 0;

        float backRayDistance = .125f;
        float horizontalrayDistance = (isMoving ? .45f : backRayDistance);
        Vector3 currentForwardDirection = Vector2.right * horizontalDirection;
        Vector3 currentBackwardDirection = Vector2.right * -horizontalDirection;

        //Forward checks
        RaycastHit2D[] frontWallRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (currentForwardDirection * .2f) + (Vector3.up *.05f), currentForwardDirection, horizontalrayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (currentForwardDirection * .2f) + (Vector3.up * 1f), currentForwardDirection, horizontalrayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (currentForwardDirection * .2f) + (Vector3.up * 1.7f), currentForwardDirection, horizontalrayDistance, terrainLayer)
        };
        //Backward checks
        RaycastHit2D[] backWallRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (currentBackwardDirection * .2f) + (Vector3.up *.05f), currentBackwardDirection, backRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1f), currentBackwardDirection, backRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1.7f), currentBackwardDirection, backRayDistance, terrainLayer)
        };
        #region Collision Debugs
        //Show Back Wall Collision Checks
        Debug.DrawLine(transform.position + (currentForwardDirection * .2f) + (Vector3.up * .05f), transform.position + (Vector3.up * .05f) + (currentForwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentForwardDirection * .2f) + (Vector3.up * 1f), transform.position + (Vector3.up * 1f) + (currentForwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentForwardDirection * .2f) + (Vector3.up * 1.7f), transform.position + (Vector3.up * 1.7f) + (currentForwardDirection * horizontalrayDistance), Color.red);
        //Show Front Wall Collision Checks
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * .05f), transform.position + (Vector3.up * .05f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1f), transform.position + (Vector3.up * 1f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1.7f), transform.position + (Vector3.up * 1.7f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        //
        #endregion
        RaycastHit2D forwardHit = frontWallRaycasts.FirstOrDefault(ray => ray.collider != null);
        RaycastHit2D backwardHit = backWallRaycasts.FirstOrDefault(ray => ray.collider != null);

        if (forwardHit)
            newPosition = new Vector2(forwardHit.point.x + (widthOffset + (isMoving ? .125f : .06125f)) * (-horizontalDirection), transform.position.y);
        else if (backwardHit)
            newPosition = new Vector2(backwardHit.point.x + (widthOffset + (.06125f)) * (horizontalDirection), transform.position.y);
        transform.position = newPosition;
        #endregion

    }
    
    void Jump()
    {
        jumping = true;
        currentJumpSpeed = jumpSpeed;
    }
    public void AddGold(int gold)
    {
        gold += goldAmount;
    }
    
}
