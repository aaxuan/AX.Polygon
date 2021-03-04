using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AX.Polygon.Admin
{
    /// <summary>
    /// 支持主键基类
    /// </summary>
    public class BaseModel
    {
        [ExplicitKey]
        [Display(Name = "唯一主键")]
        public string Id { get; set; }

        public void InitId()
        { this.Id = Util.IOCManager.GetService<Util.IDGenerator>().CreateID(); }
    }

    /// <summary>
    /// 支持创建基类
    /// </summary>
    public interface BaseCreateModel
    {
        [Display(Name = "创建时间")]
        public DateTime? BaseCreateTime { get; set; }

        [Display(Name = "创建用户Id")]
        public string BaseCreatorId { get; set; }

        public async Task InitCreate()
        {
            if (this.BaseCreateTime == null) { this.BaseCreateTime = DateTime.Now; }
            if (this.BaseCreatorId == null)
            {
                //获取当前用户信息
                //TODO
            }
        }
    }

    /// <summary>
    /// 支持修改基类
    /// </summary>
    public interface BaseModifyModel
    {
        [Display(Name = "修改时间")]
        public DateTime? BaseModifyTime { get; set; }

        [Display(Name = "修改用户Id")]
        public string BaseModifierId { get; set; }

        public async Task InitModify()
        {
            this.BaseModifyTime = DateTime.Now;
            //获取当前用户信息
            //TODO
        }
    }

    /// <summary>
    /// 支持排序基类
    /// </summary>
    public interface BaseOrderModel
    {
        [Display(Name = "排序")]
        public int BaseOrder { get; set; }
    }

    /// <summary>
    /// 禁用启用支持基类
    /// </summary>
    public interface BaseEnabledModel
    {
        [Display(Name = "是否启用")]
        public bool BaseEnabled { get; set; }
    }
}