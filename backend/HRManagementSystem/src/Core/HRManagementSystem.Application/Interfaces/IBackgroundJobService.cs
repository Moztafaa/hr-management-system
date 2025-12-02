using System.Linq.Expressions;

namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Service for scheduling and managing background jobs using Hangfire.
/// Provides abstraction over Hangfire's job scheduling capabilities.
/// </summary>
public interface IBackgroundJobService
{
    /// <summary>
    /// Enqueues a job to be executed immediately in the background.
    /// </summary>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <returns>The job ID.</returns>
    string Enqueue(Expression<Action> methodCall);

    /// <summary>
    /// Enqueues a job to be executed immediately in the background.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <returns>The job ID.</returns>
    string Enqueue<T>(Expression<Action<T>> methodCall);

    /// <summary>
    /// Enqueues an async job to be executed immediately in the background.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="methodCall">Expression representing the async method to execute.</param>
    /// <returns>The job ID.</returns>
    string Enqueue<T>(Expression<Func<T, Task>> methodCall);

    /// <summary>
    /// Schedules a job to be executed after a specified delay.
    /// </summary>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <param name="delay">The time to wait before executing the job.</param>
    /// <returns>The job ID.</returns>
    string Schedule(Expression<Action> methodCall, TimeSpan delay);

    /// <summary>
    /// Schedules a job to be executed after a specified delay.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <param name="delay">The time to wait before executing the job.</param>
    /// <returns>The job ID.</returns>
    string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

    /// <summary>
    /// Schedules an async job to be executed after a specified delay.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="methodCall">Expression representing the async method to execute.</param>
    /// <param name="delay">The time to wait before executing the job.</param>
    /// <returns>The job ID.</returns>
    string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

    /// <summary>
    /// Schedules a job to be executed at a specific time.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="methodCall">Expression representing the async method to execute.</param>
    /// <param name="enqueueAt">The specific time to execute the job.</param>
    /// <returns>The job ID.</returns>
    string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);

    /// <summary>
    /// Creates or updates a recurring job.
    /// </summary>
    /// <param name="recurringJobId">Unique identifier for the recurring job.</param>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <param name="cronExpression">Cron expression defining the schedule.</param>
    void AddOrUpdateRecurringJob(string recurringJobId, Expression<Action> methodCall, string cronExpression);

    /// <summary>
    /// Creates or updates a recurring job.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="recurringJobId">Unique identifier for the recurring job.</param>
    /// <param name="methodCall">Expression representing the method to execute.</param>
    /// <param name="cronExpression">Cron expression defining the schedule.</param>
    void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression);

    /// <summary>
    /// Creates or updates a recurring async job.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="recurringJobId">Unique identifier for the recurring job.</param>
    /// <param name="methodCall">Expression representing the async method to execute.</param>
    /// <param name="cronExpression">Cron expression defining the schedule.</param>
    void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression);

    /// <summary>
    /// Removes a recurring job.
    /// </summary>
    /// <param name="recurringJobId">The ID of the recurring job to remove.</param>
    void RemoveRecurringJob(string recurringJobId);

    /// <summary>
    /// Triggers a recurring job to execute immediately.
    /// </summary>
    /// <param name="recurringJobId">The ID of the recurring job to trigger.</param>
    void TriggerRecurringJob(string recurringJobId);

    /// <summary>
    /// Deletes a background job.
    /// </summary>
    /// <param name="jobId">The ID of the job to delete.</param>
    /// <returns>True if the job was deleted, false otherwise.</returns>
    bool Delete(string jobId);

    /// <summary>
    /// Continues with another job after a parent job completes.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <param name="parentJobId">The ID of the parent job.</param>
    /// <param name="methodCall">Expression representing the async method to execute.</param>
    /// <returns>The continuation job ID.</returns>
    string ContinueWith<T>(string parentJobId, Expression<Func<T, Task>> methodCall);
}
