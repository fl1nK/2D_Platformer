namespace Player
{
    public interface IEntetyInputSource
    {
        float HorizontalDirection{ get; }
        float VerticalDirection { get; }
        bool Jump { get; }
        bool Attack { get; }
        
        void ResetOneTimeActions();
    }
}