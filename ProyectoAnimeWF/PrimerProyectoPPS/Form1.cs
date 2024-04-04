using System;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization.Formatters.Binary;

namespace PrimerProyectoPPS
{
    public partial class Form1 : Form
    {
        string listaAnimeSeleccionada;
        List<Personaje> personajesOnePiece = new List<Personaje>();
        List<Personaje> personajesNaruto = new List<Personaje>();
        List<Personaje> personajesDragonBall = new List<Personaje>();
        List<Personaje> personajesBokuNoHero = new List<Personaje>();

        Dictionary<string, List<Personaje>> miDiccionario = new Dictionary<string, List<Personaje>>();
        List<List<Personaje>>[] listaListas = new List<List<Personaje>>[3];
        Boolean modificar = false;
        Boolean insertar = false;
        static int numeroRegistros = 0;


        static public int indice = 0;
        public Form1()
        {
            InitializeComponent();
            //iniciarDiccionario();
            CargarDiccionario();
            mostrarDatos();
            botonEliminar.Enabled = false;
            registosField.Enabled = false;
            RegistroPorListaField.Enabled = false;


        }

        public void iniciarDiccionario()
        {
            personajesOnePiece.Add(new Personaje("Luffy", "One Piece", 19, 175, Image.FromFile("../../../Imagenes/Luffy.jpg"),
                "Monkey D. Luffy es el carismático protagonista del manga y anime \"One Piece\", creado por Eiichiro Oda. Luffy es un joven intrépido y soñador que busca convertirse en el Rey de los Piratas. Su personalidad es alegre, valiente y un tanto ingenua, pero posee una determinación inquebrantable."));
            personajesOnePiece.Add(new Personaje("Zoro", "One Piece", 21, 181, Image.FromFile("../../../Imagenes/Zoro.jpg"),
            "Roronoa Zoro es uno de los principales personajes en \"One Piece\". Es el espadachín del sombrero de paja y el espadachín principal del equipo de Luffy. Zoro es conocido por su habilidad con las tres espadas y su determinación inquebrantable en convertirse en el mejor espadachín del mundo."));

            personajesOnePiece.Add(new Personaje("Sanji", "One Piece", 21, 180, Image.FromFile("../../../Imagenes/Sanji.jpg"),
                "Vinsmoke Sanji, también conocido como Black Leg, es el cocinero de la tripulación del sombrero de paja. Sanji es experto en el arte marcial del Black Leg Style y utiliza técnicas de piernas en combate. Aunque a veces se presenta como un mujeriego, Sanji es un valioso miembro de la tripulación de Luffy."));

            personajesOnePiece.Add(new Personaje("Nami", "One Piece", 20, 170, Image.FromFile("../../../Imagenes/Nami.jpg"),
                "Nami es la navegante del sombrero de paja y una talentosa cartógrafa. Su sueño es trazar un mapa completo del mundo. Inicialmente, Nami se une a la tripulación de Luffy con la esperanza de reunir suficiente dinero para comprar la libertad de su aldea natal. A lo largo de la serie, se convierte en una leal amiga y miembro indispensable de la tripulación."));

            personajesOnePiece.Add(new Personaje("Usopp", "One Piece", 20, 176, Image.FromFile("../../../Imagenes/Usopp.jpg"),
                "Usopp es el tirador de la tripulación del sombrero de paja y un hábil francotirador. Su sueño es convertirse en un valiente guerrero de los mares y crear un mapa del mundo de los mares del norte. Usopp es conocido por sus historias exageradas y su habilidad para fabricar inventos sorprendentes. A pesar de su tendencia a mentir, es un miembro querido de la tripulación."));

            personajesNaruto.Add(new Personaje("Naruto Uzumaki", "Naruto", 17, 180, Image.FromFile("../../../Imagenes/Naruto.jpg"),
                "Naruto Uzumaki es el protagonista de la serie. Su sueño es convertirse en el Hokage, el líder de su aldea. A pesar de ser un marginado en su infancia, Naruto demuestra ser un ninja valiente y poderoso con un espíritu indomable."));

            personajesNaruto.Add(new Personaje("Sasuke Uchiha", "Naruto", 17, 183, Image.FromFile("../../../Imagenes/Sasuke.jpg"),
                "Sasuke Uchiha es un talentoso ninja con el deseo de vengar la destrucción de su clan. A lo largo de la serie, su búsqueda de poder lo lleva por caminos oscuros, pero sigue siendo una figura central en la historia."));

            personajesNaruto.Add(new Personaje("Sakura Haruno", "Naruto", 17, 165, Image.FromFile("../../../Imagenes/Sakura.jpg"),
                "Sakura Haruno es una ninja médico y miembro del equipo de Naruto. A lo largo de la serie, desarrolla sus habilidades y demuestra ser una aliada leal. Su relación con Naruto y Sasuke es fundamental en la trama."));

            personajesNaruto.Add(new Personaje("Kakashi Hatake", "Naruto", 30, 181, Image.FromFile("../../../Imagenes/Kakashi.jpg"),
                "Kakashi Hatake es un experimentado ninja y líder del equipo de Naruto. Con su famoso Sharingan, Kakashi es un maestro estratega y un mentor crucial para los jóvenes ninjas."));

            personajesNaruto.Add(new Personaje("Hinata Hyuga", "Naruto", 17, 160, Image.FromFile("../../../Imagenes/Hinata.jpg"),
                "Hinata Hyuga es una ninja tímida pero poderosa con habilidades únicas. Su amor no correspondido por Naruto la impulsa a superar sus propias limitaciones y destacar como una kunoichi valiente."));


            personajesDragonBall.Add(new Personaje("Goku", "Dragon Ball", 25, 175, Image.FromFile("../../../Imagenes/Goku.jpg"),
                "Goku es el legendario Saiyan y protagonista de Dragon Ball. Su búsqueda de poder y deseo de superación lo convierten en uno de los guerreros más fuertes del universo."));

            personajesDragonBall.Add(new Personaje("Vegeta", "Dragon Ball", 28, 170, Image.FromFile("../../../Imagenes/Vegeta.jpg"),
                "Vegeta, el Príncipe de los Saiyan, es un guerrero orgulloso con una rivalidad intensa con Goku. A lo largo de la serie, busca superar sus límites y proteger a su familia."));

            personajesDragonBall.Add(new Personaje("Bulma", "Dragon Ball", 23, 165, Image.FromFile("../../../Imagenes/Bulma.jpg"),
                "Bulma es una brillante científica y amiga cercana de Goku. Su ingenio tecnológico y su valentía son fundamentales en la lucha contra las fuerzas del mal en Dragon Ball."));

            personajesDragonBall.Add(new Personaje("Piccolo", "Dragon Ball", 30, 226, Image.FromFile("../../../Imagenes/Piccolo.jpg"),
                "Piccolo, el Namekiano, es un poderoso guerrero y mentor de Gohan. A pesar de su origen inicialmente malévolo, Piccolo se convierte en uno de los héroes clave en la historia."));

            personajesDragonBall.Add(new Personaje("Gohan", "Dragon Ball", 18, 175, Image.FromFile("../../../Imagenes/Gohan.jpg"),
                "Gohan, el hijo de Goku, es un prodigio con un gran potencial. A medida que crece, demuestra ser un guerrero formidable con un corazón noble."));

            personajesBokuNoHero.Add(new Personaje("Izuku Midoriya", "Boku no Hero Academia", 16, 166, Image.FromFile("../../../Imagenes/Midoriya.jpg"),
                "Izuku Midoriya, también conocido como Deku, es un estudiante en la U.A. High School. A pesar de nacer sin poderes, su determinación lo lleva a convertirse en un héroe respetado."));

            personajesBokuNoHero.Add(new Personaje("Katsuki Bakugo", "Boku no Hero Academia", 16, 172, Image.FromFile("../../../Imagenes/Bakugo.jpg"),
                "Katsuki Bakugo es un estudiante talentoso con una personalidad explosiva. Su objetivo es convertirse en el héroe número uno, y su rivalidad con Deku impulsa su crecimiento."));

            personajesBokuNoHero.Add(new Personaje("Ochaco Uraraka", "Boku no Hero Academia", 16, 160, Image.FromFile("../../../Imagenes/Uraraka.jpg"),
                "Ochaco Uraraka es una estudiante amigable con la capacidad de manipular la gravedad. Su deseo de apoyar a su familia la motiva a convertirse en una heroína exitosa."));

            personajesBokuNoHero.Add(new Personaje("Tenya Iida", "Boku no Hero Academia", 16, 179, Image.FromFile("../../../Imagenes/Iida.jpg"),
                "Tenya Iida es un estudiante disciplinado y el vicepresidente de la clase. Su sentido de la justicia y velocidad lo convierten en un héroe valioso en la academia."));

            personajesBokuNoHero.Add(new Personaje("Shoto Todoroki", "Boku no Hero Academia", 16, 180, Image.FromFile("../../../Imagenes/Todoroki.jpg"),
                "Shoto Todoroki es un estudiante con habilidades de hielo y fuego. Su lucha interna con su padre lo impulsa a encontrar su propio camino como héroe."));

            miDiccionario.Add("One Piece", personajesOnePiece);
            miDiccionario.Add("Naruto", personajesNaruto);
            miDiccionario.Add("Dragon Ball", personajesDragonBall);
            miDiccionario.Add("Boku No Hero", personajesBokuNoHero);

            SerializarDatos();

            listaAnimes.Items.Add("One Piece");
            listaAnimes.Items.Add("Naruto");
            listaAnimes.Items.Add("Dragon Ball");
            listaAnimes.Items.Add("Boku No Hero");



        }

