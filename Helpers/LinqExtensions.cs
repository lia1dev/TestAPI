using TestAPI.Entities.Base;

namespace TestAPI.Helpers
{
    public static class LinqExtensions
    {
        public static bool IsAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static bool IsInvalid(this BaseEntity entity) => entity is null || entity.Id == 0;
    }
}