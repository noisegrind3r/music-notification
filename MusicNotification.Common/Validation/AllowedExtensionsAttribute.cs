using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicNotification.Common.Validation;

public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
{
    private readonly string[] _extensions = extensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var files = value as IList<IFormFile>;
        foreach (var file in files ?? [])
        {
            if (file == null) continue;
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage(file.FileName));
            }
        }

        return ValidationResult.Success;
    }

    private static string GetErrorMessage(string name)
    {
        return $"Некорректный формат файла {name}!";
    }
}
