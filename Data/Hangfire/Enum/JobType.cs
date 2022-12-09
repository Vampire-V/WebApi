namespace WebApi.Data.Hangfire.Enum
{
    public enum JobType
    {
        // Delayed jobs are executed only once too, but not immediately, after a certain time interval. 
        Schedule = 0,
        // Recurring jobs fire many times on the specified CRON schedule. 
        Recurring = 1,
        // Continuations are executed when its parent job has been finished.
        ContinueWith = 2
    }
}