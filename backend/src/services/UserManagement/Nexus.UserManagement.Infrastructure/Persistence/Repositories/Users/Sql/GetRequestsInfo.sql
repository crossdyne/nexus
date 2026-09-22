SELECT 
    u.id::text as "UserId",
    u.user_name as "UserName",
    u.avatar_key as "AvatarKey"
FROM users u
WHERE u.id = ANY(@userIds)