        public void CargarDiccionario()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Configura el cuadro de diálogo
            openFileDialog1.Filter = "Archivos de datos|*.data";
            openFileDialog1.Title = "Selecciona una archivo";

            // Muestra el cuadro de diálogo
            DialogResult result = openFileDialog1.ShowDialog();

            // Si el usuario hace clic en "Aceptar", actualiza la ruta de la imagen
            if (result == DialogResult.OK)
            {
                miDiccionario = DeserializeData(openFileDialog1.FileName);
                listaAnimes.Items.Add("One Piece");
                listaAnimes.Items.Add("Naruto");
                listaAnimes.Items.Add("Dragon Ball");
                listaAnimes.Items.Add("Boku No Hero");
                

            }
            else
            {
                DialogResult result2 = MessageBox.Show("No ha seleccionado ningun archivo, ¿desea crear el diccionario predeterminado?", "Continuar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result2 == DialogResult.Yes)
                {
                    iniciarDiccionario();
                }
                else if (result2 == DialogResult.No)
                {
                    MessageBox.Show("Cerrando aplicación");
                    Close();
                }
            }
        }

        //metodo para mostrar datos
        public void mostrarDatos()
        {
            //Desactiva la edicion
            editarTextos(false);

            if (indice == 0)
            {
                botonRetroceder.Enabled = false;
            }
            if (listaAnimes.SelectedItem == null)
            {
                vaciarDatos();
                botonEliminar.Enabled = false;
                botonInsertar.Enabled = false;
                botonGuardar.Enabled = false;
                botonModificar.Enabled = false;
                botonAvanzar.Enabled = false;
            }
            else
            {
                //Extraigo la lista que va a ser manipulada
                List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];
                if (aux.Count != 0)
                {
                    //Se realizan las configuraciones oportunas
                    contarNumeroRegistros();
                    mostrarRegistros();
                    NombreField.Text = aux[indice].Nombre;
                    pictureBox.Image = aux[indice].Imagen;
                    EdadField.Text = "" + aux[indice].Edad;
                    AnimeField.Text = aux[indice].Anime;
                    AlturaField.Text = "" + aux[indice].Altura + " cm";
                    DescripcionField.Text = aux[indice].Descripcion;
                    contarNumeroRegistros();
                    mostrarRegistros();
                }
                else
                {
                    vaciarDatos();
                    botonEliminar.Enabled = false;
                    botonModificar.Enabled = false;
                    botonAvanzar.Enabled = false;
                }

            }

        }

