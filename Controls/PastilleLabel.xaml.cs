#nullable disable
using BigBazar.Messages;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Diagnostics;

namespace BigBazar.Controls;

public partial class PastilleLabel : ContentView
{

    public PastilleLabel()
    {
        InitializeComponent();
        BindingContext = this;
        Guard.IsNotNull(Application.Current);
        if (Application.Current?.RequestedTheme == AppTheme.Light)
        {
            var hasValue = Application.Current.Resources.TryGetValue("Gray200", out object theColor);
            if (hasValue)
            {
                BackColorPastille = (Color)theColor;
            }
        }
        else
        {
            var hasValue = Application.Current.Resources.TryGetValue("Gray950", out object theColor);
            if (hasValue)
            {
                BackColorPastille = (Color)theColor;
            }
        }

    }

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText), typeof(string), typeof(PastilleLabel), default(string));

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
        nameof(IconSource), typeof(string), typeof(PastilleLabel), "closecircle.png");

    public static readonly BindableProperty FlashColorProperty = BindableProperty.Create(
        nameof(FlashColor), typeof(Color), typeof(PastilleLabel), Colors.White);

    public static readonly BindableProperty FlashDurationProperty = BindableProperty.Create(
        nameof(FlashDuration), typeof(int), typeof(PastilleLabel), 100);

    public static readonly BindableProperty PayloadProperty = BindableProperty.Create(
        nameof(Payload), typeof(object), typeof(PastilleLabel), null);

    public static readonly BindableProperty BackColorPastilleProperty = BindableProperty.Create(
        nameof(BackColorPastille), typeof(Color), typeof(PastilleLabel), Colors.Black, propertyChanged: OnBackColorPastilleChanged);

    public static readonly BindableProperty PayloadIconKindProperty = BindableProperty.Create(
        nameof(PayloadIconKind),
        typeof(PayloadKind),
        typeof(PastilleLabel),
        PayloadKind.Delete,
        propertyChanged: OnPayloadIconKindChanged);

    private static void OnPayloadIconKindChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PastilleLabel)bindable;
        var newPayloadIconKind = (PayloadKind)newValue;
        control.IconSource = newPayloadIconKind switch
        {
            PayloadKind.None => null,
            PayloadKind.Add => "circleadd.png",
            PayloadKind.Delete => "closecircle.png",
            PayloadKind.Custom => control.IconSource,
            _ => throw new ArgumentOutOfRangeException()
        };
    }


    private static void OnBackColorPastilleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Guard.IsNotNull(bindable);
        var pastilleLabel = (PastilleLabel)bindable;
        pastilleLabel.frameBase.BackgroundColor = (Color)newValue;
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public Color FlashColor
    {
        get => (Color)GetValue(FlashColorProperty);
        set => SetValue(FlashColorProperty, value);
    }

    public int FlashDuration
    {
        get => (int)GetValue(FlashDurationProperty);
        set => SetValue(FlashDurationProperty, value);
    }

    public object Payload
    {
        get => (object)GetValue(PayloadProperty);
        set => SetValue(PayloadProperty, value);
    }

    public Color BackColorPastille
    {
        get => (Color)GetValue(BackColorPastilleProperty);
        set => SetValue(BackColorPastilleProperty, value);
    }

    public PayloadKind PayloadIconKind
    {
        get => (PayloadKind)GetValue(PayloadIconKindProperty);
        set => SetValue(PayloadIconKindProperty, value);
    }

    private DateTime lastClickTime;

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        // debounce the click
        if ((DateTime.Now - lastClickTime).TotalMilliseconds < 500) // 500 ms debounce
            return;
       

        Guard.IsNotNull(sender);
        var b = (ImageButton)sender;
        if (!b.IsEnabled)
        {
            return;
        }

       
        lastClickTime = DateTime.Now;

        b.IsEnabled = false;
        try
        {
            var originalColor = frameBase.BackgroundColor;
            frameBase.BackgroundColor = FlashColor;
            await Task.Delay(FlashDuration);
            frameBase.BackgroundColor = originalColor;
            var message = new PastilleMessage(this, Payload, PayloadIconKind);
            WeakReferenceMessenger.Default.Send(message);
        }
        finally
        {
            b.IsEnabled = true;
        }
    }
}