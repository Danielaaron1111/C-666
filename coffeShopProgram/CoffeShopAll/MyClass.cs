public class MyClass {
    // 1. Private fields
    // 2. Constants
    // 3. Properties with validation
    // 4. Constructor
    // 5. ToString() for CSV write
    // 6. static Parse() for CSV read

    public override string ToString()
    {
        // Implement your ToString logic here for CSV write
    }

    public static MyClass Parse(string csvLine)
    {
        // Implement your Parse logic here for CSV read
    }
}

// Usage example
MyClass obj = new MyClass(...);
string path = $@"{webHostEnvironment.ContentRootPath}/Data/file.csv";
string line = $"{obj.ToString()}\n";
System.IO.File.AppendAllText(path, line);

string[] lines = System.IO.File.ReadAllLines(filePath);
foreach (string line in lines) {
    try {
        MyClass obj = MyClass.Parse(line);
        list.Add(obj);
    }
    catch (Exception ex) {
        errors.Add(ex.Message);
    }
}

<select @bind="myEnum">
    <option value="">-- Select --</option>
    @foreach (var item in Enum.GetValues(typeof(MyEnum))) {
        <option value="@item">@item</option>
    }
</select>

@code {
    private MyEnum? myEnum = null;
    
    // Validate:
    if (!myEnum.HasValue) { error }
    
    // Use:
    new MyClass(myEnum.Value)
}