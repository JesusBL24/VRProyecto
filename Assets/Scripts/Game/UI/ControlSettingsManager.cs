using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


//Script que gestiona el cambio de bindings
public class ControlSettingsManager : MonoBehaviour
{
    [SerializeField] private ActionManager _actionManager;
    private InputActionAsset _inputActions;

    [SerializeField] private TextMeshProUGUI[] associatedButtonsText = new TextMeshProUGUI[4];
    
    private bool _waitForNextButton = false;
    private int _bindingToChange;
    private int _temporalbidingUpdate;
    private int _bidingToChangeUpdate;


    [SerializeField]  private string[] _buttonTexts = new string[4];
    
    private int[] _actionBindings = new int[4]; //0: Select, 1: ActivarRayos, 2: recognition, 3: Teleport
    
    //Control Derecho
    private InputAction _checkRightSecondary;
    private InputAction _checkRightPrimary;
    private InputAction _checkRightTrigger;
    private InputAction _checkRightGrip;
    
    //Control Izquierdo
    private InputAction _checkLeftSecondary;
    private InputAction _checkLeftPrimary;
    private InputAction _checkLeftTrigger;
    private InputAction _checkLeftGrip;
    
    private const  int TRIGGER = 0;
    private const  int GRIP = 1;
    private const  int PRIMARY_BUTTON = 2;
    private const  int SECONDARY_BUTTON = 3;
    
    private void Awake()
    {
        _inputActions = _actionManager._InputActions;
        
        //Checks de la mano izquierda
        _checkLeftSecondary = _inputActions.FindAction("XRI LeftHand/CheckSecondary");
        _checkLeftPrimary = _inputActions.FindAction("XRI LeftHand/CheckPrimary");
        _checkLeftTrigger = _inputActions.FindAction("XRI LeftHand/CheckTrigger");
        _checkLeftGrip = _inputActions.FindAction("XRI LeftHand/CheckGrip");
        
        //Checks de la mano derehca
        _checkRightSecondary = _inputActions.FindAction("XRI RightHand/CheckSecondary");
        _checkRightPrimary = _inputActions.FindAction("XRI RightHand/CheckPrimary");
        _checkRightTrigger = _inputActions.FindAction("XRI RightHand/CheckTrigger");
        _checkRightGrip = _inputActions.FindAction("XRI RightHand/CheckGrip");
        
        //PlayerPrefs.DeleteAll();
        
        _actionBindings[0] = PlayerPrefs.GetInt("Binding0", TRIGGER);
        _actionBindings[1] = PlayerPrefs.GetInt("Binding1", GRIP);
        _actionBindings[2] = PlayerPrefs.GetInt("Binding2", PRIMARY_BUTTON); 
        _actionBindings[3] = PlayerPrefs.GetInt("Binding3", SECONDARY_BUTTON);
        
        Debug.Log(_actionBindings[0]);
        Debug.Log(_actionBindings[1]);
        Debug.Log(_actionBindings[2]);
        Debug.Log(_actionBindings[3]);

    }

    private void Start()
    {
        Rebind();
        CorrectTexts();
    }

