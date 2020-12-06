namespace Taskmato_2.Models
{
    public class Taskmato
    {
        public int TaskmatoId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Pomodoros { get; set; }
        public bool Complete { get; set; }

        public Taskmato()
        {
            Complete = false;
        }
    }
}