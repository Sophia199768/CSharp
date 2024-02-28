namespace Controllers.Chain;

public abstract record ControllersResult
{
        private ControllersResult()
        {
        }

        public record SuccessRegistration() : ControllersResult;

        public record Success() : ControllersResult;

        public record SuccessBalance(int AmountOfMoney) : ControllersResult;

        public record NoSuchCommand(string Message) : ControllersResult;

        public record MistakeOfSign(string Message) : ControllersResult;
}