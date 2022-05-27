using System;

// Token: 0x020002C8 RID: 712
public class object_mp
{
	// Token: 0x060019CD RID: 6605 RVA: 0x00107329 File Offset: 0x00105529
	public object_mp(int id_, int typ_, float posX_, float posY_, float rotation_)
	{
		this.id = id_;
		this.typ = typ_;
		this.posX = posX_;
		this.posY = posY_;
		this.rotation = rotation_;
	}

	// Token: 0x040020C5 RID: 8389
	public int id;

	// Token: 0x040020C6 RID: 8390
	public int typ;

	// Token: 0x040020C7 RID: 8391
	public float posX;

	// Token: 0x040020C8 RID: 8392
	public float posY;

	// Token: 0x040020C9 RID: 8393
	public float rotation;
}
