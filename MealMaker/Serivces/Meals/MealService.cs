using MealMaker.Models;
using ErrorOr;
using MealMaker.ServiceErrors;
using MealMaker.Contracts.Meal;

namespace MealMaker.Services.Meals;

public class MealService : IMealService   
{
    // "save" a meal to memory for now. Implement actual database later!
    private static readonly Dictionary<Guid, Meal> _meals =  new();

    // These variables are for validation
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 150;
    
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

    public ErrorOr<Meal> Create(
        string name,
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        List<string> savory, 
        List<string> sweet,
        Guid? id = null
        )
    {
        List<Error> errors = new();

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Meal.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Meal.InvalidDescription);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Meal(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet
        );
    }

    public ErrorOr<Meal> From(CreateMealRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Sweet,
            request.Sweet
        );
    }

    public ErrorOr<Meal> From(Guid id, UpsertMealRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Sweet,
            request.Sweet,
            id
        );
    }
}
