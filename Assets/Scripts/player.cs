using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class player : entity
{
    private float jumpSpeed = 9;
    private float currentJumpSpeed;
    private float jumpDecreaseSpeed = 18;

    private bool jumping;

    [SerializeField] private LayerMask terrainLayer;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            Jump();
        }
        CheckCollision();

        Vector3 horizontalMovement = Vector2.right * Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        transform.position += horizontalMovement;

        if (jumping)
        {
            transform.position += Vector3.up * Time.deltaTime * currentJumpSpeed;// GRAVITY) ;
            currentJumpSpeed += Time.deltaTime * -1 * jumpDecreaseSpeed;
        }
    }

    void CheckCollision()
    {
        #region HORIZONTAL CHECK
        float horizontalDirection = Input.GetAxisRaw("Horizontal");

        bool isMoving = horizontalDirection != 0;

        float backRayDistance = .125f;
        float horizontalrayDistance = (isMoving ? .45f : backRayDistance);
        float widthOffset = (GetComponent<BoxCollider2D>().size.x / 2);
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
        //*//Show Front Wall Collision Checks
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * .05f), transform.position + (Vector3.up * .05f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1f), transform.position + (Vector3.up * 1f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        Debug.DrawLine(transform.position + (currentBackwardDirection * .2f) + (Vector3.up * 1.7f), transform.position + (Vector3.up * 1.7f) + (currentBackwardDirection * horizontalrayDistance), Color.red);
        //*/
        #endregion
        RaycastHit2D forwardHit = frontWallRaycasts.FirstOrDefault(ray => ray.collider != null);
        RaycastHit2D backwardHit = backWallRaycasts.FirstOrDefault(ray => ray.collider != null);
        Vector2 newPosition = transform.position;
        if (forwardHit)
            newPosition = new Vector2(forwardHit.point.x + (widthOffset + (isMoving ? .125f : .06125f)) * (-horizontalDirection), transform.position.y);
        else if (backwardHit)
            newPosition = new Vector2(backwardHit.point.x + (widthOffset + (.06125f)) * (horizontalDirection), transform.position.y);
        transform.position = newPosition;
        #endregion
        
        #region VERTICAL CHECKS
        //Vector3 moveVector = Vector3.up * -1 * gravityDown * Time.deltaTime;
        float verticalRayDistance = 1.15f;

        RaycastHit2D[] floorRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (Vector3.up * .5f), Vector2.down, verticalRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset*.9f), Vector2.down, verticalRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset*.9f), Vector2.down, verticalRayDistance, terrainLayer)
        };
        verticalRayDistance = .125f;
        float verticalOffset = 1.75f;
        RaycastHit2D[] ceilingRaycasts = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset), Vector2.up, verticalRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset*.9f), Vector2.up, verticalRayDistance, terrainLayer),
            Physics2D.Raycast(transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset*.9f), Vector2.up, verticalRayDistance, terrainLayer)
        };
        #region Debug Vertical Collisions
        //Show Floor Collision Checks
        Debug.DrawLine(transform.position + (Vector3.up * .5f), transform.position + (Vector3.up * .5f) + (Vector3.down * verticalRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * .5f) - Vector3.right * (widthOffset * .9f) + (Vector3.down * verticalRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * .5f) + Vector3.right * (widthOffset * .9f) + (Vector3.down * verticalRayDistance), Color.red);
        //Show Ceiling Collision Checks
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset), transform.position + (Vector3.up * verticalOffset) + (Vector3.up * verticalRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * verticalOffset) - Vector3.right * (widthOffset * .9f) + (Vector3.up * verticalRayDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset * .9f), transform.position + (Vector3.up * verticalOffset) + Vector3.right * (widthOffset * .9f) + (Vector3.up * verticalRayDistance), Color.red);

        #endregion
        #endregion

    }

    void Jump()
    {
        jumping = true;
        currentJumpSpeed = jumpSpeed;
    }
}
