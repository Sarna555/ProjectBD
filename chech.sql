select dbo.Users.name, operations.name , groups.name
from Users, operations, users2operation, groups, users2groups
where Users.user_ID = users2operation.user_ID and operations.operation_ID = users2operation.operation_ID
and Users.user_ID = users2groups.user_ID and groups.group_ID = users2groups.group_ID