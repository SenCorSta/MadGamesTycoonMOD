using System;

// Token: 0x02000059 RID: 89
public class ChartsWeek
{
	// Token: 0x060002DC RID: 732 RVA: 0x0002CAA0 File Offset: 0x0002ACA0
	public ChartsWeek(int gameID_, int sells_, gameScript s_)
	{
		this.gameID = gameID_;
		this.sells = sells_;
		this.script_ = s_;
	}

	// Token: 0x040005AB RID: 1451
	public int gameID;

	// Token: 0x040005AC RID: 1452
	public int sells;

	// Token: 0x040005AD RID: 1453
	public gameScript script_;
}
