namespace romanlee17.ConsoleContainerRuntime {

    public class ConsoleContainer : IConsoleContainer {

        public static IConsoleContainer Create() {
            return new ConsoleContainer();
        }

        // Prevent external instantiation.
        private ConsoleContainer() {

        }

        public void CreateMessage(object source, string message) {
            string typeName = source.GetType().Name;
            CreateMessage(typeName, message);
        }

        public void CreateMessage(string source, string message) {

        }

        public void CreateWarning(object source, string message) {
            string typeName = source.GetType().Name;
            CreateWarning(typeName, message);
        }

        public void CreateWarning(string source, string message) {

        }

        public void CreateError(object source, string message) {
            string typeName = source.GetType().Name;
            CreateError(typeName, message);
        }

        public void CreateError(string source, string message) {

        }

    }

}