using Stl.Internal;

namespace Stl.Frozen 
{
    public static class FrozenEx
    {
        // This method isn't a part of the interface mainly because otherwise
        // it's going to be a virtual generic method (i.e. w/ super slow invocation).
        public static T ToUnfrozen<T>(this T frozen, bool deep = false) 
            where T : IFrozen 
            => (T) frozen.BaseToUnfrozen(deep); 

        // ThrowIfXxx

        public static void ThrowIfUnfrozen(this IFrozen frozen)
        {
            if (!frozen.IsFrozen) throw Errors.MustBeFrozen();
        }

        public static void ThrowIfUnfrozen(this IFrozen frozen, string paramName)
        {
            if (!frozen.IsFrozen) throw Errors.MustBeFrozen(paramName);
        }

        public static void ThrowIfFrozen(this IFrozen frozen)
        {
            if (frozen.IsFrozen) throw Errors.MustBeUnfrozen();
        }

        public static void ThrowIfFrozen(this IFrozen frozen, string paramName)
        {
            if (frozen.IsFrozen) throw Errors.MustBeUnfrozen(paramName);
        }
    }
}
