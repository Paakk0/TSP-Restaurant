using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Model
{
    /*----------------------------------------------------------------USER----------------------------------------------------------------*/
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int? Role { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    /*----------------------------------------------------------------EMPLOYEE----------------------------------------------------------------*/
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool IsOnline { get; set; }
    }

    /*----------------------------------------------------------------DISHTYPE----------------------------------------------------------------*/
    public class DishType
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
    }

    /*----------------------------------------------------------------DISH----------------------------------------------------------------*/
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        public int DishTypeId { get; set; }

        [Range(0.01, double.MaxValue)]
        [RegularExpression(@"^\d+\.\d{2}$")]
        public double Price { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [ForeignKey(nameof(DishTypeId))]
        public DishType? DishType { get; set; }
    }

    /*----------------------------------------------------------------INGREDIENT----------------------------------------------------------------*/
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }
    }

    /*----------------------------------------------------------------DISH_INGREDIENTS----------------------------------------------------------------*/
    [Keyless]
    public class Dish_Ingredient
    {
        public int IngredientId { get; set; }

        [ForeignKey(nameof(IngredientId))]
        public Ingredient? Ingredient { get; set; }

        public int DishId { get; set; }

        [ForeignKey(nameof(DishId))]
        public Dish? Dish { get; set; }
    }

    /*----------------------------------------------------------------ORDER----------------------------------------------------------------*/
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int Status { get; set; }

        public string? Location { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public double Tip { get; set; }
    }

    /*----------------------------------------------------------------ORDER_DISH----------------------------------------------------------------*/
    [Keyless]
    public class Order_Dish
    {
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        public int DishId { get; set; }

        [ForeignKey(nameof(DishId))]
        public Dish? Dish { get; set; }

        public int Quantity { get; set; }
    }

}
