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
        public bool Result { get; private set; }
        public string InputText { get; private set; } = null;

        public static EditorInputDialog Show(string title, string desc, string OKButtonText = "OK", string CancelButtonText = "Cancel")
        {
            EditorInputDialog wnd = CreateInstance<EditorInputDialog>();
            wnd.titleContent = new GUIContent(title);

            VisualElement root = wnd.rootVisualElement;

            root.style.paddingTop = 10;
            root.style.paddingBottom = 10;
            root.style.paddingLeft = 10;
            root.style.paddingRight = 10;

            Label descLabel = new Label(desc);
            descLabel.name = "desc-label";
            descLabel.style.textOverflow = TextOverflow.Clip;
            descLabel.style.whiteSpace = WhiteSpace.Normal;
            descLabel.style.width = new Length(350, LengthUnit.Pixel);

            TextField textInput = new TextField();
            textInput.name = "text-input";
            textInput.style.width = new Length(350, LengthUnit.Pixel);
            textInput.style.marginTop = 10;

            VisualElement buttons_Box = new VisualElement();
            
            buttons_Box.name = "buttons-box";
            buttons_Box.style.width = new Length(100, LengthUnit.Percent);
            buttons_Box.style.height = new Length(30, LengthUnit.Pixel);
            buttons_Box.style.justifyContent = Justify.FlexEnd;
            buttons_Box.style.flexDirection = FlexDirection.Row;

            Button buttonOK = new Button(() => OKButtonCallback(wnd));
            buttonOK.style.width = new Length(100, LengthUnit.Pixel);
            buttonOK.style.height = new Length(20, LengthUnit.Pixel);
            buttonOK.style.marginTop = 10;
            buttonOK.text = OKButtonText;

            Button buttonCancel = new Button(() => CancelButtonCallback(wnd));
            buttonCancel.style.width = new Length(100, LengthUnit.Pixel);
            buttonCancel.style.height = new Length(20, LengthUnit.Pixel);
            buttonCancel.style.marginTop = 10;
            buttonCancel.text = CancelButtonText;

            buttons_Box.Add(buttonOK);
            buttons_Box.Add(buttonCancel);

            root.Add(descLabel);
            root.Add(textInput);
            root.Add(buttons_Box);

            root.RegisterCallback<GeometryChangedEvent>(evt => SetWindowHeight(root, wnd));

            wnd.ShowModal();

            return wnd;
        }
        
        // Events
        private static void SetWindowHeight(VisualElement root, EditorWindow wnd)
        {
            float neededHeight = 0;
            foreach (var element in root.Children()) neededHeight += element.resolvedStyle.height + element.resolvedStyle.marginTop;
            
            neededHeight += root.resolvedStyle.paddingTop + root.resolvedStyle.paddingBottom + 1;

            wnd.minSize = new Vector2(370, neededHeight);
            wnd.maxSize = wnd.minSize;

            wnd.position = new Rect((Screen.currentResolution.width - wnd.minSize.x) / 2,
                (Screen.currentResolution.height - wnd.minSize.y) / 2, wnd.minSize.x, wnd.minSize.y);

            root.UnregisterCallback<GeometryChangedEvent>(null);
        }
        private static void OKButtonCallback(EditorInputDialog wnd)
        {
            TextField textField = wnd.rootVisualElement.Q<TextField>();
            if (textField.text == "") return;

            wnd.InputText = textField.text;
            wnd.Close();
            wnd.Result = true;
        }
        private static void CancelButtonCallback(EditorInputDialog wnd)
        {
            wnd.Close();
            wnd.Result = false;
            wnd.InputText = null;
        }
    }
}