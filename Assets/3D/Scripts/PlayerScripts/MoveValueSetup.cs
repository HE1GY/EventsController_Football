namespace PlayerScripts
{
    public struct MoveValueSetup
    {
        public readonly float WalkSpeed;
        public readonly float RunSpeed;
        public readonly float JumpHeight;

        public MoveValueSetup(float walkSpeed, float runSpeed, float jumpHeight)
        {
            WalkSpeed = walkSpeed;
            RunSpeed = runSpeed;
            JumpHeight = jumpHeight;
        }
    }
}