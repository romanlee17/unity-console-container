namespace romanlee17.ConsoleContainerRuntime {

    /// <summary>Defines a contract for creating text messages, warning
    /// messages or error messages inside dedicated container.</summary>
    public interface IConsoleContainer {

        /// <summary>Creates a text message inside the dedicated container.</summary>
        /// <param name="source">The source object of the text message.</param>
        /// <param name="message">The text message related to the source.</param>
        public void CreateText(object source, string message);

        /// <summary>Creates a text message inside the dedicated container.</summary>
        /// <param name="source">The source name of the text message.</param>
        /// <param name="message">The text message related to the source.</param>
        public void CreateText(string source, string message);

        /// <summary>Creates a warning message inside the dedicated container.</summary>
        /// <param name="source">The source object of the warning message.</param>
        /// <param name="message">The warning message related to the source.</param>
        public void CreateWarning(object source, string message);

        /// <summary>Creates a warning message inside the dedicated container.</summary>
        /// <param name="source">The source name of the warning message.</param>
        /// <param name="message">The warning message related to the source.</param>
        public void CreateWarning(string source, string message);

        /// <summary>Creates an error message inside the dedicated container.</summary>
        /// <param name="source">The source object of the error message.</param>
        /// <param name="message">The error message related to the source.</param>
        public void CreateError(object source, string message);

        /// <summary>Creates an error message inside the dedicated container.</summary>
        /// <param name="source">The source name of the error message.</param>
        /// <param name="message">The error message related to the source.</param>
        public void CreateError(string source, string message);

        /// <summary>Clear all messages in container and dispose it.</summary>
        public void Dispose();

    }

}