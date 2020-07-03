using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D characterRigidBody;
    private Vector3 change;

    private Animator animator;

    private bool clickMovementActive;
    private Vector3 clickedPosition;
    //To prevent shaking of the character
    private float mouseDeltaThreshold = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.characterRigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        this.clickedPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        SetChangeBasedOnKeyBoard();
        SetChangeBasedOnMouseOrTouchClick();

        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

            MoveCharacter();
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        characterRigidBody.MovePosition(transform.position + this.change * this.speed * Time.deltaTime);
    }

    void SetChangeBasedOnKeyBoard()
    {
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero) {
            clickMovementActive = false;
        }
    }

    void SetChangeBasedOnMouseOrTouchClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Mouse
            clickedPosition = Input.mousePosition;
            clickMovementActive = true;
        } else if(Input.touchCount > 0)
        {
            //Touch (phone)
            Touch touch = Input.GetTouch(0);
            clickedPosition = touch.position;
            clickMovementActive = true;
        }

        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (clickMovementActive)
        {
            float xDelta = clickedPosition.x - playerPosition.x;
            float yDelta = clickedPosition.y - playerPosition.y;

            //TODO better way?

            if (xDelta < -mouseDeltaThreshold)
            {
                change.x = -1;
            }
            else if (xDelta > mouseDeltaThreshold)
            {
                change.x = 1;
            }

            if (yDelta < -mouseDeltaThreshold)
            {
                change.y = -1;
            }
            else if (yDelta > mouseDeltaThreshold)
            {
                change.y = 1;
            }

            if(change == Vector3.zero)
            {
                clickMovementActive = false;
            }
        }
    }
}
