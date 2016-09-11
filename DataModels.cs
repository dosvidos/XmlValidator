using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace XmlValidator
{
    [DataContract(Namespace = ValidationUtilities.Namespace)]
    class FileEntry
    {
        [DataMember]
        public String Name { get; private set; }
        [DataMember]
        public Int32 Size { get; private set; }
        [DataMember]
        public DateTime ModificationTime { get; private set; }

        public FileEntry(String name, Int32 size, DateTime modificationTime)
        {
            Name = name;
            Size = size;
            ModificationTime = modificationTime;
        }
    }

    [DataContract(Namespace = ValidationUtilities.Namespace)]
    class DirectoryEntry
    {
        [DataMember]
        public String Name { get; private set; }

        [DataMember]
        public List<FileEntry> Files { get; private set; }

        public DirectoryEntry(String name, IEnumerable<FileEntry> files)
        {
            Name = name;
            Files = files.ToList();
        }
    }

    [DataContract(Namespace = ValidationUtilities.Namespace)]
    class Registry
    {
        [DataMember]
        public Int32 ID { get; private set; }

        [DataMember]
        public List<DirectoryEntry> Directories { get; private set; }

        public Registry(Int32 id, IEnumerable<DirectoryEntry> directories)
        {
            ID = id;
            Directories = directories.ToList();
        }
    }
}
