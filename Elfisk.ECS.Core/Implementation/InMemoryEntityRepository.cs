using System;
using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;

namespace Elfisk.ECS.Core.Implementation
{
  public class InMemoryEntityRepository : IEntityRepository
  {
    private Dictionary<EntityId, Entity> Entities { get; set; }

    private Dictionary<Type, List<IComponent>> Components { get; set; }


    public InMemoryEntityRepository()
    {
      Entities = new Dictionary<EntityId, Entity>();
      Components = new Dictionary<Type, List<IComponent>>();
    }


    public void AddEntity(Entity e)
    {
      Condition.Requires(e, nameof(e)).IsNotNull();

      if (Entities.ContainsKey(e.Id))
        throw new InvalidOperationException($"Cannot add entity with ID {e.Id} twice.");

      Entities[e.Id] = e;

      if (e.Components != null)
      {
        foreach (var c in e.Components)
        {
          if (c != null)
          {
            Type t = c.GetType();
            if (!Components.ContainsKey(t))
              Components[t] = new List<IComponent>();
            Components[t].Add(c);
          }
        }
      }
    }


    public void RemoveEntity(EntityId id)
    {
      Entity entity;
      if (Entities.TryGetValue(id, out entity))
      {
        Entities.Remove(id);
        foreach (Component c in entity.Components)
        {
          if (c != null)
          {
            Type t = c.GetType();
            if (Components.ContainsKey(t))
              Components[t].Remove(c);
          }
        }
      }
    }


    public Entity GetEntity(EntityId id)
    {
      if (Entities.ContainsKey(id))
        return Entities[id];
      else
        return null;
    }


    public IEnumerable<Entity> GetAllEntities()
    {
      return Entities.Values;
    }


    public IEnumerable<TC> GetComponents<TC>(EntityId entityId)
    {
      Entity e = GetEntity(entityId);
      if (e != null && e.Components != null)
        return e.Components.OfType<TC>();
      else
        return Enumerable.Empty<TC>();
    }


    public IEnumerable<TC1> GetComponents<TC1>()
      where TC1 : IComponent
    {
      if (!Components.ContainsKey(typeof(TC1)))
        return Enumerable.Empty<TC1>();

      return Components[typeof(TC1)].Cast<TC1>();
    }


    public IEnumerable<Tuple<TC1, TC2>> GetComponents<TC1, TC2>()
      where TC1 : IComponent
      where TC2 : IComponent
    {
      foreach (var c1 in GetComponents<TC1>())
      {
        foreach (var c2 in GetComponents<TC2>(c1.EntityId))
          yield return new Tuple<TC1, TC2>(c1, c2);
      }
    }
  }
}
