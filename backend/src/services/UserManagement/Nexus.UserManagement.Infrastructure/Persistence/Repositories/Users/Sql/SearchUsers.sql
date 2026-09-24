WITH matched_users AS (
    SELECT
        u.id::text          AS "UserId",
        u.friendship_code   AS "FriendshipCode",
        u.user_name         AS "UserName",
        u.avatar_key        AS "AvatarKey",
        CASE
            WHEN u.friendship_code = @searchQuery THEN 1
            ELSE 2
        END AS priority
    FROM users u
    WHERE (
        u.friendship_code = @searchQuery 
        OR u.user_name 
        COLLATE "C" 
        ILIKE '%' || @searchQuery || '%' 
        AND (@notIncludeLogin IS NULL OR u.login COLLATE "C" NOT ILIKE @notIncludeLogin)
    )
)
SELECT "UserId", "FriendshipCode", "UserName", "AvatarKey"
FROM matched_users
WHERE NOT EXISTS (SELECT 1 FROM matched_users WHERE priority = 1) OR priority = 1
ORDER BY priority, "UserName";