        public void iniciarBotones(Boolean b)
        {
            botonEliminar.Enabled = b;
            botonInsertar.Enabled = b;
            botonGuardar.Enabled = b;
            botonModificar.Enabled = b;
            botonAvanzar.Enabled = b;
        }
        //Metodo para ocultar botones y mostrar solo los de cancelar y aceptar y viceversa
        public void activarCancelarAceptar(Boolean b)
        {
            botonInsertar.Visible = !b;
            botonEliminar.Visible = !b;
            botonModificar.Enabled = !b;
            botonGuardar.Enabled = !b;
            botonAceptar.Visible = b;
            botonCancelar.Visible = b;
            botonAvanzar.Enabled = !b;
            botonCancelar.Enabled = !b;
        }

        //metodo para modificar la edicion de elementos
        public void editarTextos(Boolean b)
        {
            NombreField.Enabled = b;
            EdadField.Enabled = b;
            AnimeField.Enabled = b;
            AlturaField.Enabled = b;
            DescripcionField.Enabled = b;
        }

        public void vaciarDatos()
        {
            pictureBox.Image = null;
            NombreField.Text = null;
            EdadField.Text = null;
            AnimeField.Text = null;
            AlturaField.Text = null;
            DescripcionField.Text = null;
        }

        public void avanzar()
        {

            botonRetroceder.Enabled = true;
            indice++;
            List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];
            if (indice == aux.Count - 1)
            {
                botonAvanzar.Enabled = false;
            }
            mostrarDatos();
        }

        public void retroceder()
        {
            botonAvanzar.Enabled = true;
            indice--;
            if (indice == 0)
            {
                botonRetroceder.Enabled = false;
            }
            mostrarDatos();

        }

        //metodo que utilizo para borrar registros
        public void borrar()
        {

            List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];

            if (aux.Count == 1)
            {
                aux.RemoveAt(indice);
                botonEliminar.Enabled = false;
                botonModificar.Enabled = false;
                botonAvanzar.Enabled = false;
                vaciarDatos();
            }
            else
            {
                aux.RemoveAt(indice);
                mostrarDatos();

            }
            indice = 0;

            contarNumeroRegistros();
            mostrarRegistros();


        }

        //Metodo para crear nuevos registros
        public void CrearDatos()
        {
            //Compruebo si los huecos estan vacios, en ese caso no hace nada y muestra mensaje de error
            if (NombreField.Text.Length == 0 || AnimeField.Text.Length == 0 || EdadField.Text.Length == 0 || AlturaField.Text.Length == 0 || DescripcionField.Text.Length == 0)
            {
                MessageBox.Show("No se ha podido crear el personaje porque faltan datos");
            }
            else
            {
                //selecciono la lista y abro un openFileDialog
                List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];
                string archivoImagen = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Configura el cuadro de diálogo
                openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
                openFileDialog1.Title = "Selecciona una imagen";

                // Muestra el cuadro de diálogo
                DialogResult result = openFileDialog1.ShowDialog();

                // Si el usuario hace clic en "Aceptar", actualiza la ruta de la imagen
                if (result == DialogResult.OK)
                {
                    archivoImagen = openFileDialog1.FileName;
                    Text = archivoImagen;

                }
                if (archivoImagen == null)
                {
                    try
                    {
                        aux.Add(new Personaje(NombreField.Text, listaAnimeSeleccionada, int.Parse(EdadField.Text), int.Parse(AlturaField.Text), Image.FromFile("../../../Imagenes/No_Imagen.jpg"), DescripcionField.Text));
                    }
                    catch (FormatException)
                    {
                        // Captura la excepción si el formato no es válido (por ejemplo, si se introducen letras)
                        MessageBox.Show("Por favor, introduce una edad o altura válidas.");
                    }
                    catch (OverflowException)
                    {
                        // Captura la excepción si el número es demasiado grande o demasiado pequeño para un int
                        MessageBox.Show("El número introducido es demasiado grande o demasiado pequeño.");
                    }
                    
                }
                else
                {
                    
                    try
                    {
                        aux.Add(new Personaje(NombreField.Text, listaAnimeSeleccionada, int.Parse(EdadField.Text), int.Parse(AlturaField.Text), Image.FromFile(archivoImagen), DescripcionField.Text));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Por favor, introduce una altura válida.");
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("El número introducido es demasiado grande o demasiado pequeño.");
                    }
                }
                
            }
            contarNumeroRegistros();
            mostrarRegistros();
        }
        //Metodo para modificar datos
        private void ModificarDatos()
        {

            List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];
            string archivoImagen = null;
            if (NombreField.Text.Length == 0 || EdadField.Text.Length == 0 || AlturaField.Text.Length == 0 || DescripcionField.Text.Length == 0)
            {
                MessageBox.Show("No pueden haber datos vacios!");
            }
            else
            {//Si los campos de texto no estan vacios, da la opcion de modificar la imagen
                aux[indice].Nombre = NombreField.Text;
                try
                {
                    // Intenta convertir el texto a un número entero
                    aux[indice].Edad = int.Parse(EdadField.Text);

                }
                catch (FormatException)
                {
                    // Captura la excepción si el formato no es válido (por ejemplo, si se introducen letras)
                    MessageBox.Show("La edad no va a ser modificada ya que ha introducido un texto");
                }
                catch (OverflowException)
                {
                    // Captura la excepción si el número es demasiado grande o demasiado pequeño para un int
                    MessageBox.Show("El número introducido es demasiado grande o demasiado pequeño.");
                }
                try
                {
                    aux[indice].Altura = int.Parse(AlturaField.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("La altura no va a ser modificada ya que ha introducido un texto");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("El número introducido es demasiado grande o demasiado pequeño.");
                }
                
                aux[indice].Descripcion = DescripcionField.Text;

                DialogResult result = MessageBox.Show("¿Quieres modificar la imagen?", "Continuar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Puedes verificar el resultado para tomar decisiones basadas en la elección del usuario
                if (result == DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();

                    // Configura el cuadro de diálogo
                    openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                    openFileDialog1.Title = "Selecciona una imagen";

                    // Muestra el cuadro de diálogo
                    DialogResult resultado = openFileDialog1.ShowDialog();

                    // Si el usuario hace clic en "Aceptar", actualiza la ruta de la imagen
                    if (resultado == DialogResult.OK)
                    {
                        archivoImagen = openFileDialog1.FileName;
                        aux[indice].Imagen = Image.FromFile(archivoImagen);
                    }
                    else if (result == DialogResult.No)
                    {
                        Console.WriteLine("No guardar");
                    }
                }
            }
        }

        
        static void SerializeData(Dictionary<string, List<Personaje>> data, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }

        static Dictionary<string, List<Personaje>> DeserializeData(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, List<Personaje>>)formatter.Deserialize(stream);
            }
        }

        //He creado los metodos pero llego a conseguir implementarlos por lo que voy a hacer uso de la serialización
        //del diccionario

        public void SerializarDatos()
        {
            try
            {
                //Creo el binaryWritter aqui una vez para no tener que abrir y cerrar el 
                //binaryWritter cada vez que ejecuto el metodo
                BinaryWriter binaryWritter = new BinaryWriter(File.OpenWrite("databank.data"));
                foreach (var kvp in miDiccionario)
                {
                    string clave = kvp.Key;
                    List<Personaje> listaDePersonajes = kvp.Value;

                    // Iterar sobre la lista de personajes
                    foreach (Personaje personaje in listaDePersonajes)
                    {
                        personaje.insertarPersonaje(binaryWritter);
                    }
                }
                binaryWritter.Close();
            }
            catch (Exception ex)
            {

            }
            
        }

        public void DeserializarDatos()
        {
            try
            {
                BinaryReader b = new BinaryReader(File.Open("databank.data", FileMode.Open));
                while (b.BaseStream.Position < b.BaseStream.Length)
                {
                    Personaje p = Personaje.extraerPersonaje(b);
                    switch (p.Anime)
                    {
                        case "One Piece":
                            personajesOnePiece.Add(p);
                            break;
                        case "Naruto":
                            personajesNaruto.Add(p);
                            break;
                        case "Dragon Ball":
                            personajesDragonBall.Add(p);
                            break;
                        case "Boku No Hero":
                            personajesBokuNoHero.Add(p);
                            break;
                    }
                }
                }catch(Exception ex)
            {

            }
        }

        public void contarNumeroRegistros()
        {
            numeroRegistros = 0;
            foreach (var kvp in miDiccionario)
            {
                // Acceder a la lista asociada a la clave actual (kvp.Key)
                List<Personaje> listaDePersonajes = kvp.Value;

                // Contar los elementos en la lista actual
                numeroRegistros += listaDePersonajes.Count;
            }
        }

        //Muestra los dos campos de conteo de registros
        public void mostrarRegistros()
        {
            registosField.Text = "" + numeroRegistros;
            List<Personaje> aux = (List<Personaje>)miDiccionario[(string)listaAnimes.SelectedItem];
            RegistroPorListaField.Text = "" + aux.Count;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void NombreField_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnimeField_TextChanged(object sender, EventArgs e)
        {

        }

        private void listaAnimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaAnimeSeleccionada = listaAnimes.Text;
            indice = 0;
            iniciarBotones(true);
            mostrarDatos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            avanzar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            retroceder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            borrar();

        }

        private void botonInsertar_Click(object sender, EventArgs e)
        {
            vaciarDatos();
            editarTextos(true);
            AnimeField.Enabled = false;
            AnimeField.Text = listaAnimeSeleccionada;
            botonInsertar.Visible = false;
            botonEliminar.Visible = false;
            botonModificar.Enabled = false;
            botonGuardar.Enabled = false;
            botonAceptar.Visible = true;
            botonCancelar.Visible = true;
            botonAvanzar.Enabled = false;
            botonCancelar.Enabled = false;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            if (modificar == true)
            {
                ModificarDatos();
                modificar = false;
                editarTextos(false);
                activarCancelarAceptar(false);
                
            }
            else
            {
                CrearDatos();
                insertar = false;
            }


            indice = 0;
            mostrarDatos();
            botonInsertar.Visible = true;
            botonEliminar.Visible = true;
            botonGuardar.Enabled = true;
            botonModificar.Enabled = true;
            botonAceptar.Visible = false;
            botonCancelar.Visible = false;
            botonAvanzar.Enabled = true;
            botonCancelar.Enabled = true;
        }

        private void botonModificar_Click(object sender, EventArgs e)
        {
            modificar = true;
            vaciarDatos();
            activarCancelarAceptar(true);
            editarTextos(true);
            AnimeField.Enabled = false;

        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            SerializeData(miDiccionario, "databank.data");
            MessageBox.Show("¡Guardado completado!");
        }
    }
}
