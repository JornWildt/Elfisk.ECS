using CuttingEdge.Conditions;

namespace Elfisk.ECS.Core.Components
{
  public class NameComponent : Component
  {
    public string Name { get; set; }


    public NameComponent(EntityId entityId, string name)
      : base(entityId)
    {
      Condition.Requires(name, nameof(name)).IsNotNullOrEmpty();

      Name = name;
    }


    public override string ToString()
    {
      return $"Name: {Name}";
    }
  }
}
