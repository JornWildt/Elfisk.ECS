using System;
using System.Collections.Generic;

namespace Elfisk.ECS.Core
{
  public interface IEntityRepository
  {
    void AddEntity(Entity e);

    void RemoveEntity(EntityId id);

    Entity GetEntity(EntityId id);

    IEnumerable<Entity> GetAllEntities();

    TC1 GetComponent<TC1>(EntityId id);

    IEnumerable<TC1> GetComponents<TC1>()
      where TC1 : IComponent;

    IEnumerable<Tuple<TC1,TC2>> GetComponents<TC1,TC2>()
      where TC1 : IComponent
      where TC2 : IComponent;
  }
}
