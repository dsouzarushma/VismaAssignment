using System;
using System.ComponentModel.DataAnnotations;

public class AuthorDataModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
}
