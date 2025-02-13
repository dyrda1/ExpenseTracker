using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models;

public class Transaction
{
    [Key] public int TransactionId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
    public int Amount { get; set; }

    [Column(TypeName = "text")] public string? Note { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    [NotMapped] public string CategoryTitleWithIcon => Category == null ? "" : Category.Icon + " " + Category.Title;

    [NotMapped]
    public string FormattedAmount =>
        (Category == null || Category.Type == "Expense" ? "- " : "+ ") + Amount.ToString("C0");
}