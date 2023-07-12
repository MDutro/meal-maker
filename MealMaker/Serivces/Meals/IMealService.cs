using MealMaker.Models;
using ErrorOr;
using MealMaker.Contracts.Meal;

namespace MealMaker.Services.Meals;

public interface IMealService
{
    ErrorOr<Created> CreateMeal(Meal meal);
    ErrorOr<Meal> GetMeal(Guid id);
    ErrorOr<UpsertedMeal> UpsertMeal(Meal meal);
    ErrorOr<Deleted> DeleteMeal(Guid id);
    ErrorOr<Meal> From (CreateMealRequest request);
    ErrorOr<Meal> From (Guid id, UpsertMealRequest request);
}