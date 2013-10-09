using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderIO;

namespace Functions
{
    class HeaderValues
    {
        IndexHeaderFunctions IHF = new IndexHeaderFunctions();

        public int[] Halo4Values(string _path)
        {
            Reader r = new Reader(_path);

            r.Position = 0x2F8;
            int virtualbase = r.ReadInt32();
            r.Position = 0x300;
            int part0 = r.ReadInt32();
            r.Position = 0x304;
            int part0sz = r.ReadInt32();
            r.Position = 0x308;
            int part1 = r.ReadInt32();
            r.Position = 0x30C;
            int part1sz = r.ReadInt32();
            r.Position = 0x310;
            int part2 = r.ReadInt32();
            r.Position = 0x314;
            int part2sz = r.ReadInt32();
            r.Position = 0x318;
            int part3 = r.ReadInt32();
            r.Position = 0x31C;
            int part3sz = r.ReadInt32();
            r.Position = 0x320;
            int part4 = r.ReadInt32();
            r.Position = 0x324;
            int part4sz = r.ReadInt32();
            r.Position = 0x328;
            int part5 = r.ReadInt32();
            r.Position = 0x32C;
            int part5sz = r.ReadInt32();

            r.Position = 0x480;
            int assetdata = r.ReadInt32();
            r.Position = 0x488;
            int localemagic = r.ReadInt32();
            r.Position = 0x48C;
            int stringmagic = r.ReadInt32();
            r.Position = 0x494;
            int alternaterawoffset = r.ReadInt32();
            r.Position = 0x498;
            int assetdatasize = r.ReadInt32();
            r.Position = 0x4A4;
            int localeindextablerawoffset = r.ReadInt32();
            r.Position = 0x4A8;
            int localetotalsize = r.ReadInt32();

            int metasize = (part0sz + part1sz + part2sz + part3sz + part4sz + part5sz);

            int[] halo4values = {
                                    virtualbase,
                                    part0,
                                    part0sz,
                                    part1,
                                    part1sz,
                                    part2,
                                    part2sz,
                                    part3,
                                    part3sz,
                                    part4,
                                    part4sz,
                                    part5,
                                    part5sz,
                                    assetdata,
                                    localemagic,
                                    stringmagic,
                                    alternaterawoffset,
                                    assetdatasize,
                                    localeindextablerawoffset,
                                    localetotalsize,
                                    metasize
                                };
            return halo4values;
        }

        public int[] Halo4StandardValues(string _path)
        {
            int halo4header = 0x1E000;
            int indexheaderaddress = AllThirdGenInt(_path)[1];
            int[] h4values = Halo4Values(_path);

            int virtualbase = h4values[0];
            int assetdata = h4values[13];
            int stringmagic = h4values[15];
            int assetdatasize = h4values[17];

            Reader r = new Reader(_path);

            r.Position = 0x160;
            int stringindextablerawoffset = r.ReadInt32();
            int stringindextableoffset = (stringindextablerawoffset + halo4header - stringmagic);
            r.Position = 0x164;
            int stringtablerawoffset = r.ReadInt32();
            int stringtableoffset = (stringtablerawoffset + halo4header - stringmagic);
            r.Position = 0x2C0;
            int tagindextablerawoffset = r.ReadInt32();
            int tagindextableoffset = (tagindextablerawoffset + halo4header - stringmagic);
            r.Position = 0x2B8;
            int tagdatatablerawoffset = r.ReadInt32();
            int tagdatatableoffset = (tagdatatablerawoffset + halo4header - stringmagic);

            int metaoff = (assetdata + assetdatasize);

            int tagmask = (virtualbase - metaoff);

            int indexheaderoffset = (indexheaderaddress - tagmask);

            int[] h4standardvalues = {
                                    metaoff,
                                    stringindextableoffset,
                                    stringtableoffset,
                                    tagindextableoffset,
                                    tagdatatableoffset,
                                    assetdata,
                                    tagmask,
                                    indexheaderoffset
                                };
            return h4standardvalues;
        }

        public int[] Halo4NoRawValues(string _path)
        {
            int indexheaderaddress = AllThirdGenInt(_path)[1];
            int[] h4values = Halo4Values(_path);

            int virtualbase = h4values[0];
            int alternaterawoffset = h4values[16];

            Reader r = new Reader(_path);

            r.Position = 0x14;
            int metaoff = r.ReadInt32();

            r.Position = 0x160;
            int stringindextableoffset = r.ReadInt32();
            r.Position = 0x164;
            int stringtableoffset = r.ReadInt32();
            r.Position = 0x2C0;
            int tagindextableoffset = r.ReadInt32();
            r.Position = 0x2B8;
            int tagdatatableoffset = r.ReadInt32();

            int tagmask = (virtualbase - metaoff);

            int indexheaderoffset = (indexheaderaddress - tagmask);

            int[] h4norawvalues = {
                                    metaoff,
                                    stringindextableoffset,
                                    stringtableoffset,
                                    tagindextableoffset,
                                    tagdatatableoffset,
                                    alternaterawoffset,
                                    tagmask,
                                    indexheaderoffset
                                };
            return h4norawvalues;
            
        }

