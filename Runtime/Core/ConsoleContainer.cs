using System.Collections.Generic;

namespace romanlee17.ConsoleContainerRuntime {

    public class ConsoleContainer : IConsoleContainer {

        public static IConsoleContainer Create(string name) {
            return new ConsoleContainer(name);
        }

        // Prevent external instantiation.
        private ConsoleContainer(string name) {
            this.name = name;
        }

        internal readonly string name;
        internal static readonly Dictionary<IConsoleContainer, string[]> containers = new();

        public void CreateText(object source, string message) {
            string typeName = source.GetType().Name;
            CreateText(typeName, message);
        }

        public void CreateText(string source, string message) {

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