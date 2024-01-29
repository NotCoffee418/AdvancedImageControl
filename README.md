# Advanced Image Control for WPF

[![NuGet](https://img.shields.io/nuget/v/AdvancedImageControl.svg)](https://www.nuget.org/packages/AdvancedImageControl/)


## Features
- Inherit from `Image` control, supporting all it's features
- Panning and zooming

## Usage
Install the [NuGet package](https://www.nuget.org/packages/AdvancedImageControl/):
```powershell
Install-Package AdvancedImageControl
```

Add the following namespace to your XAML file:
```
xmlns:aic="clr-namespace:AdvancedImageControl;assembly=AdvancedImageControl"
```

Then you can use the control like this:
```xml
<aic:AdvancedImageControl Source="image.png" />
```