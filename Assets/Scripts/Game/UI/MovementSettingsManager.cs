using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementSettingsManager : ASingleton<MovementSettingsManager>
{
    //Valores de los ajustes
    [HideInInspector] public float snapAngle;
    [HideInInspector] public bool isSnapActivated;
    
    [HideInInspector] public float tunnelingRatio;
    [HideInInspector] public float transparencyValue;
    [HideInInspector] public bool isTunnelingActivated;
    
    //Elementos gráficos del frontEnd
    [SerializeField] private Slider _turnAngleSlider;
    [SerializeField] private TextMeshProUGUI _changeTypeOfTurnText;
    [SerializeField] private TextMeshProUGUI _degreesOfTurnText;
    
    [SerializeField] private Slider _ratioSlider;
    [SerializeField] private Slider _transparencySlider;
    [SerializeField] private Toggle _tunnelingToggle;
    
    //Scripts asociados
    //Turn
    [SerializeField] private ActionBasedContinuousTurnProvider _contiousTurnProvider;
    [SerializeField] private ActionBasedSnapTurnProvider _snapTurnProvider;
    
    //Tunneling
    [SerializeField] private TunnelingVignetteController _tunnelingController;
    
    private void Awake()
    {
        //Llama al método awake de ASingleton
        base.Awake();
        
        //Obtiene los valores guardados en las preferencias
        Instance.isSnapActivated = PlayerPrefs.GetInt("snapActivated", 1) == 1 ? true : false;
        Instance.snapAngle = PlayerPrefs.GetFloat("snapAngle",1f);
        
        Instance.tunnelingRatio = PlayerPrefs.GetFloat("tunnelingRatio",1f);
        Instance.transparencyValue = PlayerPrefs.GetFloat("transparencyValue",1f);
        Instance.isTunnelingActivated = PlayerPrefs.GetInt("isTunnelingActivated", 1) == 1 ? true : false;
    }
    private void Start()
    {
        //Turn
        Instance._turnAngleSlider.value = Instance.snapAngle;
        _snapTurnProvider.turnAmount = (Instance._turnAngleSlider.value * 90f);
        
        //Tunneling
        
            //Activado
            Instance._tunnelingToggle.isOn = Instance.isTunnelingActivated;
            Instance._tunnelingController.enabled = Instance.isTunnelingActivated;
        
            //Radio
        Instance._ratioSlider.value = Instance.tunnelingRatio;
        _tunnelingController.defaultParameters.apertureSize = Instance.tunnelingRatio;
        
            //Transparencia
        Instance._transparencySlider.value = Instance.transparencyValue;
        _tunnelingController.defaultParameters.featheringEffect = Instance.transparencyValue;
        
        Instance.isSnapActivated = !Instance.isSnapActivated;
        SetDegreesText();
        ChangeTypeOfTurn();
        ChangeTransparency();
        ChangeSnapAngle();
        ChangeTunnelingActivation();
        ChangeTunnelingRatio();
        
    }
    
    //**************AJUSTES DE TURN********************
    
    //Función que cambia el angulo de giro del snap turn
    public void ChangeSnapAngle()
    {
        Instance._snapTurnProvider.turnAmount = (Instance._turnAngleSlider.value * 90f);
        SetDegreesText();
        
        PlayerPrefs.SetFloat("snapAngle", Instance._turnAngleSlider.value);
    }

    //Función que cambia el tipo de giro
    public void ChangeTypeOfTurn()
    {
         Instance.isSnapActivated = !Instance.isSnapActivated;

        if (Instance.isSnapActivated)
        {
            _changeTypeOfTurnText.text = "Saltos";
            _snapTurnProvider.enabled = true;
            _contiousTurnProvider.enabled = false;
            Instance._turnAngleSlider.interactable = true;
        }
        else
        {
            _changeTypeOfTurnText.text = "Continuo";
            _snapTurnProvider.enabled = false;
            _contiousTurnProvider.enabled = true;
            Instance._turnAngleSlider.interactable = false;
        }
        PlayerPrefs.SetFloat("snapActivated", Instance.isSnapActivated == true ? 1 : 0);
    }

    //Cambiar el texto donde se incluye el angulo de giro
    private void SetDegreesText()
    {
        _degreesOfTurnText.text = "Ángulo de giro: " + Mathf.FloorToInt(Instance._turnAngleSlider.value * 90f) + "º";
    }

    //**************AJUSTES DE TUNNELING********************
    public void ChangeTunnelingActivation()
    {
        _tunnelingController.gameObject.SetActive(_tunnelingToggle.isOn);
        PlayerPrefs.SetFloat("isTunnelingActivated",  Instance._tunnelingToggle.isOn ? 1 : 0);
    }
    
    //Función que cambia el radio del Tunneling
    public void ChangeTunnelingRatio()
    {
        Instance.tunnelingRatio = Instance._ratioSlider.value;
        _tunnelingController.defaultParameters.apertureSize = Instance.tunnelingRatio;
        PlayerPrefs.SetFloat("tunnelingRatio", Instance.tunnelingRatio);
    }
    
    //Función que cambia el radio del Tunneling
    public void ChangeTransparency()
    {
        Instance.transparencyValue = Instance._transparencySlider.value;
        _tunnelingController.defaultParameters.featheringEffect = Instance.transparencyValue;
        PlayerPrefs.SetFloat("transparencyValue", Instance.transparencyValue);
    }

    
}

