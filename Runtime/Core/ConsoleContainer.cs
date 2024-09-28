namespace romanlee17.ConsoleContainerRuntime {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleContainer : IConsoleContainer {

        public static IConsoleContainer Create(string name = default) {
            if (containers.Any(x => x.Key.name == name)) {
                int duplicateCount = containers.Count(x => x.Key.name.Contains($"{name} ("));
                name = $"{name} ({duplicateCount + 1})";
            }
            return new ConsoleContainer(name);
        }

        // Prevent external instantiation.
        private ConsoleContainer(string name) {
            this.name = string.IsNullOrEmpty(name) ? $"Console [{containers.Count}]" : name;
            containers.Add(this, new());
            OnConstructorEvent?.Invoke();
        }

        // Editor runtime fields.
        internal readonly string name;
        internal static event Action OnConstructorEvent;
        internal event Action<ConsoleMessage> OnConsoleMessage;
        internal static readonly Dictionary<ConsoleContainer, MessageCollection> containers = new();

        private void CreateConsoleMessage(string source, string message, MessageType type) {
            ConsoleMessage consoleMessage = new(source, message, type);
            containers[this].Add(consoleMessage);
            OnConsoleMessage?.Invoke(consoleMessage);
        }

        // Text message.

        public void CreateText(object source, string message) {
            CreateText(source.GetType().Name, message);
        }

        public void CreateText(string source, string message) {
            CreateConsoleMessage(source, message, MessageType.Text);
        }

        // Warning message.

        public void CreateWarning(object source, string message) {
            CreateWarning(source.GetType().Name, message);
        }

        public void CreateWarning(string source, string message) {
            CreateConsoleMessage(source, message, MessageType.Warning);
        }

        // Error message.

        public void CreateError(object source, string message) {
            CreateError(source.GetType().Name, message);
        }

        public void CreateError(string source, string message) {
            CreateConsoleMessage(source, message, MessageType.Error);
        }

    }

}