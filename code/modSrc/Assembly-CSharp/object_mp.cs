using System;

// Token: 0x020002C5 RID: 709
public class object_mp
{
	// Token: 0x06001984 RID: 6532 RVA: 0x000111B8 File Offset: 0x0000F3B8
	public object_mp(int id_, int typ_, float posX_, float posY_, float rotation_)
	{
		this.id = id_;
		this.typ = typ_;
		this.posX = posX_;
		this.posY = posY_;
		this.rotation = rotation_;
	}

	// Token: 0x040020A6 RID: 8358
	public int id;

	// Token: 0x040020A7 RID: 8359
	public int typ;

	// Token: 0x040020A8 RID: 8360
	public float posX;

	// Token: 0x040020A9 RID: 8361
	public float posY;

	// Token: 0x040020AA RID: 8362
	public float rotation;
}
