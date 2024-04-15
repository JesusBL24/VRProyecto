using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MyLightActivation : MonoBehaviour
{
    [SerializeField] private InputActionReference actionTrigger;
    private XRGrabInteractable grab;

    [SerializeField] private Light spotLight;
    // Start is called before the first frame update
    void Start()
    {
        grab = gameObject.GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.isSelected)
        {
            var val1 = (actionTrigger.action.ReadValue<float>());
            var min1 = 0;
            var max1 = 1;
            var range1 = max1 - min1;

            var min2 = 0;
            var max2 = 5;
            var range2 = max2 - min2;

            var val2 = val1*range2/range1 + min2;
            spotLight.intensity = val2;
        }
    }
}
