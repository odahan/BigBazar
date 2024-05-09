#nullable disable
using System.Windows.Input;
namespace BigBazar.Controls;

public partial class LabeledEntry : Grid
{
    public static readonly BindableProperty LabelTextProperty 
        = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(LabeledEntry), default(string));
    public static readonly BindableProperty EntryTextProperty 
        = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(LabeledEntry), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty TextChangedCommandProperty 
        = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(LabeledEntry), default(ICommand));
    public static readonly BindableProperty LabelWidthProperty 
        = BindableProperty.Create(nameof(LabelWidth), typeof(GridLength), typeof(LabeledEntry), new GridLength(1, GridUnitType.Star));
    public static readonly BindableProperty PlaceholderProperty 
        = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(LabeledEntry), default(string));
    public static readonly BindableProperty IsEntryReadOnlyProperty 
        = BindableProperty.Create(nameof(IsEntryReadOnly), typeof(bool), typeof(LabeledEntry), default(bool));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string EntryText
    {
        get => (string)GetValue(EntryTextProperty);
        set => SetValue(EntryTextProperty, value);
    }

    public ICommand TextChangedCommand
    {
        get => (ICommand)GetValue(TextChangedCommandProperty);
        set => SetValue(TextChangedCommandProperty, value);
    }

    public GridLength LabelWidth
    {
        get => (GridLength)GetValue(LabelWidthProperty);
        set => SetValue(LabelWidthProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public bool IsEntryReadOnly
    {
        get => (bool)GetValue(IsEntryReadOnlyProperty);
        set => SetValue(IsEntryReadOnlyProperty, value);
    }

    public LabeledEntry()
    {
        ColumnDefinitions.Add(new ColumnDefinition { Width = LabelWidth });
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var label = new Label();
        label.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
        this.Add(label);

        var entry = new Entry();
        entry.SetBinding(Entry.TextProperty, new Binding(nameof(EntryText), BindingMode.TwoWay, source: this));
        entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
        entry.SetBinding(InputView.IsReadOnlyProperty, new Binding(nameof(IsEntryReadOnly), source: this));
        entry.TextChanged += (s, e) => TextChangedCommand?.Execute(e.NewTextValue);
        this.Add(entry, 1);
    }
}