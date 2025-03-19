
namespace MatchHype.Platform.Helpers
{
    public static class TablesHelpers
    {
        public static async Task<T> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> enumerable)
        {
            var enumerator = enumerable.GetAsyncEnumerator();
            await enumerator.MoveNextAsync();
            return enumerator.Current;
        }
    }
}
