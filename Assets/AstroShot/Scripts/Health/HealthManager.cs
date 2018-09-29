using System.Collections.Generic;
using UnityEngine;

public class HealthManager {

    static Dictionary<GameObject, IHealth> healthObjects;

    static bool _isInitialized = false;

    /// <summary>Clears the <see cref="healthObjects"/> dictionary</summary>
    public static void ClearHealthObjects() {
        healthObjects.Clear();
    }

    public static void Initialize() {
        if(_isInitialized) return;

        if(healthObjects == null)
            healthObjects = new Dictionary<GameObject, IHealth>();

        _isInitialized = true;
    }

    public static int Damage(int damageValue, GameObject effectedGameObject, GameObject effectorGameObject) {
        IHealth health;

        if(TryGetHealthObject(effectedGameObject, out health))
            return health.HealthInfo.Damage(damageValue, effectedGameObject, effectorGameObject);

        return 0;
    }

    public static int Heal(int healValue, GameObject effectedGameObject, GameObject effectorGameObject) {
        IHealth health;

        if(TryGetHealthObject(effectedGameObject, out health))
            return health.HealthInfo.Heal(healValue, effectedGameObject, effectorGameObject);

        return 0;
    }

    public static void RegisterHealthObject(GameObject gameObject, IHealth health) {
        Initialize();

        healthObjects.Add(gameObject, health);
    }

    public static bool TryGetHealthObject(GameObject gameObject, out IHealth health) {
        return healthObjects.TryGetValue(gameObject, out health);
    }

    public static void UnregisterHealthObject(GameObject gameObject) {
        healthObjects.Remove(gameObject);
    }
}