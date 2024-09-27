namespace romanlee17.ConsoleContainerEditor {
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    internal class ConsoleContainerWindow : EditorWindow {

        [SerializeField] private Texture2D consoleContainerIconDark;
        [SerializeField] private Texture2D consoleContainerIconLight;
        [SerializeField] private VisualTreeAsset consoleContainerUXML;

        [MenuItem("romanlee17/Console Container Window")]
        public static void OpenWindow() {
            // Get existing open window or if none, make a new one.
            ConsoleContainerWindow window = CreateWindow<ConsoleContainerWindow>();
            window.minSize = new Vector2(600, 400);
        }

        // Runtime fields.

        private VisualElement contentRoot = null;

        public void CreateGUI() {
            titleContent.text = "Console Container";
            titleContent.image = EditorGUIUtility.isProSkin ? consoleContainerIconLight : consoleContainerIconDark;
            // Create a root visual element.
            VisualElement windowRoot = consoleContainerUXML.Instantiate();
            windowRoot.style.flexGrow = 1;
            contentRoot = windowRoot.Q<VisualElement>("content-root");
            // Clear button.
            VisualElement clearButton = windowRoot.Q<VisualElement>("clear-button");
            clearButton.RegisterCallback<ClickEvent>(clickEvent => {
                contentRoot.Clear();
            });
            // Add window root to root visual element.
            rootVisualElement.Add(windowRoot);
        }


    }

}