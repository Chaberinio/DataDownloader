using Core.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace DataSaver
{
    public class RevolutDataSaver : IDataSaver
    {
        public async Task SaveAsJson(string json, string name)
        {
            await File.WriteAllTextAsync(name + "Data.json", json);
        }

        public async Task SaveAsTxt(string value, string name)
        {
            await File.WriteAllTextAsync(name + "Data.txt", value);
        }
    }
}