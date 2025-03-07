using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InsertUsers(migrationBuilder);
            InsertEmployees(migrationBuilder);
            InsertIngredients(migrationBuilder);
            InsertDishTypes(migrationBuilder);
            InsertDishes(migrationBuilder);
            InsertDishIngredients(migrationBuilder);
        }

        private void InsertUsers(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Users",
               columns: new[] { "Id", "Name", "Email", "Address", "City", "Password", "Role", "RegistrationDate" },
               values: new object[,]
               {
                    { 1, "Admin",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 1, DateTime.Now.AddMonths(-10) },

                    { 2, "User1",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 0, DateTime.Now.AddMonths(-9) },
                    { 3, "User2",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 0, DateTime.Now.AddMonths(-8) },
                    { 4, "User3",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 0, DateTime.Now.AddMonths(-7) },
                    { 5, "User4",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 0, DateTime.Now.AddMonths(-6) },
                    { 6, "User5",   "beko1972@abv.bg", "Address1", "Varna",         "12345678", 0, DateTime.Now.AddMonths(-5) },
                    { 7, "User6",   "beko1972@abv.bg", "Address2", "Sofia",         "12345678", 0, DateTime.Now.AddMonths(-4) },
                    { 8, "User7",   "beko1972@abv.bg", "Address3", "Plovdiv",       "12345678", 0, DateTime.Now.AddMonths(-3) },
                    { 9, "User8",   "beko1972@abv.bg", "Address4", "Burgas",        "12345678", 0, DateTime.Now.AddMonths(-2) },
                    { 10, "User9",  "beko1972@abv.bg", "Address5", "Ruse",          "12345678", 0, DateTime.Now.AddMonths(-1) },
                    { 11, "User10", "beko1972@abv.bg", "Address3", "Plovdiv",       "12345678", 0, DateTime.Now.AddMonths(-5) },
                    { 12, "User11", "beko1972@abv.bg", "Address4", "Burgas",        "12345678", 0, DateTime.Now.AddMonths(-4) },
                    { 13, "User12", "beko1972@abv.bg", "Address5", "Ruse",          "12345678", 0, DateTime.Now.AddMonths(-3) },
                    { 14, "User13", "beko1972@abv.bg", "Address6", "Stara Zagora",  "12345678", 0, DateTime.Now.AddMonths(-2) },
                    { 15, "User14", "beko1972@abv.bg", "Address6", "Stara Zagora",  "12345678", 0, DateTime.Now.AddMonths(-1) }
               });
        }

        private void InsertEmployees(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Employees",
               columns: new[] { "Id", "Name", "Email", "Password", "EmploymentDate", "IsOnline" },
               values: new object[,]
               {
                    { 1, "Employee1", "beko1972@abv.bg", "12345678", DateTime.Now.AddMonths(-6), true },
                    { 2, "Employee2", "beko1972@abv.bg", "12345678", DateTime.Now.AddMonths(-4), false },
                    { 3, "Employee3", "beko1972@abv.bg", "12345678", DateTime.Now.AddMonths(-1), true },
               });
        }

        private void InsertIngredients(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Ingredients",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { 1, "Flour",},
                    { 2, "Milk"},
                    {3, "Eggs"},
                    {4, "Sugar"},
                    {5, "Butter"},
                    {6, "Salt"},
                    {7, "Vanilla Extract"},
                    {8, "Lemon"},
                    {9, "Baking Powder"},
                    {10, "Olive Oil"}
               });
        }

        private void InsertDishTypes(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DishTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    {1, "Breakfast Dishes"},
                    {2, "Appetizers"},
                    {3, "Soups"},
                    {4, "Salads"},
                    {5, "Main Courses"},
                    {6, "Side Dishes"},
                    {7, "Snacks"},
                    {8, "Desserts"},
                    {9, "Drinks"},
                    {10, "Cultural Foods"},
                });
        }

        private void InsertDishes(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Name", "DishTypeId", "Price" },
                values: new object[,]
                {
                    // Breakfast Dishes (DishTypeId = 1)
                    { 1, "Pancakes",                   1,  7.49 },
                    { 2, "Waffles",                    1,  7.99 },
                    { 3, "Omelette",                   1,  6.99 },
                    { 4, "French Toast",               1,  7.49 },
                    { 5, "Breakfast Burrito",          1,  8.49 },
                    { 6, "Eggs Benedict",              1,  9.99 },
                    { 7, "Bagel with Cream Cheese",    1,  5.99 },
                    { 8, "Avocado Toast",              1,  6.99 },
                    { 9, "Breakfast Sausages",         1,  7.49 },
                    { 10, "Hash Browns",               1,  4.99 },

                    // Appetizers (DishTypeId = 2)
                    { 11, "Spring Rolls",              2,  6.99 },
                    { 12, "Bruschetta",                2,  7.49 },
                    { 13, "Mozzarella Sticks",         2,  8.49 },
                    { 14, "Stuffed Mushrooms",         2,  7.99 },
                    { 15, "Garlic Bread",              2,  5.99 },
                    { 16, "Deviled Eggs",              2,  6.49 },
                    { 17, "Potato Skins",              2,  8.99 },
                    { 18, "Chicken Wings",             2, 10.99 },
                    { 19, "Crab Cakes",                2, 11.99 },
                    { 20, "Mini Tacos",                2,  7.99 },

                    // Soups (DishTypeId = 3)
                    { 21, "Tomato Soup",               3,  5.99 },
                    { 22, "Chicken Noodle Soup",       3,  6.49 },
                    { 23, "Minestrone Soup",           3,  6.99 },
                    { 24, "French Onion Soup",         3,  7.49 },
                    { 25, "Clam Chowder",              3,  8.99 },
                    { 26, "Lentil Soup",               3,  5.99 },
                    { 27, "Pumpkin Soup",              3,  6.49 },
                    { 28, "Beef Barley Soup",          3,  7.99 },
                    { 29, "Hot and Sour Soup",         3,  7.49 },
                    { 30, "Cream of Mushroom Soup",    3,  6.99 },

                    // Salads (DishTypeId = 4)
                    { 31, "Caesar Salad",              4,  8.99 },
                    { 32, "Greek Salad",               4,  7.99 },
                    { 33, "Cobb Salad",                4,  9.99 },
                    { 34, "Caprese Salad",             4,  8.49 },
                    { 35, "Garden Salad",              4,  6.99 },
                    { 36, "Waldorf Salad",             4,  8.59 },
                    { 37, "Pasta Salad",               4,  7.49 },
                    { 38, "Nicoise Salad",             4,  9.29 },
                    { 39, "Asian Slaw",                4,  7.99 },
                    { 40, "Quinoa Salad",              4,  9.49 },

                    // Main Courses (DishTypeId = 5)
                    { 41, "Grilled Chicken",           5, 15.99 },
                    { 42, "Spaghetti Bolognese",       5, 13.99 },
                    { 43, "Beef Steak",                5, 20.99 },
                    { 44, "Vegetable Stir Fry",        5, 12.99 },
                    { 45, "Butter Chicken",            5, 16.99 },
                    { 46, "Salmon Teriyaki",           5, 18.99 },
                    { 47, "Lamb Chops",                5, 22.99 },
                    { 48, "Eggplant Parmesan",         5, 14.99 },
                    { 49, "Seafood Paella",            5, 19.99 },
                    { 50, "Beef Tacos",                5, 14.49 },

                    // Side Dishes (DishTypeId = 6)
                    { 51, "Mashed Potatoes",           6,  5.99 },
                    { 52, "Steamed Vegetables",        6,  4.99 },
                    { 53, "French Fries",              6,  3.99 },
                    { 54, "Onion Rings",               6,  4.49 },
                    { 55, "Coleslaw",                  6,  3.99 },
                    { 56, "Roasted Asparagus",         6,  6.49 },
                    { 57, "Garlic Mashed Potatoes",    6,  5.49 },
                    { 58, "Grilled Corn",              6,  4.99 },
                    { 59, "Baked Sweet Potatoes",      6,  6.99 },
                    { 60, "Quinoa Side Salad",         6,  5.49 },

                    // Snacks (DishTypeId = 7)
                    { 61, "Potato Chips",              7,  3.99 },
                    { 62, "Nachos",                    7,  6.99 },
                    { 63, "Popcorn",                   7,  2.99 },
                    { 64, "Pretzels",                  7,  3.49 },
                    { 65, "Cheese Puffs",              7,  3.99 },
                    { 66, "Trail Mix",                 7,  4.99 },
                    { 67, "Granola Bars",              7,  4.49 },
                    { 68, "Beef Jerky",                7,  7.99 },
                    { 69, "Fruit Snacks",              7,  3.99 },
                    { 70, "Mini Sandwiches",           7,  6.99 },

                    // Desserts (DishTypeId = 8)
                    { 71, "Chocolate Cake",            8,  7.99 },
                    { 72, "Vanilla Ice Cream",         8,  5.99 },
                    { 73, "Apple Pie",                 8,  6.99 },
                    { 74, "Cheesecake",                8,  8.49 },
                    { 75, "Brownies",                  8,  5.49 },
                    { 76, "Panna Cotta",               8,  7.49 },
                    { 77, "Lemon Tart",                8,  6.99 },
                    { 78, "Tiramisu",                  8,  8.99 },
                    { 79, "Fruit Salad",               8,  5.49 },
                    { 80, "Chocolate Mousse",          8,  7.99 },

                    // Drinks (DishTypeId = 9)
                    { 81, "Smoothies",                 9,  4.99 },
                    { 82, "Orange Juice",              9,  3.99 },
                    { 83, "Cocktails",                 9,  6.99 },
                    { 84, "Coffee",                    9,  2.99 },
                    { 85, "Tea",                       9,  2.49 },
                    { 86, "Soda",                      9,  1.99 },
                    { 87, "Milkshake",                 9,  5.99 },
                    { 88, "Lemonade",                  9,  3.49 },
                    { 89, "Iced Tea",                  9,  3.99 },
                    { 90, "Hot Chocolate",             9,  4.49 },

                    // Cultural Foods (DishTypeId = 10)
                    { 91, "Sushi",                      10, 12.99 },
                    { 92, "Tacos",                      10,  9.99 },
                    { 93, "Pad Thai",                   10, 10.99 },
                    { 94, "Butter Chicken",             10, 13.49 },
                    { 95, "Dim Sum",                    10, 11.99 },
                    { 96, "Falafel",                    10,  8.99 },
                    { 97, "Paella",                     10, 14.99 },
                    { 98, "Ramen",                      10, 12.49 },
                    { 99, "Bulgogi",                    10, 13.99 },
                    { 100, "Poutine",                   10,  9.49 },
                });
        }

        private void InsertDishIngredients(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dish_Ingredients",
                columns: new[] { "DishId", "IngredientId" },
                values: new object[,]
                {
                { 1, 1 }, { 1, 4 }, { 1, 6 }, { 1, 7 }, { 1, 10 },  // Caesar Salad
                { 2, 8 }, { 2, 9 }, { 2, 6 }, { 2, 1 },             // Greek Salad
                { 3, 1 }, { 3, 10 }, { 3, 6 },                      // Cobb Salad
                { 4, 1 }, { 4, 10 }, { 4, 8 }, { 4, 6 },            // Caprese Salad
                { 5, 6 }, { 5, 7 }, { 5, 10 },                      // Garden Salad
                { 6, 1 }, { 6, 4 }, { 6, 6 }, { 6, 10 },            // Waldorf Salad
                { 7, 1 }, { 7, 6 }, { 7, 10 }, { 7, 7 },            // Pasta Salad
                { 8, 8 }, { 8, 10 }, { 8, 6 }, { 8, 4 },            // Nicoise Salad
                { 9, 6 }, { 9, 7 }, { 9, 1 },                       // Asian Slaw
                { 10, 1 }, { 10, 6 }, { 10, 8 },                    // Quinoa Salad
            
                { 11, 2 }, { 11, 4 }, { 11, 6 }, { 11, 3 },         // Spring Rolls
                { 12, 1 }, { 12, 10 }, { 12, 6 }, { 12, 7 },        // Bruschetta
                { 13, 1 }, { 13, 6 }, { 13, 7 }, { 13, 8 },         // Mozzarella Sticks
                { 14, 1 }, { 14, 6 }, { 14, 9 }, { 14, 10 },        // Stuffed Mushrooms
                { 15, 2 }, { 15, 4 }, { 15, 6 }, { 15, 3 },         // Garlic Bread
                { 16, 2 }, { 16, 6 }, { 16, 10 }, { 16, 3 },        // Deviled Eggs
                { 17, 1 }, { 17, 6 }, { 17, 4 }, { 17, 10 },        // Potato Skins
                { 18, 2 }, { 18, 6 }, { 18, 10 },                   // Chicken Wings
                { 19, 1 }, { 19, 2 }, { 19, 6 }, { 19, 10 },        // Crab Cakes
                { 20, 1 }, { 20, 6 }, { 20, 4 }, { 20, 10 },        // Mini Tacos
            
                { 21, 1 }, { 21, 10 }, { 21, 6 },                   // Grilled Chicken
                { 22, 1 }, { 22, 10 }, { 22, 4 }, { 22, 6 },        // Spaghetti Bolognese
                { 23, 1 }, { 23, 10 }, { 23, 6 }, { 23, 2 },        // Beef Steak
                { 24, 1 }, { 24, 10 }, { 24, 6 }, { 24, 2 },        // Vegetable Stir Fry
                { 25, 1 }, { 25, 10 }, { 25, 6 }, { 25, 3 },        // Butter Chicken
                { 26, 1 }, { 26, 10 }, { 26, 6 },                   // Salmon Teriyaki
                { 27, 1 }, { 27, 10 }, { 27, 6 }, { 27, 2 },        // Lamb Chops
                { 28, 1 }, { 28, 10 }, { 28, 6 }, { 28, 3 },        // Eggplant Parmesan
                { 29, 1 }, { 29, 10 }, { 29, 6 },                   // Seafood Paella
                { 30, 1 }, { 30, 10 }, { 30, 6 }, { 30, 2 },        // Beef Tacos
            
                { 31, 5 }, { 31, 6 }, { 31, 2 },                    // Mashed Potatoes
                { 32, 1 }, { 32, 6 }, { 32, 3 },                    // Steamed Vegetables
                { 33, 6 }, { 33, 2 }, { 33, 1 },                    // French Fries
                { 34, 5 }, { 34, 2 }, { 34, 6 },                    // Onion Rings
                { 35, 6 }, { 35, 7 }, { 35, 2 },                    // Coleslaw
                { 36, 1 }, { 36, 6 }, { 36, 2 },                    // Roasted Asparagus
                { 37, 5 }, { 37, 6 }, { 37, 1 },                    // Garlic Mashed Potatoes
                { 38, 1 }, { 38, 6 }, { 38, 2 },                    // Grilled Corn
                { 39, 5 }, { 39, 6 }, { 39, 2 },                    // Baked Sweet Potatoes
                { 40, 1 }, { 40, 6 }, { 40, 7 },                    // Quinoa Side Salad
            
                { 41, 6 }, { 41, 2 }, { 41, 1 },                    // Potato Chips
                { 42, 1 }, { 42, 6 }, { 42, 5 },                    // Nachos
                { 43, 2 }, { 43, 6 }, { 43, 1 },                    // Popcorn
                { 44, 5 }, { 44, 6 }, { 44, 1 },                    // Pretzels
                { 45, 2 }, { 45, 6 }, { 45, 3 },                    // Cheese Puffs
                { 46, 1 }, { 46, 6 }, { 46, 5 },                    // Trail Mix
                { 47, 2 }, { 47, 6 }, { 47, 1 },                    // Granola Bars
                { 48, 5 }, { 48, 6 }, { 48, 1 },                    // Beef Jerky
                { 49, 2 }, { 49, 6 }, { 49, 1 },                    // Fruit Snacks
                { 50, 6 }, { 50, 2 }, { 50, 1 },                    // Mini Sandwiches
            
                { 51, 1 }, { 51, 4 }, { 51, 6 }, { 51, 2 },         // Chocolate Cake
                { 52, 1 }, { 52, 6 }, { 52, 3 },                    // Vanilla Ice Cream
                { 53, 6 }, { 53, 2 }, { 53, 1 },                    // Apple Pie
                { 54, 1 }, { 54, 6 }, { 54, 2 },                    // Cheesecake
                { 55, 1 }, { 55, 4 }, { 55, 2 },                    // Brownies
                { 56, 1 }, { 56, 2 }, { 56, 6 },                    // Panna Cotta
                { 57, 2 }, { 57, 6 }, { 57, 1 },                    // Lemon Tart
                { 58, 1 }, { 58, 6 }, { 58, 2 },                    // Tiramisu
                { 59, 2 }, { 59, 6 }, { 59, 1 },                    // Fruit Salad
                { 60, 1 }, { 60, 4 }, { 60, 6 },                    // Chocolate Mousse
            
                { 61, 6 }, { 61, 2 }, { 61, 3 },                    // Smoothies
                { 62, 2 }, { 62, 6 }, { 62, 1 },                    // Orange Juice
                { 63, 1 }, { 63, 6 }, { 63, 5 },                    // Cocktails
                { 64, 6 }, { 64, 2 }, { 64, 1 },                    // Coffee
                { 65, 6 }, { 65, 2 }, { 65, 3 },                    // Tea
                { 66, 6 }, { 66, 1 }, { 66, 4 },                    // Soda
                { 67, 6 }, { 67, 2 }, { 67, 5 },                    // Milkshake
                { 68, 6 }, { 68, 2 }, { 68, 1 },                    // Lemonade
                { 69, 6 }, { 69, 2 }, { 69, 1 },                    // Iced Tea
                { 70, 2 }, { 70, 6 }, { 70, 3 },                    // Hot Chocolate
            
                { 71, 2 }, { 71, 6 }, { 71, 1 },                    // Tomato Soup
                { 72, 2 }, { 72, 6 }, { 72, 3 },                    // Miso Soup
                { 73, 6 }, { 73, 2 }, { 73, 1 },                    // Chicken Noodle Soup
                { 74, 2 }, { 74, 6 }, { 74, 3 },                    // Clam Chowder
                { 75, 6 }, { 75, 2 }, { 75, 1 },                    // Vegetable Soup
                { 76, 2 }, { 76, 6 }, { 76, 4 },                    // Corn Soup
                { 77, 6 }, { 77, 2 }, { 77, 5 },                    // French Onion Soup
                { 78, 6 }, { 78, 2 }, { 78, 3 },                    // Beef Stew
                { 79, 6 }, { 79, 2 }, { 79, 4 },                    // Chicken Tortilla Soup
                { 80, 2 }, { 80, 6 }, { 80, 5 },                    // Lobster Bisque
            
                { 81, 6 }, { 81, 2 }, { 81, 3 },                    // Grilled Vegetables
                { 82, 6 }, { 82, 2 }, { 82, 1 },                    // Caesar Wrap
                { 83, 1 }, { 83, 6 }, { 83, 5 },                    // Chicken Caesar Wrap
                { 84, 6 }, { 84, 2 }, { 84, 3 },                    // Buffalo Wrap
                { 85, 1 }, { 85, 2 }, { 85, 6 },                    // Veggie Wrap
                { 86, 6 }, { 86, 2 }, { 86, 3 },                    // Tuna Wrap
                { 87, 6 }, { 87, 2 }, { 87, 1 },                    // Chicken Salad Wrap
                { 88, 6 }, { 88, 2 }, { 88, 5 },                    // Chicken Fajita Wrap
                { 89, 1 }, { 89, 6 }, { 89, 4 },                    // Turkey Wrap
                { 90, 6 }, { 90, 2 }, { 90, 1 },                    // BLT Wrap
            
                { 91, 6 }, { 91, 2 }, { 91, 3 },                    // Steak Wrap
                { 92, 6 }, { 92, 2 }, { 92, 4 },                    // Shrimp Wrap
                { 93, 1 }, { 93, 6 }, { 93, 2 },                    // Falafel Wrap
                { 94, 1 }, { 94, 6 }, { 94, 3 },                    // Chicken Tenders Wrap
                { 95, 6 }, { 95, 2 }, { 95, 5 },                    // BBQ Chicken Wrap
                { 96, 6 }, { 96, 2 }, { 96, 3 },                    // Egg Salad Wrap
                { 97, 1 }, { 97, 6 }, { 97, 5 },                    // Chicken Pesto Wrap
                { 98, 1 }, { 98, 6 }, { 98, 2 },                    // Grilled Cheese Wrap
                { 99, 6 }, { 99, 2 }, { 99, 1 },                    // Buffalo Chicken Wrap
                { 100, 6 }, { 100, 2 }, { 100, 3 }                  // Shrimp Caesar Wrap
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");
            migrationBuilder.Sql("DELETE FROM Employees");
            migrationBuilder.Sql("DELETE FROM Ingredients");
            migrationBuilder.Sql("DELETE FROM Order_Dishes");
            migrationBuilder.Sql("DELETE FROM Dishes");
            migrationBuilder.Sql("DELETE FROM DishTypes");
            migrationBuilder.Sql("DELETE FROM Dish_Ingredients");
            migrationBuilder.Sql("DELETE FROM Orders");

        }
    }
}
