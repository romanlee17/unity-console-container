namespace romanlee17.ConsoleContainerRuntime {

    internal readonly struct ConsoleMessage {

        public readonly string source;
        public readonly string message;
        public readonly MessageType type;

        public ConsoleMessage(string source, string message, MessageType type) {
            this.source = source;
            this.message = message;
            this.type = type;
        }

    }

}