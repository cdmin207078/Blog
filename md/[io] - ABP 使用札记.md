#ABP 使用札记

##创建实体
> 在ABP框架中,有一个Entity基类,它有一个Id属性,int类型是Entity基类Id的默认类型,没有特别指定类型时,实体的Id就是int类型。

```csharp

/*用户自定义类型, 继承自 ABP.Domain.Entities.Entity 或 对应的泛型类型*/
using Abp.Domain.Entities;
public class Hotel : Entity { }

/*ABP.Domain.Entities 基类中有定义泛型主键*/
namespace Abp.Domain.Entities
{
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity {
    	//somethings
    }
}

/*泛型基类,实现IEntity<TPrimaryKey> 接口*/
namespace Abp.Domain.Entities
{
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey> {
        public virtual TPrimaryKey Id { get; set; }
    }
}

/*IEntity<TPrimaryKey> 接口*/
namespace Abp.Domain.Entities
{
    public interface IEntity<TPrimaryKey> {
        TPrimaryKey Id { get; set; }
    }
}

```