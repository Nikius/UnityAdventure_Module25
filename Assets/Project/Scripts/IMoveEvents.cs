namespace Project.Scripts
{
    public interface IMoveEvents
    {
        void OnMoveStarted();
        void OnMoveComplete();

        void OnHit();

        void OnDead();
    }
}