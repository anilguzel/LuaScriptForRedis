
local userId = @userId

local result = {}

local topThree = redis.call("ZREVRANGE", "Leaderboard", "0",  "2",  "WITHSCORES")

local isRequestedUser = false

local rank = 1
for i=0, 2 do
    local idx = i * 2 + 1
    table.insert(result, {
                    UserId = topThree[idx],
                    Score = topThree[idx + 1],
                    Rank = rank
                })
    rank = rank + 1;

    if topThree[idx] == userId then
        isRequestedUser = true
    end
end

if isRequestedUser == false
then
    table.insert(result, {
        UserId = userId,
        Score = redis.call("ZSCORE", "Leaderboard", userId),
        Rank = redis.call("ZREVRANK", "Leaderboard", userId)
    })
end

return cjson.encode(result)


