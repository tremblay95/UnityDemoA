namespace UnityDemoA
{
    public interface IEffectHandler<T>
    {
        void HandleEffect(T effect);
    }
    
    public interface IEffectHandler : IEffectHandler<int>
    {
        // void HandleEffect(int effect);
    }
}