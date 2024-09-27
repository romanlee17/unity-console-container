namespace romanlee17.ConsoleContainerRuntime {
    using System;
    using System.Collections.Generic;

    public class ConsoleContainer : IConsoleContainer {

        public static IConsoleContainer Create(string name = default) {
            return new ConsoleContainer(name);
        }

        // Prevent external instantiation.
        private ConsoleContainer(string name) {
            Name = string.IsNullOrEmpty(name) ? $"Console [{containers.Count}]" : name;
            containers.Add(this, new());
            OnConsoleCreated?.Invoke();
        }

        // Editor runtime fields.
        internal static event Action OnConsoleCreated;
        internal event Action<ConsoleMessage> OnConsoleMessage;
        internal static readonly Dictionary<IConsoleContainer, MessageCollection> containers = new();

        private void CreateConsoleMessage(string source, string message, MessageType type) {
            ConsoleMessage consoleMessage = new(source, message, type);
            containers[this].Add(consoleMessage);
            OnConsoleMessage?.Invoke(consoleMessage);
        }

        public string Name { get; } = string.Empty;

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