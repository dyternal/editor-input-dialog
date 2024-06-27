# Editor Input Dialog

This Unity package provides a simple input dialog for your Unity Editors. It is useful for creating custom editor tools.

![With Cancel Button](https://i.hizliresim.com/pcaz29f.png)
![Without Cancel Button](https://i.hizliresim.com/68tpj2w.png)


## Installation

1. Download the [latest release](https://github.com/dyternal/editor-input-dialog/releases)
2. Import the package into your Unity project.
3. Done!

## Usage

Add the following using directive to your code:
```csharp
using Dyternal.EditorTools.InputDialog;
```

<hr>

### Declaration of the `Show` function:
```csharp
public static EditorInputDialog Show(string title, string desc, string OKButtonText = "OK", string CancelButtonText = null)
```

### Function parameters:

- `string title`: The title of the dialog.
- `string desc`: The description of the dialog.
- `string OKButtonText="OK"`: The text of the OK button.
- `string CancelButtonText=null`: The text of the Cancel button. If you don't want to show the Cancel button, leave it as `null`.

### Properties of the `EditorInputDialog` class:

- `string InputText`: The text entered by the user.
- `ResultType Result`: The result of the dialog. It can be `ResultType.OK` or `ResultType.Cancel`.

# Usage Example:
```csharp
EditorInputDialog myInputDialog = EditorInputDialog.Show("Title", "Description", "OK", "Cancel");
if(myInputDialog.Result == EditorInputDialog.ResultType.OK)
{
    Debug.Log("OK button clicked");
    Debug.Log("Input: " + myInputDialog.InputText);
}
else if(myInputDialog.Result == EditorInputDialog.ResultType.Cancel)
{
    Debug.Log("Cancel button clicked");
}
```
