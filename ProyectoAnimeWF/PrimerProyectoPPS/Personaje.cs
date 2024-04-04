using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerProyectoPPS
{
    [Serializable]
    internal class Personaje
    {
        public string Nombre {  get; set; }
        public string Anime {  get; set; }
        public int Edad {  get; set; }
        public int Altura {  get; set; }
        public Image Imagen { get; set; }

        public string Descripcion { get; set; }


        public Personaje(string nombre, string anime, int edad, int altura, Image imagen, string descripcion) { 
            
            this.Nombre = nombre;
            this.Anime = anime;
            this.Edad = edad;
            this.Altura = altura;
            this.Imagen = imagen;
            this.Descripcion = descripcion;
        }

        //hacer un metodo para meter en un archivo todos los datos de cada personaje

        public void insertarPersonaje(BinaryWriter b)
        {
            byte[] imagen = imageToByteArray(Imagen);
            try
            {
                
                b.Write(Nombre);
                b.Write(Anime);
                b.Write(Edad);
                b.Write(Altura);
                b.Write(imagen.Length);
                b.Write(imagen);
                b.Write(Descripcion);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static Personaje extraerPersonaje(BinaryReader b)
        {
            Personaje aux = null;
            
            try
            {
                string nombre = b.ReadString();
                string anime =  b.ReadString();
                int edad = b.Read();
                int altura = b.Read();
                int tamaño = b.ReadInt32();
                byte[] imagen = b.ReadBytes(tamaño);
                string descripcion = b.ReadString();
                aux = new Personaje(nombre, anime, edad, altura, null, descripcion);
                aux.Imagen = aux.byteArrayToImage(imagen);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return aux;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }



    }
}
