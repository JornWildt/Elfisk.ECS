namespace Elfisk.ECS.Core.Messages
{
  public class RenderMessage
  {
    public string SpriteId { get; set; }

    public string Texture { get; set; }

    public string Label { get; set; }

    public int X { get; set; }

    public int Y { get; set; }


    public override string ToString()
    {
      return $"Render {Texture}({Label} at ({X};{Y})";
    }
  }
}
