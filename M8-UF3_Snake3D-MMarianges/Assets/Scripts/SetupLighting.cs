using UnityEngine;

public class SetupLighting : MonoBehaviour
{
    void Start()
    {
        // Crear una luz direccional si no existe
        if (FindObjectOfType<Light>() == null)
        {
            GameObject lightGameObject = new GameObject("Directional Light");
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.type = LightType.Directional;
            lightComp.color = Color.white;
            lightComp.intensity = 1.0f;
            lightComp.transform.rotation = Quaternion.Euler(50, -30, 0); // Ajusta la dirección de la luz
        }

        // Configurar la luz ambiental
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = Color.gray; // Ajusta esto para cambiar la luz ambiental

        // Configurar el Skybox si es necesario
        // RenderSettings.skybox = ... (asigna tu material de Skybox aquí si es necesario)
    }
}
