namespace Elfisk.ECS.Core.Components
{
  public class NamedComponent : Component
  {
    public string Name { get; set; }


    public NamedComponent(EntityId entityId, string name)
      : base(entityId)
    {
      Name = name;
    }


    public override string ToString()
    {
      return $"Name: {Name}";
    }
  }
}
