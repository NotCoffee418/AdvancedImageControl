namespace AdvancedImageControl.Behaviors;

public class PanBehavior : Behavior<Image>
{
    private Point _origin;
    private Point _start;
    private ScaleTransform scaleTransform;
    private TranslateTransform translateTransform;

    protected override void OnAttached()
    {
        base.OnAttached();

        // Initialize the ScaleTransform and TranslateTransform
        InitializeTransforms();

        AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
        AssociatedObject.MouseMove += OnMouseMove;
    }

    private void InitializeTransforms()
    {
        var image = AssociatedObject;

        // Ensure the image has a TransformGroup as its RenderTransform.
        TransformGroup group = image.RenderTransform as TransformGroup;
        if (group == null)
        {
            group = new TransformGroup();
            image.RenderTransform = group;
        }

        // Try to get the ScaleTransform and TranslateTransform from the group.
        scaleTransform = group.Children.OfType<ScaleTransform>().FirstOrDefault();
        translateTransform = group.Children.OfType<TranslateTransform>().FirstOrDefault();

        // If not found, create new instances and add them to the group.
        if (scaleTransform == null)
        {
            scaleTransform = new ScaleTransform();
            group.Children.Add(scaleTransform);
        }

        if (translateTransform == null)
        {
            translateTransform = new TranslateTransform();
            group.Children.Add(translateTransform);
        }
    }


    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
        AssociatedObject.MouseMove -= OnMouseMove;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var image = sender as Image;
        _origin = e.GetPosition(image.Parent as UIElement);

        // Use the current values from the transforms
        _start = new Point(translateTransform.X, translateTransform.Y);

        image.CaptureMouse();
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var image = sender as Image;
        image.ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!AssociatedObject.IsMouseCaptured) return;

        var image = sender as Image;
        var currentPosition = e.GetPosition(image.Parent as UIElement); // Get position relative to the image's container

        // Calculate the deltas accounting for the scale
        var dx = (currentPosition.X - _origin.X) / scaleTransform.ScaleX;
        var dy = (currentPosition.Y - _origin.Y) / scaleTransform.ScaleY;

        // Apply the adjusted deltas to the translation
        translateTransform.X = _start.X + dx;
        translateTransform.Y = _start.Y + dy;
    }
}
