namespace Domain.Common.Authorization;

public static class Actions
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);

    private static readonly IEnumerable<string> Values = new[]
    {
        View,
        Search,
        Create,
        Update,
        Delete
    };

    public static bool IsInActions(string action)
    {
        return Values.Contains(action);
    }
}