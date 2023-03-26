namespace Application.Interfaces
{
    /// <IDateTimeService>
    /// With this interface we will help us to place the data of when it was created and when it was modified in the properties of the auditable class.
    /// </IDateTimeService>
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
