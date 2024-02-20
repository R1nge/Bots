using System;
using UnityEngine;

namespace _Assets.Scripts.Gameplay.Parts.Move
{
    public class MoveWheelPart : InGameBotPart, IMovingPart
    {
        [SerializeField] protected float speed;
        [SerializeField] protected Transform rotatableTransform;
        [SerializeField] protected Rigidbody botRigidbody;
        [SerializeField] protected float suspensionRestDistance;
        [SerializeField] protected float springStrength;
        [SerializeField] protected float dampingForce;
        [SerializeField] protected float tireGripFactor;
        [SerializeField] protected float mass;
        [SerializeField] protected float topSpeed;

        private void Start()
        {
            botRigidbody = transform.root.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Raycast(out var hitPosition))
            {
                Suspension(hitPosition);
                Steering();
                //Acceleration();
            }
        }

        private void Suspension(float rayDistance)
        {
            Vector3 springForceDirection = rotatableTransform.up;
            Vector3 tireWorldVelocity = botRigidbody.GetPointVelocity(rotatableTransform.position);
            float offset = suspensionRestDistance - rayDistance;
            float velocity = Vector3.Dot(springForceDirection, tireWorldVelocity);
            float force = (offset * springStrength) - (velocity * dampingForce);
            botRigidbody.AddForceAtPosition(springForceDirection * force, rotatableTransform.position);
        }

        private void Steering()
        {
            Vector3 steeringDirection = rotatableTransform.right;
            Vector3 tireWorldVelocity = botRigidbody.GetPointVelocity(rotatableTransform.position);
            float steeringVelocity = Vector3.Dot(steeringDirection, tireWorldVelocity);
            float desiredVelocityChange = -steeringVelocity * tireGripFactor;
            float desiredAcceleration = desiredVelocityChange / Time.fixedDeltaTime;
            botRigidbody.AddForceAtPosition(steeringDirection * (desiredAcceleration * mass), rotatableTransform.position);
        }

        private void Acceleration()
        {
            Vector3 accelerationDirection = rotatableTransform.forward;

            //float speed = Vector3.Dot(botRigidbody.transform.forward, botRigidbody.velocity);
            //float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(speed) / topSpeed);
            //float avaliableTorque = botRigidbody.mass * botRigidbody.angularVelocity.magnitude;
            botRigidbody.AddForceAtPosition(accelerationDirection * (100 * Time.fixedTime), rotatableTransform.position);
        }

        private bool Raycast(out float rayDistance)
        {
            var ray = new Ray(rotatableTransform.position, Vector3.down);
            if (Physics.Raycast(ray, out var hit))
            {
                rayDistance = hit.distance;
                return true;
            }

            rayDistance = 0;
            return false;
        }

        public void Move(Vector3 direction)
        {
            //rotatableTransform.Rotate(direction * speed);
            Acceleration();
        }
    }
}