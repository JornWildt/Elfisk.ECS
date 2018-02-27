using System;
using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using System.Collections.Concurrent;

namespace Elfisk.ECS.Core.Implementation
{
  // Not the most impressive solution - but at least thread safe

  public class InMemoryEntityRepository : IEntityRepository
  {
    private static object Locker = new object();

    private Dictionary<EntityId, Entity> Entities { get; set; }

    private Dictionary<Type, List<IComponent>> Components { get; set; }


    public InMemoryEntityRepository()
    {
      Entities = new Dictionary<EntityId, Entity>();
      Components = new Dictionary<Type, List<IComponent>>();
    }


    public void AddEntity(Entity e)
    {
      lock (Locker)
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
    }


    public void RemoveEntity(EntityId id)
    {
      lock (Locker)
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
    }


    public Entity GetEntity(EntityId id)
    {
      lock (Locker)
      {
        if (Entities.ContainsKey(id))
          return Entities[id];
        else
          return null;
      }
    }


    public IEnumerable<Entity> GetAllEntities()
    {
      lock (Locker)
      {
        return Entities.Values.ToArray();
      }
    }


    public TC GetComponent<TC>(EntityId entityId)
    {
      Entity e = GetEntity(entityId);
      if (e != null && e.Components != null)
        return e.Components.OfType<TC>().FirstOrDefault();
      else
        return default(TC);
    }

    public TC1 GetSingletonComponent<TC1>()
    {
      lock (Locker)
      {
        if (!Components.ContainsKey(typeof(TC1)))
          return default(TC1);

        return Components[typeof(TC1)].Cast<TC1>().First();
      }
    }


    public IEnumerable<TC> GetComponents<TC>(EntityId entityId)
    {
      lock (Locker)
      {
        Entity e = GetEntity(entityId);
        if (e != null && e.Components != null)
          return e.Components.OfType<TC>().ToArray();
        else
          return Enumerable.Empty<TC>();
      }
    }


    public IEnumerable<TC1> GetComponents<TC1>()
      where TC1 : IComponent
    {
      lock (Locker)
      {
        if (!Components.ContainsKey(typeof(TC1)))
          return Enumerable.Empty<TC1>();

        return Components[typeof(TC1)].Cast<TC1>().ToArray();
      }
    }


    public IEnumerable<Tuple<TC1, TC2>> GetComponents<TC1, TC2>()
      where TC1 : IComponent
      where TC2 : IComponent
    {
      lock (Locker)
      {
        List<Tuple<TC1, TC2>> components = new List<Tuple<TC1, TC2>>();

        foreach (var c1 in GetComponents<TC1>())
        {
          foreach (var c2 in GetComponents<TC2>(c1.EntityId))
            components.Add(new Tuple<TC1, TC2>(c1, c2));
        }

        return components;
      }
    }
  }
}
