SELECT
    u.id::text as "UserId"
FROM users u
WHERE u.friendship_code = @inviteCode