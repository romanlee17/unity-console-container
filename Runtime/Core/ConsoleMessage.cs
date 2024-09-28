namespace romanlee17.ConsoleContainerRuntime {
    using System;

    internal readonly struct ConsoleMessage {

        public readonly string source;
        public readonly string message;
        public readonly MessageType type;
        public readonly DateTime dateTime;

        public ConsoleMessage(string source, string message, MessageType type) {
            this.source = source;
            this.message = message;
            this.type = type;
            dateTime = DateTime.Now;
        }

        public override string ToString() {
            return $"[{dateTime:HH:mm:ss}] {source}: {message}";
        }

    }

}