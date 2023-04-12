using Shared.Models;

namespace FileData;

public class DataContainer
{
    // read data from the file and load into these two collections
    public ICollection<User> Users { get; set; }
    public ICollection<Forum> Forums { get; set; }
}