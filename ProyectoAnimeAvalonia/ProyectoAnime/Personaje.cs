using System;
using System.Drawing;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media;
using Bitmap = System.Drawing.Bitmap;
namespace ProyectoAnime;

[Serializable]
public class Personaje 
{
    public string Nombre {  get; set; }
    public string Anime {  get; set; }
    public int Edad {  get; set; }
    public int Altura {  get; set; }
    public Avalonia.Media.Imaging.Bitmap Imagen { get; set; }

    public byte[] ImagenByte { get; set; }
    public string Descripcion { get; set; }


    public Personaje(string nombre, string anime, int edad, int altura, Avalonia.Media.Imaging.Bitmap imagen, string descripcion) { 
    
        this.Nombre = nombre;
        this.Anime = anime;
        this.Edad = edad;
        this.Altura = altura;
        this.Imagen = imagen;
        this.Descripcion = descripcion;
    }
    public Personaje(string nombre, string anime, int edad, int altura, string descripcion) { 
    
        this.Nombre = nombre;
        this.Anime = anime;
        this.Edad = edad;
        this.Altura = altura;
        this.Imagen = null;
        this.Descripcion = descripcion;
    }
    
    public Avalonia.Media.Imaging.Bitmap GetFoto()
    {
        if (ImagenByte != null && ImagenByte.Length > 0)
        {
            return ByteArrayToBitmap(ImagenByte);
        }
        return null;
    }

    public byte[] GetFotoBytes()
    {
        return BitmapToByteArray(Imagen);
    }

    public void SetFotoBytes(Avalonia.Media.Imaging.Bitmap foto)
    {
        if (foto != null)
        {
            Imagen = foto;
        }
    }

    private byte[] BitmapToByteArray(Avalonia.Media.Imaging.Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms);
            return ms.ToArray();
        }
    }

    private Avalonia.Media.Imaging.Bitmap ByteArrayToBitmap(byte[] byteArray)
    {
        using (MemoryStream ms = new MemoryStream(byteArray))
        {
            return new Avalonia.Media.Imaging.Bitmap(ms);
        }
    }
    
    public Avalonia.Media.Imaging.Bitmap byteArrayToImage(byte[] byteArrayIn)
    {
        try
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                // Creamos una imagen a partir de la secuencia de memoria
                var bitmap = new Avalonia.Media.Imaging.Bitmap(ms);
                return bitmap;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al convertir bytes a imagen: {ex.Message}");
            return null;
        }
    }
}
