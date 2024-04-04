using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Bitmap = System.Drawing.Bitmap;


namespace ProyectoAnime;

public partial class MainWindow : Window
{
    static public int indice = 0;
    
    List<Personaje> personajesOnePiece = new List<Personaje>();
    List<Personaje> personajesNaruto = new List<Personaje>();
    List<Personaje> personajesDragonBall = new List<Personaje>();
    List<Personaje> personajesBokuNoHero = new List<Personaje>();
    Dictionary<string, List<Personaje>> miDiccionario = new Dictionary<string, List<Personaje>>();
    public List<Personaje> listaSeleccionada;

    public bool Modificacion = false;
    public bool Creacion = false;
    
    
    public MainWindow()
    {
        InitializeComponent();
        //iniciarDiccionario();
        cargarDatos();
        mostrarDatos();
        this.CanResize = false;
        editarTextos(false);
        iniciarBotones(false);
        ListaAnimesLB.IsEnabled = true;
        BotonAtras.IsEnabled = false;
        BotonSiguiente.IsEnabled = false;
    }

    //METODOS PARA LOS BOTONES
    private void Click_Siguiente(object sender, RoutedEventArgs e)
    {
        if (Modificacion)
        {
            ModificarDatos();
            iniciarBotones(true);
            editarTextos(false);
            Modificacion = false;
            botonCambiarImagen.IsVisible = false;

        }else if (Creacion)
        {
            CrearDatos();
            iniciarBotones(true);
            editarTextos(false);
            Creacion = false;
        }
        else
        {
            BotonAtras.IsEnabled = true;
            indice++;
            if (indice == listaSeleccionada.Count - 1)
            {
                BotonSiguiente.IsEnabled = false;
            }
        }
        mostrarDatos();
    }
    
    private void Click_Atras(object sender, RoutedEventArgs e)
    {
        if (Modificacion)
        {
            iniciarBotones(true);
            editarTextos(false);
            Modificacion = false;
            botonCambiarImagen.IsVisible = false;
        }
        else
        {
            BotonSiguiente.IsEnabled = true;
            indice--;
            if (indice == 0)
            {
                BotonAtras.IsEnabled = false;
            }
        }
        mostrarDatos();
        
    }
    
    private void Click_Insertar(object sender, RoutedEventArgs e)
    {
        Creacion = true;
        VaciarDatos();
        editarTextos(true);
        iniciarBotones(false);
        ContarRegistrosTotales();
        ContarRegistros();
        AnimeField.IsEnabled = false;
    }
    
    private void Click_Modificar(object sender, RoutedEventArgs e)
    {
        Modificacion = true;
        editarTextos(true);
        iniciarBotones(false);
        BotonAtras.IsEnabled = true;
        BotonSiguiente.IsEnabled = true;
        AnimeField.IsEnabled = false;
        botonCambiarImagen.IsVisible = true;

    }
    
    private void Click_Eliminar(object sender, RoutedEventArgs e)
    {
        borrar();
        mostrarDatos();
        ContarRegistrosTotales();
        ContarRegistros();
        BotonEliminar.IsEnabled = !isEmpty();
    }
    
    private void Click_Guardar(object? sender, RoutedEventArgs e)
    {
        SerializarDatos();
        //Creo un CustomDialog
        var dialogoFinal = new CustomDialog("Datos guardados con éxito!");
        dialogoFinal.ShowDialog(this);
        
    }
    
