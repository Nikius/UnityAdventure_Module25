using UnityEngine;

namespace Project.Scripts
{
    public class Health
    {
        private const float InjureThreshold = 0.3f;
        
        private readonly IDamageEvents[] _listeners;
        private readonly float _maxHealth;
        
        private float _currentHealth;

        public Health(IDamageEvents[] listeners, float health)
        {
            _listeners = listeners;
            _maxHealth = health;
            _currentHealth = health;
        }
        
        public float GetCurrentHealth => _currentHealth;

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth / _maxHealth <= InjureThreshold)
                foreach (IDamageEvents listener in _listeners)
                    listener.OnInjured();
            
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                foreach (IDamageEvents listener in _listeners)
                    listener.OnDead();
            }
            else
            {
                foreach (IDamageEvents listener in _listeners)
                    listener.OnHit();
            }
            
            Debug.Log($"Current health: {_currentHealth}");
        }
    }
}