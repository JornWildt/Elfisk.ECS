namespace Elfisk.ECS.Core.Components
{
  public class NameComponent : Component
  {
    public string Name { get; set; }


    public NameComponent(EntityId entityId, string name)
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
