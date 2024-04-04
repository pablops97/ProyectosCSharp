﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ProyectoAnime;

public partial class CustomDialog : Window
{
    private TextBlock mensajeTextBlock;

    public CustomDialog(string mensaje)
    {
        InitializeComponent();
        mensajeTextBlock.Text = mensaje;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        mensajeTextBlock = this.FindControl<TextBlock>("MensajeTextBlock");
    }

    private void CerrarButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}