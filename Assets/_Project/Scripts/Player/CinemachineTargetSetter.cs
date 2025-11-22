using UnityEngine;
using Photon.Pun;

#if UNITY_2023_1_OR_NEWER
using Unity.Cinemachine;
#else
using Cinemachine;
#endif

public class CinemachineTargetSetter : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine) return;

        var vcam = GameObject.Find("CM_TopDownCam")?.GetComponent<CinemachineVirtualCameraBase>();
        if (vcam != null)
        {
            vcam.Follow = transform;
            Debug.Log("Caméra liée à : " + this.name);
        }
        else
        {
            Debug.LogWarning("Caméra Cinemachine non trouvée !");
        }
    }
}
