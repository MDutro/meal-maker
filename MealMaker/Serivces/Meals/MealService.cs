using MealMaker.Models;
using ErrorOr;
using MealMaker.ServiceErrors;

namespace MealMaker.Services.Meals;

public class MealService : IMealService
{
    // "save" a meal to memory for now. Implement actual database later!
    private static readonly Dictionary<Guid, Meal> _meals =  new();
    
    public ErrorOr<Created> CreateMeal(Meal meal)
    {
        _meals.Add(meal.Id, meal);

        return Result.Created;
    }

    public ErrorOr<Meal> GetMeal(Guid id)
    {
        if (_meals.TryGetValue(id, out var meal))
        {
            return meal;
        }

        return Errors.Meal.NotFound;
    }

    public ErrorOr<UpsertedMeal> UpsertMeal(Meal meal)
    {
        var IsNewlyCreated = !_meals.ContainsKey(meal.Id);
        _meals[meal.Id] = meal;

        return new UpsertedMeal(IsNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteMeal(Guid id)
    {
        _meals.Remove(id);

        return Result.Deleted;
    }
}