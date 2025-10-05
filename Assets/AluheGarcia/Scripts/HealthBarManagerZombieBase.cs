using UnityEngine;
using UnityEngine.UI;

public class HealthBarManagerZombieBase : MonoBehaviour
{
    Slider _sliderHealthBar;
    Camera _camera;

    HealthManagerZombieBase _healthManager;

    int _healtBarValue;
    float _healthBarValue;
    void Start()
    {
        _camera = Camera.main;
        _sliderHealthBar = GetComponentInChildren<Slider>();
        _healthManager = GetComponentInParent<HealthManagerZombieBase>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform cam = _camera.transform;
        transform.LookAt(cam);
        _sliderHealthBar.value = _healthManager.Health/100f;
    }
}

