using UnityEngine;

[System.Serializable]
public class Health {

    [SerializeField]
    byte _currentValue;

    [SerializeField]
    byte _maximumValue;

    [SerializeField]
    bool _invincible;

    [SerializeField]
    bool _incurable;

    [SerializeField]
    HealthEventState _lastHealthState;

    event HealthEventHandler _onChangeHealth;

    #region Properties
    public byte CurrentValue {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    public bool Incurable {
        get { return _incurable; }
        set { _incurable = value; }
    }

    public bool Invincible {
        get { return _invincible; }
        set { _invincible = value; }
    }

    public byte MaximumValue {
        get { return _maximumValue; }
        set { _maximumValue = value; }
    }

    public event HealthEventHandler OnChangeHealth {
        add { _onChangeHealth += value; }
        remove { _onChangeHealth -= value; }
    }
    #endregion

    public Health(byte health, byte maximumHealth) {
        _currentValue = health;
        _maximumValue = maximumHealth;
    }

    /// <summary>Add or subtract from the <see cref="CurrentValue"/></summary>
    /// <returns>Health change amount</returns>
    public int ChangeHealth(int changeValue, GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        return SetHealth(_currentValue + changeValue);
    }

    /// <summary>Lowers the <see cref="CurrentValue"/></summary>
    /// <returns>Health change amount</returns>
    public int Damage(int damageValue, bool invokeChangeEvent = true) {
        return Damage(damageValue, null, null, invokeChangeEvent);
    }
    /// <summary>Lowers the <see cref="CurrentValue"/></summary>
    /// <returns>Health change amount</returns>
    public int Damage(int damageValue, GameObject effectedGameObject, bool invokeChangeEvent = true) {
        return Damage(damageValue, effectedGameObject, null, invokeChangeEvent);
    }
    /// <summary>Lowers the <see cref="CurrentValue"/></summary>
    /// <returns>Health change amount</returns>
    public int Damage(int damageValue, GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        if(damageValue == 0) return 0;
        if(damageValue > 0) damageValue = -damageValue;

        return ChangeHealth(damageValue, effectedGameObject, effectorGameObject, invokeChangeEvent);
    }

    /// <summary>Raises the <see cref="CurrentValue"/></summary>
    public int Heal(int healValue, bool invokeChangeEvent = true) {
        return Heal(healValue, null, null, invokeChangeEvent);
    }
    /// <summary>Raises the <see cref="CurrentValue"/></summary>
    public int Heal(int healValue, GameObject effectedGameObject, bool invokeChangeEvent = true) {
        return Heal(healValue, effectedGameObject, null, invokeChangeEvent);
    }
    /// <summary>Raises the <see cref="CurrentValue"/></summary>
    public int Heal(int healValue, GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        if(healValue == 0) return 0;
        if(healValue < 0) healValue = -healValue;

        return ChangeHealth(healValue, effectedGameObject, effectorGameObject, invokeChangeEvent);
    }

    /// <summary>Causes the OnChangeHealth event to be triggered</summary>
    /// <returns>Health change amount</returns>
    public void InvokeOnChangeHealthEvent(HealthEvent healthEvent) {
        if(_onChangeHealth != null) _onChangeHealth(healthEvent);
    }

    /// <summary>Set the <see cref="CurrentValue"/> to a specified value</summary>
    /// <returns>Health change amount</returns>
    public int SetHealth(int value, bool invokeChangeEvent = true) {
        return SetHealth(value, null, null, invokeChangeEvent);
    }
    /// <summary>Set the <see cref="CurrentValue"/> to a specified value</summary>
    /// <returns>Health change amount</returns>
    public int SetHealth(int value, GameObject effectedGameObject, bool invokeChangeEvent = true) {
        return SetHealth(value, effectedGameObject, null, invokeChangeEvent);
    }
    /// <summary>Set the <see cref="CurrentValue"/> to a specified value</summary>
    /// <returns>Health change amount</returns>
    public int SetHealth(int value, GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        int currentHealth = _currentValue;
        var newHealth = Mathf.Clamp(value, 0, byte.MaxValue);

        if(newHealth < 0) newHealth = 0;
        else if(newHealth > _maximumValue) newHealth = _maximumValue;

        var healthChange = newHealth - currentHealth;

        if(healthChange != 0) {
            HealthEventState eventState;

            if(healthChange > 0) {
                if(_incurable) return 0;

                eventState = newHealth == _maximumValue ? HealthEventState.Full : HealthEventState.Healed;
            }
            else {
                if(_invincible) return 0;

                eventState = newHealth == 0 ? HealthEventState.Died : HealthEventState.Damaged;
            }

            _currentValue = (byte)newHealth;

            var healthEvent = new HealthEvent(healthChange, _maximumValue - _currentValue, _currentValue, eventState, this, effectedGameObject, effectorGameObject);

            _lastHealthState = eventState;

            if(invokeChangeEvent)
                InvokeOnChangeHealthEvent(healthEvent);

            return healthChange;
        }

        return healthChange;
    }

    /// <summary>Set the <see cref="CurrentValue"/> to 0</summary>
    /// <returns>Health change amount</returns>
    public int SetHealthToEmpty(GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        return SetHealth(0, effectedGameObject, effectedGameObject, invokeChangeEvent);
    }

    /// <summary>Set the <see cref="CurrentValue"/> to <see cref="MaximumValue"/></summary>
    /// <returns>Health change amount</returns>
    public int SetHealthToFull(GameObject effectedGameObject, GameObject effectorGameObject, bool invokeChangeEvent = true) {
        return SetHealth(_maximumValue, effectedGameObject, effectedGameObject, invokeChangeEvent);
    }
}

/// <summary>Contains values that have changed and <see cref="GameObject"/>s that are associated with the changes</summary>
public struct HealthEvent {
    public Health effectedHealth;

    /// <summary>The amount of health changed</summary>
    public int changeValue;

    /// <summary>How much health is needed to reach maximum health</summary>
    public int missingValue;

    /// <summary>Current amount of health</summary>
    public byte currentHealth;

    /// <summary>What happened during this health event</summary>
    public HealthEventState eventState;

    /// <summary>The GameObject that was effected</summary>
    public GameObject effectedGameObject;

    /// <summary>The GameObject that caused this HealthEvent</summary>
    public GameObject effectorGameObject;

    public HealthEvent(int changeValue, int missingValue, byte currentHealth, HealthEventState eventState, Health effectedHealth, GameObject effectedGameObject, GameObject effectorGameObject) {
        this.eventState = eventState;
        this.missingValue = missingValue;
        this.changeValue = changeValue;
        this.currentHealth = currentHealth;
        this.effectedHealth = effectedHealth;
        this.effectedGameObject = effectedGameObject;
        this.effectorGameObject = effectorGameObject;
    }
}

public enum HealthEventState {
    Unchanged = 0,
    Died = 1,
    Damaged = 2,
    Full = 3,
    Healed = 4
}

public delegate void HealthEventHandler(HealthEvent healthInfo);

public interface IHealth {
    Health HealthInfo { get; }
}