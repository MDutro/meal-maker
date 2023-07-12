using ErrorOr;
using MealMaker.Contracts.Meal;
using MealMaker.ServiceErrors;
using Microsoft.AspNetCore.SignalR;

namespace MealMaker.Models;

public class Meal
{
    // public const int MinNameLength = 3;
    // public const int MaxNameLength = 50;

    // public const int MinDescriptionLength = 10;
    // public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    public Meal(
        Guid id, 
        string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        DateTime lastModifiedDateTime, 
        List<string> savory, 
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }
}

//     public static ErrorOr<Meal> Create(
//         string name,
//         string description, 
//         DateTime startDateTime, 
//         DateTime endDateTime, 
//         List<string> savory, 
//         List<string> sweet,
//         Guid? id = null
//         )
//     {
//         List<Error> errors = new();

//         if (name.Length is < MinNameLength or > MaxNameLength)
//         {
//             errors.Add(Errors.Meal.InvalidName);
//         }

//         if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
//         {
//             errors.Add(Errors.Meal.InvalidDescription);
//         }

//         if (errors.Count > 0)
//         {
//             return errors;
//         }

//         return new Meal(
//             id ?? Guid.NewGuid(),
//             name,
//             description,
//             startDateTime,
//             endDateTime,
//             DateTime.UtcNow,
//             savory,
//             sweet
//         );
//     }

//     internal static ErrorOr<Meal> From(CreateMealRequest request)
//     {
//         return Create(
//             request.Name,
//             request.Description,
//             request.StartDateTime,
//             request.EndDateTime,
//             request.Sweet,
//             request.Sweet
//         );
//     }

//     internal static ErrorOr<Meal> From(Guid id, UpsertMealRequest request)
//     {
//         return Create(
//             request.Name,
//             request.Description,
//             request.StartDateTime,
//             request.EndDateTime,
//             request.Sweet,
//             request.Sweet,
//             id
//         );
//     }
// }