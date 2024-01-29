namespace AdvancedImageControl;

public class AdvancedImage : Image
{
    public AdvancedImage()
    {
        var transformGroup = new TransformGroup();
        var scaleTransform = new ScaleTransform();
        var translateTransform = new TranslateTransform();
        transformGroup.Children.Add(scaleTransform); // For zooming
        transformGroup.Children.Add(translateTransform); // For panning

        RenderTransform = transformGroup;
        RenderTransformOrigin = new Point(0.5, 0.5);

        ZoomBehavior zoomBehavior = new ZoomBehavior();
        PanBehavior panBehavior = new PanBehavior();
        Interaction.GetBehaviors(this).Add(zoomBehavior);
        Interaction.GetBehaviors(this).Add(panBehavior);
    }

}
