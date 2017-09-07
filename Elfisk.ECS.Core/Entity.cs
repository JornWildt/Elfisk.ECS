using System.Collections.Generic;
using System.Linq;

namespace Elfisk.ECS.Core
{
  public class Entity
  {
    public EntityId Id { get; set; }

    public IEnumerable<IComponent> Components { get; protected set; }


    public Entity(IEnumerable<IComponent> components)
      : this(EntityId.NewId(), components)
    {
    }


    public Entity(EntityId id, IEnumerable<IComponent> components)
    {
      Id = id;
      Components = components;
    }

    public override string ToString()
    {
      string component = (Components != null ? Components.Select(c => c.ToString()).Aggregate((a, b) => a + "," + b) : "");
      return Id + (component.Length == 0 ? "" : ": " + component);
    }
  }
}
