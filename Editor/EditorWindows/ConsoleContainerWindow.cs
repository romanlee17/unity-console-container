namespace romanlee17.ConsoleContainerEditor {
    using romanlee17.ConsoleContainerRuntime;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using MessageType = ConsoleContainerRuntime.MessageType;

    internal class ConsoleContainerWindow : EditorWindow {

        [SerializeField] private Texture2D windowDarkIcon;
        [SerializeField] private Texture2D windowLightIcon;
        [SerializeField] private VisualTreeAsset consoleContainerUXML;

        [MenuItem("romanlee17/Console Container Window")]
        public static void OpenWindow() {
            ConsoleContainerWindow window = CreateWindow<ConsoleContainerWindow>();
            window.titleContent.text = "Console Container";
            window.titleContent.image = EditorGUIUtility.isProSkin ? window.windowLightIcon : window.windowDarkIcon;
            window.Show();
        }
        private void RefreshDropdownChoices() {
            if (consoleContainerDropdown == null) {
                return;
            }
            consoleContainerDropdown.choices = ConsoleContainer.containers.Keys.Select(x => x.name).ToList();
        }

        private void DisplayConsoleMessage(ConsoleMessage consoleMessage) {
            Label label = new(consoleMessage.ToString());
            label.AddToClassList(consoleMessage.type switch {
                MessageType.Text => "message-text",
                MessageType.Warning => "message-warning",
                MessageType.Error => "message-error",
                _ => "message-text"
            });
            consoleContainerContent.Add(label);
        }

        private void SelectConsoleContainer(string key) {
            // Check if the key is null or empty.
            if (string.IsNullOrEmpty(key) == true) {
                return;
            }
            // Check if the selected console container exists.
            if (ConsoleContainer.containers.Keys.Any(x => x.name == selectedConsoleContainerKey) == false) {
                return;
            }
            // Unsubscribe from the previous console container messages.
            if (consoleContainer != null) {
                consoleContainer.OnConsoleMessage -= DisplayConsoleMessage;
            }
            // Clear the console container content.
            consoleContainerContent.Clear();
            // Select the new console container.
            consoleContainer = ConsoleContainer.containers.Keys.First(x => x.name == key);
            selectedConsoleContainerKey = key;
            // Add all console messages to the console container content.
            foreach (ConsoleMessage consoleMessage in ConsoleContainer.containers[consoleContainer]) {
                DisplayConsoleMessage(consoleMessage);
            }
            // Subscribe to the new console container messages.
            consoleContainer.OnConsoleMessage += DisplayConsoleMessage;
        }

        private string selectedConsoleContainerKey = null;
        private ConsoleContainer consoleContainer = null;

        // Visual elements.
        private VisualElement consoleContainerContent = null;
        private DropdownField consoleContainerDropdown = null;

        private void OnEnable() {
            ConsoleContainer.OnConstructorEvent += RefreshDropdownChoices;
            SelectConsoleContainer(selectedConsoleContainerKey);
        }

        private void OnDisable() {
            ConsoleContainer.OnConstructorEvent -= RefreshDropdownChoices;
            if (consoleContainer != null) {
                consoleContainer.OnConsoleMessage -= DisplayConsoleMessage;
            }
        }

        private void CreateGUI() {
            VisualElement consoleContainerTree = consoleContainerUXML.CloneTree();
            consoleContainerTree.style.flexGrow = 1;
            consoleContainerContent = consoleContainerTree.Q<VisualElement>("content");
            // Dropdown field.
            consoleContainerDropdown = consoleContainerTree.Q<DropdownField>("dropdown");
            consoleContainerDropdown.RegisterValueChangedCallback(callback => {
                consoleContainerDropdown.value = callback.newValue;
                SelectConsoleContainer(callback.newValue);
            });
            RefreshDropdownChoices();
            // Clear button.
            VisualElement clearButton = consoleContainerTree.Q<VisualElement>("clear-button");
            clearButton.RegisterCallback<ClickEvent>(clickEvent => {
                consoleContainerContent.Clear();
                if (consoleContainer != null) {
                    // Assuming that console container always has own message collection.
                    ConsoleContainer.containers[consoleContainer].Clear();
                }
            });
            rootVisualElement.Add(consoleContainerTree);
        }

    }

}