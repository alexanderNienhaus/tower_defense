using UnityEngine;

public class AnimationSpeedHelper : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplierMin = 0.75f;
    [SerializeField]
    private float speedMultiplierMax = 1.25f;


    private void Start()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            Random.InitState(Random.seed + System.DateTime.Now.Millisecond);
            animator.speed *= Random.Range(speedMultiplierMin, speedMultiplierMax);
        }
    }
}
