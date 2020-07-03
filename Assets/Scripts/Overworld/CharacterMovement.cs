using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D characterRigidBody;
    private Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        this.characterRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.change = Vector3.zero;
        this.change.x = Input.GetAxisRaw("Horizontal");
        this.change.y = Input.GetAxisRaw("Vertical");

        this.MoveCharacter();
    }

    void MoveCharacter()
    {
        characterRigidBody.MovePosition(transform.position + this.change * this.speed * Time.deltaTime);
    }
}
