using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneHandTraining.model;

public class Author
{
    
    public string username { get; set; }
    public string? bio { get; set; }
    public string? image { get; set; }     
    public bool following { get; set; }
}