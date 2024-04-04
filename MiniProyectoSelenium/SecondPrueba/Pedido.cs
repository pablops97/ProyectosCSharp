namespace SecondPrueba;

public class Pedido
{
    public string nombre
    {
        get;
        set;
    }

    public int edad { get; set; }
    public float rating{ get; set; }
    public string enlace{ get; set; }
    public float precio { get; set; }

    public Pedido(string nombre, int edad, float rating, float precio, string enlace)
    {
        this.nombre = nombre;
        this.edad = edad;
        this.rating = rating;
        this.enlace = enlace;
        this.precio = precio;
    }

    public Pedido(string nombre, int edad, string enlace)
    {
        this.nombre = nombre;
        this.edad = edad;
        this.rating = 0;
        this.enlace = enlace;
    }

    public override string ToString()
    {
        return $"Nombre: {nombre}, edad: {edad}, rating: {rating}, precio: {precio}, enlace: {enlace}";
    }
}