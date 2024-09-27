namespace romanlee17.ConsoleContainerEditor {
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    internal class ConsoleContainerWindow : EditorWindow {

        [SerializeField] private VisualTreeAsset consoleContainerUXML;

        [MenuItem("romanlee17/Console Container Window")]
        public static void OpenWindow() {
            // Get existing open window or if none, make a new one.
            ConsoleContainerWindow window = GetWindow<ConsoleContainerWindow>();
            window.titleContent = new GUIContent("Console Container");
            window.minSize = new Vector2(600, 400);
        }

    }

}