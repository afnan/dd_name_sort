using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSortingExercise.Infrastructure
{
    public class FileNameRepository : INameRepository
    {
        public async Task<IReadOnlyList<string>> ReadAllAsync(string path)
            => (await File.ReadAllLinesAsync(path, Encoding.UTF8)).ToList();

        public async Task WriteAllAsync(string path, IEnumerable<string> lines)
            => await File.WriteAllLinesAsync(path, lines, Encoding.UTF8);
    }
}
