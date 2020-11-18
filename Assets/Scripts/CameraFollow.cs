using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
  public class CameraFollow : MonoBehaviour
  {
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private void Awake() {
      m_LastTargetPosition = target.position;
      m_OffsetZ = (transform.position - target.position).z;
      transform.parent = null;
    }

    private void Update()
    {
      Vector3 targetPosition = new Vector3(target.position.x + 50f, target.position.y, target.position.z);
      float xMoveDelta = (targetPosition - m_LastTargetPosition).x;
      bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
      if (updateLookAheadTarget) {
          m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
      } else {
          m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
      }
      Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
      Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
      transform.position = newPos;
      m_LastTargetPosition = target.position;
    }
  }
}