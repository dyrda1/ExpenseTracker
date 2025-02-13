using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models;

public class Category
{
    [Key] public int CategoryId { get; set; }

    [Column(TypeName = "text")]
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = default!;

    [Column(TypeName = "text")] public string Icon { get; set; } = "";

    [Column(TypeName = "text")] public string Type { get; set; } = "Expense";

    [NotMapped] public string TitleWithIcon => Icon + " " + Title;
}