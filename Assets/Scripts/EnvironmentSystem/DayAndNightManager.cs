using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[ExecuteAlways]
public class DayAndNightManager : MonoBehaviour
{
    [SerializeField] private  Light directionalLight;
    [SerializeField] private DayAndNightCycle dayAndNightSetting;
    [SerializeField, Range(0, 2400)] private float timeOfDay;


    private void Update(){
        if(dayAndNightSetting == null){
            return;
        }

        if(Application.isPlaying){
            timeOfDay += Time.deltaTime;
            timeOfDay %= 2400; // clamp between 0-24
            UpdateLighting(timeOfDay / 2400f);
        }else{
            UpdateLighting(timeOfDay / 2400f);
        }
    }

    private void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = dayAndNightSetting.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = dayAndNightSetting.fogColor.Evaluate(timePercent);

        if(directionalLight != null){
            directionalLight.color = dayAndNightSetting.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) -90f, -170f, 0));
        }
    }

    private void OnValidate() {
        if(directionalLight != null){
            return;
        }        

        if(RenderSettings.sun != null){
            directionalLight = RenderSettings.sun;
        }else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights){
                if(light.type == LightType.Directional){
                    directionalLight = light;
                    return;
                }
            }
        }
    }

}

