// See https://aka.ms/new-console-template for more information

using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SecondPrueba;

int numeroPedidos = 3;
int edadMinima;
int edadMaxima;
float precioMaximo;

//Solicitar datos
Console.WriteLine("Introduce la edad minima");
edadMinima = int.Parse(Console.ReadLine());
Console.WriteLine("Introduce la edad maxima");
edadMaxima = int.Parse(Console.ReadLine());
Console.WriteLine("Introduce un precio maximo");
precioMaximo = float.Parse(Console.ReadLine());



//Abrir driver para iniciar en la página
IWebDriver driver = new FirefoxDriver();
driver.Navigate().GoToUrl("https://amazondating.co/");


//Crear una lista de elementos con los nombres, edades , precios y enlaces para comprar
List<Pedido> pedidos = new List<Pedido>();
crearLista(pedidos);

visualizar();
pedidos = pedidos.OrderByDescending(p => p.rating).ToList();
Console.WriteLine("PEDIDOS ORDENADOS");
Console.WriteLine("-----------------------------------");
visualizar();

comprarElementos(numeroPedidos);

//sacar el rating una vez tengamos la lista de las personas seleccionadas, yendo al enlace
//y sacandolo de ahi

Console.ReadLine();
//driver.Close();












void visualizar()
{
    foreach (var VARIABLE in pedidos)
    {
        Console.WriteLine(VARIABLE.ToString());
    }
}

void crearLista(List<Pedido> pedidos)
{
    
    ReadOnlyCollection<IWebElement> elementos = driver.FindElements(by: By.ClassName("product-name"));
    ReadOnlyCollection<IWebElement> precios = driver.FindElements(by: By.ClassName("product-price"));
    ReadOnlyCollection<IWebElement> ratings = driver.FindElements(by: By.ClassName("product-rating"));
    for(int i = 0; i < elementos.Count; i++)
    {
        int edad;
        string nombre;
        string URL;
        float rating;
    
        string[] separacion = elementos[i].Text.Split(',');
        nombre = separacion[0].Trim();
        edad = int.Parse(separacion[1].Trim());
        var precio = precios[i].FindElement(By.TagName("p")).Text;
        var precioform = precio.Split(" ");
        string precioFormateado;
    
        precioFormateado = precioform[0].Substring(1);
        precioFormateado = precioFormateado.Replace(".", ",");
        float precioFinal;
        if (float.TryParse(precioFormateado, out float precioFloat))
        {
            precioFinal = precioFloat;
        }
        else
        {
            precioFinal = -1;
        }

        Console.WriteLine(precioFinal);
        if (edad >= edadMinima && edad <= edadMaxima && precioMaximo >= precioFinal)
        {
            //para sacar el ratio
            IWebElement element = ratings[i].FindElement(by: By.TagName("div"));
            string ratio = element.GetAttribute("class").Substring(11).Replace("-", ",");
            Console.WriteLine(ratio);
            URL = elementos[i].GetAttribute("href");
            pedidos.Add(new Pedido(nombre, edad, float.Parse(ratio), float.Parse(precioFormateado), URL));
        }
    }
}

void comprarElementos(int cantidad)
{
    for (int i = 0; i < cantidad; i++)
    {
        driver.Navigate().GoToUrl(pedidos[i].enlace);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        
        // Esperar a que el botón de compra esté presente y clickeable
        IWebElement botonCompra = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("buy-now")));
        botonCompra.Click();
        
        // Esperar a que el cuadrado de checkout esté presente
        IWebElement cuadradoCheckout = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("checkout-items-and-shipping")));
        
        // Selección del elemento de acuerdo al caso
        IWebElement elemento = null;
        switch (i)
        {
            case 0:
                elemento = cuadradoCheckout.FindElement(By.Id("chk-1"));
                break;
            case 1:
                elemento = cuadradoCheckout.FindElement(By.Id("chk-2"));
                break;
            case 2:
                elemento = cuadradoCheckout.FindElement(By.Id("chk-3"));
                break;
            default:
                // Si no hay un caso definido, salir del switch
                continue;
        }

        // Desplazar a la vista si es necesario
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elemento);

        // Esperar a que el elemento sea clickeable
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elemento)).Click();

        // Encontrar y clickear el botón para proceder con el pedido
        IWebElement cuadradoPedido = driver.FindElement(by: By.ClassName("checkout-sidebar"));
        IWebElement botonPedido = cuadradoPedido.FindElement(by: By.TagName("button"));
        botonPedido.Click();
        Thread.Sleep(2000);
    }
}
