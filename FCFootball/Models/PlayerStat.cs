using System;
using System.Collections.Generic;

namespace FCFootball.Models
{
    public partial class PlayerStat
    {
        public int PlayerStatID { get; set; }
        public int? PlayerID { get; set; }
        public int? GameID { get; set; }
        public short? PassAtt { get; set; }
        public short? PassComp { get; set; }
        public short? PassYds { get; set; }
        public short? PassTd { get; set; }
        public short? RushAtt { get; set; }
        public short? RushYds { get; set; }
        public short? RushTd { get; set; }
        public short? Receptions { get; set; }
        public short? RecYds { get; set; }
        public short? RecTd { get; set; }

        public virtual Game? Game { get; set; }
        public virtual Player? Player { get; set; }
    }
}
