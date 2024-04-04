using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ProyectoAnime;

public partial class CustomDialogSiNo : Window
{
    private TextBlock mensajeTextBlock;
    public bool resultado;
    public CustomDialogSiNo(string mensaje, ref bool resultado)
    {
        InitializeComponent();
        mensajeTextBlock.Text = mensaje;
        this.resultado = resultado;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        mensajeTextBlock = this.FindControl<TextBlock>("MensajeTextBlock");
    }

    private void Click_si(object? sender, RoutedEventArgs e)
    {
        resultado = true;
        Close();
    }
    
    private void Click_no(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    
}