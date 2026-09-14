SELECT 
    u.friendship_code as "FriendshipCode",
    u.user_name as "UserName",
    u.avatar_key as "AvatarKey"
FROM users u
WHERE u.user_name = @userName