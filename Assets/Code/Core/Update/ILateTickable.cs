namespace Code.Core.Update
{
    public interface ILateTickable
    {
        public void LateTick(float deltaTime);
    }
}