        public int[] HeaderSpecificValuesNoH4(string _path, int _header)
        {
            int[] commonnoh4 = CommonNoH4(_path);
            int stringmagic = commonnoh4[15];

            Reader r = new Reader(_path);

            r.Position = 0x160;
            int stringindextablerawoffset = r.ReadInt32();
            int stringindextableoffset = (stringindextablerawoffset + _header - stringmagic);

            r.Position = 0x164;
            int stringtablerawoffset = r.ReadInt32();
            int stringtableoffset = (stringtablerawoffset + _header - stringmagic);

            r.Position = 0x2C0;
            int tagindextablerawoffset = r.ReadInt32();
            int tagindextableoffset = (tagindextablerawoffset + _header - stringmagic);

            r.Position = 0x2B8;
            int tagdatatablerawoffset = r.ReadInt32();
            int tagdatatableoffset = (tagdatatablerawoffset + _header - stringmagic);

            int[] headerspecificvals = {
                                           stringindextableoffset,
                                           stringtableoffset,
                                           tagindextableoffset,
                                           tagdatatableoffset
                                       };
            return headerspecificvals;
        }

        public int[] CommonNoH4(string _path)
        {
            int indexheaderaddress = AllThirdGenInt(_path)[1];

            Reader r = new Reader(_path);

            r.Position = 0x2E8;
            int virtualbase = r.ReadInt32();
            r.Position = 0x2F0;
            int part0 = r.ReadInt32();
            r.Position = 0x2F4;
            int part0sz = r.ReadInt32();
            r.Position = 0x2F8;
            int part1 = r.ReadInt32();
            r.Position = 0x2FC;
            int part1sz = r.ReadInt32();
            r.Position = 0x300;
            int part2 = r.ReadInt32();
            r.Position = 0x304;
            int part2sz = r.ReadInt32();
            r.Position = 0x308;
            int part3 = r.ReadInt32();
            r.Position = 0x30C;
            int part3sz = r.ReadInt32();
            r.Position = 0x310;
            int part4 = r.ReadInt32();
            r.Position = 0x314;
            int part4sz = r.ReadInt32();
            r.Position = 0x318;
            int part5 = r.ReadInt32();
            r.Position = 0x31C;
            int part5sz = r.ReadInt32();

            r.Position = 0x470;
            int assetdata = r.ReadInt32();
            r.Position = 0x478;
            int localemagic = r.ReadInt32();
            r.Position = 0x47C;
            int stringmagic = r.ReadInt32();
            r.Position = 0x488;
            int assetdatasize = r.ReadInt32();
            r.Position = 0x494;
            int localeindextablerawoffset = r.ReadInt32();
            r.Position = 0x498;
            int localetotalsize = r.ReadInt32();

            int metaoff = (assetdata + assetdatasize);
            int metasize = (part0sz + part1sz + part2sz + part3sz + part4sz + part5sz);
            int tagmask = (virtualbase - metaoff);
            int indexheaderoffset = (indexheaderaddress - tagmask);
            int localeindextableoffset = (localeindextablerawoffset + localemagic);

            int[] commonnoh4 = {
                                   virtualbase,
                                   part0,
                                   part0sz,
                                   part1,
                                   part1sz,
                                   part2,
                                   part2sz,
                                   part3,
                                   part3sz,
                                   part4,
                                   part4sz,
                                   part5,
                                   part5sz,
                                   assetdata,
                                   localemagic,
                                   stringmagic,
                                   assetdatasize,
                                   localeindextablerawoffset,
                                   localetotalsize,
                                   metaoff,
                                   metasize,
                                   tagmask,
                                   indexheaderoffset,
                                   localeindextableoffset
                               };
            return commonnoh4;
        }

        public int[] CommonValues(string _path)
        {
            Reader r = new Reader(_path);

            r.Position = 0x168;
            byte engine = r.ReadByte();

            //stringids
            r.Position = 0x158;
            int stringcount = r.ReadInt32();
            int stringindextablesize = (stringcount * 4);

            r.Position = 0x15C;
            int stringtablesize = r.ReadInt32();

            //tagnames
            r.Position = 0x2B4;
            int tagnamecount = r.ReadInt32();
            int tagnamecountsize = (tagnamecount * 4);

            r.Position = 0x2BC;
            int tagtablesize = r.ReadInt32();

            int[] commonvalues = {
                                     engine,
                                     stringcount,
                                     stringindextablesize,
                                     stringtablesize,
                                     tagnamecount,
                                     tagnamecountsize,
                                     tagtablesize,
                                 };
            return commonvalues;
        }

