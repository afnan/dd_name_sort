
namespace NameSortingExercise.Infrastructure;

public interface INameRepository
{
    Task<IReadOnlyList<string>> ReadAllAsync(string path);
    Task WriteAllAsync(string path, IEnumerable<string> lines);
}