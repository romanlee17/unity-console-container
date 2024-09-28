namespace romanlee17.ConsoleContainerEditor {
    using romanlee17.ConsoleContainerRuntime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
            window.minSize = new Vector2(600, 400);
            window.Show();
        }

        private string WindowUniqueKey {
            get => $"{GetType().Name}[{GetInstanceID()}]";
        }

        private string CurrentDropdownChoice {
            get => EditorPrefs.GetString($"{WindowUniqueKey}.CurrentDropdownChoice", string.Empty);
            set => EditorPrefs.SetString($"{WindowUniqueKey}.CurrentDropdownChoice", value);
        }

        private VisualElement consoleContainerContent = null;
        private DropdownField consoleContainerDropdown = null;
        private List<string> dropdownChoices = new();
        private ConsoleContainer consoleContainer = null;

        private void OnEnable() {
            ConsoleContainer.OnConstructorEvent += OnConsoleContainerCreated;
        }

        private void OnDisable() {
            ConsoleContainer.OnConstructorEvent -= OnConsoleContainerCreated;
        }

        private void CreateGUI() {
            VisualElement consoleContainerTree = consoleContainerUXML.CloneTree();
            consoleContainerTree.style.flexGrow = 1;

            // Content inside ScrollView.
            consoleContainerContent = consoleContainerTree.Q<VisualElement>("content");

            // Dropdown console container selection.
            consoleContainerDropdown = consoleContainerTree.Q<DropdownField>("dropdown");
            consoleContainerDropdown.value = CurrentDropdownChoice;
            consoleContainerDropdown.choices = dropdownChoices;
            consoleContainerDropdown.RegisterValueChangedCallback(callback => {
                consoleContainerDropdown.value = callback.newValue;
                SelectConsoleContainer(callback.newValue);
            });
            RefreshDropdownChoices();

            // Try to select last console container chosen.
            SelectConsoleContainer(CurrentDropdownChoice);

            rootVisualElement.Add(consoleContainerTree);
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

        private void OnConsoleContainerCreated() {
            RefreshDropdownChoices();
            string currentDropdownChoice = CurrentDropdownChoice;
            // Check if dropdown choice is valid.
            if (string.IsNullOrEmpty(currentDropdownChoice) == true) {
                return;
            }
            // Check if selected non-existing console container now exists.
            if (ConsoleContainer.containers.Keys.Any(x => x.name == currentDropdownChoice) == false) {
                return;
            }
            // Check if the selected console container is already selected.
            if (consoleContainer != null && consoleContainer.name == currentDropdownChoice) {
                return;
            }
            // Assign the selected console container to current window.
            SelectConsoleContainer(currentDropdownChoice);
        }

        private void RefreshDropdownChoices() {
            // Clear the dropdown choices.
            dropdownChoices.Clear();
            // Add all console container names to the dropdown choices.
            foreach (string name in ConsoleContainer.containers.Keys.Select(x => x.name)) {
                dropdownChoices.Add(name);
            }
        }

        private void SelectConsoleContainer(string name) {
            CurrentDropdownChoice = name;
            // Check if the window container element exists.
            if (consoleContainerContent == null) {
                return;
            }
            // Check if the selected console container exists.
            if (ConsoleContainer.containers.Keys.Any(x => x.name == name) == false) {
                return;
            }
            // Unsubscribe from the previous console container messages.
            if (consoleContainer != null) {
                consoleContainer.OnConsoleMessage -= DisplayConsoleMessage;
            }
            // Clear the console container content.
            consoleContainerContent.Clear();
            // Select the new console container.
            consoleContainer = ConsoleContainer.containers.Keys.First(x => x.name == name);
            // Add all console messages to the console container content.
            foreach (ConsoleMessage consoleMessage in ConsoleContainer.containers[consoleContainer]) {
                DisplayConsoleMessage(consoleMessage);
            }
            // Subscribe to the new console container messages.
            consoleContainer.OnConsoleMessage += DisplayConsoleMessage;
        }

    }

}