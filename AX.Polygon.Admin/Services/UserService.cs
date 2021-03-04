using AX.Polygon.Admin.DataModel;
using AX.Polygon.DataRepository;
using AX.Polygon.Util;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    public class UserService : BaseService<User>
    {
        #region 私有方法

        public static string SHA256Hash(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            var hashBytes = new SHA256Managed().ComputeHash(data);
            return Convert.ToBase64String(hashBytes);
        }

        private string GetRandomSalt()
        {
            return new Random().Next(1, 100000).ToString();
        }

        #endregion 私有方法

        //public async Task<TData<UserEntity>> GetEntity(long id)
        //{
        //    TData<UserEntity> obj = new TData<UserEntity>();
        //    obj.Data = await userService.GetEntity(id);

        //    await GetUserBelong(obj.Data);

        //    if (obj.Data.DepartmentId > 0)
        //    {
        //        DepartmentEntity departmentEntity = await departmentService.GetEntity(obj.Data.DepartmentId.Value);
        //        if (departmentEntity != null)
        //        {
        //            obj.Data.DepartmentName = departmentEntity.DepartmentName;
        //        }
        //    }

        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<User> LoginCheck(string loginName, string password)
        {
            if (string.IsNullOrWhiteSpace(loginName)) { throw new WarningMessageException("用户名或密码不能为空"); }
            if (string.IsNullOrWhiteSpace(password)) { throw new WarningMessageException("用户名或密码不能为空"); }

            var db = IOCManager.GetScopeService<IRepository>();
            var user = await db.QuerySingleAsync<User>("select * from base_user where LoginName = @LoginName", new { LoginName = loginName });

            if (user == null)
            {
                await new Admin.Services.SystemLoginLogService().DefualtInsert(new SystemLoginLog() { LoginName = loginName, Type = "登陆失败-用户名错误" });
                throw new WarningMessageException("用户名或密码错误");
            }
            if (user.Password != SHA256Hash(password + user.Salt))
            {
                await new Admin.Services.SystemLoginLogService().DefualtInsert(new SystemLoginLog() { LoginName = loginName, Type = "登陆失败-密码错误" });
                throw new WarningMessageException("用户名或密码错误");
            }

            user.LoginCount++;
            user.IsOnline = true;
            if (user.FirstVisit == null) { user.FirstVisit = DateTime.Now; }
            user.PreviousVisit = user.LastVisit;
            user.LastVisit = DateTime.Now;

            await db.Update<User>(user);
            return user;
        }

        //public async Task<TData<string>> SaveForm(UserEntity entity)
        //{
        //    TData<string> obj = new TData<string>();
        //    if (userService.ExistUserName(entity))
        //    {
        //        obj.Message = "用户名已经存在！";
        //        return obj;
        //    }
        //    if (entity.Id.IsNullOrZero())
        //    {
        //        entity.Salt = GetPasswordSalt();
        //        entity.Password = EncryptUserPassword(entity.Password, entity.Salt);
        //    }
        //    if (!entity.Birthday.IsEmpty())
        //    {
        //        entity.Birthday = entity.Birthday.ParseToDateTime().ToString("yyyy-MM-dd");
        //    }
        //    await userService.SaveForm(entity);

        //    await RemoveCacheById(entity.Id.Value);

        //    obj.Data = entity.Id.ParseToString();
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData> DeleteForm(string ids)
        //{
        //    TData obj = new TData();
        //    if (string.IsNullOrEmpty(ids))
        //    {
        //        obj.Message = "参数不能为空";
        //        return obj;
        //    }
        //    await userService.DeleteForm(ids);

        //    await RemoveCacheById(ids);

        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<long>> ResetPassword(UserEntity entity)
        //{
        //    TData<long> obj = new TData<long>();
        //    if (entity.Id > 0)
        //    {
        //        UserEntity dbUserEntity = await userService.GetEntity(entity.Id.Value);
        //        if (dbUserEntity.Password == entity.Password)
        //        {
        //            obj.Message = "密码未更改";
        //            return obj;
        //        }
        //        entity.Salt = GetPasswordSalt();
        //        entity.Password = EncryptUserPassword(entity.Password, entity.Salt);
        //        await userService.ResetPassword(entity);

        //        await RemoveCacheById(entity.Id.Value);

        //        obj.Data = entity.Id.Value;
        //        obj.Tag = 1;
        //    }
        //    return obj;
        //}

        //public async Task<TData<long>> ChangePassword(ChangePasswordParam param)
        //{
        //    TData<long> obj = new TData<long>();
        //    if (param.Id > 0)
        //    {
        //        if (string.IsNullOrEmpty(param.Password) || string.IsNullOrEmpty(param.NewPassword))
        //        {
        //            obj.Message = "新密码不能为空";
        //            return obj;
        //        }
        //        UserEntity dbUserEntity = await userService.GetEntity(param.Id.Value);
        //        if (dbUserEntity.Password != EncryptUserPassword(param.Password, dbUserEntity.Salt))
        //        {
        //            obj.Message = "旧密码不正确";
        //            return obj;
        //        }
        //        dbUserEntity.Salt = GetPasswordSalt();
        //        dbUserEntity.Password = EncryptUserPassword(param.NewPassword, dbUserEntity.Salt);
        //        await userService.ResetPassword(dbUserEntity);

        //        await RemoveCacheById(param.Id.Value);

        //        obj.Data = dbUserEntity.Id.Value;
        //        obj.Tag = 1;
        //    }
        //    return obj;
        //}

        ///// <summary>
        ///// 用户自己修改自己的信息
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public async Task<TData<long>> ChangeUser(UserEntity entity)
        //{
        //    TData<long> obj = new TData<long>();
        //    if (entity.Id > 0)
        //    {
        //        await userService.ChangeUser(entity);

        //        await RemoveCacheById(entity.Id.Value);

        //        obj.Data = entity.Id.Value;
        //        obj.Tag = 1;
        //    }
        //    return obj;
        //}

        //public async Task<TData> UpdateUser(UserEntity entity)
        //{
        //    TData obj = new TData();
        //    await userService.UpdateUser(entity);

        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData> ImportUser(ImportParam param, List<UserEntity> list)
        //{
        //    TData obj = new TData();
        //    if (list.Any())
        //    {
        //        foreach (UserEntity entity in list)
        //        {
        //            UserEntity dbEntity = await userService.GetEntity(entity.UserName);
        //            if (dbEntity != null)
        //            {
        //                entity.Id = dbEntity.Id;
        //                if (param.IsOverride == 1)
        //                {
        //                    await userService.SaveForm(entity);
        //                    await RemoveCacheById(entity.Id.Value);
        //                }
        //            }
        //            else
        //            {
        //                await userService.SaveForm(entity);
        //                await RemoveCacheById(entity.Id.Value);
        //            }
        //        }
        //        obj.Tag = 1;
        //    }
        //    else
        //    {
        //        obj.Message = " 未找到导入的数据";
        //    }
        //    return obj;
        //}

        //#endregion 提交数据

        //#region 私有方法

        ///// <summary>
        ///// 移除缓存里面的token
        ///// </summary>
        ///// <param name="id"></param>
        //private async Task RemoveCacheById(string ids)
        //{
        //    foreach (long id in ids.Split(',').Select(p => long.Parse(p)))
        //    {
        //        await RemoveCacheById(id);
        //    }
        //}

        //private async Task RemoveCacheById(long id)
        //{
        //    var dbEntity = await userService.GetEntity(id);
        //    if (dbEntity != null)
        //    {
        //        CacheFactory.Cache.RemoveCache(dbEntity.WebToken);
        //    }
        //}

        ///// <summary>
        ///// 获取用户的职位和角色
        ///// </summary>
        ///// <param name="user"></param>
        //private async Task GetUserBelong(UserEntity user)
        //{
        //    List<UserBelongEntity> userBelongList = await userBelongService.GetList(new UserBelongEntity { UserId = user.Id });

        //    List<UserBelongEntity> roleBelongList = userBelongList.Where(p => p.BelongType == UserBelongTypeEnum.Role.ParseToInt()).ToList();
        //    if (roleBelongList.Count > 0)
        //    {
        //        user.RoleIds = string.Join(",", roleBelongList.Select(p => p.BelongId).ToList());
        //    }

        //    List<UserBelongEntity> positionBelongList = userBelongList.Where(p => p.BelongType == UserBelongTypeEnum.Position.ParseToInt()).ToList();
        //    if (positionBelongList.Count > 0)
        //    {
        //        user.PositionIds = string.Join(",", positionBelongList.Select(p => p.BelongId).ToList());
        //    }
        //}

        //#endregion 私有方法
    }
}