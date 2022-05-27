using System;

// Token: 0x02000059 RID: 89
public class ChartsWeek
{
	// Token: 0x060002D7 RID: 727 RVA: 0x000037EF File Offset: 0x000019EF
	public ChartsWeek(int gameID_, int sells_, gameScript s_)
	{
		this.gameID = gameID_;
		this.sells = sells_;
		this.script_ = s_;
	}

	// Token: 0x040005AE RID: 1454
	public int gameID;

	// Token: 0x040005AF RID: 1455
	public int sells;

	// Token: 0x040005B0 RID: 1456
	public gameScript script_;
}
