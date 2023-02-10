declare @sqlCmd nvarchar(max)
if exists(select name
          from sys.objects
          where name = 'vw_UserPermissions')
    begin
        drop view vw_UserPermissions
    end;

select @sqlCmd = 'create view vw_UserPermissions as
select u.Id as UserId, r.Id as RoleId, rc.Value as Permission
from Users u
         inner join UserRoles ur on u.Id = ur.UserId
         inner join Roles r on r.Id = ur.RoleId
         inner join RoleClaims rc on r.Id = rc.RoleId'
exec sp_executesql @sqlCmd