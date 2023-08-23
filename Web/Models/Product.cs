namespace Web.Models;

public class Product
{
    public Product(string firstname, string lastname, string handle)
    {
        Firstname = firstname;
        Lastname = lastname;
        Handle = handle;
    }

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Handle { get; set; }
    
}