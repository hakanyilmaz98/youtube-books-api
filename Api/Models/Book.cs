using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Book
{
    [Key]
    public required Guid Id { get; init; }
    
    [MaxLength(100)]
    public required string Title { get; set; }
    
    [MaxLength(13)]
    public required string Isbn { get; init; }
}