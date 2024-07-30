namespace Core.View
{
    public interface ICharacterInputs
    {
        public float MoveAxisForward { get; set; }
        public float MoveAxisRight { get; set; }
        public bool JumpDown { get; set; }
        public bool CrouchDown { get; set; }
        public bool CrouchUp { get; set; }
    }
}
