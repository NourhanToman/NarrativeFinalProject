using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public bool Grounded;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 CheckPos;
    private InputManager _inputManager; //Rhods

    private void Start() => _inputManager = ServiceLocator.Instance.GetService<InputManager>(); //Rhods

    private void FixedUpdate()
    {
        Grounded = Physics.CheckSphere(transform.position + CheckPos, radius, groundLayer);
        if(Grounded == true && _inputManager.isJumping == true) //Rhods
        {
            _inputManager.isJumping = false; //Rhods
            _inputManager.canJump = true; //Rhods
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + CheckPos, radius);
    }
}
