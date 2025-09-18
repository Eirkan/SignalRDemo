namespace SignalR.Demo.SignalR;

public interface IProgressReporterFactory
{
    IProgress<double> GetLoadingBarReporter(string connectionId);
}
