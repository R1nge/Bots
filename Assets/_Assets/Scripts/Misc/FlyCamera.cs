using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class FlyCamera : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float acceleration = 50;
        [SerializeField] private float sprintAccelerationMultiplier = 4;
        [SerializeField] private float lookSensitivity = 1;
        [SerializeField] private float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
        [SerializeField] private float lookXLimit = 90;
        private Vector3 _velocity;
        private float _rotationX, _rotationY;

        public Camera Camera => camera;

        private void Update()
        {
            UpdateInput();
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
            transform.position += _velocity * Time.deltaTime;
        }

        private void UpdateInput()
        {
            _velocity += GetAccelerationVector() * Time.deltaTime;
            var mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            var mouseY = -Input.GetAxis("Mouse Y") * lookSensitivity;
            _rotationX = Mathf.Clamp(_rotationX + mouseY, -lookXLimit, lookXLimit);
            camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.localRotation *= Quaternion.Euler(0, mouseX, 0);
        }

        private Vector3 GetAccelerationVector()
        {
            Vector3 moveInput = default;

            void AddMovement(KeyCode key, Vector3 dir)
            {
                if (Input.GetKey(key))
                    moveInput += dir;
            }

            AddMovement(KeyCode.W, Vector3.forward);
            AddMovement(KeyCode.S, Vector3.back);
            AddMovement(KeyCode.D, Vector3.right);
            AddMovement(KeyCode.A, Vector3.left);
            AddMovement(KeyCode.Space, Vector3.up);
            AddMovement(KeyCode.LeftControl, Vector3.down);
            var direction = camera.transform.TransformVector(moveInput.normalized);

            if (Input.GetKey(KeyCode.LeftShift))
                return direction * (acceleration * sprintAccelerationMultiplier);
            return direction * acceleration;
        }
    }
}