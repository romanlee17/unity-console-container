namespace romanlee17.ConsoleContainerEditor {
    using romanlee17.ConsoleContainerRuntime;
    using System.Collections.Generic;
    using System.Linq;
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
        private DropdownField instanceDropdown = null;
        private ConsoleContainer consoleContainer = null;

        private void OnEnable() {
            ConsoleContainer.OnConsoleCreated += OnConsoleCreated;
        }

        private void OnDisable() {
            ConsoleContainer.OnConsoleCreated -= OnConsoleCreated;
        }

        private void OnConsoleCreated() {
            if (instanceDropdown == null) {
                return;
            }
            // Refresh instance dropdown.
            instanceDropdown.choices = ConsoleContainer.containers.Keys.Select(x => x.name).ToList();
        }

        private void OnConsoleMessage(ConsoleMessage consoleMessage) {
            // Add message to content root.

        }

        private void OnDropdownInstanceChange(string value) {
            if (consoleContainer != null) {
                // Unsubscribe from previous instance.
                consoleContainer.OnConsoleMessage -= OnConsoleMessage;
            }
            // Clear console from previous messages.
            contentRoot.Clear();
            // Get messages from selected instance.
            consoleContainer = ConsoleContainer.containers.Keys.First(x => x.name == value);
            MessageCollection messageCollection = ConsoleContainer.containers[consoleContainer];
            // Add messages to content root.
            foreach (ConsoleMessage consoleMessage in messageCollection) {

            }
        }

        public void CreateGUI() {

            titleContent.text = "Console Container";
            titleContent.image = EditorGUIUtility.isProSkin ? consoleContainerIconLight : consoleContainerIconDark;

            // Create a window root visual element.
            VisualElement windowRoot = consoleContainerUXML.Instantiate();
            windowRoot.style.flexGrow = 1;

            // Get content root from window root.
            contentRoot = windowRoot.Q<VisualElement>("content-root");

            // Clear button.
            VisualElement clearButton = windowRoot.Q<VisualElement>("clear-button");
            clearButton.RegisterCallback<ClickEvent>(clickEvent => {
                contentRoot.Clear();
            });

            // Instance dropdown.
            instanceDropdown = windowRoot.Q<DropdownField>("instance-dropdown");
            instanceDropdown.choices = ConsoleContainer.containers.Keys.Select(x => x.name).ToList();
            instanceDropdown.RegisterValueChangedCallback<string>(value => {
                OnDropdownInstanceChange(value.newValue);
            });

            // Add window root to root visual element.
            rootVisualElement.Add(windowRoot);

        }


    }

}