    private void Update()
    {
        if (_waitForNextButton)
        {
            Debug.Log("CambiarTecla");
            //Boton Secundario
            if (_checkLeftSecondary.triggered || _checkRightSecondary.triggered)
            {
                _waitForNextButton = false;
                Debug.Log(_checkLeftSecondary);
                for (int i = 0; i < _actionBindings.Length; i++)
                {
                    if (_actionBindings[i] == SECONDARY_BUTTON)
                    {
                        _bidingToChangeUpdate = i;

                    }
                }

                _temporalbidingUpdate = _actionBindings[_bindingToChange];
                _actionBindings[_bindingToChange] = SECONDARY_BUTTON;
                _actionBindings[_bidingToChangeUpdate] = _temporalbidingUpdate;
            }
            
            //Boton Primario
            else if (_checkLeftPrimary.triggered || _checkRightPrimary.triggered )
            {
                _waitForNextButton = false;
                Debug.Log(_checkLeftPrimary);
                for (int i = 0; i < _actionBindings.Length; i++)
                {
                    if (_actionBindings[i] == PRIMARY_BUTTON)
                    {
                        _bidingToChangeUpdate = i;

                    }
                }

                _temporalbidingUpdate = _actionBindings[_bindingToChange];
                _actionBindings[_bindingToChange] = PRIMARY_BUTTON;
                _actionBindings[_bidingToChangeUpdate] = _temporalbidingUpdate;
            }
            
            //Boton Trigger
            else if (_checkLeftTrigger.triggered || _checkRightTrigger.triggered)
            {
                _waitForNextButton = false;
                Debug.Log(_checkLeftTrigger);
                for (int i = 0; i < _actionBindings.Length; i++)
                {
                    if (_actionBindings[i] == TRIGGER)
                    {
                        _bidingToChangeUpdate = i;

                    }
                }

                _temporalbidingUpdate = _actionBindings[_bindingToChange];
                _actionBindings[_bindingToChange] = TRIGGER;
                _actionBindings[_bidingToChangeUpdate] = _temporalbidingUpdate;
            }
            
            //Boton Grip
            else if (_checkLeftGrip.triggered || _checkRightGrip.triggered)
            {
                _waitForNextButton = false;
                Debug.Log(_checkLeftTrigger);
                for (int i = 0; i < _actionBindings.Length; i++)
                {
                    if (_actionBindings[i] == GRIP)
                    {
                        _bidingToChangeUpdate = i;

                    }
                }

                _temporalbidingUpdate = _actionBindings[_bindingToChange];
                _actionBindings[_bindingToChange] = GRIP;
                _actionBindings[_bidingToChangeUpdate] = _temporalbidingUpdate;
            }

            Rebind();
            CorrectTexts();
            SafeSettings();
            
        }
    }

    public void ActiveBindingRecognition(int bindingToChange)
    {
        _bindingToChange = bindingToChange;
        _waitForNextButton = true;
    }

    private void Rebind()
    {
        _actionManager._leftSelect.ApplyBindingOverride(0,"<XRController>{LeftHand}/{" +_buttonTexts[_actionBindings[0]] +"}");
        _actionManager._rightSelect.ApplyBindingOverride(0,"<XRController>{RightHand}/{" +_buttonTexts[_actionBindings[0]] +"}");

        _actionManager._activateRayL.ApplyBindingOverride(0,"<XRController>{LeftHand}/{" +_buttonTexts[_actionBindings[1]] +"}");
        _actionManager._activateRayR.ApplyBindingOverride(0,"<XRController>{RightHand}/{" +_buttonTexts[_actionBindings[1]] +"}");
        
        _actionManager._leftActivateRecognition.ApplyBindingOverride(0,"<XRController>{LeftHand}/{" +_buttonTexts[_actionBindings[2]] +"}");
        _actionManager._rigthActivateRecognition.ApplyBindingOverride(0,"<XRController>{RightHand}/{" +_buttonTexts[_actionBindings[2]] +"}");

        _actionManager._activateTeleport.ApplyBindingOverride(0, "<XRController>{RightHand}/{" + _buttonTexts[_actionBindings[3]] + "}");
    }
    private void CorrectTexts()
    {
        for (int i = 0; i < _actionBindings.Length; i++)
        {
            switch (_actionBindings[i])
            {
                case TRIGGER:
                    associatedButtonsText[i].text = _buttonTexts[0];
                    break;
                case GRIP:
                    associatedButtonsText[i].text = _buttonTexts[1];
                    break;
                case PRIMARY_BUTTON:
                    associatedButtonsText[i].text = _buttonTexts[2];
                    break;
                case SECONDARY_BUTTON:
                    associatedButtonsText[i].text = _buttonTexts[3];
                    break;
            }
        }
    }
    private void SafeSettings()
    {
        PlayerPrefs.SetInt("Binding0", _actionBindings[0]);
        PlayerPrefs.SetInt("Binding1", _actionBindings[1]);
        PlayerPrefs.SetInt("Binding2", _actionBindings[2]);
        PlayerPrefs.SetInt("Binding3", _actionBindings[3]);
        
        Debug.Log(_actionBindings[0]);
        Debug.Log(_actionBindings[1]);
        Debug.Log(_actionBindings[2]);
        Debug.Log(_actionBindings[3]);
    }
}
