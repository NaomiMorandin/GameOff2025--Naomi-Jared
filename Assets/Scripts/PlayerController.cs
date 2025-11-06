    using UnityEngine;

    public class CharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            float currentSpeed = movement.magnitude;

            animator.SetFloat("Speed", currentSpeed); // "Speed" is the parameter name in the Animator
        }
    }