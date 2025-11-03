namespace HRManagementSystem.Application.Common;

public record Response<T>(T Data, string ErrorMessage, bool HasError);
