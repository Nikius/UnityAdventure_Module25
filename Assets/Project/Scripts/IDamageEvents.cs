namespace Project.Scripts
{
    public interface IDamageEvents
    {
        void OnInjured();
        void OnHit();
        void OnDead();
    }
}