using System.Threading.Tasks;

namespace MyWebApp.Domain.Whiteboards
{
    public interface IWhiteboardApi
    {
        Task DrawLine(DrawLineDto dto);
        Task DrawLines(DrawLinesDto dto);
    }
    
    public class DrawLinesDto
    {
        //todo
    }

    public class DrawLineDto
    {
        //todo
        public int prevX { get; set; }
        public int prevY { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string color { get; set; }
    }

    #region temp

    //public class SharedDraws
    //{
    //    public SharedDraws()
    //    {
    //        Draws = new List<SharedDraw>();
    //    }

    //    public BoardInfo Board { get; set; }
    //    public IList<SharedDraw> Draws { get; set; }
    //}

    //public class BoardInfo
    //{
    //    public int Width { get; set; }
    //    public int Height { get; set; }
    //}

    //public class SharedDraw
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public string Color { get; set; }
    //}
    //public class DrawData
    //{
    //    public float X { get; set; }
    //    public float Y { get; set; }
    //    public DateTime? CreateAt { get; set; }
    //}

    #endregion
}
