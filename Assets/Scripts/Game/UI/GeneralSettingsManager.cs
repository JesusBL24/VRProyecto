using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Patterns.Singleton
{
    //Clase encargada de gestionar los ajustes del juego
    public class GeneralSettingsManager : ASingleton<GeneralSettingsManager>
    {
        //Valores de los ajustes
        [HideInInspector] public float generalVolume;
        //[HideInInspector] public AudioSource musica;
        [HideInInspector] public float brillo;
        
        //Elementos gráficos del frontEnd
        [SerializeField] private Image _panelBrillo;
        [SerializeField] private Slider _sliderGeneralVolume;
        [SerializeField] private Slider _sliderBrillo;
        
        private AudioSource[] allAudioSources; //Todos los audiosources de la escena
        private void Awake()
        {
            //Llama al método awake de ASingleton
            base.Awake();
            
            //Obtiene los valores guardados en las preferencias
            Instance.brillo = PlayerPrefs.GetFloat("brillo",0.7f);
            Instance.generalVolume = PlayerPrefs.GetFloat("volume",1f);
        }
        private void Start()
        {
            //Desasocia los eventos al slider de volumen general, si no se desasocia llama a su evento
            //y estropea la carga del volumen de musica y sonido.

            //Actualiza los elementos de los ajustes
            Instance._sliderGeneralVolume.value = Instance.generalVolume;
            Instance._sliderBrillo.value = Instance.brillo;
            
            Instance.allAudioSources = FindObjectsOfType<AudioSource>(); //obtener los audioSurces

            //Iniciar a valores por defecto o valores guardado
            ChangeAudioSourcesVolume();
            changeBrightnessPanel();
        }
        
        
        //Función que cambia el volumen general 
        public void ChangeGeneralVolume(float addEventListenerHandler)
        {
            Instance.generalVolume = Instance._sliderGeneralVolume.value;
            ChangeAudioSourcesVolume();
            
            PlayerPrefs.SetFloat("volume", Instance.generalVolume);
        }

        private void ChangeAudioSourcesVolume()
        {
            foreach (var audio in allAudioSources)
            {
                audio.volume = Instance.generalVolume;
            }
        }

        //Función que cambia el brillo de la pantalla
        public void ChangeBrillo(float addEventListenerHandler)
        {
            PlayerPrefs.SetFloat("brillo", Instance.brillo);
            Instance.brillo = Instance._sliderBrillo.value;
            changeBrightnessPanel();
        }

        //Iguala el alpha del panel al de los ajustes 
        private void changeBrightnessPanel()
        {
            Instance._panelBrillo.color = new Color(Instance._panelBrillo.color.r, Instance._panelBrillo.color.g, Instance._panelBrillo.color.b, Instance.brillo);
        }
    }
}
