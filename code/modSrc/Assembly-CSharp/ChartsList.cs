using System;

// Token: 0x0200005A RID: 90
public class ChartsList
{
	// Token: 0x060002DD RID: 733 RVA: 0x0002CABD File Offset: 0x0002ACBD
	public ChartsList(int gameID_, long wert_, gameScript s_)
	{
		this.gameID = gameID_;
		this.wert = wert_;
		this.script_ = s_;
	}

	// Token: 0x040005AE RID: 1454
	public int gameID;

	// Token: 0x040005AF RID: 1455
	public long wert;

	// Token: 0x040005B0 RID: 1456
	public gameScript script_;
}
