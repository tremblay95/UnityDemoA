namespace UnityDemoA
{
    public interface IEffectHandler<T> where T : IGameplayEffect
    {
        void HandleEffect(T effect);
    }
}