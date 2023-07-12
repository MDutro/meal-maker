using ErrorOr;

namespace MealMaker.ServiceErrors;

public static class Errors
{
    public static class Meal
    {
        public static Error InvalidName => Error.Validation(
            code: "Meal.InvalidName",
            description: $"Meal name must be between {Services.Meals.MealService.MinNameLength} and {Services.Meals.MealService.MaxNameLength} characters"
        );

        public static Error InvalidDescription => Error.Validation(
            code: "Meal.InvalidDescription",
            description: $"Descprition must be between {Services.Meals.MealService.MinDescriptionLength} and {Services.Meals.MealService.MaxDescriptionLength} characters"
        );
        
        public static Error NotFound => Error.NotFound(
            code: "Meal.NotFound",
            description: "Meal not found"
        );
    }
}