using UnityEngine;

public class PunchingBag : MonoBehaviour, IHealth {
    [SerializeField]
    Health _healthInfo;

    [SerializeField]
    float _healDelay = 3;

    [SerializeField]
    float _healSpeed = 1;

    [SerializeField]
    Renderer _punchingBagRenderer;

    [SerializeField]
    Color _fullHealthColor;

    [SerializeField]
    Color _noHealthColor;

    float _timeTillHeal;
    float _healSeconds;

    MaterialPropertyBlock _propertyBlock;

    public Health HealthInfo {
        get { return _healthInfo; }
    }

    void Awake() {
        _healthInfo.OnChangeHealth += OnChangeHealth;

        _propertyBlock = new MaterialPropertyBlock();
        _propertyBlock.SetColor("_Color", _fullHealthColor);

        UpdateRendererColor();
    }

    void OnEnable() {
        HealthManager.RegisterHealthObject(gameObject, this);
    }

    void OnDisable() {
        HealthManager.UnregisterHealthObject(gameObject);
    }


    Color GetLerpedColor() {
        float time = (float)_healthInfo.CurrentValue / _healthInfo.MaximumValue;

        return Color.Lerp(_noHealthColor, _fullHealthColor, time);
    }

    void OnChangeHealth(HealthEvent healthEvent) {
        switch(healthEvent.eventState) {
            case HealthEventState.Died:
            case HealthEventState.Damaged:
                _timeTillHeal = _healDelay;
                _healSeconds = 0;

                break;

            case HealthEventState.Full:
                //case HealthEventState.Healed:
                _healSeconds = 0;

                break;
        }

        _propertyBlock.SetColor("_Color", GetLerpedColor());
        UpdateRendererColor();
    }

    void Update() {
        if(_healthInfo.CurrentValue < _healthInfo.MaximumValue)
            if(_timeTillHeal <= 0) {
                _healSeconds += Time.deltaTime;

                if(_healSeconds >= 1) {
                    var healAmount = (int)_healSeconds;

                    _healthInfo.Heal(healAmount);

                    _healSeconds -= healAmount;
                }
            }
            else {
                _timeTillHeal = Mathf.Max(_timeTillHeal - Time.deltaTime, 0);
            }
    }

    public void UpdateRendererColor() {
        _punchingBagRenderer.SetPropertyBlock(_propertyBlock, 1);
    }

    [ContextMenu("Test Damage")]
    void TestDamage() {
        _healthInfo.Damage(1);
    }
}