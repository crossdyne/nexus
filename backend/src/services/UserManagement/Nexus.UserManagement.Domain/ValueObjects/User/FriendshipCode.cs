namespace Nexus.UserManagement.Domain.ValueObjects.User
{
    public readonly record struct FriendshipCode
    {
        public string Value { get; }

        internal FriendshipCode(string value) => Value = value;

        public static FriendshipCode Create(string friendshipCode)
        {
            if (string.IsNullOrWhiteSpace(friendshipCode))
                throw new ArgumentException("Код дружбы не может быть пустым.", nameof(friendshipCode));

            return new FriendshipCode(friendshipCode);
        }

        public override string ToString() => Value;

        public static implicit operator string(FriendshipCode friendshipCode) => friendshipCode.Value;

    }
}