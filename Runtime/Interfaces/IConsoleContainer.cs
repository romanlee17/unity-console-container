namespace romanlee17.ConsoleContainerRuntime {

    /// <summary>Defines a contract for creating messages, warnings or errors inside dedicated container.</summary>
    public interface IConsoleContainer {

        /// <summary>Creates a message inside the dedicated container.</summary>
        /// <param name="source">The source object of the message.</param>
        /// <param name="message">The message related to the source.</param>
        public void CreateMessage(object source, string message);

        /// <summary>Creates a message inside the dedicated container.</summary>
        /// <param name="source">The source name of the message.</param>
        /// <param name="message">The message related to the source.</param>
        public void CreateMessage(string source, string message);

        /// <summary>Creates a warning inside the dedicated container.</summary>
        /// <param name="source">The source object of the warning.</param>
        /// <param name="message">The warning related to the source.</param>
        public void CreateWarning(object source, string message);

        /// <summary>Creates a warning inside the dedicated container.</summary>
        /// <param name="source">The source name of the warning.</param>
        /// <param name="message">The warning related to the source.</param>
        public void CreateWarning(string source, string message);

        /// <summary>Creates an error inside the dedicated container.</summary>
        /// <param name="source">The source object of the error.</param>
        /// <param name="message">The error related to the source.</param>
        public void CreateError(object source, string message);

        /// <summary>Creates an error inside the dedicated container.</summary>
        /// <param name="source">The source name of the error.</param>
        /// <param name="message">The error related to the source.</param>
        public void CreateError(string source, string message);

    }

}