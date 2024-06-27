using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/* input dialog for your editors
 developed by Dyternal
*/

namespace Dyternal.EditorTools.InputDialog
{
    public class EditorInputDialog : EditorWindow
    {
        public ResultType Result { get; private set; }
        public string InputText { get; private set; } = null;

        private static EditorInputDialog wnd;

        public enum ResultType
        {
            OK,
            Cancel
        };

        public static EditorInputDialog Show(string title, string desc, string OKButtonText = "OK", string CancelButtonText = null)
        {
            wnd = CreateInstance<EditorInputDialog>();
            wnd.titleContent = new GUIContent(title);

            VisualElement root = wnd.rootVisualElement;
            
            root.style.paddingTop = 10;
            root.style.paddingBottom = 10;
            root.style.paddingLeft = 10;
            root.style.paddingRight = 10;
            
            Label descLabel = new Label(desc);
            
            descLabel.style.textOverflow = TextOverflow.Clip;
            descLabel.style.whiteSpace = WhiteSpace.Normal;
            descLabel.style.width = new Length(350, LengthUnit.Pixel);

            TextField textInput = new TextField();

            textInput.style.width = new Length(350, LengthUnit.Pixel);
            textInput.style.height = new Length(20, LengthUnit.Pixel);
            textInput.style.marginTop = 10;

            GroupBox buttons_Box = new GroupBox();

            buttons_Box.style.width = new Length(100, LengthUnit.Percent);
            buttons_Box.style.height = new Length(30, LengthUnit.Pixel);
            buttons_Box.style.justifyContent = Justify.FlexEnd;
            buttons_Box.style.flexDirection = FlexDirection.Row;

            Button buttonOK = new Button(() => OKButtonCallback());
            buttonOK.style.width = new Length(100, LengthUnit.Pixel);
            buttonOK.style.height = new Length(20, LengthUnit.Pixel);
            buttonOK.text = OKButtonText;
            buttons_Box.Add(buttonOK);

            if (CancelButtonText != null)
            {
                Button buttonCancel = new Button(() => wnd.Close());
                buttonCancel.style.width = new Length(100, LengthUnit.Pixel);
                buttonCancel.style.height = new Length(20, LengthUnit.Pixel);
                buttonCancel.text = CancelButtonText;
                buttons_Box.Add(buttonCancel);
            }
            
            root.Add(descLabel);
            root.Add(textInput);
            root.Add(buttons_Box);

            root.RegisterCallback<GeometryChangedEvent>(evt => SetWindowHeight(root));

            wnd.ShowModal();
            
            return wnd;
        }

        private void OnDisable()
        {
            wnd.Result = ResultType.Cancel;
            wnd.InputText = null;
        }

        // Events
        private static void SetWindowHeight(VisualElement root)
        {
            float neededHeight = 0;
            foreach (var element in root.Children()) neededHeight += element.resolvedStyle.height + element.resolvedStyle.marginTop;

            neededHeight += root.resolvedStyle.paddingTop + root.resolvedStyle.paddingBottom;

            wnd.minSize = new Vector2(370, neededHeight);
            wnd.maxSize = wnd.minSize;

            wnd.position = new Rect((Screen.currentResolution.width - wnd.minSize.x) / 2,
                (Screen.currentResolution.height - wnd.minSize.y) / 2, wnd.minSize.x, wnd.minSize.y);

            root.UnregisterCallback<GeometryChangedEvent>(null);
        }
        private static void OKButtonCallback()
        {
            TextField textField = wnd.rootVisualElement.Q<TextField>();
            if (textField.text == "") return;

            wnd.Close();
            wnd.InputText = textField.text;
            wnd.Result = ResultType.OK;
            
        }
    }
}