        public string[] CommonStrings(string _path)
        {
            Reader r = new Reader(_path);

            r.Position = 0x18C;
            string Internal = r.ReadString(0x20);

            r.Position = 0x1B0;
            string Scenario = r.ReadString(0x100);

            string[] strings = {
                                   Internal,
                                   Scenario
                               };
            return strings;
        }

        public string[] H3BStrings(string _path)
        {
            Reader r = new Reader(_path);

            r.Position = 0x194;
            string Internal = r.ReadString(0x20);

            r.Position = 0x1B8;
            string Scenario = r.ReadString(0x100);

            string[] strings = {
                                   Internal,
                                   Scenario
                               };
            return strings;
        }

        public int[] H3BTags(string _path)
        {
            int[] allthirdgen = AllThirdGenInt(_path);

            int indexheaderaddress = allthirdgen[1];

            Reader r = new Reader(_path);

            r.Position = 0x14;
            int metaoff = r.ReadInt32();

            r.Position = 0x2F0;
            int virtualbase = r.ReadInt32();

            int tagmask = (virtualbase - metaoff);
            int indexheaderoffset = (indexheaderaddress - tagmask);

            int[] indexheadervalues = IHF.IndexHeaderValues(_path, indexheaderoffset, tagmask);

            int classcount = indexheadervalues[0];
            int classsize = indexheadervalues[1];
            int classoff = indexheadervalues[2];
            int tagcount = indexheadervalues[3];
            int tagindexsize = indexheadervalues[4];
            int tagtableoff = indexheadervalues[5];
            int imptagcount = indexheadervalues[6];
            int imptagsoff = indexheadervalues[7];

            int[] h3btags = {
                                tagmask,
                                indexheaderoffset,
                                metaoff,
                                virtualbase,
                                classcount,
                                classsize,
                                classoff,
                                tagcount,
                                tagindexsize,
                                tagtableoff,
                                imptagcount,
                                imptagsoff
                            };
            return h3btags;
        }

        public int[] H3BValues(string _path)
        {
            int[] allthirdgen = AllThirdGenInt(_path);

            int filesize = allthirdgen[0];
            int indexheaderaddress = allthirdgen[1];
            int type = allthirdgen[2];

            Reader r = new Reader(_path);

            r.Position = 0x14;
            int metaoff = r.ReadInt32();

            r.Position = 0x2F0;
            int virtualbase = r.ReadInt32();

            r.Position = 0x2F8;
            int part0 = r.ReadInt32();

            r.Position = 0x2FC;
            int part0sz = r.ReadInt32();

            r.Position = 0x300;
            int part1 = r.ReadInt32();

            r.Position = 0x304;
            int part1sz = r.ReadInt32();

            r.Position = 0x308;
            int part2 = r.ReadInt32();

            r.Position = 0x30C;
            int part2sz = r.ReadInt32();

            int metasize = (part0sz + part1sz + part2sz);
            int tagmask = (virtualbase - metaoff);

            //stringids
            r.Position = 0x160;
            int stringcount = r.ReadInt32();
            int stringindextablesize = (stringcount * 4);

            r.Position = 0x164;
            int stringtablesize = r.ReadInt32();

            r.Position = 0x168;
            int stringindextableoffset = r.ReadInt32();

            r.Position = 0x16C;
            int stringtableoffset = r.ReadInt32();

            //tagnames
            r.Position = 0x2BC;
            int tagnamecount = r.ReadInt32();
            int tagnamecountsize = (tagnamecount * 4);

            r.Position = 0x2C4;
            int tagtablesize = r.ReadInt32();

            r.Position = 0x2C8;
            int tagindextableoffset = r.ReadInt32();

            r.Position = 0x2C0;
            int tagdatatableoffset = r.ReadInt32();

            int[] h3bvalues = {
                                  filesize,
                                  indexheaderaddress,
                                  part0,
                                  part0sz,
                                  part1,
                                  part1sz,
                                  part2,
                                  part2sz,
                                  metasize,
                                  stringcount,
                                  stringindextablesize,
                                  stringtablesize,
                                  stringindextableoffset,
                                  stringtableoffset,
                                  tagnamecount,
                                  tagnamecountsize,
                                  tagtablesize,
                                  tagindextableoffset,
                                  tagdatatableoffset,
                                  type
                              };
            return h3bvalues;
        }

        public int[] AllThirdGenInt(string _path)
        {
            Reader r = new Reader(_path);

            r.Position = 0x8;
            int filesize = r.ReadInt32();

            r.Position = 0x10;
            int indexheaderaddress = r.ReadInt32();

            r.Position = 0x13C;
            short type = r.ReadInt16();

            int[] allthirdgen = {
                                    filesize,
                                    indexheaderaddress,
                                    type
                                };
            return allthirdgen;
        }
    }
}
