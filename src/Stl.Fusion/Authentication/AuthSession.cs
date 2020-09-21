using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Stl.Extensibility;
using Stl.Fusion.Authentication.Internal;

namespace Stl.Fusion.Authentication
{
    [Serializable]
    [JsonConverter(typeof(SessionJsonConverter))]
    [TypeConverter(typeof(SessionTypeConverter))]
    public class AuthSession : IHasId<string>, IEquatable<AuthSession>, IConvertibleTo<string>
    {
        public string Id { get; }

        [JsonConstructor]
        public AuthSession(string id) => Id = id;

        public override string ToString() => Id;
        string IConvertibleTo<string>.Convert() => Id;

        // Equality

        public virtual bool Equals(AuthSession? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (ReferenceEquals(null, other))
                return false;
            if (other.GetType() != GetType())
                return false;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => obj is AuthSession s && Equals(s);
        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(AuthSession? left, AuthSession? right) => Equals(left, right);
        public static bool operator !=(AuthSession? left, AuthSession? right) => !Equals(left, right);
    }
}