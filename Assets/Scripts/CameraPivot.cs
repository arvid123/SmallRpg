using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook freelook;
    [SerializeField]
    private float panSpeed = 0.025f;
    [SerializeField]
    private float smoothFactor = 0.3f;
    private bool isPanning = false;

    public void OnMoveMouse(InputAction.CallbackContext context) {
        float delta = context.ReadValue<float>();
        if (isPanning && delta != 0) {
            freelook.m_XAxis.m_InputAxisValue = delta;
        } else {
            freelook.m_XAxis.m_InputAxisValue = 0;
        }
    }

    public void OnPanCamera(InputAction.CallbackContext context) {
        isPanning = context.ReadValueAsButton();
    }

    void Update()
    {
        
    }
}
