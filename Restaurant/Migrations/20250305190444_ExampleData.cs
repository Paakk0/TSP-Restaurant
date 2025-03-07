using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class ExampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InsertOrders(migrationBuilder);
            InsertOrderDishes(migrationBuilder);
        }

        private void InsertOrders(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "UserId", "EmployeeId", "Status", "Location", "Date", "Tip" },
                values: new object[,]
                {
                    //Id,   UserId, EmployeeId, Status, Location,    Date,                           Tip
                    { 1,    5,      1,          1,      "Home",      DateTime.Now.AddMinutes(-30),   7.20 },
                    { 2,    9,      1,          1,      "Table 3",   DateTime.Now,                   4.50 },
                    { 3,    3,      1,          1,      "Table 11",  DateTime.Now.AddHours(-1),      8.90 },
                    { 4,    7,      1,          1,      "Table 1",   DateTime.Now.AddHours(-2),      2.30 },
                    { 5,    1,      1,          1,      "Home",      DateTime.Now.AddHours(-3),      5.60 },
                    { 6,    7,      3,          1,      "Table 1",   DateTime.Now.AddHours(-4),      1.20 },
                    { 7,    1,      3,          1,      "Table 5",   DateTime.Now.AddMinutes(-30),   9.80 },
                    { 8,    5,      3,          1,      "Home",      DateTime.Now.AddHours(-6),      6.50 },
                    { 9,    9,      3,          1,      "Home",      DateTime.Now.AddHours(-7),      3.20 },
                    { 10,   3,      3,          1,      "Table 11",  DateTime.Now.AddHours(-8),      7.80 },
                    { 11,   5,      1,          0,      "Home",      DateTime.Now.AddMinutes(-30),   4.30 },
                    { 12,   9,      1,          0,      "Table 3",   DateTime.Now,                   5.40 },
                    { 13,   3,      1,          0,      "Table 11",  DateTime.Now.AddHours(-1),      6.70 },
                    { 14,   7,      1,          0,      "Table 1",   DateTime.Now.AddHours(-2),      2.30 },
                    { 15,   1,      1,          0,      "Home",      DateTime.Now.AddHours(-3),      8.90 },
                    { 16,   7,      3,          0,      "Table 1",   DateTime.Now.AddHours(-4),      1.20 },
                    { 17,   1,      3,          0,      "Table 5",   DateTime.Now.AddMinutes(-30),   9.80 },
                    { 18,   5,      3,          0,      "Home",      DateTime.Now.AddHours(-6),      6.50 },
                    { 19,   9,      3,          0,      "Home",      DateTime.Now.AddHours(-7),      3.20 },
                    { 20,   3,      3,          0,      "Table 11",  DateTime.Now.AddHours(-8),      7.80 }
                });
        }

        private void InsertOrderDishes(MigrationBuilder migrationBuilder)
{
    migrationBuilder.InsertData(
        table: "Order_Dishes",
        columns: new[] { "OrderId", "DishId", "Quantity" },
        values: new object[,]
        {
            // OrderId, DishId, Quantity
            { 1, 1, 2 },    // 2 Pancakes
            { 1, 3, 1 },    // 1 Omelette
            { 1, 5, 3 },    // 3 Breakfast Burrito
            { 2, 11, 3 },   // 3 Spring Rolls
            { 2, 12, 2 },   // 2 Bruschetta
            { 2, 13, 1 },   // 1 Mozzarella Sticks
            { 3, 21, 1 },   // 1 Tomato Soup
            { 3, 22, 2 },   // 2 Chicken Noodle Soup
            { 3, 23, 3 },   // 3 Minestrone Soup
            { 4, 31, 1 },   // 1 Caesar Salad
            { 4, 32, 1 },   // 1 Greek Salad
            { 4, 33, 2 },   // 2 Cobb Salad
            { 5, 41, 1 },   // 1 Grilled Chicken
            { 5, 42, 1 },   // 1 Spaghetti Bolognese
            { 5, 43, 2 },   // 2 Beef Steak
            { 6, 51, 2 },   // 2 Mashed Potatoes
            { 6, 52, 1 },   // 1 Steamed Vegetables
            { 6, 53, 3 },   // 3 French Fries
            { 7, 61, 3 },   // 3 Potato Chips
            { 7, 62, 2 },   // 2 Nachos
            { 7, 63, 1 },   // 1 Popcorn
            { 8, 71, 1 },   // 1 Chocolate Cake
            { 8, 72, 1 },   // 1 Vanilla Ice Cream
            { 8, 73, 2 },   // 2 Apple Pie
            { 9, 81, 2 },   // 2 Smoothies
            { 9, 82, 1 },   // 1 Orange Juice
            { 9, 83, 3 },   // 3 Cocktails
            { 10, 91, 1 },  // 1 Sushi
            { 10, 92, 1 },  // 1 Tacos
            { 10, 93, 2 },  // 2 Pad Thai
            { 11, 1, 2 },   // 2 Pancakes
            { 11, 3, 1 },   // 1 Omelette
            { 11, 5, 3 },   // 3 Breakfast Burrito
            { 12, 11, 3 },  // 3 Spring Rolls
            { 12, 12, 2 },  // 2 Bruschetta
            { 12, 13, 1 },  // 1 Mozzarella Sticks
            { 13, 21, 1 },  // 1 Tomato Soup
            { 13, 22, 2 },  // 2 Chicken Noodle Soup
            { 13, 23, 3 },  // 3 Minestrone Soup
            { 14, 31, 1 },  // 1 Caesar Salad
            { 14, 32, 1 },  // 1 Greek Salad
            { 14, 33, 2 },  // 2 Cobb Salad
            { 15, 41, 1 },  // 1 Grilled Chicken
            { 15, 42, 1 },  // 1 Spaghetti Bolognese
            { 15, 43, 2 },  // 2 Beef Steak
            { 16, 51, 2 },  // 2 Mashed Potatoes
            { 16, 52, 1 },  // 1 Steamed Vegetables
            { 16, 53, 3 },  // 3 French Fries
            { 17, 61, 3 },  // 3 Potato Chips
            { 17, 62, 2 },  // 2 Nachos
            { 17, 63, 1 },  // 1 Popcorn
            { 18, 71, 1 },  // 1 Chocolate Cake
            { 18, 72, 1 },  // 1 Vanilla Ice Cream
            { 18, 73, 2 },  // 2 Apple Pie
            { 19, 81, 2 },  // 2 Smoothies
            { 19, 82, 1 },  // 1 Orange Juice
            { 19, 83, 3 },  // 3 Cocktails
            { 20, 91, 1 },  // 1 Sushi
            { 20, 92, 1 },  // 1 Tacos
            { 20, 93, 2 },  // 2 Pad Thai
        });
}


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Orders");
            migrationBuilder.Sql("DELETE FROM Order_Dishes");
        }
    }
}
