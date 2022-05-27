using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Mirror
{
	// Token: 0x02000405 RID: 1029
	[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
	public static class GeneratedNetworkCode
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x00172468 File Offset: 0x00170668
		public static ErrorMessage ErrorMessage(NetworkReader reader)
		{
			return new ErrorMessage
			{
				value = reader.ReadByte()
			};
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00018877 File Offset: 0x00016A77
		public static void ErrorMessage(NetworkWriter writer, ErrorMessage value)
		{
			writer.WriteByte(value.value);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00172490 File Offset: 0x00170690
		public static ReadyMessage ReadyMessage(NetworkReader reader)
		{
			return default(ReadyMessage);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00002098 File Offset: 0x00000298
		public static void ReadyMessage(NetworkWriter writer, ReadyMessage value)
		{
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x001724A8 File Offset: 0x001706A8
		public static NotReadyMessage NotReadyMessage(NetworkReader reader)
		{
			return default(NotReadyMessage);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00002098 File Offset: 0x00000298
		public static void NotReadyMessage(NetworkWriter writer, NotReadyMessage value)
		{
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x001724C0 File Offset: 0x001706C0
		public static AddPlayerMessage AddPlayerMessage(NetworkReader reader)
		{
			return default(AddPlayerMessage);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00002098 File Offset: 0x00000298
		public static void AddPlayerMessage(NetworkWriter writer, AddPlayerMessage value)
		{
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x001724D8 File Offset: 0x001706D8
		public static DisconnectMessage DisconnectMessage(NetworkReader reader)
		{
			return default(DisconnectMessage);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00002098 File Offset: 0x00000298
		public static void DisconnectMessage(NetworkWriter writer, DisconnectMessage value)
		{
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x001724F0 File Offset: 0x001706F0
		public static ConnectMessage ConnectMessage(NetworkReader reader)
		{
			return default(ConnectMessage);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00002098 File Offset: 0x00000298
		public static void ConnectMessage(NetworkWriter writer, ConnectMessage value)
		{
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00172508 File Offset: 0x00170708
		public static SceneMessage SceneMessage(NetworkReader reader)
		{
			return new SceneMessage
			{
				sceneName = reader.ReadString(),
				sceneOperation = GeneratedNetworkCode._Read_Mirror.SceneOperation(reader),
				customHandling = reader.ReadBoolean()
			};
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00018885 File Offset: 0x00016A85
		public static SceneOperation SceneOperation(NetworkReader reader)
		{
			return (SceneOperation)reader.ReadByte();
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0001888D File Offset: 0x00016A8D
		public static void SceneMessage(NetworkWriter writer, SceneMessage value)
		{
			writer.WriteString(value.sceneName);
			GeneratedNetworkCode._Write_Mirror.SceneOperation(writer, value.sceneOperation);
			writer.WriteBoolean(value.customHandling);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000188B3 File Offset: 0x00016AB3
		public static void SceneOperation(NetworkWriter writer, SceneOperation value)
		{
			writer.WriteByte((byte)value);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00172550 File Offset: 0x00170750
		public static CommandMessage CommandMessage(NetworkReader reader)
		{
			return new CommandMessage
			{
				netId = reader.ReadUInt32(),
				componentIndex = reader.ReadInt32(),
				functionHash = reader.ReadInt32(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000188BC File Offset: 0x00016ABC
		public static void CommandMessage(NetworkWriter writer, CommandMessage value)
		{
			writer.WriteUInt32(value.netId);
			writer.WriteInt32(value.componentIndex);
			writer.WriteInt32(value.functionHash);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x001725A4 File Offset: 0x001707A4
		public static RpcMessage RpcMessage(NetworkReader reader)
		{
			return new RpcMessage
			{
				netId = reader.ReadUInt32(),
				componentIndex = reader.ReadInt32(),
				functionHash = reader.ReadInt32(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000188EE File Offset: 0x00016AEE
		public static void RpcMessage(NetworkWriter writer, RpcMessage value)
		{
			writer.WriteUInt32(value.netId);
			writer.WriteInt32(value.componentIndex);
			writer.WriteInt32(value.functionHash);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x001725F8 File Offset: 0x001707F8
		public static SpawnMessage SpawnMessage(NetworkReader reader)
		{
			return new SpawnMessage
			{
				netId = reader.ReadUInt32(),
				isLocalPlayer = reader.ReadBoolean(),
				isOwner = reader.ReadBoolean(),
				sceneId = reader.ReadUInt64(),
				assetId = reader.ReadGuid(),
				position = reader.ReadVector3(),
				rotation = reader.ReadQuaternion(),
				scale = reader.ReadVector3(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00172698 File Offset: 0x00170898
		public static void SpawnMessage(NetworkWriter writer, SpawnMessage value)
		{
			writer.WriteUInt32(value.netId);
			writer.WriteBoolean(value.isLocalPlayer);
			writer.WriteBoolean(value.isOwner);
			writer.WriteUInt64(value.sceneId);
			writer.WriteGuid(value.assetId);
			writer.WriteVector3(value.position);
			writer.WriteQuaternion(value.rotation);
			writer.WriteVector3(value.scale);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00172714 File Offset: 0x00170914
		public static ObjectSpawnStartedMessage ObjectSpawnStartedMessage(NetworkReader reader)
		{
			return default(ObjectSpawnStartedMessage);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00002098 File Offset: 0x00000298
		public static void ObjectSpawnStartedMessage(NetworkWriter writer, ObjectSpawnStartedMessage value)
		{
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0017272C File Offset: 0x0017092C
		public static ObjectSpawnFinishedMessage ObjectSpawnFinishedMessage(NetworkReader reader)
		{
			return default(ObjectSpawnFinishedMessage);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00002098 File Offset: 0x00000298
		public static void ObjectSpawnFinishedMessage(NetworkWriter writer, ObjectSpawnFinishedMessage value)
		{
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00172744 File Offset: 0x00170944
		public static ObjectDestroyMessage ObjectDestroyMessage(NetworkReader reader)
		{
			return new ObjectDestroyMessage
			{
				netId = reader.ReadUInt32()
			};
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00018920 File Offset: 0x00016B20
		public static void ObjectDestroyMessage(NetworkWriter writer, ObjectDestroyMessage value)
		{
			writer.WriteUInt32(value.netId);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0017276C File Offset: 0x0017096C
		public static ObjectHideMessage ObjectHideMessage(NetworkReader reader)
		{
			return new ObjectHideMessage
			{
				netId = reader.ReadUInt32()
			};
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0001892E File Offset: 0x00016B2E
		public static void ObjectHideMessage(NetworkWriter writer, ObjectHideMessage value)
		{
			writer.WriteUInt32(value.netId);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00172794 File Offset: 0x00170994
		public static UpdateVarsMessage UpdateVarsMessage(NetworkReader reader)
		{
			return new UpdateVarsMessage
			{
				netId = reader.ReadUInt32(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0001893C File Offset: 0x00016B3C
		public static void UpdateVarsMessage(NetworkWriter writer, UpdateVarsMessage value)
		{
			writer.WriteUInt32(value.netId);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x001727CC File Offset: 0x001709CC
		public static NetworkPingMessage NetworkPingMessage(NetworkReader reader)
		{
			return new NetworkPingMessage
			{
				clientTime = reader.ReadDouble()
			};
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00018956 File Offset: 0x00016B56
		public static void NetworkPingMessage(NetworkWriter writer, NetworkPingMessage value)
		{
			writer.WriteDouble(value.clientTime);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x001727F4 File Offset: 0x001709F4
		public static NetworkPongMessage NetworkPongMessage(NetworkReader reader)
		{
			return new NetworkPongMessage
			{
				clientTime = reader.ReadDouble(),
				serverTime = reader.ReadDouble()
			};
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00018964 File Offset: 0x00016B64
		public static void NetworkPongMessage(NetworkWriter writer, NetworkPongMessage value)
		{
			writer.WriteDouble(value.clientTime);
			writer.WriteDouble(value.serverTime);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0017282C File Offset: 0x00170A2C
		public static mpCalls.c_Forschung _Read_mpCalls/c_Forschung(NetworkReader reader)
		{
			return new mpCalls.c_Forschung
			{
				playerID = reader.ReadInt32(),
				forschungSonstiges = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres = GeneratedNetworkCode._Read_System.Boolean[](reader),
				themes = GeneratedNetworkCode._Read_System.Boolean[](reader),
				engineFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardware = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardwareFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader)
			};
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0001897E File Offset: 0x00016B7E
		public static bool[] Boolean[](NetworkReader reader)
		{
			return reader.ReadArray<bool>();
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x001728BC File Offset: 0x00170ABC
		public static void _Write_mpCalls/c_Forschung(NetworkWriter writer, mpCalls.c_Forschung value)
		{
			writer.WriteInt32(value.playerID);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.forschungSonstiges);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.themes);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.engineFeatures);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardware);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardwareFeatures);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00018986 File Offset: 0x00016B86
		public static void Boolean[](NetworkWriter writer, bool[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0017292C File Offset: 0x00170B2C
		public static mpCalls.c_ChangeID _Read_mpCalls/c_ChangeID(NetworkReader reader)
		{
			return new mpCalls.c_ChangeID
			{
				playerID = reader.ReadInt32(),
				newID = reader.ReadInt32()
			};
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0001898F File Offset: 0x00016B8F
		public static void _Write_mpCalls/c_ChangeID(NetworkWriter writer, mpCalls.c_ChangeID value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.newID);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00172964 File Offset: 0x00170B64
		public static mpCalls.c_AllAwards _Read_mpCalls/c_AllAwards(NetworkReader reader)
		{
			return new mpCalls.c_AllAwards
			{
				playerID = reader.ReadInt32(),
				awards = GeneratedNetworkCode._Read_System.Int32[](reader)
			};
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000189A9 File Offset: 0x00016BA9
		public static int[] Int32[](NetworkReader reader)
		{
			return reader.ReadArray<int>();
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000189B1 File Offset: 0x00016BB1
		public static void _Write_mpCalls/c_AllAwards(NetworkWriter writer, mpCalls.c_AllAwards value)
		{
			writer.WriteInt32(value.playerID);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.awards);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000189CB File Offset: 0x00016BCB
		public static void Int32[](NetworkWriter writer, int[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0017299C File Offset: 0x00170B9C
		public static mpCalls.c_Help _Read_mpCalls/c_Help(NetworkReader reader)
		{
			return new mpCalls.c_Help
			{
				playerID = reader.ReadInt32(),
				toPlayerID = reader.ReadInt32(),
				what = reader.ReadInt32(),
				valueA = reader.ReadInt32(),
				valueB = reader.ReadInt32(),
				valueC = reader.ReadInt32()
			};
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00172A10 File Offset: 0x00170C10
		public static void _Write_mpCalls/c_Help(NetworkWriter writer, mpCalls.c_Help value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.toPlayerID);
			writer.WriteInt32(value.what);
			writer.WriteInt32(value.valueA);
			writer.WriteInt32(value.valueB);
			writer.WriteInt32(value.valueC);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00172A68 File Offset: 0x00170C68
		public static mpCalls.c_ObjectDelete _Read_mpCalls/c_ObjectDelete(NetworkReader reader)
		{
			return new mpCalls.c_ObjectDelete
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32()
			};
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000189D4 File Offset: 0x00016BD4
		public static void _Write_mpCalls/c_ObjectDelete(NetworkWriter writer, mpCalls.c_ObjectDelete value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00172AA0 File Offset: 0x00170CA0
		public static mpCalls.c_Object _Read_mpCalls/c_Object(NetworkReader reader)
		{
			return new mpCalls.c_Object
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				x = reader.ReadSingle(),
				y = reader.ReadSingle(),
				rot = reader.ReadSingle()
			};
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00172B14 File Offset: 0x00170D14
		public static void _Write_mpCalls/c_Object(NetworkWriter writer, mpCalls.c_Object value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
			writer.WriteInt32(value.typ);
			writer.WriteSingle(value.x);
			writer.WriteSingle(value.y);
			writer.WriteSingle(value.rot);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00172B6C File Offset: 0x00170D6C
		public static mpCalls.c_Map _Read_mpCalls/c_Map(NetworkReader reader)
		{
			return new mpCalls.c_Map
			{
				playerID = reader.ReadInt32(),
				x = reader.ReadByte(),
				y = reader.ReadByte(),
				id = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				door = reader.ReadInt32(),
				window = reader.ReadByte()
			};
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00172BF0 File Offset: 0x00170DF0
		public static void _Write_mpCalls/c_Map(NetworkWriter writer, mpCalls.c_Map value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteByte(value.x);
			writer.WriteByte(value.y);
			writer.WriteInt32(value.id);
			writer.WriteInt32(value.typ);
			writer.WriteInt32(value.door);
			writer.WriteByte(value.window);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00172C54 File Offset: 0x00170E54
		public static mpCalls.c_Trend _Read_mpCalls/c_Trend(NetworkReader reader)
		{
			return new mpCalls.c_Trend
			{
				trendWeeks = reader.ReadInt32(),
				trendTheme = reader.ReadInt32(),
				trendAntiTheme = reader.ReadInt32(),
				trendGenre = reader.ReadInt32(),
				trendAntiGenre = reader.ReadInt32()
			};
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000189EE File Offset: 0x00016BEE
		public static void _Write_mpCalls/c_Trend(NetworkWriter writer, mpCalls.c_Trend value)
		{
			writer.WriteInt32(value.trendWeeks);
			writer.WriteInt32(value.trendTheme);
			writer.WriteInt32(value.trendAntiTheme);
			writer.WriteInt32(value.trendGenre);
			writer.WriteInt32(value.trendAntiGenre);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00172CB8 File Offset: 0x00170EB8
		public static mpCalls.c_Payment _Read_mpCalls/c_Payment(NetworkReader reader)
		{
			return new mpCalls.c_Payment
			{
				playerID = reader.ReadInt32(),
				toPlayerID = reader.ReadInt32(),
				what = reader.ReadInt32(),
				money = reader.ReadInt32()
			};
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00018A2C File Offset: 0x00016C2C
		public static void _Write_mpCalls/c_Payment(NetworkWriter writer, mpCalls.c_Payment value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.toPlayerID);
			writer.WriteInt32(value.what);
			writer.WriteInt32(value.money);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00172D0C File Offset: 0x00170F0C
		public static mpCalls.c_Engine _Read_mpCalls/c_Engine(NetworkReader reader)
		{
			return new mpCalls.c_Engine
			{
				myID = reader.ReadInt32(),
				playerEngine = reader.ReadBoolean(),
				multiplayerSlot = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				gekauft = reader.ReadBoolean(),
				myName = reader.ReadString(),
				features = GeneratedNetworkCode._Read_System.Boolean[](reader),
				spezialgenre = reader.ReadInt32(),
				spezialplatform = reader.ReadInt32(),
				sellEngine = reader.ReadBoolean(),
				preis = reader.ReadInt32(),
				gewinnbeteiligung = reader.ReadInt32()
			};
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00172DD8 File Offset: 0x00170FD8
		public static void _Write_mpCalls/c_Engine(NetworkWriter writer, mpCalls.c_Engine value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteBoolean(value.playerEngine);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteBoolean(value.gekauft);
			writer.WriteString(value.myName);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.features);
			writer.WriteInt32(value.spezialgenre);
			writer.WriteInt32(value.spezialplatform);
			writer.WriteBoolean(value.sellEngine);
			writer.WriteInt32(value.preis);
			writer.WriteInt32(value.gewinnbeteiligung);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00172E78 File Offset: 0x00171078
		public static mpCalls.c_Platform _Read_mpCalls/c_Platform(NetworkReader reader)
		{
			return new mpCalls.c_Platform
			{
				myID = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				date_year_end = reader.ReadInt32(),
				date_month_end = reader.ReadInt32(),
				npc = reader.ReadBoolean(),
				price = reader.ReadInt32(),
				dev_costs = reader.ReadInt32(),
				tech = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				marktanteil = reader.ReadSingle(),
				needFeatures = GeneratedNetworkCode._Read_System.Int32[](reader),
				units = reader.ReadInt32(),
				units_max = reader.ReadInt32(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_HU = reader.ReadString(),
				name_JA = reader.ReadString(),
				manufacturer_EN = reader.ReadString(),
				manufacturer_GE = reader.ReadString(),
				manufacturer_TU = reader.ReadString(),
				manufacturer_CH = reader.ReadString(),
				manufacturer_FR = reader.ReadString(),
				manufacturer_HU = reader.ReadString(),
				manufacturer_JA = reader.ReadString(),
				pic1_file = reader.ReadString(),
				pic2_file = reader.ReadString(),
				pic2_year = reader.ReadInt32(),
				games = reader.ReadInt32(),
				exklusivGames = reader.ReadInt32(),
				erfahrung = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				inBesitz = reader.ReadBoolean(),
				vomMarktGenommen = reader.ReadBoolean(),
				complex = reader.ReadInt32(),
				internet = reader.ReadBoolean(),
				powerFromMarket = reader.ReadSingle(),
				myName = reader.ReadString(),
				playerConsole = reader.ReadBoolean(),
				multiplaySlot = reader.ReadInt32(),
				gameID = reader.ReadInt32(),
				anzController = reader.ReadInt32(),
				conHueShift = reader.ReadSingle(),
				conSaturation = reader.ReadSingle(),
				component_cpu = reader.ReadInt32(),
				component_gfx = reader.ReadInt32(),
				component_ram = reader.ReadInt32(),
				component_hdd = reader.ReadInt32(),
				component_sfx = reader.ReadInt32(),
				component_cooling = reader.ReadInt32(),
				component_disc = reader.ReadInt32(),
				component_controller = reader.ReadInt32(),
				component_case = reader.ReadInt32(),
				component_monitor = reader.ReadInt32(),
				hwFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				devPoints = reader.ReadSingle(),
				devPointsStart = reader.ReadSingle(),
				entwicklungsKosten = reader.ReadInt64(),
				einnahmen = reader.ReadInt64(),
				hype = reader.ReadSingle(),
				costs_marketing = reader.ReadInt32(),
				costs_mitarbeiter = reader.ReadInt32(),
				startProduktionskosten = reader.ReadInt32(),
				verkaufspreis = reader.ReadInt32(),
				kostenreduktion = reader.ReadSingle(),
				autoPreis = reader.ReadBoolean(),
				thridPartyGames = reader.ReadBoolean(),
				umsatzTotal = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				weeksOnMarket = reader.ReadInt32(),
				review = reader.ReadSingle(),
				performancePoints = reader.ReadInt32()
			};
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00173304 File Offset: 0x00171504
		public static void _Write_mpCalls/c_Platform(NetworkWriter writer, mpCalls.c_Platform value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.date_year_end);
			writer.WriteInt32(value.date_month_end);
			writer.WriteBoolean(value.npc);
			writer.WriteInt32(value.price);
			writer.WriteInt32(value.dev_costs);
			writer.WriteInt32(value.tech);
			writer.WriteInt32(value.typ);
			writer.WriteSingle(value.marktanteil);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.needFeatures);
			writer.WriteInt32(value.units);
			writer.WriteInt32(value.units_max);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_HU);
			writer.WriteString(value.name_JA);
			writer.WriteString(value.manufacturer_EN);
			writer.WriteString(value.manufacturer_GE);
			writer.WriteString(value.manufacturer_TU);
			writer.WriteString(value.manufacturer_CH);
			writer.WriteString(value.manufacturer_FR);
			writer.WriteString(value.manufacturer_HU);
			writer.WriteString(value.manufacturer_JA);
			writer.WriteString(value.pic1_file);
			writer.WriteString(value.pic2_file);
			writer.WriteInt32(value.pic2_year);
			writer.WriteInt32(value.games);
			writer.WriteInt32(value.exklusivGames);
			writer.WriteInt32(value.erfahrung);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteBoolean(value.inBesitz);
			writer.WriteBoolean(value.vomMarktGenommen);
			writer.WriteInt32(value.complex);
			writer.WriteBoolean(value.internet);
			writer.WriteSingle(value.powerFromMarket);
			writer.WriteString(value.myName);
			writer.WriteBoolean(value.playerConsole);
			writer.WriteInt32(value.multiplaySlot);
			writer.WriteInt32(value.gameID);
			writer.WriteInt32(value.anzController);
			writer.WriteSingle(value.conHueShift);
			writer.WriteSingle(value.conSaturation);
			writer.WriteInt32(value.component_cpu);
			writer.WriteInt32(value.component_gfx);
			writer.WriteInt32(value.component_ram);
			writer.WriteInt32(value.component_hdd);
			writer.WriteInt32(value.component_sfx);
			writer.WriteInt32(value.component_cooling);
			writer.WriteInt32(value.component_disc);
			writer.WriteInt32(value.component_controller);
			writer.WriteInt32(value.component_case);
			writer.WriteInt32(value.component_monitor);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hwFeatures);
			writer.WriteSingle(value.devPoints);
			writer.WriteSingle(value.devPointsStart);
			writer.WriteInt64(value.entwicklungsKosten);
			writer.WriteInt64(value.einnahmen);
			writer.WriteSingle(value.hype);
			writer.WriteInt32(value.costs_marketing);
			writer.WriteInt32(value.costs_mitarbeiter);
			writer.WriteInt32(value.startProduktionskosten);
			writer.WriteInt32(value.verkaufspreis);
			writer.WriteSingle(value.kostenreduktion);
			writer.WriteBoolean(value.autoPreis);
			writer.WriteBoolean(value.thridPartyGames);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteInt64(value.costs_production);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteSingle(value.review);
			writer.WriteInt32(value.performancePoints);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x001736A4 File Offset: 0x001718A4
		public static mpCalls.c_Chat _Read_mpCalls/c_Chat(NetworkReader reader)
		{
			return new mpCalls.c_Chat
			{
				playerID = reader.ReadInt32(),
				text = reader.ReadString()
			};
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00018A5E File Offset: 0x00016C5E
		public static void _Write_mpCalls/c_Chat(NetworkWriter writer, mpCalls.c_Chat value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteString(value.text);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x001736DC File Offset: 0x001718DC
		public static mpCalls.c_Command _Read_mpCalls/c_Command(NetworkReader reader)
		{
			return new mpCalls.c_Command
			{
				playerID = reader.ReadInt32(),
				command = reader.ReadInt32()
			};
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00018A78 File Offset: 0x00016C78
		public static void _Write_mpCalls/c_Command(NetworkWriter writer, mpCalls.c_Command value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.command);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00173714 File Offset: 0x00171914
		public static mpCalls.c_Money _Read_mpCalls/c_Money(NetworkReader reader)
		{
			return new mpCalls.c_Money
			{
				playerID = reader.ReadInt32(),
				money = reader.ReadInt64(),
				fans = reader.ReadInt32()
			};
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00018A92 File Offset: 0x00016C92
		public static void _Write_mpCalls/c_Money(NetworkWriter writer, mpCalls.c_Money value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt64(value.money);
			writer.WriteInt32(value.fans);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0017375C File Offset: 0x0017195C
		public static mpCalls.c_PlayerInfos _Read_mpCalls/c_PlayerInfos(NetworkReader reader)
		{
			return new mpCalls.c_PlayerInfos
			{
				playerID = reader.ReadInt32(),
				playerName = reader.ReadString(),
				companyName = reader.ReadString(),
				logo = reader.ReadInt32(),
				country = reader.ReadInt32(),
				ready = reader.ReadBoolean()
			};
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x001737D0 File Offset: 0x001719D0
		public static void _Write_mpCalls/c_PlayerInfos(NetworkWriter writer, mpCalls.c_PlayerInfos value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteString(value.playerName);
			writer.WriteString(value.companyName);
			writer.WriteInt32(value.logo);
			writer.WriteInt32(value.country);
			writer.WriteBoolean(value.ready);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00173828 File Offset: 0x00171A28
		public static mpCalls.c_DeleteArbeitsmarkt _Read_mpCalls/c_DeleteArbeitsmarkt(NetworkReader reader)
		{
			return new mpCalls.c_DeleteArbeitsmarkt
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32(),
				eingestellt = reader.ReadBoolean()
			};
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public static void _Write_mpCalls/c_DeleteArbeitsmarkt(NetworkWriter writer, mpCalls.c_DeleteArbeitsmarkt value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
			writer.WriteBoolean(value.eingestellt);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00173870 File Offset: 0x00171A70
		public static mpCalls.c_BuyLizenz _Read_mpCalls/c_BuyLizenz(NetworkReader reader)
		{
			return new mpCalls.c_BuyLizenz
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32()
			};
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00018ADE File Offset: 0x00016CDE
		public static void _Write_mpCalls/c_BuyLizenz(NetworkWriter writer, mpCalls.c_BuyLizenz value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x001738A8 File Offset: 0x00171AA8
		public static mpCalls.c_exklusivKonsolenSells _Read_mpCalls/c_exklusivKonsolenSells(NetworkReader reader)
		{
			return new mpCalls.c_exklusivKonsolenSells
			{
				gameID = reader.ReadInt32(),
				exklusivKonsolenSells = reader.ReadInt64()
			};
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public static void _Write_mpCalls/c_exklusivKonsolenSells(NetworkWriter writer, mpCalls.c_exklusivKonsolenSells value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteInt64(value.exklusivKonsolenSells);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x001738E0 File Offset: 0x00171AE0
		public static mpCalls.c_GameData _Read_mpCalls/c_GameData(NetworkReader reader)
		{
			return new mpCalls.c_GameData
			{
				gameID = reader.ReadInt32(),
				sellsTotal = reader.ReadInt64(),
				umsatzTotal = reader.ReadInt64(),
				isOnMarket = reader.ReadBoolean(),
				weeksOnMarket = reader.ReadInt32(),
				userPositiv = reader.ReadInt32(),
				userNegativ = reader.ReadInt32(),
				costs_entwicklung = reader.ReadInt64(),
				costs_mitarbeiter = reader.ReadInt64(),
				costs_marketing = reader.ReadInt64(),
				costs_enginegebuehren = reader.ReadInt64(),
				costs_server = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				costs_updates = reader.ReadInt64(),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				abonnements = reader.ReadInt32(),
				abonnementsWoche = reader.ReadInt32(),
				bestAbonnements = reader.ReadInt32(),
				bestChartPosition = reader.ReadInt32(),
				exklusivKonsolenSells = reader.ReadInt64(),
				multiplayerSlot = reader.ReadInt32(),
				ipPunkte = reader.ReadSingle(),
				pubAngebot = reader.ReadBoolean(),
				pubAngebot_Weeks = reader.ReadInt32(),
				pubAngebot_Verhandlung = reader.ReadSingle(),
				pubAngebot_Retail = reader.ReadBoolean(),
				pubAngebot_Digital = reader.ReadBoolean(),
				pubAngebot_Garantiesumme = reader.ReadInt32(),
				pubAngebot_Gewinnbeteiligung = reader.ReadSingle(),
				auftragsspiel = reader.ReadBoolean(),
				auftragsspiel_gehalt = reader.ReadInt32(),
				auftragsspiel_bonus = reader.ReadInt32(),
				auftragsspiel_zeitInWochen = reader.ReadInt32(),
				auftragsspiel_wochenAlsAngebot = reader.ReadInt32(),
				auftragsspiel_zeitAbgelaufen = reader.ReadBoolean(),
				auftragsspiel_mindestbewertung = reader.ReadInt32(),
				ipName = reader.ReadString()
			};
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00173B24 File Offset: 0x00171D24
		public static void _Write_mpCalls/c_GameData(NetworkWriter writer, mpCalls.c_GameData value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteInt64(value.sellsTotal);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteBoolean(value.isOnMarket);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteInt32(value.userPositiv);
			writer.WriteInt32(value.userNegativ);
			writer.WriteInt64(value.costs_entwicklung);
			writer.WriteInt64(value.costs_mitarbeiter);
			writer.WriteInt64(value.costs_marketing);
			writer.WriteInt64(value.costs_enginegebuehren);
			writer.WriteInt64(value.costs_server);
			writer.WriteInt64(value.costs_production);
			writer.WriteInt64(value.costs_updates);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			writer.WriteInt32(value.abonnements);
			writer.WriteInt32(value.abonnementsWoche);
			writer.WriteInt32(value.bestAbonnements);
			writer.WriteInt32(value.bestChartPosition);
			writer.WriteInt64(value.exklusivKonsolenSells);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteSingle(value.ipPunkte);
			writer.WriteBoolean(value.pubAngebot);
			writer.WriteInt32(value.pubAngebot_Weeks);
			writer.WriteSingle(value.pubAngebot_Verhandlung);
			writer.WriteBoolean(value.pubAngebot_Retail);
			writer.WriteBoolean(value.pubAngebot_Digital);
			writer.WriteInt32(value.pubAngebot_Garantiesumme);
			writer.WriteSingle(value.pubAngebot_Gewinnbeteiligung);
			writer.WriteBoolean(value.auftragsspiel);
			writer.WriteInt32(value.auftragsspiel_gehalt);
			writer.WriteInt32(value.auftragsspiel_bonus);
			writer.WriteInt32(value.auftragsspiel_zeitInWochen);
			writer.WriteInt32(value.auftragsspiel_wochenAlsAngebot);
			writer.WriteBoolean(value.auftragsspiel_zeitAbgelaufen);
			writer.WriteInt32(value.auftragsspiel_mindestbewertung);
			writer.WriteString(value.ipName);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00173CF0 File Offset: 0x00171EF0
		public static mpCalls.c_Game _Read_mpCalls/c_Game(NetworkReader reader)
		{
			return new mpCalls.c_Game
			{
				gameID = reader.ReadInt32(),
				myName = reader.ReadString(),
				ipName = reader.ReadString(),
				playerGame = reader.ReadBoolean(),
				multiplayerSlot = reader.ReadInt32(),
				inDevelopment = reader.ReadBoolean(),
				developerID = reader.ReadInt32(),
				publisherID = reader.ReadInt32(),
				ownerID = reader.ReadInt32(),
				engineID = reader.ReadInt32(),
				hype = reader.ReadSingle(),
				isOnMarket = reader.ReadBoolean(),
				warBeiAwards = reader.ReadBoolean(),
				weeksOnMarket = reader.ReadInt32(),
				usk = reader.ReadInt32(),
				freigabeBudget = reader.ReadInt32(),
				reviewGameplay = reader.ReadInt32(),
				reviewGrafik = reader.ReadInt32(),
				reviewSound = reader.ReadInt32(),
				reviewSteuerung = reader.ReadInt32(),
				reviewTotal = reader.ReadInt32(),
				reviewGameplayText = reader.ReadInt32(),
				reviewGrafikText = reader.ReadInt32(),
				reviewSoundText = reader.ReadInt32(),
				reviewSteuerungText = reader.ReadInt32(),
				reviewTotalText = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				date_start_year = reader.ReadInt32(),
				date_start_month = reader.ReadInt32(),
				sellsTotal = reader.ReadInt64(),
				umsatzTotal = reader.ReadInt64(),
				costs_entwicklung = reader.ReadInt64(),
				costs_mitarbeiter = reader.ReadInt64(),
				costs_marketing = reader.ReadInt64(),
				costs_enginegebuehren = reader.ReadInt64(),
				costs_server = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				costs_updates = reader.ReadInt64(),
				typ_standard = reader.ReadBoolean(),
				typ_nachfolger = reader.ReadBoolean(),
				originalIP = reader.ReadInt32(),
				teile = reader.ReadInt32(),
				typ_contractGame = reader.ReadBoolean(),
				typ_remaster = reader.ReadBoolean(),
				typ_spinoff = reader.ReadBoolean(),
				typ_addon = reader.ReadBoolean(),
				typ_addonStandalone = reader.ReadBoolean(),
				typ_mmoaddon = reader.ReadBoolean(),
				typ_bundle = reader.ReadBoolean(),
				typ_budget = reader.ReadBoolean(),
				typ_bundleAddon = reader.ReadBoolean(),
				typ_goty = reader.ReadBoolean(),
				originalGameID = reader.ReadInt32(),
				portID = reader.ReadInt32(),
				mainIP = reader.ReadInt32(),
				ipPunkte = reader.ReadSingle(),
				exklusiv = reader.ReadBoolean(),
				herstellerExklusiv = reader.ReadBoolean(),
				retro = reader.ReadBoolean(),
				handy = reader.ReadBoolean(),
				arcade = reader.ReadBoolean(),
				goty = reader.ReadBoolean(),
				nachfolger_created = reader.ReadBoolean(),
				remaster_created = reader.ReadBoolean(),
				budget_created = reader.ReadBoolean(),
				goty_created = reader.ReadBoolean(),
				trendsetter = reader.ReadBoolean(),
				spielbericht = reader.ReadBoolean(),
				amountUpdates = reader.ReadInt32(),
				bonusSellsUpdates = reader.ReadSingle(),
				amountAddons = reader.ReadInt32(),
				bonusSellsAddons = reader.ReadSingle(),
				amountMMOAddons = reader.ReadInt32(),
				bonusSellsMMOAddons = reader.ReadSingle(),
				addonQuality = reader.ReadSingle(),
				devAktFeature = reader.ReadInt32(),
				devPoints = reader.ReadSingle(),
				devPointsStart = reader.ReadSingle(),
				devPoints_Gesamt = reader.ReadSingle(),
				devPointsStart_Gesamt = reader.ReadSingle(),
				points_gameplay = reader.ReadSingle(),
				points_grafik = reader.ReadSingle(),
				points_sound = reader.ReadSingle(),
				points_technik = reader.ReadSingle(),
				points_bugs = reader.ReadSingle(),
				beschreibung = reader.ReadString(),
				gameTyp = reader.ReadInt32(),
				gameSize = reader.ReadInt32(),
				gameZielgruppe = reader.ReadInt32(),
				maingenre = reader.ReadInt32(),
				subgenre = reader.ReadInt32(),
				gameMainTheme = reader.ReadInt32(),
				gameSubTheme = reader.ReadInt32(),
				gameLicence = reader.ReadInt32(),
				gameCopyProtect = reader.ReadInt32(),
				gameAntiCheat = reader.ReadInt32(),
				gameAP_Gameplay = reader.ReadInt32(),
				gameAP_Grafik = reader.ReadInt32(),
				gameAP_Sound = reader.ReadInt32(),
				gameAP_Technik = reader.ReadInt32(),
				gameLanguage = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameGameplayFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gamePlatform = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameEngineFeature = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_DevDone = GeneratedNetworkCode._Read_System.Boolean[](reader),
				engineFeature_DevDone = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				grafikStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				soundStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				motionCaptureStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				bundleID = GeneratedNetworkCode._Read_System.Int32[](reader),
				portExist = GeneratedNetworkCode._Read_System.Boolean[](reader),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				verkaufspreis = GeneratedNetworkCode._Read_System.Int32[](reader),
				releaseDate = reader.ReadInt32(),
				abonnements = reader.ReadInt32(),
				abonnementsWoche = reader.ReadInt32(),
				aboPreis = reader.ReadInt32(),
				pubOffer = reader.ReadBoolean(),
				pubAngebot = reader.ReadBoolean(),
				pubAngebot_Weeks = reader.ReadInt32(),
				pubAngebot_Verhandlung = reader.ReadSingle(),
				pubAngebot_Retail = reader.ReadBoolean(),
				pubAngebot_Digital = reader.ReadBoolean(),
				pubAngebot_Garantiesumme = reader.ReadInt32(),
				pubAngebot_Gewinnbeteiligung = reader.ReadSingle(),
				auftragsspiel = reader.ReadBoolean(),
				auftragsspiel_gehalt = reader.ReadInt32(),
				auftragsspiel_bonus = reader.ReadInt32(),
				auftragsspiel_zeitInWochen = reader.ReadInt32(),
				auftragsspiel_wochenAlsAngebot = reader.ReadInt32(),
				auftragsspiel_zeitAbgelaufen = reader.ReadBoolean(),
				auftragsspiel_mindestbewertung = reader.ReadInt32(),
				f2pConverted = reader.ReadBoolean()
			};
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x001744F4 File Offset: 0x001726F4
		public static void _Write_mpCalls/c_Game(NetworkWriter writer, mpCalls.c_Game value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteString(value.myName);
			writer.WriteString(value.ipName);
			writer.WriteBoolean(value.playerGame);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteBoolean(value.inDevelopment);
			writer.WriteInt32(value.developerID);
			writer.WriteInt32(value.publisherID);
			writer.WriteInt32(value.ownerID);
			writer.WriteInt32(value.engineID);
			writer.WriteSingle(value.hype);
			writer.WriteBoolean(value.isOnMarket);
			writer.WriteBoolean(value.warBeiAwards);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteInt32(value.usk);
			writer.WriteInt32(value.freigabeBudget);
			writer.WriteInt32(value.reviewGameplay);
			writer.WriteInt32(value.reviewGrafik);
			writer.WriteInt32(value.reviewSound);
			writer.WriteInt32(value.reviewSteuerung);
			writer.WriteInt32(value.reviewTotal);
			writer.WriteInt32(value.reviewGameplayText);
			writer.WriteInt32(value.reviewGrafikText);
			writer.WriteInt32(value.reviewSoundText);
			writer.WriteInt32(value.reviewSteuerungText);
			writer.WriteInt32(value.reviewTotalText);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.date_start_year);
			writer.WriteInt32(value.date_start_month);
			writer.WriteInt64(value.sellsTotal);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteInt64(value.costs_entwicklung);
			writer.WriteInt64(value.costs_mitarbeiter);
			writer.WriteInt64(value.costs_marketing);
			writer.WriteInt64(value.costs_enginegebuehren);
			writer.WriteInt64(value.costs_server);
			writer.WriteInt64(value.costs_production);
			writer.WriteInt64(value.costs_updates);
			writer.WriteBoolean(value.typ_standard);
			writer.WriteBoolean(value.typ_nachfolger);
			writer.WriteInt32(value.originalIP);
			writer.WriteInt32(value.teile);
			writer.WriteBoolean(value.typ_contractGame);
			writer.WriteBoolean(value.typ_remaster);
			writer.WriteBoolean(value.typ_spinoff);
			writer.WriteBoolean(value.typ_addon);
			writer.WriteBoolean(value.typ_addonStandalone);
			writer.WriteBoolean(value.typ_mmoaddon);
			writer.WriteBoolean(value.typ_bundle);
			writer.WriteBoolean(value.typ_budget);
			writer.WriteBoolean(value.typ_bundleAddon);
			writer.WriteBoolean(value.typ_goty);
			writer.WriteInt32(value.originalGameID);
			writer.WriteInt32(value.portID);
			writer.WriteInt32(value.mainIP);
			writer.WriteSingle(value.ipPunkte);
			writer.WriteBoolean(value.exklusiv);
			writer.WriteBoolean(value.herstellerExklusiv);
			writer.WriteBoolean(value.retro);
			writer.WriteBoolean(value.handy);
			writer.WriteBoolean(value.arcade);
			writer.WriteBoolean(value.goty);
			writer.WriteBoolean(value.nachfolger_created);
			writer.WriteBoolean(value.remaster_created);
			writer.WriteBoolean(value.budget_created);
			writer.WriteBoolean(value.goty_created);
			writer.WriteBoolean(value.trendsetter);
			writer.WriteBoolean(value.spielbericht);
			writer.WriteInt32(value.amountUpdates);
			writer.WriteSingle(value.bonusSellsUpdates);
			writer.WriteInt32(value.amountAddons);
			writer.WriteSingle(value.bonusSellsAddons);
			writer.WriteInt32(value.amountMMOAddons);
			writer.WriteSingle(value.bonusSellsMMOAddons);
			writer.WriteSingle(value.addonQuality);
			writer.WriteInt32(value.devAktFeature);
			writer.WriteSingle(value.devPoints);
			writer.WriteSingle(value.devPointsStart);
			writer.WriteSingle(value.devPoints_Gesamt);
			writer.WriteSingle(value.devPointsStart_Gesamt);
			writer.WriteSingle(value.points_gameplay);
			writer.WriteSingle(value.points_grafik);
			writer.WriteSingle(value.points_sound);
			writer.WriteSingle(value.points_technik);
			writer.WriteSingle(value.points_bugs);
			writer.WriteString(value.beschreibung);
			writer.WriteInt32(value.gameTyp);
			writer.WriteInt32(value.gameSize);
			writer.WriteInt32(value.gameZielgruppe);
			writer.WriteInt32(value.maingenre);
			writer.WriteInt32(value.subgenre);
			writer.WriteInt32(value.gameMainTheme);
			writer.WriteInt32(value.gameSubTheme);
			writer.WriteInt32(value.gameLicence);
			writer.WriteInt32(value.gameCopyProtect);
			writer.WriteInt32(value.gameAntiCheat);
			writer.WriteInt32(value.gameAP_Gameplay);
			writer.WriteInt32(value.gameAP_Grafik);
			writer.WriteInt32(value.gameAP_Sound);
			writer.WriteInt32(value.gameAP_Technik);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameLanguage);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameGameplayFeatures);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gamePlatform);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameEngineFeature);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_DevDone);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.engineFeature_DevDone);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.grafikStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.soundStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.motionCaptureStudio);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.bundleID);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.portExist);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.verkaufspreis);
			writer.WriteInt32(value.releaseDate);
			writer.WriteInt32(value.abonnements);
			writer.WriteInt32(value.abonnementsWoche);
			writer.WriteInt32(value.aboPreis);
			writer.WriteBoolean(value.pubOffer);
			writer.WriteBoolean(value.pubAngebot);
			writer.WriteInt32(value.pubAngebot_Weeks);
			writer.WriteSingle(value.pubAngebot_Verhandlung);
			writer.WriteBoolean(value.pubAngebot_Retail);
			writer.WriteBoolean(value.pubAngebot_Digital);
			writer.WriteInt32(value.pubAngebot_Garantiesumme);
			writer.WriteSingle(value.pubAngebot_Gewinnbeteiligung);
			writer.WriteBoolean(value.auftragsspiel);
			writer.WriteInt32(value.auftragsspiel_gehalt);
			writer.WriteInt32(value.auftragsspiel_bonus);
			writer.WriteInt32(value.auftragsspiel_zeitInWochen);
			writer.WriteInt32(value.auftragsspiel_wochenAlsAngebot);
			writer.WriteBoolean(value.auftragsspiel_zeitAbgelaufen);
			writer.WriteInt32(value.auftragsspiel_mindestbewertung);
			writer.WriteBoolean(value.f2pConverted);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00174B58 File Offset: 0x00172D58
		public static mpCalls.s_AddPlayer _Read_mpCalls/s_AddPlayer(NetworkReader reader)
		{
			return new mpCalls.s_AddPlayer
			{
				playerID = reader.ReadInt32()
			};
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00018B12 File Offset: 0x00016D12
		public static void _Write_mpCalls/s_AddPlayer(NetworkWriter writer, mpCalls.s_AddPlayer value)
		{
			writer.WriteInt32(value.playerID);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00174B80 File Offset: 0x00172D80
		public static mpCalls.s_ChangeID _Read_mpCalls/s_ChangeID(NetworkReader reader)
		{
			return new mpCalls.s_ChangeID
			{
				playerID = reader.ReadInt32(),
				newID = reader.ReadInt32()
			};
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00018B20 File Offset: 0x00016D20
		public static void _Write_mpCalls/s_ChangeID(NetworkWriter writer, mpCalls.s_ChangeID value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.newID);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00174BB8 File Offset: 0x00172DB8
		public static mpCalls.s_Forschung _Read_mpCalls/s_Forschung(NetworkReader reader)
		{
			return new mpCalls.s_Forschung
			{
				playerID = reader.ReadInt32(),
				forschungSonstiges = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres = GeneratedNetworkCode._Read_System.Boolean[](reader),
				themes = GeneratedNetworkCode._Read_System.Boolean[](reader),
				engineFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardware = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardwareFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader)
			};
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00174C48 File Offset: 0x00172E48
		public static void _Write_mpCalls/s_Forschung(NetworkWriter writer, mpCalls.s_Forschung value)
		{
			writer.WriteInt32(value.playerID);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.forschungSonstiges);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.themes);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.engineFeatures);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardware);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardwareFeatures);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00174CB8 File Offset: 0x00172EB8
		public static mpCalls.s_PlayerLeave _Read_mpCalls/s_PlayerLeave(NetworkReader reader)
		{
			return new mpCalls.s_PlayerLeave
			{
				playerID = reader.ReadInt32()
			};
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00018B3A File Offset: 0x00016D3A
		public static void _Write_mpCalls/s_PlayerLeave(NetworkWriter writer, mpCalls.s_PlayerLeave value)
		{
			writer.WriteInt32(value.playerID);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00174CE0 File Offset: 0x00172EE0
		public static mpCalls.s_GenreBeliebtheit _Read_mpCalls/s_GenreBeliebtheit(NetworkReader reader)
		{
			return new mpCalls.s_GenreBeliebtheit
			{
				genreBeliebtheit = GeneratedNetworkCode._Read_System.Single[](reader)
			};
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00018B48 File Offset: 0x00016D48
		public static float[] Single[](NetworkReader reader)
		{
			return reader.ReadArray<float>();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00018B50 File Offset: 0x00016D50
		public static void _Write_mpCalls/s_GenreBeliebtheit(NetworkWriter writer, mpCalls.s_GenreBeliebtheit value)
		{
			GeneratedNetworkCode._Write_System.Single[](writer, value.genreBeliebtheit);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00018B5E File Offset: 0x00016D5E
		public static void Single[](NetworkWriter writer, float[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00174D08 File Offset: 0x00172F08
		public static mpCalls.s_AllAwards _Read_mpCalls/s_AllAwards(NetworkReader reader)
		{
			return new mpCalls.s_AllAwards
			{
				playerID = reader.ReadInt32(),
				awards = GeneratedNetworkCode._Read_System.Int32[](reader)
			};
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00018B67 File Offset: 0x00016D67
		public static void _Write_mpCalls/s_AllAwards(NetworkWriter writer, mpCalls.s_AllAwards value)
		{
			writer.WriteInt32(value.playerID);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.awards);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00174D40 File Offset: 0x00172F40
		public static mpCalls.s_GenreCombination _Read_mpCalls/s_GenreCombination(NetworkReader reader)
		{
			return new mpCalls.s_GenreCombination
			{
				genreSlot = reader.ReadInt32(),
				genres_COMBINATION = GeneratedNetworkCode._Read_System.Boolean[](reader)
			};
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00018B81 File Offset: 0x00016D81
		public static void _Write_mpCalls/s_GenreCombination(NetworkWriter writer, mpCalls.s_GenreCombination value)
		{
			writer.WriteInt32(value.genreSlot);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_COMBINATION);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00174D78 File Offset: 0x00172F78
		public static mpCalls.s_GenreDesign _Read_mpCalls/s_GenreDesign(NetworkReader reader)
		{
			return new mpCalls.s_GenreDesign
			{
				genreSlot = reader.ReadInt32(),
				genres_focus0 = reader.ReadInt32(),
				genres_focus1 = reader.ReadInt32(),
				genres_focus2 = reader.ReadInt32(),
				genres_focus3 = reader.ReadInt32(),
				genres_focus4 = reader.ReadInt32(),
				genres_focus5 = reader.ReadInt32(),
				genres_focus6 = reader.ReadInt32(),
				genres_focus7 = reader.ReadInt32(),
				genres_align0 = reader.ReadInt32(),
				genres_align1 = reader.ReadInt32(),
				genres_align2 = reader.ReadInt32()
			};
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00174E44 File Offset: 0x00173044
		public static void _Write_mpCalls/s_GenreDesign(NetworkWriter writer, mpCalls.s_GenreDesign value)
		{
			writer.WriteInt32(value.genreSlot);
			writer.WriteInt32(value.genres_focus0);
			writer.WriteInt32(value.genres_focus1);
			writer.WriteInt32(value.genres_focus2);
			writer.WriteInt32(value.genres_focus3);
			writer.WriteInt32(value.genres_focus4);
			writer.WriteInt32(value.genres_focus5);
			writer.WriteInt32(value.genres_focus6);
			writer.WriteInt32(value.genres_focus7);
			writer.WriteInt32(value.genres_align0);
			writer.WriteInt32(value.genres_align1);
			writer.WriteInt32(value.genres_align2);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00174EE4 File Offset: 0x001730E4
		public static mpCalls.s_Help _Read_mpCalls/s_Help(NetworkReader reader)
		{
			return new mpCalls.s_Help
			{
				playerID = reader.ReadInt32(),
				toPlayerID = reader.ReadInt32(),
				what = reader.ReadInt32(),
				valueA = reader.ReadInt32(),
				valueB = reader.ReadInt32(),
				valueC = reader.ReadInt32()
			};
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00174F58 File Offset: 0x00173158
		public static void _Write_mpCalls/s_Help(NetworkWriter writer, mpCalls.s_Help value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.toPlayerID);
			writer.WriteInt32(value.what);
			writer.WriteInt32(value.valueA);
			writer.WriteInt32(value.valueB);
			writer.WriteInt32(value.valueC);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00174FB0 File Offset: 0x001731B0
		public static mpCalls.s_ObjectDelete _Read_mpCalls/s_ObjectDelete(NetworkReader reader)
		{
			return new mpCalls.s_ObjectDelete
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32()
			};
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00018B9B File Offset: 0x00016D9B
		public static void _Write_mpCalls/s_ObjectDelete(NetworkWriter writer, mpCalls.s_ObjectDelete value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00174FE8 File Offset: 0x001731E8
		public static mpCalls.s_Object _Read_mpCalls/s_Object(NetworkReader reader)
		{
			return new mpCalls.s_Object
			{
				playerID = reader.ReadInt32(),
				objectID = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				x = reader.ReadSingle(),
				y = reader.ReadSingle(),
				rot = reader.ReadSingle()
			};
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0017505C File Offset: 0x0017325C
		public static void _Write_mpCalls/s_Object(NetworkWriter writer, mpCalls.s_Object value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.objectID);
			writer.WriteInt32(value.typ);
			writer.WriteSingle(value.x);
			writer.WriteSingle(value.y);
			writer.WriteSingle(value.rot);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x001750B4 File Offset: 0x001732B4
		public static mpCalls.s_Map _Read_mpCalls/s_Map(NetworkReader reader)
		{
			return new mpCalls.s_Map
			{
				playerID = reader.ReadInt32(),
				x = reader.ReadByte(),
				y = reader.ReadByte(),
				id = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				door = reader.ReadInt32(),
				window = reader.ReadByte()
			};
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00175138 File Offset: 0x00173338
		public static void _Write_mpCalls/s_Map(NetworkWriter writer, mpCalls.s_Map value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteByte(value.x);
			writer.WriteByte(value.y);
			writer.WriteInt32(value.id);
			writer.WriteInt32(value.typ);
			writer.WriteInt32(value.door);
			writer.WriteByte(value.window);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0017519C File Offset: 0x0017339C
		public static mpCalls.s_Office _Read_mpCalls/s_Office(NetworkReader reader)
		{
			return new mpCalls.s_Office
			{
				office = reader.ReadInt32()
			};
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00018BB5 File Offset: 0x00016DB5
		public static void _Write_mpCalls/s_Office(NetworkWriter writer, mpCalls.s_Office value)
		{
			writer.WriteInt32(value.office);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x001751C4 File Offset: 0x001733C4
		public static mpCalls.s_Difficulty _Read_mpCalls/s_Difficulty(NetworkReader reader)
		{
			return new mpCalls.s_Difficulty
			{
				difficulty = reader.ReadInt32()
			};
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00018BC3 File Offset: 0x00016DC3
		public static void _Write_mpCalls/s_Difficulty(NetworkWriter writer, mpCalls.s_Difficulty value)
		{
			writer.WriteInt32(value.difficulty);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x001751EC File Offset: 0x001733EC
		public static mpCalls.s_Startjahr _Read_mpCalls/s_Startjahr(NetworkReader reader)
		{
			return new mpCalls.s_Startjahr
			{
				startjahr = reader.ReadInt32()
			};
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00018BD1 File Offset: 0x00016DD1
		public static void _Write_mpCalls/s_Startjahr(NetworkWriter writer, mpCalls.s_Startjahr value)
		{
			writer.WriteInt32(value.startjahr);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00175214 File Offset: 0x00173414
		public static mpCalls.s_Spielgeschwindigkeit _Read_mpCalls/s_Spielgeschwindigkeit(NetworkReader reader)
		{
			return new mpCalls.s_Spielgeschwindigkeit
			{
				gamespeed = reader.ReadInt32()
			};
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00018BDF File Offset: 0x00016DDF
		public static void _Write_mpCalls/s_Spielgeschwindigkeit(NetworkWriter writer, mpCalls.s_Spielgeschwindigkeit value)
		{
			writer.WriteInt32(value.gamespeed);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0017523C File Offset: 0x0017343C
		public static mpCalls.s_GlobalEvent _Read_mpCalls/s_GlobalEvent(NetworkReader reader)
		{
			return new mpCalls.s_GlobalEvent
			{
				eventID = reader.ReadInt32(),
				wochen = reader.ReadInt32()
			};
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00018BED File Offset: 0x00016DED
		public static void _Write_mpCalls/s_GlobalEvent(NetworkWriter writer, mpCalls.s_GlobalEvent value)
		{
			writer.WriteInt32(value.eventID);
			writer.WriteInt32(value.wochen);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00175274 File Offset: 0x00173474
		public static mpCalls.s_EngineAbrechnung _Read_mpCalls/s_EngineAbrechnung(NetworkReader reader)
		{
			return new mpCalls.s_EngineAbrechnung
			{
				toPlayerID = reader.ReadInt32(),
				gameID = reader.ReadInt32()
			};
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00018C07 File Offset: 0x00016E07
		public static void _Write_mpCalls/s_EngineAbrechnung(NetworkWriter writer, mpCalls.s_EngineAbrechnung value)
		{
			writer.WriteInt32(value.toPlayerID);
			writer.WriteInt32(value.gameID);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x001752AC File Offset: 0x001734AC
		public static mpCalls.s_Awards _Read_mpCalls/s_Awards(NetworkReader reader)
		{
			return new mpCalls.s_Awards
			{
				bestGrafik = reader.ReadInt32(),
				bestSound = reader.ReadInt32(),
				bestStudio = reader.ReadInt32(),
				bestStudioPlayer = reader.ReadInt32(),
				bestPublisher = reader.ReadInt32(),
				bestPublisherPlayer = reader.ReadInt32(),
				bestGame = reader.ReadInt32(),
				badGame = reader.ReadInt32()
			};
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0017533C File Offset: 0x0017353C
		public static void _Write_mpCalls/s_Awards(NetworkWriter writer, mpCalls.s_Awards value)
		{
			writer.WriteInt32(value.bestGrafik);
			writer.WriteInt32(value.bestSound);
			writer.WriteInt32(value.bestStudio);
			writer.WriteInt32(value.bestStudioPlayer);
			writer.WriteInt32(value.bestPublisher);
			writer.WriteInt32(value.bestPublisherPlayer);
			writer.WriteInt32(value.bestGame);
			writer.WriteInt32(value.badGame);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x001753AC File Offset: 0x001735AC
		public static mpCalls.s_Payment _Read_mpCalls/s_Payment(NetworkReader reader)
		{
			return new mpCalls.s_Payment
			{
				playerID = reader.ReadInt32(),
				toPlayerID = reader.ReadInt32(),
				what = reader.ReadInt32(),
				money = reader.ReadInt32()
			};
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00018C21 File Offset: 0x00016E21
		public static void _Write_mpCalls/s_Payment(NetworkWriter writer, mpCalls.s_Payment value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt32(value.toPlayerID);
			writer.WriteInt32(value.what);
			writer.WriteInt32(value.money);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00175400 File Offset: 0x00173600
		public static mpCalls.s_Engine _Read_mpCalls/s_Engine(NetworkReader reader)
		{
			return new mpCalls.s_Engine
			{
				engineID = reader.ReadInt32(),
				playerEngine = reader.ReadBoolean(),
				multiplayerSlot = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				gekauft = reader.ReadBoolean(),
				myName = reader.ReadString(),
				features = GeneratedNetworkCode._Read_System.Boolean[](reader),
				spezialgenre = reader.ReadInt32(),
				spezialplatform = reader.ReadInt32(),
				sellEngine = reader.ReadBoolean(),
				preis = reader.ReadInt32(),
				gewinnbeteiligung = reader.ReadInt32()
			};
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x001754CC File Offset: 0x001736CC
		public static void _Write_mpCalls/s_Engine(NetworkWriter writer, mpCalls.s_Engine value)
		{
			writer.WriteInt32(value.engineID);
			writer.WriteBoolean(value.playerEngine);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteBoolean(value.gekauft);
			writer.WriteString(value.myName);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.features);
			writer.WriteInt32(value.spezialgenre);
			writer.WriteInt32(value.spezialplatform);
			writer.WriteBoolean(value.sellEngine);
			writer.WriteInt32(value.preis);
			writer.WriteInt32(value.gewinnbeteiligung);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0017556C File Offset: 0x0017376C
		public static mpCalls.s_Platform _Read_mpCalls/s_Platform(NetworkReader reader)
		{
			return new mpCalls.s_Platform
			{
				myID = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				date_year_end = reader.ReadInt32(),
				date_month_end = reader.ReadInt32(),
				npc = reader.ReadBoolean(),
				price = reader.ReadInt32(),
				dev_costs = reader.ReadInt32(),
				tech = reader.ReadInt32(),
				typ = reader.ReadInt32(),
				marktanteil = reader.ReadSingle(),
				needFeatures = GeneratedNetworkCode._Read_System.Int32[](reader),
				units = reader.ReadInt32(),
				units_max = reader.ReadInt32(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_HU = reader.ReadString(),
				name_JA = reader.ReadString(),
				manufacturer_EN = reader.ReadString(),
				manufacturer_GE = reader.ReadString(),
				manufacturer_TU = reader.ReadString(),
				manufacturer_CH = reader.ReadString(),
				manufacturer_FR = reader.ReadString(),
				manufacturer_HU = reader.ReadString(),
				manufacturer_JA = reader.ReadString(),
				pic1_file = reader.ReadString(),
				pic2_file = reader.ReadString(),
				pic2_year = reader.ReadInt32(),
				games = reader.ReadInt32(),
				exklusivGames = reader.ReadInt32(),
				erfahrung = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				inBesitz = reader.ReadBoolean(),
				vomMarktGenommen = reader.ReadBoolean(),
				complex = reader.ReadInt32(),
				internet = reader.ReadBoolean(),
				powerFromMarket = reader.ReadSingle(),
				myName = reader.ReadString(),
				playerConsole = reader.ReadBoolean(),
				multiplaySlot = reader.ReadInt32(),
				gameID = reader.ReadInt32(),
				anzController = reader.ReadInt32(),
				conHueShift = reader.ReadSingle(),
				conSaturation = reader.ReadSingle(),
				component_cpu = reader.ReadInt32(),
				component_gfx = reader.ReadInt32(),
				component_ram = reader.ReadInt32(),
				component_hdd = reader.ReadInt32(),
				component_sfx = reader.ReadInt32(),
				component_cooling = reader.ReadInt32(),
				component_disc = reader.ReadInt32(),
				component_controller = reader.ReadInt32(),
				component_case = reader.ReadInt32(),
				component_monitor = reader.ReadInt32(),
				hwFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				devPoints = reader.ReadSingle(),
				devPointsStart = reader.ReadSingle(),
				entwicklungsKosten = reader.ReadInt64(),
				einnahmen = reader.ReadInt64(),
				hype = reader.ReadSingle(),
				costs_marketing = reader.ReadInt32(),
				costs_mitarbeiter = reader.ReadInt32(),
				startProduktionskosten = reader.ReadInt32(),
				verkaufspreis = reader.ReadInt32(),
				kostenreduktion = reader.ReadSingle(),
				autoPreis = reader.ReadBoolean(),
				thridPartyGames = reader.ReadBoolean(),
				umsatzTotal = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				weeksOnMarket = reader.ReadInt32(),
				review = reader.ReadSingle(),
				performancePoints = reader.ReadInt32()
			};
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x001759F8 File Offset: 0x00173BF8
		public static void _Write_mpCalls/s_Platform(NetworkWriter writer, mpCalls.s_Platform value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.date_year_end);
			writer.WriteInt32(value.date_month_end);
			writer.WriteBoolean(value.npc);
			writer.WriteInt32(value.price);
			writer.WriteInt32(value.dev_costs);
			writer.WriteInt32(value.tech);
			writer.WriteInt32(value.typ);
			writer.WriteSingle(value.marktanteil);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.needFeatures);
			writer.WriteInt32(value.units);
			writer.WriteInt32(value.units_max);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_HU);
			writer.WriteString(value.name_JA);
			writer.WriteString(value.manufacturer_EN);
			writer.WriteString(value.manufacturer_GE);
			writer.WriteString(value.manufacturer_TU);
			writer.WriteString(value.manufacturer_CH);
			writer.WriteString(value.manufacturer_FR);
			writer.WriteString(value.manufacturer_HU);
			writer.WriteString(value.manufacturer_JA);
			writer.WriteString(value.pic1_file);
			writer.WriteString(value.pic2_file);
			writer.WriteInt32(value.pic2_year);
			writer.WriteInt32(value.games);
			writer.WriteInt32(value.exklusivGames);
			writer.WriteInt32(value.erfahrung);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteBoolean(value.inBesitz);
			writer.WriteBoolean(value.vomMarktGenommen);
			writer.WriteInt32(value.complex);
			writer.WriteBoolean(value.internet);
			writer.WriteSingle(value.powerFromMarket);
			writer.WriteString(value.myName);
			writer.WriteBoolean(value.playerConsole);
			writer.WriteInt32(value.multiplaySlot);
			writer.WriteInt32(value.gameID);
			writer.WriteInt32(value.anzController);
			writer.WriteSingle(value.conHueShift);
			writer.WriteSingle(value.conSaturation);
			writer.WriteInt32(value.component_cpu);
			writer.WriteInt32(value.component_gfx);
			writer.WriteInt32(value.component_ram);
			writer.WriteInt32(value.component_hdd);
			writer.WriteInt32(value.component_sfx);
			writer.WriteInt32(value.component_cooling);
			writer.WriteInt32(value.component_disc);
			writer.WriteInt32(value.component_controller);
			writer.WriteInt32(value.component_case);
			writer.WriteInt32(value.component_monitor);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hwFeatures);
			writer.WriteSingle(value.devPoints);
			writer.WriteSingle(value.devPointsStart);
			writer.WriteInt64(value.entwicklungsKosten);
			writer.WriteInt64(value.einnahmen);
			writer.WriteSingle(value.hype);
			writer.WriteInt32(value.costs_marketing);
			writer.WriteInt32(value.costs_mitarbeiter);
			writer.WriteInt32(value.startProduktionskosten);
			writer.WriteInt32(value.verkaufspreis);
			writer.WriteSingle(value.kostenreduktion);
			writer.WriteBoolean(value.autoPreis);
			writer.WriteBoolean(value.thridPartyGames);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteInt64(value.costs_production);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteSingle(value.review);
			writer.WriteInt32(value.performancePoints);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x00175D98 File Offset: 0x00173F98
		public static mpCalls.s_PlatformData _Read_mpCalls/s_PlatformData(NetworkReader reader)
		{
			return new mpCalls.s_PlatformData
			{
				platformID = reader.ReadInt32(),
				marktanteil = reader.ReadSingle(),
				units = reader.ReadInt32(),
				units_max = reader.ReadInt32(),
				date_year_end = reader.ReadInt32()
			};
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00018C53 File Offset: 0x00016E53
		public static void _Write_mpCalls/s_PlatformData(NetworkWriter writer, mpCalls.s_PlatformData value)
		{
			writer.WriteInt32(value.platformID);
			writer.WriteSingle(value.marktanteil);
			writer.WriteInt32(value.units);
			writer.WriteInt32(value.units_max);
			writer.WriteInt32(value.date_year_end);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00175DFC File Offset: 0x00173FFC
		public static mpCalls.s_Chat _Read_mpCalls/s_Chat(NetworkReader reader)
		{
			return new mpCalls.s_Chat
			{
				playerID = reader.ReadInt32(),
				text = reader.ReadString()
			};
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00018C91 File Offset: 0x00016E91
		public static void _Write_mpCalls/s_Chat(NetworkWriter writer, mpCalls.s_Chat value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteString(value.text);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00175E34 File Offset: 0x00174034
		public static mpCalls.s_Money _Read_mpCalls/s_Money(NetworkReader reader)
		{
			return new mpCalls.s_Money
			{
				playerID = reader.ReadInt32(),
				money = reader.ReadInt64(),
				fans = reader.ReadInt32()
			};
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00018CAB File Offset: 0x00016EAB
		public static void _Write_mpCalls/s_Money(NetworkWriter writer, mpCalls.s_Money value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteInt64(value.money);
			writer.WriteInt32(value.fans);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00175E7C File Offset: 0x0017407C
		public static mpCalls.s_AutoPause _Read_mpCalls/s_AutoPause(NetworkReader reader)
		{
			return new mpCalls.s_AutoPause
			{
				playerID = reader.ReadInt32(),
				pause = reader.ReadBoolean()
			};
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00018CD1 File Offset: 0x00016ED1
		public static void _Write_mpCalls/s_AutoPause(NetworkWriter writer, mpCalls.s_AutoPause value)
		{
			writer.WriteInt32(value.playerID);
			writer.WriteBoolean(value.pause);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00175EB4 File Offset: 0x001740B4
		public static mpCalls.s_Genres _Read_mpCalls/s_Genres(NetworkReader reader)
		{
			return new mpCalls.s_Genres
			{
				genres_BELIEBTHEIT = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_BELIEBTHEIT_SOLL = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_RES_POINTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_RES_POINTS_LEFT = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_PRICE = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_DEV_COSTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_DATE_YEAR = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_DATE_MONTH = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_LEVEL = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_UNLOCK = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_TARGETGROUP = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_GAMEPLAY = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_GRAPHIC = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_SOUND = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_CONTROL = GeneratedNetworkCode._Read_System.Single[](reader),
				genres_COMBINATION = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_FOCUS = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_FOCUS_KNOWN = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_ALIGN = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_ALIGN_KNOWN = GeneratedNetworkCode._Read_System.Boolean[](reader),
				genres_ICONFILE = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_EN = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_GE = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_TU = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_CH = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_FR = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_PB = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_HU = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_CT = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_ES = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_PL = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_KO = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_IT = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_AR = GeneratedNetworkCode._Read_System.String[](reader),
				genres_NAME_JA = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_EN = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_GE = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_TU = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_CH = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_FR = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_PB = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_HU = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_CT = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_ES = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_PL = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_KO = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_IT = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_AR = GeneratedNetworkCode._Read_System.String[](reader),
				genres_DESC_JA = GeneratedNetworkCode._Read_System.String[](reader),
				genres_FANS = GeneratedNetworkCode._Read_System.Int32[](reader),
				genres_MARKT = GeneratedNetworkCode._Read_System.Int32[](reader)
			};
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00018CEB File Offset: 0x00016EEB
		public static string[] String[](NetworkReader reader)
		{
			return reader.ReadArray<string>();
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x001761E8 File Offset: 0x001743E8
		public static void _Write_mpCalls/s_Genres(NetworkWriter writer, mpCalls.s_Genres value)
		{
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_BELIEBTHEIT);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_BELIEBTHEIT_SOLL);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_RES_POINTS);
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_RES_POINTS_LEFT);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_PRICE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_DEV_COSTS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_DATE_YEAR);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_DATE_MONTH);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_LEVEL);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_UNLOCK);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_TARGETGROUP);
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_GAMEPLAY);
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_GRAPHIC);
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_SOUND);
			GeneratedNetworkCode._Write_System.Single[](writer, value.genres_CONTROL);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_COMBINATION);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_FOCUS);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_FOCUS_KNOWN);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_ALIGN);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.genres_ALIGN_KNOWN);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_ICONFILE);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_NAME_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.genres_DESC_JA);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_FANS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.genres_MARKT);
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00018CF3 File Offset: 0x00016EF3
		public static void String[](NetworkWriter writer, string[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00176474 File Offset: 0x00174674
		public static mpCalls.s_GameplayFeatures _Read_mpCalls/s_GameplayFeatures(NetworkReader reader)
		{
			return new mpCalls.s_GameplayFeatures
			{
				gameplayFeatures_TYP = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_RES_POINTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_RES_POINTS_LEFT = GeneratedNetworkCode._Read_System.Single[](reader),
				gameplayFeatures_PRICE = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_DEV_COSTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_DATE_YEAR = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_DATE_MONTH = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_GAMEPLAY = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_GRAPHIC = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_SOUND = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_TECHNIK = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_LEVEL = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_UNLOCK = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures_ICONFILE = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_GOOD = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures_BAD = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures_LOCKPLATFORM = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayFeatures_NAME_EN = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_GE = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_TU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_CH = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_FR = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_PB = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_CT = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_HU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_ES = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_KO = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_RU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_IT = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_AR = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_JA = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_NAME_PL = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_EN = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_GE = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_TU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_CH = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_FR = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_PB = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_CT = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_HU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_ES = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_KO = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_RU = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_IT = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_AR = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_JA = GeneratedNetworkCode._Read_System.String[](reader),
				gameplayFeatures_DESC_PL = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0017676C File Offset: 0x0017496C
		public static void _Write_mpCalls/s_GameplayFeatures(NetworkWriter writer, mpCalls.s_GameplayFeatures value)
		{
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_TYP);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_RES_POINTS);
			GeneratedNetworkCode._Write_System.Single[](writer, value.gameplayFeatures_RES_POINTS_LEFT);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_PRICE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_DEV_COSTS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_DATE_YEAR);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_DATE_MONTH);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_GAMEPLAY);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_GRAPHIC);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_SOUND);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_TECHNIK);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameplayFeatures_LEVEL);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_UNLOCK);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_ICONFILE);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_GOOD);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_BAD);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_LOCKPLATFORM);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_NAME_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.gameplayFeatures_DESC_PL);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x001769C8 File Offset: 0x00174BC8
		public static mpCalls.s_EngineFeatures _Read_mpCalls/s_EngineFeatures(NetworkReader reader)
		{
			return new mpCalls.s_EngineFeatures
			{
				engineFeatures_TYP = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_RES_POINTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_RES_POINTS_LEFT = GeneratedNetworkCode._Read_System.Single[](reader),
				engineFeatures_PRICE = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_DEV_COSTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_TECH = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_DATE_YEAR = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_DATE_MONTH = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_GAMEPLAY = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_GRAPHIC = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_SOUND = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_TECHNIK = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_LEVEL = GeneratedNetworkCode._Read_System.Int32[](reader),
				engineFeatures_UNLOCK = GeneratedNetworkCode._Read_System.Boolean[](reader),
				engineFeatures_ICONFILE = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_EN = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_GE = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_TU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_CH = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_FR = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_PB = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_CT = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_HU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_ES = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_KO = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_AR = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_RU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_IT = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_JA = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_NAME_PL = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_EN = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_GE = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_TU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_CH = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_FR = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_PB = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_CT = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_HU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_ES = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_KO = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_AR = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_RU = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_IT = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_JA = GeneratedNetworkCode._Read_System.String[](reader),
				engineFeatures_DESC_PL = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00176CA4 File Offset: 0x00174EA4
		public static void _Write_mpCalls/s_EngineFeatures(NetworkWriter writer, mpCalls.s_EngineFeatures value)
		{
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_TYP);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_RES_POINTS);
			GeneratedNetworkCode._Write_System.Single[](writer, value.engineFeatures_RES_POINTS_LEFT);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_PRICE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_DEV_COSTS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_TECH);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_DATE_YEAR);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_DATE_MONTH);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_GAMEPLAY);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_GRAPHIC);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_SOUND);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_TECHNIK);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.engineFeatures_LEVEL);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.engineFeatures_UNLOCK);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_ICONFILE);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_NAME_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.engineFeatures_DESC_PL);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00176EE8 File Offset: 0x001750E8
		public static mpCalls.s_HardwareFeatures _Read_mpCalls/s_HardwareFeatures(NetworkReader reader)
		{
			return new mpCalls.s_HardwareFeatures
			{
				hardFeat_ICONFILE = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_RES_POINTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardFeat_RES_POINTS_LEFT = GeneratedNetworkCode._Read_System.Single[](reader),
				hardFeat_PRICE = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardFeat_DEV_COSTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardFeat_DATE_YEAR = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardFeat_DATE_MONTH = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardFeat_UNLOCK = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardFeat_ONLYSTATIONARY = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardFeat_ONLYHANDHELD = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardFeat_NEEDINTERNET = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardFeat_QUALITY = GeneratedNetworkCode._Read_System.Single[](reader),
				hardFeat_NAME_EN = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_GE = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_TU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_CH = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_FR = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_PB = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_CT = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_HU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_ES = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_KO = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_AR = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_RU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_IT = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_JA = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_NAME_PL = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_EN = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_GE = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_TU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_CH = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_FR = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_PB = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_CT = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_HU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_ES = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_KO = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_AR = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_RU = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_IT = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_JA = GeneratedNetworkCode._Read_System.String[](reader),
				hardFeat_DESC_PL = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00177194 File Offset: 0x00175394
		public static void _Write_mpCalls/s_HardwareFeatures(NetworkWriter writer, mpCalls.s_HardwareFeatures value)
		{
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_ICONFILE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardFeat_RES_POINTS);
			GeneratedNetworkCode._Write_System.Single[](writer, value.hardFeat_RES_POINTS_LEFT);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardFeat_PRICE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardFeat_DEV_COSTS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardFeat_DATE_YEAR);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardFeat_DATE_MONTH);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardFeat_UNLOCK);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardFeat_ONLYSTATIONARY);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardFeat_ONLYHANDHELD);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardFeat_NEEDINTERNET);
			GeneratedNetworkCode._Write_System.Single[](writer, value.hardFeat_QUALITY);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_NAME_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardFeat_DESC_PL);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x001773B4 File Offset: 0x001755B4
		public static mpCalls.s_Hardware _Read_mpCalls/s_Hardware(NetworkReader reader)
		{
			return new mpCalls.s_Hardware
			{
				hardware_ICONFILE = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_TYP = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_RES_POINTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_RES_POINTS_LEFT = GeneratedNetworkCode._Read_System.Single[](reader),
				hardware_PRICE = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_DEV_COSTS = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_TECH = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_DATE_YEAR = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_DATE_MONTH = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_UNLOCK = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardware_ONLYSTATIONARY = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardware_ONLYHANDHELD = GeneratedNetworkCode._Read_System.Boolean[](reader),
				hardware_NEED1 = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_NEED2 = GeneratedNetworkCode._Read_System.Int32[](reader),
				hardware_NAME_EN = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_GE = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_TU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_CH = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_FR = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_PB = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_CT = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_HU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_ES = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_KO = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_AR = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_RU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_IT = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_JA = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_NAME_PL = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_EN = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_GE = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_TU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_CH = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_FR = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_PB = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_CT = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_HU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_ES = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_CZ = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_KO = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_AR = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_RU = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_IT = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_JA = GeneratedNetworkCode._Read_System.String[](reader),
				hardware_DESC_PL = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00177680 File Offset: 0x00175880
		public static void _Write_mpCalls/s_Hardware(NetworkWriter writer, mpCalls.s_Hardware value)
		{
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_ICONFILE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_TYP);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_RES_POINTS);
			GeneratedNetworkCode._Write_System.Single[](writer, value.hardware_RES_POINTS_LEFT);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_PRICE);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_DEV_COSTS);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_TECH);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_DATE_YEAR);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_DATE_MONTH);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardware_UNLOCK);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardware_ONLYSTATIONARY);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.hardware_ONLYHANDHELD);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_NEED1);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.hardware_NEED2);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_NAME_PL);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_EN);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_GE);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_TU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_CH);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_FR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_PB);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_CT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_HU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_ES);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_CZ);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_KO);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_AR);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_RU);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_IT);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_JA);
			GeneratedNetworkCode._Write_System.String[](writer, value.hardware_DESC_PL);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x001778B8 File Offset: 0x00175AB8
		public static mpCalls.s_AntiCheat _Read_mpCalls/s_AntiCheat(NetworkReader reader)
		{
			return new mpCalls.s_AntiCheat
			{
				myID = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				price = reader.ReadInt32(),
				dev_costs = reader.ReadInt32(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_CT = reader.ReadString(),
				name_RU = reader.ReadString(),
				name_IT = reader.ReadString(),
				name_JA = reader.ReadString(),
				isUnlocked = reader.ReadBoolean(),
				effekt = reader.ReadSingle(),
				neverLooseEffect = reader.ReadBoolean()
			};
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x001779D0 File Offset: 0x00175BD0
		public static void _Write_mpCalls/s_AntiCheat(NetworkWriter writer, mpCalls.s_AntiCheat value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.price);
			writer.WriteInt32(value.dev_costs);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_CT);
			writer.WriteString(value.name_RU);
			writer.WriteString(value.name_IT);
			writer.WriteString(value.name_JA);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteSingle(value.effekt);
			writer.WriteBoolean(value.neverLooseEffect);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00177AAC File Offset: 0x00175CAC
		public static mpCalls.s_CopyProtect _Read_mpCalls/s_CopyProtect(NetworkReader reader)
		{
			return new mpCalls.s_CopyProtect
			{
				myID = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				price = reader.ReadInt32(),
				dev_costs = reader.ReadInt32(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_CT = reader.ReadString(),
				name_RU = reader.ReadString(),
				name_IT = reader.ReadString(),
				name_JA = reader.ReadString(),
				isUnlocked = reader.ReadBoolean(),
				effekt = reader.ReadSingle(),
				neverLooseEffect = reader.ReadBoolean()
			};
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00177BC4 File Offset: 0x00175DC4
		public static void _Write_mpCalls/s_CopyProtect(NetworkWriter writer, mpCalls.s_CopyProtect value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.price);
			writer.WriteInt32(value.dev_costs);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_CT);
			writer.WriteString(value.name_RU);
			writer.WriteString(value.name_IT);
			writer.WriteString(value.name_JA);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteSingle(value.effekt);
			writer.WriteBoolean(value.neverLooseEffect);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00177CA0 File Offset: 0x00175EA0
		public static mpCalls.s_NpcEngine _Read_mpCalls/s_NpcEngine(NetworkReader reader)
		{
			return new mpCalls.s_NpcEngine
			{
				myID = reader.ReadInt32(),
				playerEngine = reader.ReadBoolean(),
				multiplayerSlot = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				gekauft = reader.ReadBoolean(),
				myName = reader.ReadString(),
				umsatz = reader.ReadInt32(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_HU = reader.ReadString(),
				name_CT = reader.ReadString(),
				name_CZ = reader.ReadString(),
				name_PB = reader.ReadString(),
				name_IT = reader.ReadString(),
				name_JA = reader.ReadString(),
				features = GeneratedNetworkCode._Read_System.Boolean[](reader),
				featuresInDev = GeneratedNetworkCode._Read_System.Boolean[](reader),
				spezialgenre = reader.ReadInt32(),
				spezialplatform = reader.ReadInt32(),
				sellEngine = reader.ReadBoolean(),
				preis = reader.ReadInt32(),
				gewinnbeteiligung = reader.ReadInt32(),
				updating = reader.ReadBoolean(),
				devPoints = reader.ReadSingle(),
				devPointsStart = reader.ReadSingle(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				publisherBuyed = GeneratedNetworkCode._Read_System.Boolean[](reader),
				archiv_engine = reader.ReadBoolean()
			};
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00177E98 File Offset: 0x00176098
		public static void _Write_mpCalls/s_NpcEngine(NetworkWriter writer, mpCalls.s_NpcEngine value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteBoolean(value.playerEngine);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteBoolean(value.gekauft);
			writer.WriteString(value.myName);
			writer.WriteInt32(value.umsatz);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_HU);
			writer.WriteString(value.name_CT);
			writer.WriteString(value.name_CZ);
			writer.WriteString(value.name_PB);
			writer.WriteString(value.name_IT);
			writer.WriteString(value.name_JA);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.features);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.featuresInDev);
			writer.WriteInt32(value.spezialgenre);
			writer.WriteInt32(value.spezialplatform);
			writer.WriteBoolean(value.sellEngine);
			writer.WriteInt32(value.preis);
			writer.WriteInt32(value.gewinnbeteiligung);
			writer.WriteBoolean(value.updating);
			writer.WriteSingle(value.devPoints);
			writer.WriteSingle(value.devPointsStart);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.publisherBuyed);
			writer.WriteBoolean(value.archiv_engine);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00178028 File Offset: 0x00176228
		public static mpCalls.s_Firmenwert _Read_mpCalls/s_Firmenwert(NetworkReader reader)
		{
			return new mpCalls.s_Firmenwert
			{
				firmenwert = GeneratedNetworkCode._Read_System.Int64[](reader)
			};
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00018CFC File Offset: 0x00016EFC
		public static long[] Int64[](NetworkReader reader)
		{
			return reader.ReadArray<long>();
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00018D04 File Offset: 0x00016F04
		public static void _Write_mpCalls/s_Firmenwert(NetworkWriter writer, mpCalls.s_Firmenwert value)
		{
			GeneratedNetworkCode._Write_System.Int64[](writer, value.firmenwert);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00018D12 File Offset: 0x00016F12
		public static void Int64[](NetworkWriter writer, long[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00178050 File Offset: 0x00176250
		public static mpCalls.s_Publisher _Read_mpCalls/s_Publisher(NetworkReader reader)
		{
			return new mpCalls.s_Publisher
			{
				myID = reader.ReadInt32(),
				isUnlocked = reader.ReadBoolean(),
				name_EN = reader.ReadString(),
				name_GE = reader.ReadString(),
				name_TU = reader.ReadString(),
				name_CH = reader.ReadString(),
				name_FR = reader.ReadString(),
				name_JA = reader.ReadString(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				stars = reader.ReadSingle(),
				logoID = reader.ReadInt32(),
				developer = reader.ReadBoolean(),
				publisher = reader.ReadBoolean(),
				onlyMobile = reader.ReadBoolean(),
				share = reader.ReadSingle(),
				fanGenre = reader.ReadInt32(),
				firmenwert = reader.ReadInt64(),
				notForSale = reader.ReadBoolean(),
				lockToBuy = reader.ReadInt32()
			};
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00178194 File Offset: 0x00176394
		public static void _Write_mpCalls/s_Publisher(NetworkWriter writer, mpCalls.s_Publisher value)
		{
			writer.WriteInt32(value.myID);
			writer.WriteBoolean(value.isUnlocked);
			writer.WriteString(value.name_EN);
			writer.WriteString(value.name_GE);
			writer.WriteString(value.name_TU);
			writer.WriteString(value.name_CH);
			writer.WriteString(value.name_FR);
			writer.WriteString(value.name_JA);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteSingle(value.stars);
			writer.WriteInt32(value.logoID);
			writer.WriteBoolean(value.developer);
			writer.WriteBoolean(value.publisher);
			writer.WriteBoolean(value.onlyMobile);
			writer.WriteSingle(value.share);
			writer.WriteInt32(value.fanGenre);
			writer.WriteInt64(value.firmenwert);
			writer.WriteBoolean(value.notForSale);
			writer.WriteInt32(value.lockToBuy);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00178294 File Offset: 0x00176494
		public static mpCalls.s_exklusivKonsolenSells _Read_mpCalls/s_exklusivKonsolenSells(NetworkReader reader)
		{
			return new mpCalls.s_exklusivKonsolenSells
			{
				gameID = reader.ReadInt32(),
				exklusivKonsolenSells = reader.ReadInt64()
			};
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00018D1B File Offset: 0x00016F1B
		public static void _Write_mpCalls/s_exklusivKonsolenSells(NetworkWriter writer, mpCalls.s_exklusivKonsolenSells value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteInt64(value.exklusivKonsolenSells);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x001782CC File Offset: 0x001764CC
		public static mpCalls.s_GameData _Read_mpCalls/s_GameData(NetworkReader reader)
		{
			return new mpCalls.s_GameData
			{
				gameID = reader.ReadInt32(),
				sellsTotal = reader.ReadInt64(),
				umsatzTotal = reader.ReadInt64(),
				isOnMarket = reader.ReadBoolean(),
				weeksOnMarket = reader.ReadInt32(),
				userPositiv = reader.ReadInt32(),
				userNegativ = reader.ReadInt32(),
				costs_entwicklung = reader.ReadInt64(),
				costs_mitarbeiter = reader.ReadInt64(),
				costs_marketing = reader.ReadInt64(),
				costs_enginegebuehren = reader.ReadInt64(),
				costs_server = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				costs_updates = reader.ReadInt64(),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				abonnements = reader.ReadInt32(),
				abonnementsWoche = reader.ReadInt32(),
				bestAbonnements = reader.ReadInt32(),
				bestChartPosition = reader.ReadInt32(),
				exklusivKonsolenSells = reader.ReadInt64(),
				multiplayerSlot = reader.ReadInt32(),
				ipPunkte = reader.ReadSingle(),
				pubAngebot = reader.ReadBoolean(),
				pubAngebot_Weeks = reader.ReadInt32(),
				pubAngebot_Verhandlung = reader.ReadSingle(),
				pubAngebot_Retail = reader.ReadBoolean(),
				pubAngebot_Digital = reader.ReadBoolean(),
				pubAngebot_Garantiesumme = reader.ReadInt32(),
				pubAngebot_Gewinnbeteiligung = reader.ReadSingle(),
				auftragsspiel = reader.ReadBoolean(),
				auftragsspiel_gehalt = reader.ReadInt32(),
				auftragsspiel_bonus = reader.ReadInt32(),
				auftragsspiel_zeitInWochen = reader.ReadInt32(),
				auftragsspiel_wochenAlsAngebot = reader.ReadInt32(),
				auftragsspiel_zeitAbgelaufen = reader.ReadBoolean(),
				auftragsspiel_mindestbewertung = reader.ReadInt32(),
				ipName = reader.ReadString(),
				lastChartPosition = reader.ReadInt32()
			};
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00178520 File Offset: 0x00176720
		public static void _Write_mpCalls/s_GameData(NetworkWriter writer, mpCalls.s_GameData value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteInt64(value.sellsTotal);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteBoolean(value.isOnMarket);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteInt32(value.userPositiv);
			writer.WriteInt32(value.userNegativ);
			writer.WriteInt64(value.costs_entwicklung);
			writer.WriteInt64(value.costs_mitarbeiter);
			writer.WriteInt64(value.costs_marketing);
			writer.WriteInt64(value.costs_enginegebuehren);
			writer.WriteInt64(value.costs_server);
			writer.WriteInt64(value.costs_production);
			writer.WriteInt64(value.costs_updates);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			writer.WriteInt32(value.abonnements);
			writer.WriteInt32(value.abonnementsWoche);
			writer.WriteInt32(value.bestAbonnements);
			writer.WriteInt32(value.bestChartPosition);
			writer.WriteInt64(value.exklusivKonsolenSells);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteSingle(value.ipPunkte);
			writer.WriteBoolean(value.pubAngebot);
			writer.WriteInt32(value.pubAngebot_Weeks);
			writer.WriteSingle(value.pubAngebot_Verhandlung);
			writer.WriteBoolean(value.pubAngebot_Retail);
			writer.WriteBoolean(value.pubAngebot_Digital);
			writer.WriteInt32(value.pubAngebot_Garantiesumme);
			writer.WriteSingle(value.pubAngebot_Gewinnbeteiligung);
			writer.WriteBoolean(value.auftragsspiel);
			writer.WriteInt32(value.auftragsspiel_gehalt);
			writer.WriteInt32(value.auftragsspiel_bonus);
			writer.WriteInt32(value.auftragsspiel_zeitInWochen);
			writer.WriteInt32(value.auftragsspiel_wochenAlsAngebot);
			writer.WriteBoolean(value.auftragsspiel_zeitAbgelaufen);
			writer.WriteInt32(value.auftragsspiel_mindestbewertung);
			writer.WriteString(value.ipName);
			writer.WriteInt32(value.lastChartPosition);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x001786F8 File Offset: 0x001768F8
		public static mpCalls.s_Game _Read_mpCalls/s_Game(NetworkReader reader)
		{
			return new mpCalls.s_Game
			{
				gameID = reader.ReadInt32(),
				myName = reader.ReadString(),
				ipName = reader.ReadString(),
				playerGame = reader.ReadBoolean(),
				multiplayerSlot = reader.ReadInt32(),
				inDevelopment = reader.ReadBoolean(),
				developerID = reader.ReadInt32(),
				publisherID = reader.ReadInt32(),
				ownerID = reader.ReadInt32(),
				engineID = reader.ReadInt32(),
				hype = reader.ReadSingle(),
				isOnMarket = reader.ReadBoolean(),
				warBeiAwards = reader.ReadBoolean(),
				weeksOnMarket = reader.ReadInt32(),
				usk = reader.ReadInt32(),
				freigabeBudget = reader.ReadInt32(),
				reviewGameplay = reader.ReadInt32(),
				reviewGrafik = reader.ReadInt32(),
				reviewSound = reader.ReadInt32(),
				reviewSteuerung = reader.ReadInt32(),
				reviewTotal = reader.ReadInt32(),
				reviewGameplayText = reader.ReadInt32(),
				reviewGrafikText = reader.ReadInt32(),
				reviewSoundText = reader.ReadInt32(),
				reviewSteuerungText = reader.ReadInt32(),
				reviewTotalText = reader.ReadInt32(),
				date_year = reader.ReadInt32(),
				date_month = reader.ReadInt32(),
				date_start_year = reader.ReadInt32(),
				date_start_month = reader.ReadInt32(),
				sellsTotal = reader.ReadInt64(),
				umsatzTotal = reader.ReadInt64(),
				costs_entwicklung = reader.ReadInt64(),
				costs_mitarbeiter = reader.ReadInt64(),
				costs_marketing = reader.ReadInt64(),
				costs_enginegebuehren = reader.ReadInt64(),
				costs_server = reader.ReadInt64(),
				costs_production = reader.ReadInt64(),
				costs_updates = reader.ReadInt64(),
				typ_standard = reader.ReadBoolean(),
				typ_nachfolger = reader.ReadBoolean(),
				originalIP = reader.ReadInt32(),
				teile = reader.ReadInt32(),
				typ_contractGame = reader.ReadBoolean(),
				typ_remaster = reader.ReadBoolean(),
				typ_spinoff = reader.ReadBoolean(),
				typ_addon = reader.ReadBoolean(),
				typ_addonStandalone = reader.ReadBoolean(),
				typ_mmoaddon = reader.ReadBoolean(),
				typ_bundle = reader.ReadBoolean(),
				typ_budget = reader.ReadBoolean(),
				typ_bundleAddon = reader.ReadBoolean(),
				typ_goty = reader.ReadBoolean(),
				originalGameID = reader.ReadInt32(),
				portID = reader.ReadInt32(),
				mainIP = reader.ReadInt32(),
				ipPunkte = reader.ReadSingle(),
				exklusiv = reader.ReadBoolean(),
				herstellerExklusiv = reader.ReadBoolean(),
				retro = reader.ReadBoolean(),
				handy = reader.ReadBoolean(),
				arcade = reader.ReadBoolean(),
				goty = reader.ReadBoolean(),
				nachfolger_created = reader.ReadBoolean(),
				remaster_created = reader.ReadBoolean(),
				budget_created = reader.ReadBoolean(),
				goty_created = reader.ReadBoolean(),
				trendsetter = reader.ReadBoolean(),
				spielbericht = reader.ReadBoolean(),
				amountUpdates = reader.ReadInt32(),
				bonusSellsUpdates = reader.ReadSingle(),
				amountAddons = reader.ReadInt32(),
				bonusSellsAddons = reader.ReadSingle(),
				amountMMOAddons = reader.ReadInt32(),
				bonusSellsMMOAddons = reader.ReadSingle(),
				addonQuality = reader.ReadSingle(),
				devAktFeature = reader.ReadInt32(),
				devPoints = reader.ReadSingle(),
				devPointsStart = reader.ReadSingle(),
				devPoints_Gesamt = reader.ReadSingle(),
				devPointsStart_Gesamt = reader.ReadSingle(),
				points_gameplay = reader.ReadSingle(),
				points_grafik = reader.ReadSingle(),
				points_sound = reader.ReadSingle(),
				points_technik = reader.ReadSingle(),
				points_bugs = reader.ReadSingle(),
				beschreibung = reader.ReadString(),
				gameTyp = reader.ReadInt32(),
				gameSize = reader.ReadInt32(),
				gameZielgruppe = reader.ReadInt32(),
				maingenre = reader.ReadInt32(),
				subgenre = reader.ReadInt32(),
				gameMainTheme = reader.ReadInt32(),
				gameSubTheme = reader.ReadInt32(),
				gameLicence = reader.ReadInt32(),
				gameCopyProtect = reader.ReadInt32(),
				gameAntiCheat = reader.ReadInt32(),
				gameAP_Gameplay = reader.ReadInt32(),
				gameAP_Grafik = reader.ReadInt32(),
				gameAP_Sound = reader.ReadInt32(),
				gameAP_Technik = reader.ReadInt32(),
				gameLanguage = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameGameplayFeatures = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gamePlatform = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameEngineFeature = GeneratedNetworkCode._Read_System.Int32[](reader),
				gameplayFeatures_DevDone = GeneratedNetworkCode._Read_System.Boolean[](reader),
				engineFeature_DevDone = GeneratedNetworkCode._Read_System.Boolean[](reader),
				gameplayStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				grafikStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				soundStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				motionCaptureStudio = GeneratedNetworkCode._Read_System.Boolean[](reader),
				bundleID = GeneratedNetworkCode._Read_System.Int32[](reader),
				portExist = GeneratedNetworkCode._Read_System.Boolean[](reader),
				sellsPerWeek = GeneratedNetworkCode._Read_System.Int32[](reader),
				verkaufspreis = GeneratedNetworkCode._Read_System.Int32[](reader),
				releaseDate = reader.ReadInt32(),
				abonnements = reader.ReadInt32(),
				abonnementsWoche = reader.ReadInt32(),
				aboPreis = reader.ReadInt32(),
				pubOffer = reader.ReadBoolean(),
				pubAngebot = reader.ReadBoolean(),
				pubAngebot_Weeks = reader.ReadInt32(),
				pubAngebot_Verhandlung = reader.ReadSingle(),
				pubAngebot_Retail = reader.ReadBoolean(),
				pubAngebot_Digital = reader.ReadBoolean(),
				pubAngebot_Garantiesumme = reader.ReadInt32(),
				pubAngebot_Gewinnbeteiligung = reader.ReadSingle(),
				auftragsspiel = reader.ReadBoolean(),
				auftragsspiel_gehalt = reader.ReadInt32(),
				auftragsspiel_bonus = reader.ReadInt32(),
				auftragsspiel_zeitInWochen = reader.ReadInt32(),
				auftragsspiel_wochenAlsAngebot = reader.ReadInt32(),
				auftragsspiel_zeitAbgelaufen = reader.ReadBoolean(),
				auftragsspiel_mindestbewertung = reader.ReadInt32(),
				f2pConverted = reader.ReadBoolean()
			};
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00178EFC File Offset: 0x001770FC
		public static void _Write_mpCalls/s_Game(NetworkWriter writer, mpCalls.s_Game value)
		{
			writer.WriteInt32(value.gameID);
			writer.WriteString(value.myName);
			writer.WriteString(value.ipName);
			writer.WriteBoolean(value.playerGame);
			writer.WriteInt32(value.multiplayerSlot);
			writer.WriteBoolean(value.inDevelopment);
			writer.WriteInt32(value.developerID);
			writer.WriteInt32(value.publisherID);
			writer.WriteInt32(value.ownerID);
			writer.WriteInt32(value.engineID);
			writer.WriteSingle(value.hype);
			writer.WriteBoolean(value.isOnMarket);
			writer.WriteBoolean(value.warBeiAwards);
			writer.WriteInt32(value.weeksOnMarket);
			writer.WriteInt32(value.usk);
			writer.WriteInt32(value.freigabeBudget);
			writer.WriteInt32(value.reviewGameplay);
			writer.WriteInt32(value.reviewGrafik);
			writer.WriteInt32(value.reviewSound);
			writer.WriteInt32(value.reviewSteuerung);
			writer.WriteInt32(value.reviewTotal);
			writer.WriteInt32(value.reviewGameplayText);
			writer.WriteInt32(value.reviewGrafikText);
			writer.WriteInt32(value.reviewSoundText);
			writer.WriteInt32(value.reviewSteuerungText);
			writer.WriteInt32(value.reviewTotalText);
			writer.WriteInt32(value.date_year);
			writer.WriteInt32(value.date_month);
			writer.WriteInt32(value.date_start_year);
			writer.WriteInt32(value.date_start_month);
			writer.WriteInt64(value.sellsTotal);
			writer.WriteInt64(value.umsatzTotal);
			writer.WriteInt64(value.costs_entwicklung);
			writer.WriteInt64(value.costs_mitarbeiter);
			writer.WriteInt64(value.costs_marketing);
			writer.WriteInt64(value.costs_enginegebuehren);
			writer.WriteInt64(value.costs_server);
			writer.WriteInt64(value.costs_production);
			writer.WriteInt64(value.costs_updates);
			writer.WriteBoolean(value.typ_standard);
			writer.WriteBoolean(value.typ_nachfolger);
			writer.WriteInt32(value.originalIP);
			writer.WriteInt32(value.teile);
			writer.WriteBoolean(value.typ_contractGame);
			writer.WriteBoolean(value.typ_remaster);
			writer.WriteBoolean(value.typ_spinoff);
			writer.WriteBoolean(value.typ_addon);
			writer.WriteBoolean(value.typ_addonStandalone);
			writer.WriteBoolean(value.typ_mmoaddon);
			writer.WriteBoolean(value.typ_bundle);
			writer.WriteBoolean(value.typ_budget);
			writer.WriteBoolean(value.typ_bundleAddon);
			writer.WriteBoolean(value.typ_goty);
			writer.WriteInt32(value.originalGameID);
			writer.WriteInt32(value.portID);
			writer.WriteInt32(value.mainIP);
			writer.WriteSingle(value.ipPunkte);
			writer.WriteBoolean(value.exklusiv);
			writer.WriteBoolean(value.herstellerExklusiv);
			writer.WriteBoolean(value.retro);
			writer.WriteBoolean(value.handy);
			writer.WriteBoolean(value.arcade);
			writer.WriteBoolean(value.goty);
			writer.WriteBoolean(value.nachfolger_created);
			writer.WriteBoolean(value.remaster_created);
			writer.WriteBoolean(value.budget_created);
			writer.WriteBoolean(value.goty_created);
			writer.WriteBoolean(value.trendsetter);
			writer.WriteBoolean(value.spielbericht);
			writer.WriteInt32(value.amountUpdates);
			writer.WriteSingle(value.bonusSellsUpdates);
			writer.WriteInt32(value.amountAddons);
			writer.WriteSingle(value.bonusSellsAddons);
			writer.WriteInt32(value.amountMMOAddons);
			writer.WriteSingle(value.bonusSellsMMOAddons);
			writer.WriteSingle(value.addonQuality);
			writer.WriteInt32(value.devAktFeature);
			writer.WriteSingle(value.devPoints);
			writer.WriteSingle(value.devPointsStart);
			writer.WriteSingle(value.devPoints_Gesamt);
			writer.WriteSingle(value.devPointsStart_Gesamt);
			writer.WriteSingle(value.points_gameplay);
			writer.WriteSingle(value.points_grafik);
			writer.WriteSingle(value.points_sound);
			writer.WriteSingle(value.points_technik);
			writer.WriteSingle(value.points_bugs);
			writer.WriteString(value.beschreibung);
			writer.WriteInt32(value.gameTyp);
			writer.WriteInt32(value.gameSize);
			writer.WriteInt32(value.gameZielgruppe);
			writer.WriteInt32(value.maingenre);
			writer.WriteInt32(value.subgenre);
			writer.WriteInt32(value.gameMainTheme);
			writer.WriteInt32(value.gameSubTheme);
			writer.WriteInt32(value.gameLicence);
			writer.WriteInt32(value.gameCopyProtect);
			writer.WriteInt32(value.gameAntiCheat);
			writer.WriteInt32(value.gameAP_Gameplay);
			writer.WriteInt32(value.gameAP_Grafik);
			writer.WriteInt32(value.gameAP_Sound);
			writer.WriteInt32(value.gameAP_Technik);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameLanguage);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameGameplayFeatures);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gamePlatform);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.gameEngineFeature);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayFeatures_DevDone);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.engineFeature_DevDone);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.gameplayStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.grafikStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.soundStudio);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.motionCaptureStudio);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.bundleID);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.portExist);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.sellsPerWeek);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.verkaufspreis);
			writer.WriteInt32(value.releaseDate);
			writer.WriteInt32(value.abonnements);
			writer.WriteInt32(value.abonnementsWoche);
			writer.WriteInt32(value.aboPreis);
			writer.WriteBoolean(value.pubOffer);
			writer.WriteBoolean(value.pubAngebot);
			writer.WriteInt32(value.pubAngebot_Weeks);
			writer.WriteSingle(value.pubAngebot_Verhandlung);
			writer.WriteBoolean(value.pubAngebot_Retail);
			writer.WriteBoolean(value.pubAngebot_Digital);
			writer.WriteInt32(value.pubAngebot_Garantiesumme);
			writer.WriteSingle(value.pubAngebot_Gewinnbeteiligung);
			writer.WriteBoolean(value.auftragsspiel);
			writer.WriteInt32(value.auftragsspiel_gehalt);
			writer.WriteInt32(value.auftragsspiel_bonus);
			writer.WriteInt32(value.auftragsspiel_zeitInWochen);
			writer.WriteInt32(value.auftragsspiel_wochenAlsAngebot);
			writer.WriteBoolean(value.auftragsspiel_zeitAbgelaufen);
			writer.WriteInt32(value.auftragsspiel_mindestbewertung);
			writer.WriteBoolean(value.f2pConverted);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00179560 File Offset: 0x00177760
		public static mpCalls.s_Lizenz _Read_mpCalls/s_Lizenz(NetworkReader reader)
		{
			return new mpCalls.s_Lizenz
			{
				lizenzID = reader.ReadInt32(),
				angebot = reader.ReadInt32(),
				quality = reader.ReadSingle()
			};
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x00018D35 File Offset: 0x00016F35
		public static void _Write_mpCalls/s_Lizenz(NetworkWriter writer, mpCalls.s_Lizenz value)
		{
			writer.WriteInt32(value.lizenzID);
			writer.WriteInt32(value.angebot);
			writer.WriteSingle(value.quality);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x001795A8 File Offset: 0x001777A8
		public static mpCalls.s_Trend _Read_mpCalls/s_Trend(NetworkReader reader)
		{
			return new mpCalls.s_Trend
			{
				trendWeeks = reader.ReadInt32(),
				trendTheme = reader.ReadInt32(),
				trendAntiTheme = reader.ReadInt32(),
				trendGenre = reader.ReadInt32(),
				trendAntiGenre = reader.ReadInt32(),
				trendNextGenre = reader.ReadInt32(),
				trendNextAntiGenre = reader.ReadInt32(),
				trendNextTheme = reader.ReadInt32(),
				trendNextAntiTheme = reader.ReadInt32()
			};
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00179648 File Offset: 0x00177848
		public static void _Write_mpCalls/s_Trend(NetworkWriter writer, mpCalls.s_Trend value)
		{
			writer.WriteInt32(value.trendWeeks);
			writer.WriteInt32(value.trendTheme);
			writer.WriteInt32(value.trendAntiTheme);
			writer.WriteInt32(value.trendGenre);
			writer.WriteInt32(value.trendAntiGenre);
			writer.WriteInt32(value.trendNextGenre);
			writer.WriteInt32(value.trendNextAntiGenre);
			writer.WriteInt32(value.trendNextTheme);
			writer.WriteInt32(value.trendNextAntiTheme);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x001796C4 File Offset: 0x001778C4
		public static mpCalls.s_GameSpeed _Read_mpCalls/s_GameSpeed(NetworkReader reader)
		{
			return new mpCalls.s_GameSpeed
			{
				speed = reader.ReadInt32()
			};
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x00018D5B File Offset: 0x00016F5B
		public static void _Write_mpCalls/s_GameSpeed(NetworkWriter writer, mpCalls.s_GameSpeed value)
		{
			writer.WriteInt32(value.speed);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x001796EC File Offset: 0x001778EC
		public static mpCalls.s_Command _Read_mpCalls/s_Command(NetworkReader reader)
		{
			return new mpCalls.s_Command
			{
				command = reader.ReadInt32()
			};
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00018D69 File Offset: 0x00016F69
		public static void _Write_mpCalls/s_Command(NetworkWriter writer, mpCalls.s_Command value)
		{
			writer.WriteInt32(value.command);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00179714 File Offset: 0x00177914
		public static mpCalls.s_Save _Read_mpCalls/s_Save(NetworkReader reader)
		{
			return new mpCalls.s_Save
			{
				saveID = reader.ReadInt32()
			};
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00018D77 File Offset: 0x00016F77
		public static void _Write_mpCalls/s_Save(NetworkWriter writer, mpCalls.s_Save value)
		{
			writer.WriteInt32(value.saveID);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0017973C File Offset: 0x0017793C
		public static mpCalls.s_Load _Read_mpCalls/s_Load(NetworkReader reader)
		{
			return new mpCalls.s_Load
			{
				saveID = reader.ReadInt32()
			};
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00018D85 File Offset: 0x00016F85
		public static void _Write_mpCalls/s_Load(NetworkWriter writer, mpCalls.s_Load value)
		{
			writer.WriteInt32(value.saveID);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00179764 File Offset: 0x00177964
		public static mpCalls.s_PlayerID _Read_mpCalls/s_PlayerID(NetworkReader reader)
		{
			return new mpCalls.s_PlayerID
			{
				id = reader.ReadInt32(),
				version = reader.ReadString()
			};
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00018D93 File Offset: 0x00016F93
		public static void _Write_mpCalls/s_PlayerID(NetworkWriter writer, mpCalls.s_PlayerID value)
		{
			writer.WriteInt32(value.id);
			writer.WriteString(value.version);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0017979C File Offset: 0x0017799C
		public static mpCalls.s_PlayerInfos _Read_mpCalls/s_PlayerInfos(NetworkReader reader)
		{
			return new mpCalls.s_PlayerInfos
			{
				id = reader.ReadInt32(),
				playerName = reader.ReadString(),
				companyName = reader.ReadString(),
				logo = reader.ReadInt32(),
				country = reader.ReadInt32(),
				ready = reader.ReadBoolean()
			};
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00179810 File Offset: 0x00177A10
		public static void _Write_mpCalls/s_PlayerInfos(NetworkWriter writer, mpCalls.s_PlayerInfos value)
		{
			writer.WriteInt32(value.id);
			writer.WriteString(value.playerName);
			writer.WriteString(value.companyName);
			writer.WriteInt32(value.logo);
			writer.WriteInt32(value.country);
			writer.WriteBoolean(value.ready);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00179868 File Offset: 0x00177A68
		public static mpCalls.s_DeleteArbeitsmarkt _Read_mpCalls/s_DeleteArbeitsmarkt(NetworkReader reader)
		{
			return new mpCalls.s_DeleteArbeitsmarkt
			{
				objectID = reader.ReadInt32(),
				eingestellt = reader.ReadBoolean()
			};
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00018DAD File Offset: 0x00016FAD
		public static void _Write_mpCalls/s_DeleteArbeitsmarkt(NetworkWriter writer, mpCalls.s_DeleteArbeitsmarkt value)
		{
			writer.WriteInt32(value.objectID);
			writer.WriteBoolean(value.eingestellt);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x001798A0 File Offset: 0x00177AA0
		public static mpCalls.s_CreateArbeitsmarkt _Read_mpCalls/s_CreateArbeitsmarkt(NetworkReader reader)
		{
			return new mpCalls.s_CreateArbeitsmarkt
			{
				objectID = reader.ReadInt32(),
				male = reader.ReadBoolean(),
				myName = reader.ReadString(),
				wochenAmArbeitsmarkt = reader.ReadInt32(),
				legend = reader.ReadInt32(),
				beruf = reader.ReadInt32(),
				s_gamedesign = reader.ReadSingle(),
				s_programmieren = reader.ReadSingle(),
				s_grafik = reader.ReadSingle(),
				s_sound = reader.ReadSingle(),
				s_pr = reader.ReadSingle(),
				s_gametests = reader.ReadSingle(),
				s_technik = reader.ReadSingle(),
				s_forschen = reader.ReadSingle(),
				perks = GeneratedNetworkCode._Read_System.Boolean[](reader),
				model_body = reader.ReadInt32(),
				model_eyes = reader.ReadInt32(),
				model_hair = reader.ReadInt32(),
				model_beard = reader.ReadInt32(),
				model_skinColor = reader.ReadInt32(),
				model_hairColor = reader.ReadInt32(),
				model_beardColor = reader.ReadInt32(),
				model_HoseColor = reader.ReadInt32(),
				model_ShirtColor = reader.ReadInt32(),
				model_Add1Color = reader.ReadInt32()
			};
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00179A30 File Offset: 0x00177C30
		public static void _Write_mpCalls/s_CreateArbeitsmarkt(NetworkWriter writer, mpCalls.s_CreateArbeitsmarkt value)
		{
			writer.WriteInt32(value.objectID);
			writer.WriteBoolean(value.male);
			writer.WriteString(value.myName);
			writer.WriteInt32(value.wochenAmArbeitsmarkt);
			writer.WriteInt32(value.legend);
			writer.WriteInt32(value.beruf);
			writer.WriteSingle(value.s_gamedesign);
			writer.WriteSingle(value.s_programmieren);
			writer.WriteSingle(value.s_grafik);
			writer.WriteSingle(value.s_sound);
			writer.WriteSingle(value.s_pr);
			writer.WriteSingle(value.s_gametests);
			writer.WriteSingle(value.s_technik);
			writer.WriteSingle(value.s_forschen);
			GeneratedNetworkCode._Write_System.Boolean[](writer, value.perks);
			writer.WriteInt32(value.model_body);
			writer.WriteInt32(value.model_eyes);
			writer.WriteInt32(value.model_hair);
			writer.WriteInt32(value.model_beard);
			writer.WriteInt32(value.model_skinColor);
			writer.WriteInt32(value.model_hairColor);
			writer.WriteInt32(value.model_beardColor);
			writer.WriteInt32(value.model_HoseColor);
			writer.WriteInt32(value.model_ShirtColor);
			writer.WriteInt32(value.model_Add1Color);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00179B6C File Offset: 0x00177D6C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void InitReadWriters()
		{
			Writer<byte>.write = new Action<NetworkWriter, byte>(NetworkWriterExtensions.WriteByte);
			Writer<sbyte>.write = new Action<NetworkWriter, sbyte>(NetworkWriterExtensions.WriteSByte);
			Writer<char>.write = new Action<NetworkWriter, char>(NetworkWriterExtensions.WriteChar);
			Writer<bool>.write = new Action<NetworkWriter, bool>(NetworkWriterExtensions.WriteBoolean);
			Writer<ushort>.write = new Action<NetworkWriter, ushort>(NetworkWriterExtensions.WriteUInt16);
			Writer<short>.write = new Action<NetworkWriter, short>(NetworkWriterExtensions.WriteInt16);
			Writer<uint>.write = new Action<NetworkWriter, uint>(NetworkWriterExtensions.WriteUInt32);
			Writer<int>.write = new Action<NetworkWriter, int>(NetworkWriterExtensions.WriteInt32);
			Writer<ulong>.write = new Action<NetworkWriter, ulong>(NetworkWriterExtensions.WriteUInt64);
			Writer<long>.write = new Action<NetworkWriter, long>(NetworkWriterExtensions.WriteInt64);
			Writer<float>.write = new Action<NetworkWriter, float>(NetworkWriterExtensions.WriteSingle);
			Writer<double>.write = new Action<NetworkWriter, double>(NetworkWriterExtensions.WriteDouble);
			Writer<decimal>.write = new Action<NetworkWriter, decimal>(NetworkWriterExtensions.WriteDecimal);
			Writer<string>.write = new Action<NetworkWriter, string>(NetworkWriterExtensions.WriteString);
			Writer<byte[]>.write = new Action<NetworkWriter, byte[]>(NetworkWriterExtensions.WriteBytesAndSize);
			Writer<ArraySegment<byte>>.write = new Action<NetworkWriter, ArraySegment<byte>>(NetworkWriterExtensions.WriteBytesAndSizeSegment);
			Writer<Vector2>.write = new Action<NetworkWriter, Vector2>(NetworkWriterExtensions.WriteVector2);
			Writer<Vector3>.write = new Action<NetworkWriter, Vector3>(NetworkWriterExtensions.WriteVector3);
			Writer<Vector4>.write = new Action<NetworkWriter, Vector4>(NetworkWriterExtensions.WriteVector4);
			Writer<Vector2Int>.write = new Action<NetworkWriter, Vector2Int>(NetworkWriterExtensions.WriteVector2Int);
			Writer<Vector3Int>.write = new Action<NetworkWriter, Vector3Int>(NetworkWriterExtensions.WriteVector3Int);
			Writer<Color>.write = new Action<NetworkWriter, Color>(NetworkWriterExtensions.WriteColor);
			Writer<Color32>.write = new Action<NetworkWriter, Color32>(NetworkWriterExtensions.WriteColor32);
			Writer<Quaternion>.write = new Action<NetworkWriter, Quaternion>(NetworkWriterExtensions.WriteQuaternion);
			Writer<Rect>.write = new Action<NetworkWriter, Rect>(NetworkWriterExtensions.WriteRect);
			Writer<Plane>.write = new Action<NetworkWriter, Plane>(NetworkWriterExtensions.WritePlane);
			Writer<Ray>.write = new Action<NetworkWriter, Ray>(NetworkWriterExtensions.WriteRay);
			Writer<Matrix4x4>.write = new Action<NetworkWriter, Matrix4x4>(NetworkWriterExtensions.WriteMatrix4x4);
			Writer<Guid>.write = new Action<NetworkWriter, Guid>(NetworkWriterExtensions.WriteGuid);
			Writer<NetworkIdentity>.write = new Action<NetworkWriter, NetworkIdentity>(NetworkWriterExtensions.WriteNetworkIdentity);
			Writer<NetworkBehaviour>.write = new Action<NetworkWriter, NetworkBehaviour>(NetworkWriterExtensions.WriteNetworkBehaviour);
			Writer<Transform>.write = new Action<NetworkWriter, Transform>(NetworkWriterExtensions.WriteTransform);
			Writer<GameObject>.write = new Action<NetworkWriter, GameObject>(NetworkWriterExtensions.WriteGameObject);
			Writer<Uri>.write = new Action<NetworkWriter, Uri>(NetworkWriterExtensions.WriteUri);
			Writer<ErrorMessage>.write = new Action<NetworkWriter, ErrorMessage>(GeneratedNetworkCode._Write_Mirror.ErrorMessage);
			Writer<ReadyMessage>.write = new Action<NetworkWriter, ReadyMessage>(GeneratedNetworkCode._Write_Mirror.ReadyMessage);
			Writer<NotReadyMessage>.write = new Action<NetworkWriter, NotReadyMessage>(GeneratedNetworkCode._Write_Mirror.NotReadyMessage);
			Writer<AddPlayerMessage>.write = new Action<NetworkWriter, AddPlayerMessage>(GeneratedNetworkCode._Write_Mirror.AddPlayerMessage);
			Writer<DisconnectMessage>.write = new Action<NetworkWriter, DisconnectMessage>(GeneratedNetworkCode._Write_Mirror.DisconnectMessage);
			Writer<ConnectMessage>.write = new Action<NetworkWriter, ConnectMessage>(GeneratedNetworkCode._Write_Mirror.ConnectMessage);
			Writer<SceneMessage>.write = new Action<NetworkWriter, SceneMessage>(GeneratedNetworkCode._Write_Mirror.SceneMessage);
			Writer<SceneOperation>.write = new Action<NetworkWriter, SceneOperation>(GeneratedNetworkCode._Write_Mirror.SceneOperation);
			Writer<CommandMessage>.write = new Action<NetworkWriter, CommandMessage>(GeneratedNetworkCode._Write_Mirror.CommandMessage);
			Writer<RpcMessage>.write = new Action<NetworkWriter, RpcMessage>(GeneratedNetworkCode._Write_Mirror.RpcMessage);
			Writer<SpawnMessage>.write = new Action<NetworkWriter, SpawnMessage>(GeneratedNetworkCode._Write_Mirror.SpawnMessage);
			Writer<ObjectSpawnStartedMessage>.write = new Action<NetworkWriter, ObjectSpawnStartedMessage>(GeneratedNetworkCode._Write_Mirror.ObjectSpawnStartedMessage);
			Writer<ObjectSpawnFinishedMessage>.write = new Action<NetworkWriter, ObjectSpawnFinishedMessage>(GeneratedNetworkCode._Write_Mirror.ObjectSpawnFinishedMessage);
			Writer<ObjectDestroyMessage>.write = new Action<NetworkWriter, ObjectDestroyMessage>(GeneratedNetworkCode._Write_Mirror.ObjectDestroyMessage);
			Writer<ObjectHideMessage>.write = new Action<NetworkWriter, ObjectHideMessage>(GeneratedNetworkCode._Write_Mirror.ObjectHideMessage);
			Writer<UpdateVarsMessage>.write = new Action<NetworkWriter, UpdateVarsMessage>(GeneratedNetworkCode._Write_Mirror.UpdateVarsMessage);
			Writer<NetworkPingMessage>.write = new Action<NetworkWriter, NetworkPingMessage>(GeneratedNetworkCode._Write_Mirror.NetworkPingMessage);
			Writer<NetworkPongMessage>.write = new Action<NetworkWriter, NetworkPongMessage>(GeneratedNetworkCode._Write_Mirror.NetworkPongMessage);
			Writer<mpCalls.c_Forschung>.write = new Action<NetworkWriter, mpCalls.c_Forschung>(GeneratedNetworkCode._Write_mpCalls/c_Forschung);
			Writer<bool[]>.write = new Action<NetworkWriter, bool[]>(GeneratedNetworkCode._Write_System.Boolean[]);
			Writer<mpCalls.c_ChangeID>.write = new Action<NetworkWriter, mpCalls.c_ChangeID>(GeneratedNetworkCode._Write_mpCalls/c_ChangeID);
			Writer<mpCalls.c_AllAwards>.write = new Action<NetworkWriter, mpCalls.c_AllAwards>(GeneratedNetworkCode._Write_mpCalls/c_AllAwards);
			Writer<int[]>.write = new Action<NetworkWriter, int[]>(GeneratedNetworkCode._Write_System.Int32[]);
			Writer<mpCalls.c_Help>.write = new Action<NetworkWriter, mpCalls.c_Help>(GeneratedNetworkCode._Write_mpCalls/c_Help);
			Writer<mpCalls.c_ObjectDelete>.write = new Action<NetworkWriter, mpCalls.c_ObjectDelete>(GeneratedNetworkCode._Write_mpCalls/c_ObjectDelete);
			Writer<mpCalls.c_Object>.write = new Action<NetworkWriter, mpCalls.c_Object>(GeneratedNetworkCode._Write_mpCalls/c_Object);
			Writer<mpCalls.c_Map>.write = new Action<NetworkWriter, mpCalls.c_Map>(GeneratedNetworkCode._Write_mpCalls/c_Map);
			Writer<mpCalls.c_Trend>.write = new Action<NetworkWriter, mpCalls.c_Trend>(GeneratedNetworkCode._Write_mpCalls/c_Trend);
			Writer<mpCalls.c_Payment>.write = new Action<NetworkWriter, mpCalls.c_Payment>(GeneratedNetworkCode._Write_mpCalls/c_Payment);
			Writer<mpCalls.c_Engine>.write = new Action<NetworkWriter, mpCalls.c_Engine>(GeneratedNetworkCode._Write_mpCalls/c_Engine);
			Writer<mpCalls.c_Platform>.write = new Action<NetworkWriter, mpCalls.c_Platform>(GeneratedNetworkCode._Write_mpCalls/c_Platform);
			Writer<mpCalls.c_Chat>.write = new Action<NetworkWriter, mpCalls.c_Chat>(GeneratedNetworkCode._Write_mpCalls/c_Chat);
			Writer<mpCalls.c_Command>.write = new Action<NetworkWriter, mpCalls.c_Command>(GeneratedNetworkCode._Write_mpCalls/c_Command);
			Writer<mpCalls.c_Money>.write = new Action<NetworkWriter, mpCalls.c_Money>(GeneratedNetworkCode._Write_mpCalls/c_Money);
			Writer<mpCalls.c_PlayerInfos>.write = new Action<NetworkWriter, mpCalls.c_PlayerInfos>(GeneratedNetworkCode._Write_mpCalls/c_PlayerInfos);
			Writer<mpCalls.c_DeleteArbeitsmarkt>.write = new Action<NetworkWriter, mpCalls.c_DeleteArbeitsmarkt>(GeneratedNetworkCode._Write_mpCalls/c_DeleteArbeitsmarkt);
			Writer<mpCalls.c_BuyLizenz>.write = new Action<NetworkWriter, mpCalls.c_BuyLizenz>(GeneratedNetworkCode._Write_mpCalls/c_BuyLizenz);
			Writer<mpCalls.c_exklusivKonsolenSells>.write = new Action<NetworkWriter, mpCalls.c_exklusivKonsolenSells>(GeneratedNetworkCode._Write_mpCalls/c_exklusivKonsolenSells);
			Writer<mpCalls.c_GameData>.write = new Action<NetworkWriter, mpCalls.c_GameData>(GeneratedNetworkCode._Write_mpCalls/c_GameData);
			Writer<mpCalls.c_Game>.write = new Action<NetworkWriter, mpCalls.c_Game>(GeneratedNetworkCode._Write_mpCalls/c_Game);
			Writer<mpCalls.s_AddPlayer>.write = new Action<NetworkWriter, mpCalls.s_AddPlayer>(GeneratedNetworkCode._Write_mpCalls/s_AddPlayer);
			Writer<mpCalls.s_ChangeID>.write = new Action<NetworkWriter, mpCalls.s_ChangeID>(GeneratedNetworkCode._Write_mpCalls/s_ChangeID);
			Writer<mpCalls.s_Forschung>.write = new Action<NetworkWriter, mpCalls.s_Forschung>(GeneratedNetworkCode._Write_mpCalls/s_Forschung);
			Writer<mpCalls.s_PlayerLeave>.write = new Action<NetworkWriter, mpCalls.s_PlayerLeave>(GeneratedNetworkCode._Write_mpCalls/s_PlayerLeave);
			Writer<mpCalls.s_GenreBeliebtheit>.write = new Action<NetworkWriter, mpCalls.s_GenreBeliebtheit>(GeneratedNetworkCode._Write_mpCalls/s_GenreBeliebtheit);
			Writer<float[]>.write = new Action<NetworkWriter, float[]>(GeneratedNetworkCode._Write_System.Single[]);
			Writer<mpCalls.s_AllAwards>.write = new Action<NetworkWriter, mpCalls.s_AllAwards>(GeneratedNetworkCode._Write_mpCalls/s_AllAwards);
			Writer<mpCalls.s_GenreCombination>.write = new Action<NetworkWriter, mpCalls.s_GenreCombination>(GeneratedNetworkCode._Write_mpCalls/s_GenreCombination);
			Writer<mpCalls.s_GenreDesign>.write = new Action<NetworkWriter, mpCalls.s_GenreDesign>(GeneratedNetworkCode._Write_mpCalls/s_GenreDesign);
			Writer<mpCalls.s_Help>.write = new Action<NetworkWriter, mpCalls.s_Help>(GeneratedNetworkCode._Write_mpCalls/s_Help);
			Writer<mpCalls.s_ObjectDelete>.write = new Action<NetworkWriter, mpCalls.s_ObjectDelete>(GeneratedNetworkCode._Write_mpCalls/s_ObjectDelete);
			Writer<mpCalls.s_Object>.write = new Action<NetworkWriter, mpCalls.s_Object>(GeneratedNetworkCode._Write_mpCalls/s_Object);
			Writer<mpCalls.s_Map>.write = new Action<NetworkWriter, mpCalls.s_Map>(GeneratedNetworkCode._Write_mpCalls/s_Map);
			Writer<mpCalls.s_Office>.write = new Action<NetworkWriter, mpCalls.s_Office>(GeneratedNetworkCode._Write_mpCalls/s_Office);
			Writer<mpCalls.s_Difficulty>.write = new Action<NetworkWriter, mpCalls.s_Difficulty>(GeneratedNetworkCode._Write_mpCalls/s_Difficulty);
			Writer<mpCalls.s_Startjahr>.write = new Action<NetworkWriter, mpCalls.s_Startjahr>(GeneratedNetworkCode._Write_mpCalls/s_Startjahr);
			Writer<mpCalls.s_Spielgeschwindigkeit>.write = new Action<NetworkWriter, mpCalls.s_Spielgeschwindigkeit>(GeneratedNetworkCode._Write_mpCalls/s_Spielgeschwindigkeit);
			Writer<mpCalls.s_GlobalEvent>.write = new Action<NetworkWriter, mpCalls.s_GlobalEvent>(GeneratedNetworkCode._Write_mpCalls/s_GlobalEvent);
			Writer<mpCalls.s_EngineAbrechnung>.write = new Action<NetworkWriter, mpCalls.s_EngineAbrechnung>(GeneratedNetworkCode._Write_mpCalls/s_EngineAbrechnung);
			Writer<mpCalls.s_Awards>.write = new Action<NetworkWriter, mpCalls.s_Awards>(GeneratedNetworkCode._Write_mpCalls/s_Awards);
			Writer<mpCalls.s_Payment>.write = new Action<NetworkWriter, mpCalls.s_Payment>(GeneratedNetworkCode._Write_mpCalls/s_Payment);
			Writer<mpCalls.s_Engine>.write = new Action<NetworkWriter, mpCalls.s_Engine>(GeneratedNetworkCode._Write_mpCalls/s_Engine);
			Writer<mpCalls.s_Platform>.write = new Action<NetworkWriter, mpCalls.s_Platform>(GeneratedNetworkCode._Write_mpCalls/s_Platform);
			Writer<mpCalls.s_PlatformData>.write = new Action<NetworkWriter, mpCalls.s_PlatformData>(GeneratedNetworkCode._Write_mpCalls/s_PlatformData);
			Writer<mpCalls.s_Chat>.write = new Action<NetworkWriter, mpCalls.s_Chat>(GeneratedNetworkCode._Write_mpCalls/s_Chat);
			Writer<mpCalls.s_Money>.write = new Action<NetworkWriter, mpCalls.s_Money>(GeneratedNetworkCode._Write_mpCalls/s_Money);
			Writer<mpCalls.s_AutoPause>.write = new Action<NetworkWriter, mpCalls.s_AutoPause>(GeneratedNetworkCode._Write_mpCalls/s_AutoPause);
			Writer<mpCalls.s_Genres>.write = new Action<NetworkWriter, mpCalls.s_Genres>(GeneratedNetworkCode._Write_mpCalls/s_Genres);
			Writer<string[]>.write = new Action<NetworkWriter, string[]>(GeneratedNetworkCode._Write_System.String[]);
			Writer<mpCalls.s_GameplayFeatures>.write = new Action<NetworkWriter, mpCalls.s_GameplayFeatures>(GeneratedNetworkCode._Write_mpCalls/s_GameplayFeatures);
			Writer<mpCalls.s_EngineFeatures>.write = new Action<NetworkWriter, mpCalls.s_EngineFeatures>(GeneratedNetworkCode._Write_mpCalls/s_EngineFeatures);
			Writer<mpCalls.s_HardwareFeatures>.write = new Action<NetworkWriter, mpCalls.s_HardwareFeatures>(GeneratedNetworkCode._Write_mpCalls/s_HardwareFeatures);
			Writer<mpCalls.s_Hardware>.write = new Action<NetworkWriter, mpCalls.s_Hardware>(GeneratedNetworkCode._Write_mpCalls/s_Hardware);
			Writer<mpCalls.s_AntiCheat>.write = new Action<NetworkWriter, mpCalls.s_AntiCheat>(GeneratedNetworkCode._Write_mpCalls/s_AntiCheat);
			Writer<mpCalls.s_CopyProtect>.write = new Action<NetworkWriter, mpCalls.s_CopyProtect>(GeneratedNetworkCode._Write_mpCalls/s_CopyProtect);
			Writer<mpCalls.s_NpcEngine>.write = new Action<NetworkWriter, mpCalls.s_NpcEngine>(GeneratedNetworkCode._Write_mpCalls/s_NpcEngine);
			Writer<mpCalls.s_Firmenwert>.write = new Action<NetworkWriter, mpCalls.s_Firmenwert>(GeneratedNetworkCode._Write_mpCalls/s_Firmenwert);
			Writer<long[]>.write = new Action<NetworkWriter, long[]>(GeneratedNetworkCode._Write_System.Int64[]);
			Writer<mpCalls.s_Publisher>.write = new Action<NetworkWriter, mpCalls.s_Publisher>(GeneratedNetworkCode._Write_mpCalls/s_Publisher);
			Writer<mpCalls.s_exklusivKonsolenSells>.write = new Action<NetworkWriter, mpCalls.s_exklusivKonsolenSells>(GeneratedNetworkCode._Write_mpCalls/s_exklusivKonsolenSells);
			Writer<mpCalls.s_GameData>.write = new Action<NetworkWriter, mpCalls.s_GameData>(GeneratedNetworkCode._Write_mpCalls/s_GameData);
			Writer<mpCalls.s_Game>.write = new Action<NetworkWriter, mpCalls.s_Game>(GeneratedNetworkCode._Write_mpCalls/s_Game);
			Writer<mpCalls.s_Lizenz>.write = new Action<NetworkWriter, mpCalls.s_Lizenz>(GeneratedNetworkCode._Write_mpCalls/s_Lizenz);
			Writer<mpCalls.s_Trend>.write = new Action<NetworkWriter, mpCalls.s_Trend>(GeneratedNetworkCode._Write_mpCalls/s_Trend);
			Writer<mpCalls.s_GameSpeed>.write = new Action<NetworkWriter, mpCalls.s_GameSpeed>(GeneratedNetworkCode._Write_mpCalls/s_GameSpeed);
			Writer<mpCalls.s_Command>.write = new Action<NetworkWriter, mpCalls.s_Command>(GeneratedNetworkCode._Write_mpCalls/s_Command);
			Writer<mpCalls.s_Save>.write = new Action<NetworkWriter, mpCalls.s_Save>(GeneratedNetworkCode._Write_mpCalls/s_Save);
			Writer<mpCalls.s_Load>.write = new Action<NetworkWriter, mpCalls.s_Load>(GeneratedNetworkCode._Write_mpCalls/s_Load);
			Writer<mpCalls.s_PlayerID>.write = new Action<NetworkWriter, mpCalls.s_PlayerID>(GeneratedNetworkCode._Write_mpCalls/s_PlayerID);
			Writer<mpCalls.s_PlayerInfos>.write = new Action<NetworkWriter, mpCalls.s_PlayerInfos>(GeneratedNetworkCode._Write_mpCalls/s_PlayerInfos);
			Writer<mpCalls.s_DeleteArbeitsmarkt>.write = new Action<NetworkWriter, mpCalls.s_DeleteArbeitsmarkt>(GeneratedNetworkCode._Write_mpCalls/s_DeleteArbeitsmarkt);
			Writer<mpCalls.s_CreateArbeitsmarkt>.write = new Action<NetworkWriter, mpCalls.s_CreateArbeitsmarkt>(GeneratedNetworkCode._Write_mpCalls/s_CreateArbeitsmarkt);
			Reader<byte>.read = new Func<NetworkReader, byte>(NetworkReaderExtensions.ReadByte);
			Reader<sbyte>.read = new Func<NetworkReader, sbyte>(NetworkReaderExtensions.ReadSByte);
			Reader<char>.read = new Func<NetworkReader, char>(NetworkReaderExtensions.ReadChar);
			Reader<bool>.read = new Func<NetworkReader, bool>(NetworkReaderExtensions.ReadBoolean);
			Reader<short>.read = new Func<NetworkReader, short>(NetworkReaderExtensions.ReadInt16);
			Reader<ushort>.read = new Func<NetworkReader, ushort>(NetworkReaderExtensions.ReadUInt16);
			Reader<int>.read = new Func<NetworkReader, int>(NetworkReaderExtensions.ReadInt32);
			Reader<uint>.read = new Func<NetworkReader, uint>(NetworkReaderExtensions.ReadUInt32);
			Reader<long>.read = new Func<NetworkReader, long>(NetworkReaderExtensions.ReadInt64);
			Reader<ulong>.read = new Func<NetworkReader, ulong>(NetworkReaderExtensions.ReadUInt64);
			Reader<float>.read = new Func<NetworkReader, float>(NetworkReaderExtensions.ReadSingle);
			Reader<double>.read = new Func<NetworkReader, double>(NetworkReaderExtensions.ReadDouble);
			Reader<decimal>.read = new Func<NetworkReader, decimal>(NetworkReaderExtensions.ReadDecimal);
			Reader<string>.read = new Func<NetworkReader, string>(NetworkReaderExtensions.ReadString);
			Reader<byte[]>.read = new Func<NetworkReader, byte[]>(NetworkReaderExtensions.ReadBytesAndSize);
			Reader<ArraySegment<byte>>.read = new Func<NetworkReader, ArraySegment<byte>>(NetworkReaderExtensions.ReadBytesAndSizeSegment);
			Reader<Vector2>.read = new Func<NetworkReader, Vector2>(NetworkReaderExtensions.ReadVector2);
			Reader<Vector3>.read = new Func<NetworkReader, Vector3>(NetworkReaderExtensions.ReadVector3);
			Reader<Vector4>.read = new Func<NetworkReader, Vector4>(NetworkReaderExtensions.ReadVector4);
			Reader<Vector2Int>.read = new Func<NetworkReader, Vector2Int>(NetworkReaderExtensions.ReadVector2Int);
			Reader<Vector3Int>.read = new Func<NetworkReader, Vector3Int>(NetworkReaderExtensions.ReadVector3Int);
			Reader<Color>.read = new Func<NetworkReader, Color>(NetworkReaderExtensions.ReadColor);
			Reader<Color32>.read = new Func<NetworkReader, Color32>(NetworkReaderExtensions.ReadColor32);
			Reader<Quaternion>.read = new Func<NetworkReader, Quaternion>(NetworkReaderExtensions.ReadQuaternion);
			Reader<Rect>.read = new Func<NetworkReader, Rect>(NetworkReaderExtensions.ReadRect);
			Reader<Plane>.read = new Func<NetworkReader, Plane>(NetworkReaderExtensions.ReadPlane);
			Reader<Ray>.read = new Func<NetworkReader, Ray>(NetworkReaderExtensions.ReadRay);
			Reader<Matrix4x4>.read = new Func<NetworkReader, Matrix4x4>(NetworkReaderExtensions.ReadMatrix4x4);
			Reader<Guid>.read = new Func<NetworkReader, Guid>(NetworkReaderExtensions.ReadGuid);
			Reader<Transform>.read = new Func<NetworkReader, Transform>(NetworkReaderExtensions.ReadTransform);
			Reader<GameObject>.read = new Func<NetworkReader, GameObject>(NetworkReaderExtensions.ReadGameObject);
			Reader<NetworkIdentity>.read = new Func<NetworkReader, NetworkIdentity>(NetworkReaderExtensions.ReadNetworkIdentity);
			Reader<NetworkBehaviour>.read = new Func<NetworkReader, NetworkBehaviour>(NetworkReaderExtensions.ReadNetworkBehaviour);
			Reader<NetworkBehaviour.NetworkBehaviourSyncVar>.read = new Func<NetworkReader, NetworkBehaviour.NetworkBehaviourSyncVar>(NetworkReaderExtensions.ReadNetworkBehaviourSyncVar);
			Reader<Uri>.read = new Func<NetworkReader, Uri>(NetworkReaderExtensions.ReadUri);
			Reader<ErrorMessage>.read = new Func<NetworkReader, ErrorMessage>(GeneratedNetworkCode._Read_Mirror.ErrorMessage);
			Reader<ReadyMessage>.read = new Func<NetworkReader, ReadyMessage>(GeneratedNetworkCode._Read_Mirror.ReadyMessage);
			Reader<NotReadyMessage>.read = new Func<NetworkReader, NotReadyMessage>(GeneratedNetworkCode._Read_Mirror.NotReadyMessage);
			Reader<AddPlayerMessage>.read = new Func<NetworkReader, AddPlayerMessage>(GeneratedNetworkCode._Read_Mirror.AddPlayerMessage);
			Reader<DisconnectMessage>.read = new Func<NetworkReader, DisconnectMessage>(GeneratedNetworkCode._Read_Mirror.DisconnectMessage);
			Reader<ConnectMessage>.read = new Func<NetworkReader, ConnectMessage>(GeneratedNetworkCode._Read_Mirror.ConnectMessage);
			Reader<SceneMessage>.read = new Func<NetworkReader, SceneMessage>(GeneratedNetworkCode._Read_Mirror.SceneMessage);
			Reader<SceneOperation>.read = new Func<NetworkReader, SceneOperation>(GeneratedNetworkCode._Read_Mirror.SceneOperation);
			Reader<CommandMessage>.read = new Func<NetworkReader, CommandMessage>(GeneratedNetworkCode._Read_Mirror.CommandMessage);
			Reader<RpcMessage>.read = new Func<NetworkReader, RpcMessage>(GeneratedNetworkCode._Read_Mirror.RpcMessage);
			Reader<SpawnMessage>.read = new Func<NetworkReader, SpawnMessage>(GeneratedNetworkCode._Read_Mirror.SpawnMessage);
			Reader<ObjectSpawnStartedMessage>.read = new Func<NetworkReader, ObjectSpawnStartedMessage>(GeneratedNetworkCode._Read_Mirror.ObjectSpawnStartedMessage);
			Reader<ObjectSpawnFinishedMessage>.read = new Func<NetworkReader, ObjectSpawnFinishedMessage>(GeneratedNetworkCode._Read_Mirror.ObjectSpawnFinishedMessage);
			Reader<ObjectDestroyMessage>.read = new Func<NetworkReader, ObjectDestroyMessage>(GeneratedNetworkCode._Read_Mirror.ObjectDestroyMessage);
			Reader<ObjectHideMessage>.read = new Func<NetworkReader, ObjectHideMessage>(GeneratedNetworkCode._Read_Mirror.ObjectHideMessage);
			Reader<UpdateVarsMessage>.read = new Func<NetworkReader, UpdateVarsMessage>(GeneratedNetworkCode._Read_Mirror.UpdateVarsMessage);
			Reader<NetworkPingMessage>.read = new Func<NetworkReader, NetworkPingMessage>(GeneratedNetworkCode._Read_Mirror.NetworkPingMessage);
			Reader<NetworkPongMessage>.read = new Func<NetworkReader, NetworkPongMessage>(GeneratedNetworkCode._Read_Mirror.NetworkPongMessage);
			Reader<mpCalls.c_Forschung>.read = new Func<NetworkReader, mpCalls.c_Forschung>(GeneratedNetworkCode._Read_mpCalls/c_Forschung);
			Reader<bool[]>.read = new Func<NetworkReader, bool[]>(GeneratedNetworkCode._Read_System.Boolean[]);
			Reader<mpCalls.c_ChangeID>.read = new Func<NetworkReader, mpCalls.c_ChangeID>(GeneratedNetworkCode._Read_mpCalls/c_ChangeID);
			Reader<mpCalls.c_AllAwards>.read = new Func<NetworkReader, mpCalls.c_AllAwards>(GeneratedNetworkCode._Read_mpCalls/c_AllAwards);
			Reader<int[]>.read = new Func<NetworkReader, int[]>(GeneratedNetworkCode._Read_System.Int32[]);
			Reader<mpCalls.c_Help>.read = new Func<NetworkReader, mpCalls.c_Help>(GeneratedNetworkCode._Read_mpCalls/c_Help);
			Reader<mpCalls.c_ObjectDelete>.read = new Func<NetworkReader, mpCalls.c_ObjectDelete>(GeneratedNetworkCode._Read_mpCalls/c_ObjectDelete);
			Reader<mpCalls.c_Object>.read = new Func<NetworkReader, mpCalls.c_Object>(GeneratedNetworkCode._Read_mpCalls/c_Object);
			Reader<mpCalls.c_Map>.read = new Func<NetworkReader, mpCalls.c_Map>(GeneratedNetworkCode._Read_mpCalls/c_Map);
			Reader<mpCalls.c_Trend>.read = new Func<NetworkReader, mpCalls.c_Trend>(GeneratedNetworkCode._Read_mpCalls/c_Trend);
			Reader<mpCalls.c_Payment>.read = new Func<NetworkReader, mpCalls.c_Payment>(GeneratedNetworkCode._Read_mpCalls/c_Payment);
			Reader<mpCalls.c_Engine>.read = new Func<NetworkReader, mpCalls.c_Engine>(GeneratedNetworkCode._Read_mpCalls/c_Engine);
			Reader<mpCalls.c_Platform>.read = new Func<NetworkReader, mpCalls.c_Platform>(GeneratedNetworkCode._Read_mpCalls/c_Platform);
			Reader<mpCalls.c_Chat>.read = new Func<NetworkReader, mpCalls.c_Chat>(GeneratedNetworkCode._Read_mpCalls/c_Chat);
			Reader<mpCalls.c_Command>.read = new Func<NetworkReader, mpCalls.c_Command>(GeneratedNetworkCode._Read_mpCalls/c_Command);
			Reader<mpCalls.c_Money>.read = new Func<NetworkReader, mpCalls.c_Money>(GeneratedNetworkCode._Read_mpCalls/c_Money);
			Reader<mpCalls.c_PlayerInfos>.read = new Func<NetworkReader, mpCalls.c_PlayerInfos>(GeneratedNetworkCode._Read_mpCalls/c_PlayerInfos);
			Reader<mpCalls.c_DeleteArbeitsmarkt>.read = new Func<NetworkReader, mpCalls.c_DeleteArbeitsmarkt>(GeneratedNetworkCode._Read_mpCalls/c_DeleteArbeitsmarkt);
			Reader<mpCalls.c_BuyLizenz>.read = new Func<NetworkReader, mpCalls.c_BuyLizenz>(GeneratedNetworkCode._Read_mpCalls/c_BuyLizenz);
			Reader<mpCalls.c_exklusivKonsolenSells>.read = new Func<NetworkReader, mpCalls.c_exklusivKonsolenSells>(GeneratedNetworkCode._Read_mpCalls/c_exklusivKonsolenSells);
			Reader<mpCalls.c_GameData>.read = new Func<NetworkReader, mpCalls.c_GameData>(GeneratedNetworkCode._Read_mpCalls/c_GameData);
			Reader<mpCalls.c_Game>.read = new Func<NetworkReader, mpCalls.c_Game>(GeneratedNetworkCode._Read_mpCalls/c_Game);
			Reader<mpCalls.s_AddPlayer>.read = new Func<NetworkReader, mpCalls.s_AddPlayer>(GeneratedNetworkCode._Read_mpCalls/s_AddPlayer);
			Reader<mpCalls.s_ChangeID>.read = new Func<NetworkReader, mpCalls.s_ChangeID>(GeneratedNetworkCode._Read_mpCalls/s_ChangeID);
			Reader<mpCalls.s_Forschung>.read = new Func<NetworkReader, mpCalls.s_Forschung>(GeneratedNetworkCode._Read_mpCalls/s_Forschung);
			Reader<mpCalls.s_PlayerLeave>.read = new Func<NetworkReader, mpCalls.s_PlayerLeave>(GeneratedNetworkCode._Read_mpCalls/s_PlayerLeave);
			Reader<mpCalls.s_GenreBeliebtheit>.read = new Func<NetworkReader, mpCalls.s_GenreBeliebtheit>(GeneratedNetworkCode._Read_mpCalls/s_GenreBeliebtheit);
			Reader<float[]>.read = new Func<NetworkReader, float[]>(GeneratedNetworkCode._Read_System.Single[]);
			Reader<mpCalls.s_AllAwards>.read = new Func<NetworkReader, mpCalls.s_AllAwards>(GeneratedNetworkCode._Read_mpCalls/s_AllAwards);
			Reader<mpCalls.s_GenreCombination>.read = new Func<NetworkReader, mpCalls.s_GenreCombination>(GeneratedNetworkCode._Read_mpCalls/s_GenreCombination);
			Reader<mpCalls.s_GenreDesign>.read = new Func<NetworkReader, mpCalls.s_GenreDesign>(GeneratedNetworkCode._Read_mpCalls/s_GenreDesign);
			Reader<mpCalls.s_Help>.read = new Func<NetworkReader, mpCalls.s_Help>(GeneratedNetworkCode._Read_mpCalls/s_Help);
			Reader<mpCalls.s_ObjectDelete>.read = new Func<NetworkReader, mpCalls.s_ObjectDelete>(GeneratedNetworkCode._Read_mpCalls/s_ObjectDelete);
			Reader<mpCalls.s_Object>.read = new Func<NetworkReader, mpCalls.s_Object>(GeneratedNetworkCode._Read_mpCalls/s_Object);
			Reader<mpCalls.s_Map>.read = new Func<NetworkReader, mpCalls.s_Map>(GeneratedNetworkCode._Read_mpCalls/s_Map);
			Reader<mpCalls.s_Office>.read = new Func<NetworkReader, mpCalls.s_Office>(GeneratedNetworkCode._Read_mpCalls/s_Office);
			Reader<mpCalls.s_Difficulty>.read = new Func<NetworkReader, mpCalls.s_Difficulty>(GeneratedNetworkCode._Read_mpCalls/s_Difficulty);
			Reader<mpCalls.s_Startjahr>.read = new Func<NetworkReader, mpCalls.s_Startjahr>(GeneratedNetworkCode._Read_mpCalls/s_Startjahr);
			Reader<mpCalls.s_Spielgeschwindigkeit>.read = new Func<NetworkReader, mpCalls.s_Spielgeschwindigkeit>(GeneratedNetworkCode._Read_mpCalls/s_Spielgeschwindigkeit);
			Reader<mpCalls.s_GlobalEvent>.read = new Func<NetworkReader, mpCalls.s_GlobalEvent>(GeneratedNetworkCode._Read_mpCalls/s_GlobalEvent);
			Reader<mpCalls.s_EngineAbrechnung>.read = new Func<NetworkReader, mpCalls.s_EngineAbrechnung>(GeneratedNetworkCode._Read_mpCalls/s_EngineAbrechnung);
			Reader<mpCalls.s_Awards>.read = new Func<NetworkReader, mpCalls.s_Awards>(GeneratedNetworkCode._Read_mpCalls/s_Awards);
			Reader<mpCalls.s_Payment>.read = new Func<NetworkReader, mpCalls.s_Payment>(GeneratedNetworkCode._Read_mpCalls/s_Payment);
			Reader<mpCalls.s_Engine>.read = new Func<NetworkReader, mpCalls.s_Engine>(GeneratedNetworkCode._Read_mpCalls/s_Engine);
			Reader<mpCalls.s_Platform>.read = new Func<NetworkReader, mpCalls.s_Platform>(GeneratedNetworkCode._Read_mpCalls/s_Platform);
			Reader<mpCalls.s_PlatformData>.read = new Func<NetworkReader, mpCalls.s_PlatformData>(GeneratedNetworkCode._Read_mpCalls/s_PlatformData);
			Reader<mpCalls.s_Chat>.read = new Func<NetworkReader, mpCalls.s_Chat>(GeneratedNetworkCode._Read_mpCalls/s_Chat);
			Reader<mpCalls.s_Money>.read = new Func<NetworkReader, mpCalls.s_Money>(GeneratedNetworkCode._Read_mpCalls/s_Money);
			Reader<mpCalls.s_AutoPause>.read = new Func<NetworkReader, mpCalls.s_AutoPause>(GeneratedNetworkCode._Read_mpCalls/s_AutoPause);
			Reader<mpCalls.s_Genres>.read = new Func<NetworkReader, mpCalls.s_Genres>(GeneratedNetworkCode._Read_mpCalls/s_Genres);
			Reader<string[]>.read = new Func<NetworkReader, string[]>(GeneratedNetworkCode._Read_System.String[]);
			Reader<mpCalls.s_GameplayFeatures>.read = new Func<NetworkReader, mpCalls.s_GameplayFeatures>(GeneratedNetworkCode._Read_mpCalls/s_GameplayFeatures);
			Reader<mpCalls.s_EngineFeatures>.read = new Func<NetworkReader, mpCalls.s_EngineFeatures>(GeneratedNetworkCode._Read_mpCalls/s_EngineFeatures);
			Reader<mpCalls.s_HardwareFeatures>.read = new Func<NetworkReader, mpCalls.s_HardwareFeatures>(GeneratedNetworkCode._Read_mpCalls/s_HardwareFeatures);
			Reader<mpCalls.s_Hardware>.read = new Func<NetworkReader, mpCalls.s_Hardware>(GeneratedNetworkCode._Read_mpCalls/s_Hardware);
			Reader<mpCalls.s_AntiCheat>.read = new Func<NetworkReader, mpCalls.s_AntiCheat>(GeneratedNetworkCode._Read_mpCalls/s_AntiCheat);
			Reader<mpCalls.s_CopyProtect>.read = new Func<NetworkReader, mpCalls.s_CopyProtect>(GeneratedNetworkCode._Read_mpCalls/s_CopyProtect);
			Reader<mpCalls.s_NpcEngine>.read = new Func<NetworkReader, mpCalls.s_NpcEngine>(GeneratedNetworkCode._Read_mpCalls/s_NpcEngine);
			Reader<mpCalls.s_Firmenwert>.read = new Func<NetworkReader, mpCalls.s_Firmenwert>(GeneratedNetworkCode._Read_mpCalls/s_Firmenwert);
			Reader<long[]>.read = new Func<NetworkReader, long[]>(GeneratedNetworkCode._Read_System.Int64[]);
			Reader<mpCalls.s_Publisher>.read = new Func<NetworkReader, mpCalls.s_Publisher>(GeneratedNetworkCode._Read_mpCalls/s_Publisher);
			Reader<mpCalls.s_exklusivKonsolenSells>.read = new Func<NetworkReader, mpCalls.s_exklusivKonsolenSells>(GeneratedNetworkCode._Read_mpCalls/s_exklusivKonsolenSells);
			Reader<mpCalls.s_GameData>.read = new Func<NetworkReader, mpCalls.s_GameData>(GeneratedNetworkCode._Read_mpCalls/s_GameData);
			Reader<mpCalls.s_Game>.read = new Func<NetworkReader, mpCalls.s_Game>(GeneratedNetworkCode._Read_mpCalls/s_Game);
			Reader<mpCalls.s_Lizenz>.read = new Func<NetworkReader, mpCalls.s_Lizenz>(GeneratedNetworkCode._Read_mpCalls/s_Lizenz);
			Reader<mpCalls.s_Trend>.read = new Func<NetworkReader, mpCalls.s_Trend>(GeneratedNetworkCode._Read_mpCalls/s_Trend);
			Reader<mpCalls.s_GameSpeed>.read = new Func<NetworkReader, mpCalls.s_GameSpeed>(GeneratedNetworkCode._Read_mpCalls/s_GameSpeed);
			Reader<mpCalls.s_Command>.read = new Func<NetworkReader, mpCalls.s_Command>(GeneratedNetworkCode._Read_mpCalls/s_Command);
			Reader<mpCalls.s_Save>.read = new Func<NetworkReader, mpCalls.s_Save>(GeneratedNetworkCode._Read_mpCalls/s_Save);
			Reader<mpCalls.s_Load>.read = new Func<NetworkReader, mpCalls.s_Load>(GeneratedNetworkCode._Read_mpCalls/s_Load);
			Reader<mpCalls.s_PlayerID>.read = new Func<NetworkReader, mpCalls.s_PlayerID>(GeneratedNetworkCode._Read_mpCalls/s_PlayerID);
			Reader<mpCalls.s_PlayerInfos>.read = new Func<NetworkReader, mpCalls.s_PlayerInfos>(GeneratedNetworkCode._Read_mpCalls/s_PlayerInfos);
			Reader<mpCalls.s_DeleteArbeitsmarkt>.read = new Func<NetworkReader, mpCalls.s_DeleteArbeitsmarkt>(GeneratedNetworkCode._Read_mpCalls/s_DeleteArbeitsmarkt);
			Reader<mpCalls.s_CreateArbeitsmarkt>.read = new Func<NetworkReader, mpCalls.s_CreateArbeitsmarkt>(GeneratedNetworkCode._Read_mpCalls/s_CreateArbeitsmarkt);
		}
	}
}
