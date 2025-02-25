using UnityEngine;

public class Eyes : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform rightEye;
    [SerializeField] private Transform leftEye;
    [SerializeField] private Transform rightPupil;
    [SerializeField] private Transform leftPupil;

    [Header("Settings")]
    [SerializeField] private float maxMoveMagnitude;

    private void Update()
    {
        MoveEyes();
    }

    private void MoveEyes()
    {
        Vector3 targetPos = Vector3.zero;

        Vector3 rightPupilDirection = (targetPos - rightEye.position).normalized;
        Vector3 rightPupilTargetLocalPosition = rightPupilDirection * maxMoveMagnitude;

        rightPupil.localPosition = rightPupilTargetLocalPosition;

        Vector3 leftPupilDirection = (targetPos - leftEye.position).normalized;
        Vector3 leftPupilTargetLocalPosition = leftPupilDirection * maxMoveMagnitude;

        leftPupil.localPosition = leftPupilTargetLocalPosition;
    }
}
