using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing.CC
{
	public partial class CCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://rewiki.regengedanken.de/wiki/.CC");
			}
			return _dfr;
		}

		private byte mvarKey = 0x35;
		/// <summary>
		/// The key to use to encrypt the data.
		/// </summary>
		public byte Key { get { return mvarKey; } set { mvarKey = value; } }

		#region File name table
		private string[] possibleFileNames = new string[]
		{
			"adlib.drv", "archer.mon", "award.bin", "back.raw", "ballface.mon", "bank.icn", "bank.m",
			"bank.out", "bank2.icn", "barbaran.mon", "bat.brd", "bat.mon", "beetle.mon", "beholder.mon",
			"blaster.drv", "blknight.mon", "bublman.mon", "bugeye.mon", "buy.icn", "candle.mon", "cast.icn",
			"castle.til", "cataplr.mon", "cave.til", "caves.m", "charpow.icn", "city.m", "cleric.mon", "cobra.mon", 
			"combat.icn", "combat.m", "comet.vga", "comf1.vga", "comf2.vga", "comf3.vga", "comf4.vga", "comf5.vga",
			"computer.raw", "confirm.icn", "confirm2.icn", "control.raw", "corak.bin", "covox.drv", "cpanel.icn", "cr1.vga",
			"cr2.vga", "cr3.vga", "cr4.vga", "cr5.vga", "create.icn", "create.raw", "cris.mon", "cyber.m", "cyclop.mon",
			"day.vga", "death.vga", "demo.drv", "demon.mon", "desert.vga", "detctmon.icn", "detect.icn", "devil.mon",
			"dice.vga", "dino.mon", "dirt.vga", "draglord.mon", "dse.fac", "dthlocus.mon", "dtree.vga", "dung.til", "dungeon.m",
			"duplicat.icn", "dwarf.mon", "eerie.m", "eface01.out", "eface02.out", "eface03.out", "eface04.out", "eface05.out",
			"eface06.out", "eface07.out", "eface08.out", "eface09.out", "element.icn", "equip.icn", "esc.icn", "fba1.vga", "fba2.vga",
			"fbb1.vga", "fbb2.vga", "fca1.vga", "fca2.vga", "fcb1.vga", "fcb2.vga", "fda1.vga", "fdb1.vga", "fecp.brd", "firelzrd.mon",
			"firemon.mon", "flytrap.mon", "frame.out", "frnt.vga", "front.raw", "gargoyle.brd", "gargoyle.mon", "gatekepr.mon", "ghost.mon",
			"ghoul.mon", "global.icn", "goblin.mon", "grabber.brd", "grndrgn.mon", "grounds.m", "guild.m", "guild1.out", "guild2.out", "hamr.mon",
			"hand.mon", "head.mon", "higrass.vga", "hire0.fac", "hire1.fac", "hire2.fac", "hire3.fac", "hire4.fac", "hire5.fac", "hire6.fac",
			"hire7.fac", "hire8.fac", "hire9.fac", "honky.m", "hpbars.icn", "hud.raw", "hud.vga", "hydra.mon", "ibm.drv", "ina.vga", "inb.vga",
			"inc.vga", "ind.vga", "inh.raw", "inn.icn", "inn.out", "itit0.vga", "itit1.vga", "itit2.vga", "itit3.vga", "itit4.vga", "itit5.vga",
			"itit6.vga", "itit7.vga", "itit8.vga", "itxt.vga", "jester.mon", "lava.vga", "lbut.vga", "led.vga", "lich.mon", "lizard.mon", "lloyds.icn",
			"logy5.raw", "ltree.vga", "lytp1.vga", "lytp2.vga", "lytp3.vga", "lyts1.vga", "lyts2.vga", "lyts3.vga", "lyts4.vga", "lyts5.vga", "lyts6.vga",
			"lyts7.vga", "lyts8.vga", "lyts9.vga", "main.icn", "mantis.mon", "martface.mon", "medieval.m", "medusa.mon", "mess.vga", "minidrgn.mon", "minotaur.mon",
			"mm3theme.m", "MonAC.dat", "MonAcid.dat", "MonAttP.dat", "MonCold.dat", "MonDmgN.dat", "MonDmgS.dat", "MonDmgT.dat", "MonElec.dat", "MonEner.dat",
			"MonExp.dat", "MonFire.dat", "MonGems.dat", "MonGold.dat", "MonHitB.dat", "MonHP.dat", "MonMagi.dat", "MonNumA.dat", "MonPhys.dat", "MonRang.dat",
			"MonSpd.dat", "MonSpec.dat", "MonTrea.dat", "mount.vga", "mouse.icn", "mummy.mon", "night.vga", "ninja.mon", "octobest.mon", "orc.mon", "out.til",
			"out0.spl", "out1.spl", "out2.spl", "out3.spl", "outa.vga", "outb.vga", "outc.vga", "outd.vga", "outh.raw", "paladin.mon", "palms.vga", "pan.vga",
			"pegasus.mon", "phantom.mon", "pirana.mon", "plasmoid.mon", "pow0.icn", "pow10.icn", "pow11.icn", "pow12.icn", "pow13.icn", "pow14.icn", "pow2.icn",
			"pow3.icn", "pow4.icn", "pow5.icn", "pow7.icn", "pow8.icn", "pow9.icn", "powsorc.mon", "protect.icn", "quest.bin", "ranger.mon", "rat.mon", "reaper.mon",
			"reg.bin", "repthed.mon", "restore.icn", "road.vga", "robo.mon", "robo2.mon", "roc.mon", "roland.drv", "scifi.til", "scn1.vga", "scn2.vga", "scon.vga",
			"scor.vga", "scorpia.mon", "scroll.icn", "sell.icn", "send.vga", "skel.mon", "snomtn.vga", "snotree.vga", "snow.vga", "sonicnja.mon", "sorc.mon",
			"spfx1.icn", "spfx2.icn", "spfx3.icn", "spfx4.icn", "spfx5.icn", "spfx6.icn", "spfx7.icn", "spfx8.icn", "spfx9.icn", "spider.mon", "sprite.mon",
			"spldesc.bin", "stars.raw", "start.icn", "swmtree.vga", "takb1.vga", "takb2.vga", "take.raw", "taks1.vga", "taks2.vga", "taks3.vga", "talk.vga",
			"tandy.drv", "tavern.bin", "tavern.out", "temple.out", "temples.m", "termnatr.mon", "text01.maz", "text02.maz", "text03.maz", "text04.maz", "text05.maz",
			"text06.maz", "text07.maz", "text08.maz", "text09.maz", "text10.maz", "text11.maz", "text12.maz", "text13.maz", "text14.maz", "text15.maz", "text16.maz",
			"text17.maz", "text18.maz", "text19.maz", "text20.maz", "text21.maz", "text22.maz", "text23.maz", "text24.maz", "text25.maz", "text26.maz", "text27.maz",
			"text28.maz", "text29.maz", "text30.maz", "text31.maz", "text32.maz", "text33.maz", "text34.maz", "text35.maz", "text36.maz", "text37.maz", "text38.maz",
			"text39.maz", "text40.maz", "text41.maz", "text42.maz", "text43.maz", "text44.maz", "text45.maz", "text46.maz", "text47.maz", "text48.maz", "text49.maz",
			"text50.maz", "text51.maz", "text52.maz", "text53.maz", "text54.maz", "text55.maz", "text56.maz", "text57.maz", "text58.maz", "text59.maz", "text60.maz",
			"text61.maz", "text62.maz", "text63.maz", "text64.maz", "thief.mon", "topa1.vga", "topa2.vga", "toph.vga", "topi.vga", "topj.vga", "topk.vga", "topsea.raw",
			"town.til", "towninn.m", "train.icn", "train1.out", "train2.out", "train3.out", "train4.out", "treeglum.mon", "troll.mon", "txt1.vga", "txt2.vga", "txt3.vga",
			"txt4.vga", "txt5.vga", "undragon.mon", "vampire.mon", "venture.m", "view.icn", "water.vga", "weapon.out", "werewolf.mon", "witch.mon", "wizard.mon", "worm.mon",
			"zombie.mon"
		};
		#endregion

		private Dictionary<ushort, string> HashFileNames = new Dictionary<ushort, string>();
		public CCDataFormat()
		{
			try
			{
				foreach (string filename in possibleFileNames)
				{
					ushort hash = GetFileNameHash(filename);
					HashFileNames.Add(hash, filename);
				}
			}
			catch (Exception ex)
			{
			}
		}

		private ushort GetFileNameHash(string filename)
		{
			byte h0 = 0, h1 = 0;
			byte[] filenamebytes = System.Text.Encoding.ASCII.GetBytes(filename);
			for (int i = 0; i < filenamebytes.Length; i++)
			{
				// swap h0 and h1
				byte h2 = h1;
				h1 = h0;
				h0 = h2;

				// rotate left
				h2 = (byte)((((uint)h0 << 1) & 0xff) | (((uint)h1 >> 7) & 0xff));
				byte h3 = (byte)((((uint)h1 << 1) & 0xff) | (((uint)h0 >> 7) & 0xff));
				h0 = h2;
				h1 = h3;
				// calc toUpper(h1)
				h2 = (byte)(filenamebytes[i] & 0x7f);
				if (h2 >= 0x60)
				{
					h2 = (byte)(h2 - 0x20);
				}
				// add char to h1
				uint temp = (((uint)h0 << 8) | (uint)h1);
				temp = (temp + h2) & 0xffff;
				h0 = (byte)((temp >> 8) & 0xff);
				h1 = (byte)(temp & 0xff);
			}
			return (ushort)(h1 | (h0 << 8));
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			ushort filecount = br.ReadUInt16();

			byte[] headerData = br.ReadBytes(2 + (filecount * 8));
			headerData = DecodeHeader(headerData);

			IO.Reader brh = new IO.Reader(new MemoryAccessor(headerData));

			for (ushort i = 0; i < filecount; i++)
			{
				ushort hash = brh.ReadUInt16();

				string filename = "FILE" + i.ToString().PadLeft(4, '0');
				if (HashFileNames.ContainsKey(hash))
				{
					filename = HashFileNames[hash];
				}

				uint offset = brh.ReadUInt24();
				ushort length = brh.ReadUInt16();
				byte nul = brh.ReadByte();

				File file = fsom.AddFile(filename);
				file.Source = new EmbeddedFileSource(brh, (long)offset, (long)length, new FileSourceTransformation[]
				{
					new FileSourceTransformation(FileSourceTransformationType.Output, delegate(System.IO.Stream inputStream, System.IO.Stream outputStream)
					{
						StreamAccessor saInput = new StreamAccessor(inputStream);
						StreamAccessor saOutput = new StreamAccessor(outputStream);

						Reader reader = new Reader(saInput);
						Writer writer = new Writer(saOutput);

						while (!reader.EndOfStream)
						{
							byte data = reader.ReadByte();
							data ^= mvarKey;
							writer.WriteByte(data);
						}
					})
				});
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}

		private byte[] DecodeHeader(byte[] data)
		{
			byte al = 0xAC, ah = 0xAC;
			byte[] output = (data.Clone() as byte[]);
			for (int i = 0; i < output.Length; i++)
			{
				al = output[i];
				al = (byte)((((int)al << 2) | ((int)al >> 6)) & 0xff); // assembly: ROL al,2
				al = (byte)((al + ah) & 0xff);
				output[i] = al;
				ah = (byte)((ah + 0x67) & 0xff);
			}
			return output;
		}
	}
}
