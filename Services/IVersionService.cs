namespace BigBazar.Services;

public interface IVersionService
{
    string Title { get; }
    string Description { get; }
    string Company { get; }
    string Product { get; }
    string Copyright { get; }
    string Version { get; }
}