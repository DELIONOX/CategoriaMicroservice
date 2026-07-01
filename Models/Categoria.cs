namespace CategoriaMicroservice.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; }=string.Empty;
    public bool Activa { get; set; }
}