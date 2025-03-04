namespace ApiService.ValidationResultFactory;

public record ValidationResult(string Title, IDictionary<string, string[]> ValidationErrors);