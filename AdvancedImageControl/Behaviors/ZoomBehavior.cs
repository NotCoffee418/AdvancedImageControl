namespace AdvancedImageControl.Behaviors;

public class ZoomBehavior : Behavior<Image>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseWheel += OnMouseWheel;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseWheel -= OnMouseWheel;
    }

    private void OnMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var image = sender as Image;

        // Ensure the RenderTransform is a TransformGroup
        if (!(image.RenderTransform is TransformGroup transformGroup))
        {
            transformGroup = new TransformGroup();
            transformGroup.Children.Add(new ScaleTransform());
            transformGroup.Children.Add(new TranslateTransform());
            image.RenderTransform = transformGroup;
        }

        // Find or create the ScaleTransform within the TransformGroup
        var scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
        if (scaleTransform == null)
        {
            scaleTransform = new ScaleTransform();
            transformGroup.Children.Add(scaleTransform);
        }

        // Apply zoom
        var zoom = e.Delta > 0 ? 0.2 : -0.2;
        scaleTransform.ScaleX += zoom;
        scaleTransform.ScaleY += zoom;
    }

}

