using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControllerEvent{
    public class PlayerController : MonoBehaviour
    {
        CharacterController characterController;
        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private float MovementSpeed = 1;
        [SerializeField] private float Gravity = 9.8f;
        [SerializeField] private float velocity = 0;
        [SerializeField] private Camera cam;
        private Animator animator;
        public Interactable interactable {get; set;}
        public DialogueUI DialogueUI => dialogueUI;

        private void Awake()
        {
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }
    
        void Update(){

            if(Input.GetKey(KeyCode.E)){
                interactable?.Interact(this);
            }

            if(Input.GetKey(KeyCode.LeftShift)){
                MovementSpeed = 4;
            }else{
                MovementSpeed = 1;
            }
        }

        void FixedUpdate()
        {
            // if(dialogueUI.IsOpen){
            //     animate.enabled = false;
            //     return;
            // }else{
            //     animate.enabled = true;
            // }


            // player movement - forward, backward, left, right
            float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
            float vertical = Input.GetAxis("Vertical") * MovementSpeed;
            characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
            characterController.transform.eulerAngles = new Vector3(0.0f, cam.transform.eulerAngles.y, 0.0f);

            // Gravity
            if(characterController.isGrounded)
            {
                velocity = 0;
            }
            else
            {
                velocity -= Gravity * Time.deltaTime;
                characterController.Move(new Vector3(0, velocity, 0));
            }

            animator.SetBool("isWalking", vertical != 0 || horizontal != 0);

        }
    }
}