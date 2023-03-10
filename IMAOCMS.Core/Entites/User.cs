using System.ComponentModel.DataAnnotations;
namespace IMAOCMS.Core.Entites;
public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}