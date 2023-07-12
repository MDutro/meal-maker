using MealMaker.Contracts.Meal;
using MealMaker.Services.Meals;
using MealMaker.Models;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace MealMaker.Controllers;

public class MealsController : ApiController
{
    private readonly IMealService _mealService;

    public MealsController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [HttpPost]
    public IActionResult CreateMeal(CreateMealRequest request)
    {
        ErrorOr<Meal> requestToMealResult = _mealService.From(request);

        if (requestToMealResult.IsError)
        {
            return Problem(requestToMealResult.Errors);
        }

        var meal = requestToMealResult.Value;

        ErrorOr<Created> createMealResult = _mealService.CreateMeal(meal);

        return createMealResult.Match(
            created => CreatedAtGetMeal(meal),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetMeal(Guid id)
    {
        ErrorOr<Meal> getMealResult = _mealService.GetMeal(id);

        return getMealResult.Match(
            meal => Ok(MapMealResponse(meal)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertMeal(Guid id, UpsertMealRequest request)
    {
        ErrorOr<Meal> requesToMealResult = _mealService.From(id, request);

        if (requesToMealResult.IsError)
        {
            return Problem(requesToMealResult.Errors);
        }

        var meal = requesToMealResult.Value;

        ErrorOr<UpsertedMeal> upsertMealResult = _mealService.UpsertMeal(meal);
        
        return upsertMealResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetMeal(meal) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMeal(Guid id)
    {
        ErrorOr<Deleted> deletedMealResult = _mealService.DeleteMeal(id);
        
        return deletedMealResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private static MealResponse MapMealResponse(Meal meal)
    {
        return new MealResponse(
                    meal.Id,
                    meal.Name,
                    meal.Description,
                    meal.StartDateTime,
                    meal.EndDateTime,
                    meal.LastModifiedDateTime,
                    meal.Savory,
                    meal.Sweet
                );
    }

    private IActionResult CreatedAtGetMeal(Meal meal)
    {
        return CreatedAtAction(
            actionName: nameof(GetMeal),
            routeValues: new { id = meal.Id },
            value: MapMealResponse(meal)
        );
    }
}