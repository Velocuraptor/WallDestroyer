using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private void Start()
    {
        Destroy(gameObject, _particleSystem.main.duration + 0.3f);
    }
}
