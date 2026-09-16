SELECT 
    u.friendship_code as "FriendshipCode",
    u.user_name as "UserName",
    u.avatar_key as "AvatarKey"
FROM users u
WHERE u.user_name COLLATE "C" ILIKE '%' || @userName || '%'
    AND (@notIncludeLogin IS NULL OR u.login COLLATE "C" NOT ILIKE @notIncludeLogin)