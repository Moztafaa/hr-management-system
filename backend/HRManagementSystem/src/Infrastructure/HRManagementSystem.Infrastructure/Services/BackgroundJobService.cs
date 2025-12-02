using System.Linq.Expressions;
using Hangfire;
using HRManagementSystem.Application.Interfaces;

namespace HRManagementSystem.Infrastructure.Services;

/// <summary>
/// Implementation of IBackgroundJobService using Hangfire.
/// Provides a clean abstraction over Hangfire's job scheduling capabilities.
/// </summary>
public class BackgroundJobService : IBackgroundJobService
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    public BackgroundJobService(
        IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManager)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
    }

    /// <inheritdoc />
    public string Enqueue(Expression<Action> methodCall)
    {
        return _backgroundJobClient.Enqueue(methodCall);
    }

    /// <inheritdoc />
    public string Enqueue<T>(Expression<Action<T>> methodCall)
    {
        return _backgroundJobClient.Enqueue(methodCall);
    }

    /// <inheritdoc />
    public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
    {
        return _backgroundJobClient.Enqueue(methodCall);
    }

    /// <inheritdoc />
    public string Schedule(Expression<Action> methodCall, TimeSpan delay)
    {
        return _backgroundJobClient.Schedule(methodCall, delay);
    }

    /// <inheritdoc />
    public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
    {
        return _backgroundJobClient.Schedule(methodCall, delay);
    }

    /// <inheritdoc />
    public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
    {
        return _backgroundJobClient.Schedule(methodCall, delay);
    }

    /// <inheritdoc />
    public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt)
    {
        return _backgroundJobClient.Schedule(methodCall, enqueueAt);
    }

    /// <inheritdoc />
    public void AddOrUpdateRecurringJob(string recurringJobId, Expression<Action> methodCall, string cronExpression)
    {
        _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);
    }

    /// <inheritdoc />
    public void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression)
    {
        _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);
    }

    /// <inheritdoc />
    public void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression)
    {
        _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);
    }

    /// <inheritdoc />
    public void RemoveRecurringJob(string recurringJobId)
    {
        _recurringJobManager.RemoveIfExists(recurringJobId);
    }

    /// <inheritdoc />
    public void TriggerRecurringJob(string recurringJobId)
    {
        _recurringJobManager.Trigger(recurringJobId);
    }

    /// <inheritdoc />
    public bool Delete(string jobId)
    {
        return _backgroundJobClient.Delete(jobId);
    }

    /// <inheritdoc />
    public string ContinueWith<T>(string parentJobId, Expression<Func<T, Task>> methodCall)
    {
        return _backgroundJobClient.ContinueJobWith(parentJobId, methodCall);
    }
}
