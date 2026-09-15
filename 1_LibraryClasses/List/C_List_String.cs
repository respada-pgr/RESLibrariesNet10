using System.Collections;
using System.Text;

namespace _1_LibraryClassesNet10.List
{
    public class C_List_String : ICollection<string>
    {
        private List<string> StringList { get; set; }
        public int Count => StringList.Count;

        public bool IsReadOnly => false;

        public C_List_String()
        {
            StringList = new List<string>();
        }

        public C_List_String(IEnumerable<string> collection)
        {
            StringList = new List<string>(collection);
        }

        public C_List_String(int capacity)
        {
            StringList = new List<string>(capacity);
        }

        public C_List_String(C_List_String stringList)
        {
            StringList = new List<string>(stringList.StringList);
        }

        public string Get(int index)
        {
            if (index >= 0 && index < StringList.Count)
            {
                return StringList[index];
            }
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
        }

        public string GetFirst()
        {
            if (StringList.Count > 0)
            {
                return StringList[0];
            }
            throw new InvalidOperationException("List is empty");
        }

        public string GetLast()
        {
            if (StringList.Count > 0)
            {
                return StringList[StringList.Count - 1];
            }
            throw new InvalidOperationException("List is empty");
        }

        public void Set(int index, string value)
        {
            if (index >= 0 && index < StringList.Count)
            {
                StringList[index] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
            }
        }

        public void SetFirst(string value)
        {
            if (StringList.Count > 0)
            {
                StringList[0] = value;
            }
            else
            {
                throw new InvalidOperationException("List is empty");
            }
        }

        public void SetLast(string value)
        {
            if (StringList.Count > 0)
            {
                StringList[StringList.Count - 1] = value;
            }
            else
            {
                throw new InvalidOperationException("List is empty");
            }
        }

        public void Add(string item)
        {
            StringList.Add(item);
        }

        public void Add(IEnumerable<string> collection)
        {
            StringList.AddRange(collection);
        }

        public void Add(C_List_String lineList)
        {
            StringList.AddRange(lineList.StringList);
        }

        public void AddToStart(string item)
        {
            StringList.Insert(0, item);
        }

        public void AddToStart(C_List_String lineList)
        {
            StringList.InsertRange(0, lineList.StringList);
        }

        public void Insert(int index, string item)
        {
            StringList.Insert(index, item);
        }

        public void Insert(int index, IEnumerable<string> collection)
        {
            StringList.InsertRange(index, collection);
        }

        public void Insert(int index, C_List_String lineList)
        {
            StringList.InsertRange(index, lineList.StringList);
        }

        public bool Remove(string item)
        {
            return StringList.Remove(item);
        }

        public void RemoveFirst(int n = 1)
        {
            if (n > 0)
            {
                if (n > StringList.Count)
                    n = StringList.Count;
                StringList.RemoveRange(0, n);
            }
        }

        public void RemoveLast(int n = 1)
        {
            if (n > 0)
            {
                if (n > StringList.Count)
                    n = StringList.Count;
                StringList.RemoveRange(StringList.Count - n, n);
            }
        }

        public void Clear()
        {
            StringList.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in StringList)
            {
                sb.AppendLine(line);
            }
            return sb.ToString().TrimEnd();
        }

        public bool Contains(string item)
        {
            return StringList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            StringList.CopyTo(array, arrayIndex);
        }

        public void Import(string text)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringList.AddRange(lines);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return StringList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)StringList).GetEnumerator();
        }
    }
}
