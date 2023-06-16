using ErrorOr;

namespace MealMaker.ServiceErrors;

public static class Errors
{
    public static class Meal
    {
        public static Error InvalidName => Error.Validation(
            code: "Meal.InvalidName",
            description: $"Meal name must be between {Models.Meal.MinNameLength} and {Models.Meal.MaxNameLength} characters"
        );

        public static Error InvalidDescription => Error.Validation(
            code: "Meal.InvalidDescription",
            description: $"Descprition must be between {Models.Meal.MinDescriptionLength} and {Models.Meal.MaxDescriptionLength} characters"
        );
        
        public static Error NotFound => Error.NotFound(
            code: "Meal.NotFound",
            description: "Meal not found"
        );
    }
}