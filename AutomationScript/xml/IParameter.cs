namespace AutomationScript.xml
{
    public enum ParameterType
    {
        input,
        output
    }

    public interface IParameter
    {
        string Name { get; set; }
        ParameterType Type { get; set; }
        string Value { get; set; }
    }
}