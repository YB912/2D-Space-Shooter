
using UnityEngine;

public class Player : MonoBehaviour, IHealable
{
    [SerializeField] private float _maximumHealth;

    private float _currentHealth;

    private ProjectileDamageInput _projectileDamageInput;

    public float maximumHealth { get => _maximumHealth; }

    public float currentHealth { get => _currentHealth; }

    private void Awake()
    {
        _projectileDamageInput = GetComponentInChildren<ProjectileDamageInput>();
        _projectileDamageInput.takeDamage += TakeDamage;
    }

    private void Start()
    {
        _currentHealth = _maximumHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        Debug.Log($"Player suffered {damage} damage. Remaining health: {_currentHealth}");
    }

    public void Regenerate(float regeneration)
    {
        _currentHealth = Mathf.Min(_currentHealth + regeneration, _maximumHealth);
    }
}
