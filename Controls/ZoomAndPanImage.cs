#nullable disable
namespace BigBazar.Controls;


public class ZoomAndPanImage : Image
{
    double currentScale = 1;
    double startScale = 1;
    double xOffset = 0;
    double yOffset = 0;

    public ZoomAndPanImage()
    {
        var pinchGesture = new PinchGestureRecognizer();
        pinchGesture.PinchUpdated += OnPinchUpdated;
        this.GestureRecognizers.Add(pinchGesture);

        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        this.GestureRecognizers.Add(panGesture);

        var tapGesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
        tapGesture.Tapped += OnTapped;
        this.GestureRecognizers.Add(tapGesture);
    }

    void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        if (e.Status == GestureStatus.Started)
        {
            startScale = currentScale;
        }
        if (e.Status == GestureStatus.Running)
        {
            currentScale = Math.Max(1, Math.Min(startScale * e.Scale, 10));
            this.Scale = currentScale;
        }
    }

    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Running)
        {
            xOffset = xOffset + e.TotalX;
            yOffset = yOffset + e.TotalY;
            this.TranslationX = xOffset;
            this.TranslationY = yOffset;
        }
    }

    void OnTapped(object sender, EventArgs e)
    {
        this.Scale = 1;
        this.TranslationX = 0;
        this.TranslationY = 0;
        currentScale = 1;
        xOffset = 0;
        yOffset = 0;
    }
}

