using System;

// Token: 0x0200005A RID: 90
public class ChartsList
{
	// Token: 0x060002D8 RID: 728 RVA: 0x0000380C File Offset: 0x00001A0C
	public ChartsList(int gameID_, long wert_, gameScript s_)
	{
		this.gameID = gameID_;
		this.wert = wert_;
		this.script_ = s_;
	}

	// Token: 0x040005B1 RID: 1457
	public int gameID;

	// Token: 0x040005B2 RID: 1458
	public long wert;

	// Token: 0x040005B3 RID: 1459
	public gameScript script_;
}