    private async void Click_Cambiar_Imagen(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog();
        dlg.Filters!.Add(new FileDialogFilter() { Name = "Archivos de texto", Extensions = { "jpg", "jpeg", "png", "gif", "png" } });
        dlg.AllowMultiple = false;
    
        var result = await dlg.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            listaSeleccionada[indice].Imagen = new Avalonia.Media.Imaging.Bitmap(result[0]);
            FotoAnime.Source = new Avalonia.Media.Imaging.Bitmap(result[0]);
        }
        else
        {
            
        }
        
    }

    private void Seleccion_Lista(object sender, SelectionChangedEventArgs e)
    {
        //A la variable global ListaSeleccionada le doy el valor de la lista de animes que ha sido seleccionada.
        listaSeleccionada  = miDiccionario[(string)ListaAnimesLB.SelectedItem];
        indice = 0;
        BotonSiguiente.IsEnabled = true;
        BotonAtras.IsEnabled = false;
        mostrarDatos();
        ContarRegistrosTotales();
        ContarRegistros();
        iniciarBotones(true);
        BotonEliminar.IsEnabled = !isEmpty();
    }
    
    
    
    
    
    //METODOS PARA CARGAR DATOS

    public void iniciarDiccionario()
    {
        personajesOnePiece.Add(new Personaje("Luffy", "One Piece", 19, 175,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Luffy.jpg"),
            "Monkey D. Luffy es el carismático protagonista del manga y anime \"One Piece\", creado por Eiichiro Oda. Luffy es un joven intrépido y soñador que busca convertirse en el Rey de los Piratas. Su personalidad es alegre, valiente y un tanto ingenua, pero posee una determinación inquebrantable."));
        personajesOnePiece.Add(new Personaje("Zoro", "One Piece", 21, 181,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Zoro.jpg"),
            "Roronoa Zoro es uno de los principales personajes en \"One Piece\". Es el espadachín del sombrero de paja y el espadachín principal del equipo de Luffy. Zoro es conocido por su habilidad con las tres espadas y su determinación inquebrantable en convertirse en el mejor espadachín del mundo."));

        personajesOnePiece.Add(new Personaje("Sanji", "One Piece", 21, 180,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Sanji.jpg"),
            "Vinsmoke Sanji, también conocido como Black Leg, es el cocinero de la tripulación del sombrero de paja. Sanji es experto en el arte marcial del Black Leg Style y utiliza técnicas de piernas en combate. Aunque a veces se presenta como un mujeriego, Sanji es un valioso miembro de la tripulación de Luffy."));

        personajesOnePiece.Add(new Personaje("Nami", "One Piece", 20, 170,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Nami.jpg"),
            "Nami es la navegante del sombrero de paja y una talentosa cartógrafa. Su sueño es trazar un mapa completo del mundo. Inicialmente, Nami se une a la tripulación de Luffy con la esperanza de reunir suficiente dinero para comprar la libertad de su aldea natal. A lo largo de la serie, se convierte en una leal amiga y miembro indispensable de la tripulación."));

        personajesOnePiece.Add(new Personaje("Usopp", "One Piece", 20, 176,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Usopp.jpg"),
            "Usopp es el tirador de la tripulación del sombrero de paja y un hábil francotirador. Su sueño es convertirse en un valiente guerrero de los mares y crear un mapa del mundo de los mares del norte. Usopp es conocido por sus historias exageradas y su habilidad para fabricar inventos sorprendentes. A pesar de su tendencia a mentir, es un miembro querido de la tripulación."));

        personajesNaruto.Add(new Personaje("Naruto Uzumaki", "Naruto", 17, 180,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Naruto.jpg"),
            "Naruto Uzumaki es el protagonista de la serie. Su sueño es convertirse en el Hokage, el líder de su aldea. A pesar de ser un marginado en su infancia, Naruto demuestra ser un ninja valiente y poderoso con un espíritu indomable."));

        personajesNaruto.Add(new Personaje("Sasuke Uchiha", "Naruto", 17, 183,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Sasuke.jpg"),
            "Sasuke Uchiha es un talentoso ninja con el deseo de vengar la destrucción de su clan. A lo largo de la serie, su búsqueda de poder lo lleva por caminos oscuros, pero sigue siendo una figura central en la historia."));

        personajesNaruto.Add(new Personaje("Sakura Haruno", "Naruto", 17, 165,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Sakura.jpg"),
            "Sakura Haruno es una ninja médico y miembro del equipo de Naruto. A lo largo de la serie, desarrolla sus habilidades y demuestra ser una aliada leal. Su relación con Naruto y Sasuke es fundamental en la trama."));

        personajesNaruto.Add(new Personaje("Kakashi Hatake", "Naruto", 30, 181,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Kakashi.jpg"),
            "Kakashi Hatake es un experimentado ninja y líder del equipo de Naruto. Con su famoso Sharingan, Kakashi es un maestro estratega y un mentor crucial para los jóvenes ninjas."));

        personajesNaruto.Add(new Personaje("Hinata Hyuga", "Naruto", 17, 160,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Hinata.jpg"),
            "Hinata Hyuga es una ninja tímida pero poderosa con habilidades únicas. Su amor no correspondido por Naruto la impulsa a superar sus propias limitaciones y destacar como una kunoichi valiente."));


        personajesDragonBall.Add(new Personaje("Goku", "Dragon Ball", 25, 175,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Goku.jpg"),
            "Goku es el legendario Saiyan y protagonista de Dragon Ball. Su búsqueda de poder y deseo de superación lo convierten en uno de los guerreros más fuertes del universo."));

        personajesDragonBall.Add(new Personaje("Vegeta", "Dragon Ball", 28, 170,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Vegeta.jpg"),
            "Vegeta, el Príncipe de los Saiyan, es un guerrero orgulloso con una rivalidad intensa con Goku. A lo largo de la serie, busca superar sus límites y proteger a su familia."));

        personajesDragonBall.Add(new Personaje("Bulma", "Dragon Ball", 23, 165,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Bulma.jpg"),
            "Bulma es una brillante científica y amiga cercana de Goku. Su ingenio tecnológico y su valentía son fundamentales en la lucha contra las fuerzas del mal en Dragon Ball."));

        personajesDragonBall.Add(new Personaje("Piccolo", "Dragon Ball", 30, 226,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Piccolo.jpg"),
            "Piccolo, el Namekiano, es un poderoso guerrero y mentor de Gohan. A pesar de su origen inicialmente malévolo, Piccolo se convierte en uno de los héroes clave en la historia."));

        personajesDragonBall.Add(new Personaje("Gohan", "Dragon Ball", 18, 175,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Gohan.jpg"),
            "Gohan, el hijo de Goku, es un prodigio con un gran potencial. A medida que crece, demuestra ser un guerrero formidable con un corazón noble."));

        personajesBokuNoHero.Add(new Personaje("Izuku Midoriya", "Boku no Hero Academia", 16, 166,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Midoriya.jpg"),
            "Izuku Midoriya, también conocido como Deku, es un estudiante en la U.A. High School. A pesar de nacer sin poderes, su determinación lo lleva a convertirse en un héroe respetado."));

        personajesBokuNoHero.Add(new Personaje("Katsuki Bakugo", "Boku no Hero Academia", 16, 172,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Bakugo.jpg"),
            "Katsuki Bakugo es un estudiante talentoso con una personalidad explosiva. Su objetivo es convertirse en el héroe número uno, y su rivalidad con Deku impulsa su crecimiento."));

        personajesBokuNoHero.Add(new Personaje("Ochaco Uraraka", "Boku no Hero Academia", 16, 160,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Uraraka.jpg"),
            "Ochaco Uraraka es una estudiante amigable con la capacidad de manipular la gravedad. Su deseo de apoyar a su familia la motiva a convertirse en una heroína exitosa."));

        personajesBokuNoHero.Add(new Personaje("Tenya Iida", "Boku no Hero Academia", 16, 179,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Iida.jpg"),
            "Tenya Iida es un estudiante disciplinado y el vicepresidente de la clase. Su sentido de la justicia y velocidad lo convierten en un héroe valioso en la academia."));

        personajesBokuNoHero.Add(new Personaje("Shoto Todoroki", "Boku no Hero Academia", 16, 180,
            new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/Todoroki.jpg"),
            "Shoto Todoroki es un estudiante con habilidades de hielo y fuego. Su lucha interna con su padre lo impulsa a encontrar su propio camino como héroe."));

        
        miDiccionario.Add("One Piece", personajesOnePiece);
        miDiccionario.Add("Naruto", personajesNaruto);
        miDiccionario.Add("Dragon Ball", personajesDragonBall);
        miDiccionario.Add("Boku No Hero", personajesBokuNoHero);

        ListaAnimesLB.Items.Add("One Piece");
        ListaAnimesLB.Items.Add("Naruto");
        ListaAnimesLB.Items.Add("Dragon Ball");
        ListaAnimesLB.Items.Add("Boku No Hero");

    }

    public async void cargarDatos()
    {
        var dlg = new OpenFileDialog();

        dlg.Filters!.Add(new FileDialogFilter() { Name = "Todos los archivos", Extensions = { "*" } });
        dlg.AllowMultiple = false;

        var result = await dlg.ShowAsync(this);

        if (result != null && result.Length > 0)
        {
            DeserializarDatos(result[0]);
        }
        else
        {
            var dialog = new CustomDialog("Se van a cargar los datos automaticos");
            dialog.ShowDialog(this);
            iniciarDiccionario();
        }
    }
    
    //Tengo que utilizar las imagenes como bitmap en la clase personajes
    //Crear 4 archivos, cada uno con los miembros de la lista para sealizarlo y tenerlo mejor ordenado

    void SerializarDatos()
    {
        try
        {
            //con un binaryWriter sobreescribimos el archivo
            using (BinaryWriter bw = new BinaryWriter(File.Open("dataBank.data", FileMode.Create)))
            {
                List<Personaje> aux = miDiccionario["Naruto"];
                foreach (Personaje p in aux)
                {
                    bw.Write(p.Nombre);
                    bw.Write(p.Anime);
                    bw.Write(p.Edad);
                    bw.Write(p.Altura);
                    bw.Write(p.Descripcion);
                    //escribimos los datos y pasamos la foto al array de  bytes
                    byte[] fotoBytes = p.GetFotoBytes();
                    //guardamos el tamaño del array antes de la foto
                    bw.Write(fotoBytes.Length);
                    bw.Write(fotoBytes);
                    //se guarda el array
                    
                }
                aux = miDiccionario["One Piece"];
                foreach (Personaje p in aux)
                {
                    bw.Write(p.Nombre);
                    bw.Write(p.Anime);
                    bw.Write(p.Edad);
                    bw.Write(p.Altura);
                    bw.Write(p.Descripcion);
                    //escribimos los datos y pasamos la foto al array de  bytes
                    byte[] fotoBytes = p.GetFotoBytes();
                    //guardamos el tamaño del array antes de la foto
                    bw.Write(fotoBytes.Length);
                    bw.Write(fotoBytes);
                    //se guarda el array
                    
                }
                
                aux = miDiccionario["Dragon Ball"];
                foreach (Personaje p in aux)
                {
                    bw.Write(p.Nombre);
                    bw.Write(p.Anime);
                    bw.Write(p.Edad);
                    bw.Write(p.Altura);
                    bw.Write(p.Descripcion);
                    //escribimos los datos y pasamos la foto al array de  bytes
                    byte[] fotoBytes = p.GetFotoBytes();
                    //guardamos el tamaño del array antes de la foto
                    bw.Write(fotoBytes.Length);
                    bw.Write(fotoBytes);
                    //se guarda el array
                    
                }

                if (miDiccionario.ContainsKey("Boku No Hero"))
                {
                    aux = miDiccionario["Boku No Hero"];
                    foreach (Personaje p in aux)
                    {
                        bw.Write(p.Nombre);
                        bw.Write(p.Anime);
                        bw.Write(p.Edad);
                        bw.Write(p.Altura);
                        bw.Write(p.Descripcion);
                        //escribimos los datos y pasamos la foto al array de  bytes
                        byte[] fotoBytes = p.GetFotoBytes();
                        //guardamos el tamaño del array antes de la foto
                        bw.Write(fotoBytes.Length);
                        bw.Write(fotoBytes);
                        //se guarda el array
                    
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ha habido un error");
        }
    }


    public void DeserializarDatos(string rutaArchivo)
    {
        List<Personaje> listaGeneral = new List<Personaje>();
        List<Personaje> listaOnePiece = new List<Personaje>();
        List<Personaje> listaNaruto = new List<Personaje>();
        List<Personaje> listaDragonBall = new List<Personaje>();
        List<Personaje> listaBokuNoHero = new List<Personaje>();
        
        

        try
        {
            using (BinaryReader br = new BinaryReader(File.Open(rutaArchivo, FileMode.Open)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {

                    string nombre = br.ReadString();
                    string anime = br.ReadString();
                    int edad = br.ReadInt32();
                    int altura = br.ReadInt32();
                    string descripcion = br.ReadString();
                    int fotoLength = br.ReadInt32();
                    byte[] fotoBytes = br.ReadBytes(fotoLength);


                    Personaje p = new Personaje(nombre, anime, edad, altura, null, descripcion);
                    p.SetFotoBytes(p.byteArrayToImage(fotoBytes));
                    listaGeneral.Add(p);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
        }

        miDiccionario.Clear();
        foreach (Personaje p in listaGeneral)
        {
            switch (p.Anime)
            {
                case "One Piece":
                    listaOnePiece.Add(p);
                    break;
                case "Naruto":
                    listaNaruto.Add(p);
                    break;
                case "Dragon Ball":
                    listaDragonBall.Add(p);
                    break;
                default:
                    listaBokuNoHero.Add(p);
                    break;
            }
        }
        
        miDiccionario.Clear();
        miDiccionario.Add("One Piece", listaOnePiece);
        miDiccionario.Add("Naruto", listaNaruto);
        miDiccionario.Add("Dragon Ball", listaDragonBall);
        miDiccionario.Add("Boku No Hero", listaBokuNoHero);
        
        ListaAnimesLB.Items.Add("One Piece");
        ListaAnimesLB.Items.Add("Naruto");
        ListaAnimesLB.Items.Add("Dragon Ball");
        ListaAnimesLB.Items.Add("Boku No Hero");
    }
    
    
    
    
    //METODOS DE VISTA
    
    public void mostrarDatos()
    {
        if (ListaAnimesLB.SelectedItem != null)
        {
            
            if (listaSeleccionada.Count != 0)
            {
                //Se realizan las configuraciones oportunas
            
                NombreField.Text = listaSeleccionada[indice].Nombre;
                FotoAnime.Source = listaSeleccionada[indice].Imagen;
                EdadField.Text = "" + listaSeleccionada[indice].Edad;
                AnimeField.Text = listaSeleccionada[indice].Anime;
                AlturaField.Text = "" + listaSeleccionada[indice].Altura;
                DescripcionField.Text = listaSeleccionada[indice].Descripcion;
                Console.WriteLine(indice);
                
            }else
            {
                VaciarDatos();
            
            }
        }else
        {
            VaciarDatos();
            
        }
        
        
        
    }

    public void VaciarDatos()
    {
        NombreField.Text = null;
        FotoAnime.Source = new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/No_Imagen.jpg");
        EdadField.Text = null;
        AnimeField.Text = null;
        AlturaField.Text = null;
        DescripcionField.Text = null;
    }
    
    public void editarTextos(Boolean b)
    {
        NombreField.IsEnabled = b;
        EdadField.IsEnabled = b;
        AnimeField.IsEnabled = b;
        AlturaField.IsEnabled = b;
        DescripcionField.IsEnabled = b;
    }

    public void ContarRegistros()
    {
        if (listaSeleccionada.Count > 0)
        {
            NumeroRegistrosField.Text = "" + listaSeleccionada.Count;
        }
        else
        {
            NumeroRegistrosField.Text = "" + 0;
        }
        Console.WriteLine(listaSeleccionada.Count);
    }

    public void ContarRegistrosTotales()
    {
        int numeroRegistrosTotales = 0;
        foreach (var VARIABLE in miDiccionario)
        {
            numeroRegistrosTotales += VARIABLE.Value.Count;
        }

        NumeroRegistroTotalField.Text = "" + numeroRegistrosTotales;
    }

    public bool isEmpty()
    {
        return listaSeleccionada.Count == 0;
    }

    
    //METODOS PARA GESTION DE LISTAS
    
    public void borrar()
    {
        
        
        listaSeleccionada.RemoveAt(indice);
        indice = 0;
        mostrarDatos();

        
        if (listaSeleccionada.Count == 0)
        {
            BotonEliminar.IsEnabled = false;
            BotonModificar.IsEnabled = false;
            BotonSiguiente.IsEnabled = false;
            VaciarDatos();
        }
        
        

    }
    
    private void ModificarDatos()
    {

        
    var dialog = new CustomDialog("");
    
    if (NombreField.Text.Length == 0 || EdadField.Text.Length == 0 || AlturaField.Text.Length == 0 || DescripcionField.Text.Length == 0)
    {
        dialog = new CustomDialog("No pueden haber datos vacios!");
        dialog.ShowDialog(this);
        
    }
    else
    {//Si los campos de texto no estan vacios, da la opcion de modificar la imagen
        listaSeleccionada[indice].Nombre = NombreField.Text;
        try
        {
            // Intenta convertir el texto a un número entero
            listaSeleccionada[indice].Edad = int.Parse(EdadField.Text);

        }
        catch (FormatException)
        {
            // Captura la excepción si el formato no es válido (por ejemplo, si se introducen letras)
            
            dialog = new CustomDialog("La edad no va a ser modificada ya que ha introducido un texto");
            dialog.ShowDialog(this);
        }
        catch (OverflowException)
        {
            // Captura la excepción si el número es demasiado grande o demasiado pequeño para un int

            dialog = new CustomDialog("El número introducido es demasiado grande o demasiado pequeño.");
            dialog.ShowDialog(this);
        }
        try
        {
            listaSeleccionada[indice].Altura = int.Parse(AlturaField.Text);
        }
        catch (FormatException)
        {
            dialog = new CustomDialog("La altura no va a ser modificada ya que ha introducido un texto");
            dialog.ShowDialog(this);
        }
        catch (OverflowException)
        {
            dialog = new CustomDialog("El número introducido es demasiado grande o demasiado pequeño.");
            dialog.ShowDialog(this);
        }
        
        listaSeleccionada[indice].Descripcion = DescripcionField.Text;
    }
    
}
    
    public void CrearDatos()
{
    var dialog = new CustomDialog("");
    //Compruebo si los huecos estan vacios, en ese caso no hace nada y muestra mensaje de error
    if (NombreField.Text.Length == 0 || EdadField.Text.Length == 0 || AlturaField.Text.Length == 0 || DescripcionField.Text.Length == 0)
    {
        
        dialog = new CustomDialog("No se ha podido crear el personaje porque faltan datos");
        dialog.ShowDialog(this);
    }
    else
    {
        //selecciono la lista y abro un openFileDialog
            try
            {
                listaSeleccionada.Add(new Personaje(NombreField.Text, ListaAnimesLB.SelectedItem.ToString(), int.Parse(EdadField.Text), int.Parse(AlturaField.Text), new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/No_Imagen.jpg"), DescripcionField.Text));
            }
            catch (FormatException)
            {
                // Captura la excepción si el formato no es válido (por ejemplo, si se introducen letras)
                
                dialog = new CustomDialog("Por favor, introduce una edad o altura válidas.");
                dialog.ShowDialog(this);
                
            }
            catch (OverflowException)
            {
                // Captura la excepción si el número es demasiado grande o demasiado pequeño para un int
                
                dialog = new CustomDialog("El número introducido es demasiado grande o demasiado pequeño.");
                dialog.ShowDialog(this);
            }
           
        
            
        
        
    }
    ContarRegistros();
    ContarRegistrosTotales();
}

    
    
    //METODOS DE UTILIDAD


    private async void AgregarFoto()
    {
        var dlg = new OpenFileDialog();
        dlg.Filters!.Add(new FileDialogFilter() { Name = "Archivos de texto", Extensions = { "jpg", "jpeg", "png", "gif", "png" } });
        dlg.AllowMultiple = false;
    
        var result = await dlg.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            listaSeleccionada[indice].Imagen = new Avalonia.Media.Imaging.Bitmap(result[0]);
        }
        else
        {
            listaSeleccionada[indice].Imagen = new Avalonia.Media.Imaging.Bitmap("../../../Imagenes/No_Imagen.jpg");
        }
    }

    
    public void iniciarBotones(Boolean b)
    {
        BotonEliminar.IsEnabled = b;
        BotonGuardar.IsEnabled = b;
        BotonInsertar.IsEnabled = b;
        BotonModificar.IsEnabled = b;
        ListaAnimesLB.IsEnabled = b;
    }
    
    
    
}
