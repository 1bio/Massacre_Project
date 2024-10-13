using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    private float shakeTime;

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannel = virtualCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannel.m_AmplitudeGain = intensity; // ��鸲 ����
        shakeTime = time; // ��鸲 ���� �ð�

    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannel = virtualCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannel.m_AmplitudeGain = 0; // ��鸲 ������ 0���� �ʱ�ȭ
            }
        }
    }
}
