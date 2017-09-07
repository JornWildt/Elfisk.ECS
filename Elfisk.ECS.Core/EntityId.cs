using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elfisk.ECS.Core
{
  public struct EntityId
  {
    private Guid Id { get; set; }


    public static EntityId NewId()
    {
      return new EntityId { Id = Guid.NewGuid() };
    }


    public override bool Equals(object obj)
    {
      return obj is EntityId && this == (EntityId)obj;
    }


    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }


    public override string ToString()
    {
      return Id.ToString();
    }


    public static bool operator ==(EntityId x, EntityId y)
    {
      return x.Id == y.Id;
    }


    public static bool operator !=(EntityId x, EntityId y)
    {
      return !(x == y);
    }
  }
}
