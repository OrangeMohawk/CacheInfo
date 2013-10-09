using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderIO;
using Functions;
using BadBuild;

namespace CacheInfo
{
    public partial class MainForm : Form
    {
        IndexHeaderFunctions IHF = new IndexHeaderFunctions();
        HeaderValues HV = new HeaderValues();
        LocaleLanguageInfo LOC = new LocaleLanguageInfo();

        public MainForm()
        {
            InitializeComponent();
        }

        public int halo3header = 0x3000;
        public int reachbetaheader = 0x4000;
        public int reachheader = 0xA000;
        public int halo4header = 0x1E000;

        string na = "N/A";
        string o = "0x";
        string path = "";

        string halo3betabuild = "09699.07.05.01.1534.delta";

        string halo3build = "11855.07.08.20.2317.halo3_ship";
        string halo3reviewbuild = "11856.07.08.20.2332.release";
        string halo3epsilonbuild = "11729.07.08.10.0021.main";
        string halo3mythicbuild = "12065.08.08.26.0819.halo3_ship";

        string halo3odstbuild = "13895.09.04.27.2201.atlas_relea";

        string reachprebetabuild = "09449.10.03.25.1545.omaha_beta";
        string reachbetabuild = "09730.10.04.09.1309.omaha_delta";
        string reachbuild = "00095.11.04.09.1509.demo";
        string reachdemobuild = "11860.10.07.24.0147.omaha_relea";

        string halo4build = "20810.12.09.22.1647.main";
        string halo4tu2build = "21122.12.11.21.0101.main";
        string halo4tu3build = "21165.12.12.12.0112.main";
        string halo4tu4build = "21339.13.02.05.0117.main";
        string halo4tu5build = "21391.13.03.13.1711.main";
        string halo4tu6build = "21401.13.04.23.1849.main";
        string halo4tu7build = "21501.13.08.06.2311.main";

        public void H3BValues()
        {
            try
            {
                string _path = directoryBox.Text;

                int[] h3btags = HV.H3BTags(_path);
                int[] h3bvalues = HV.H3BValues(_path);
                string[] h3bstrings = HV.H3BStrings(_path);

                string Internal = h3bstrings[0];
                string Scenario = h3bstrings[1];

                int tagmask = h3btags[0];
                int indexheaderoffset = h3btags[1];
                int metaoff = h3btags[2];
                int virtualbase = h3btags[3];
                int classcount = h3btags[4];
                int classsize = h3btags[5];
                int classtableoff = h3btags[6];
                int tagcount = h3btags[7];
                int tagindexsize = h3btags[8];
                int tagtableoff = h3btags[9];
                int imptagcount = h3btags[10];
                int imptagsoff = h3btags[11];

                int filesize = h3bvalues[0];
                int indexheaderaddress = h3bvalues[1];
                int part0 = h3bvalues[2];
                int part0sz = h3bvalues[3];
                int part1 = h3bvalues[4];
                int part1sz = h3bvalues[5];
                int part2 = h3bvalues[6];
                int part2sz = h3bvalues[7];
                int metasize = h3bvalues[8];
                int stringcount = h3bvalues[9];
                int stringindextablesize = h3bvalues[10];
                int stringtablesize = h3bvalues[11];
                int stringindextableoffset = h3bvalues[12];
                int stringtableoffset = h3bvalues[13];
                int tagnamecount = h3bvalues[14];
                int tagnamecountsize = h3bvalues[15];
                int tagtablesize = h3bvalues[16];
                int tagindextableoffset = h3bvalues[17];
                int tagdatatableoffset = h3bvalues[18];
                int type = h3bvalues[19];

                int matgoffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 4, tagmask);
                int[] h3blocales = LOC.GetH3BLoc(_path, matgoffset);
                int localestart = h3blocales[3];
                int lastlocaletableoff = h3blocales[45];
                int lastlocaletablesz = h3blocales[46];

                sizeText.Paste(o + filesize.ToString("X"));
                stringmaskText.Paste(na);
                tagmaskText.Paste(o + tagmask.ToString("X"));
                localemaskText.Paste(na);
                engineText.Paste(na);

                if (type == 0x0)
                {
                    typeText.Paste("Single Player");
                }
                if (type == 0x1)
                {
                    typeText.Paste("Multiplayer");
                }
                if (type == 0x2)
                {
                    typeText.Paste("Main Menu");
                }
                if (type == 0x3)
                {
                    typeText.Paste("Shared (Shared)");
                }
                if (type == 0x4)
                {
                    typeText.Paste("Shared (Campaign)");
                }
                if (type != 0x0 && type != 0x1 && type != 0x2 && type != 0x3 && type != 0x4)
                {
                    typeText.Paste("Unknown Type!");
                }

                internalText.Paste(Internal);

                scenarioText.Paste(Scenario);

                //MapID
                if (internalText.Text == "deadlock")
                {
                    mapidText.Paste("310");
                }
                if (internalText.Text == "riverworld")
                {
                    mapidText.Paste("340");
                }
                if (internalText.Text == "snowbound")
                {
                    mapidText.Paste("360");
                }
                if (internalText.Text != "deadlock" && internalText.Text != "riverworld" && internalText.Text != "snowbound")
                {
                    mapidText.Paste(na);
                }

                //Meta
                metaText.Paste(o + metaoff.ToString("X"));
                metaszText.Paste(o + metasize.ToString("X"));
                part0Text.Paste(o + part0.ToString("X"));
                partsz0Text.Paste(o + part0sz.ToString("X"));
                part1Text.Paste(o + part1.ToString("X"));
                partsz1Text.Paste(o + part1sz.ToString("X"));
                part2Text.Paste(o + part2.ToString("X"));
                partsz2Text.Paste(o + part2sz.ToString("X"));
                part3Text.Paste(na);
                partsz3Text.Paste(o + "0");
                part4Text.Paste(na);
                partsz4Text.Paste(o + "0");
                part5Text.Paste(na);
                partsz5Text.Paste(o + "0");

                //StringIDs
                stringcountText.Paste(stringcount.ToString());
                stringindtableszText.Paste(o + stringindextablesize.ToString("X"));
                stringtablesizeText.Paste(o + stringtablesize.ToString("X"));
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));

                //Tag Names
                tagnamecountText.Paste(tagnamecount.ToString());
                tagnameindtableszText.Paste(o + tagnamecountsize.ToString("X"));
                tagnametablesizeText.Paste(o + tagtablesize.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));

                if (indexheaderaddress != 0)
                {
                    //tags
                    indexheadText.Paste(o + indexheaderoffset.ToString("X"));
                    indexheadaddText.Paste(o + indexheaderaddress.ToString("X"));

                    tagclassText.Paste(classcount.ToString());
                    tagclasssizeText.Paste(o + classsize.ToString("X"));
                    tagclasstableText.Paste(o + classtableoff.ToString("X"));
                    tagcountText.Paste(tagcount.ToString());
                    tagtablesizeText.Paste(o + tagindexsize.ToString("X"));
                    tagindextableText.Paste(o + tagtableoff.ToString("X"));

                    if (lastlocaletablesz != 0)
                    {
                        int localesize = ((lastlocaletableoff + lastlocaletablesz) + 5 * 0x800 / 10) / 0x800 * 0x800;
                        int rawoff = (localesize + localestart);

                        //locales
                        localetotalsizeText.Paste(o + localesize.ToString("X"));
                        localedataoffsetText.Paste(o + localestart.ToString("X"));

                        //raw
                        rawText.Paste(o + rawoff.ToString("X"));
                        rawszText.Paste(o + (metaoff - rawoff).ToString("X"));
                    }
                    if (lastlocaletablesz == 0)
                    {
                        int localesize = (lastlocaletableoff - localestart);
                        int rawoff = (localesize + localestart);

                        //locales
                        localetotalsizeText.Paste(o + localesize.ToString("X"));
                        localedataoffsetText.Paste(o + localestart.ToString("X"));

                        //raw
                        rawText.Paste(o + rawoff.ToString("X"));
                        rawszText.Paste(o + (metaoff - rawoff).ToString("X"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
        public void AllAftH3BValues()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] commonvalues = HV.AllThirdGenInt(_path);
                int filesize = commonvalues[0];
                int indexheaderaddress = commonvalues[1];
                int type = commonvalues[2];

                string[] strings = HV.CommonStrings(_path);
                string Internal = strings[0];
                string Scenario = strings[1];

                int[] commonvals = HV.CommonValues(_path);
                int engine = commonvals[0];
                int stringcount = commonvals[1];
                int stringindextablesize = commonvals[2];
                int stringtablesize = commonvals[3];
                int tagnamecount = commonvals[4];
                int tagnamecountsize = commonvals[5];
                int tagtablesize = commonvals[6];

                if (type == 0x0)
                {
                    typeText.Paste("Single Player");
                }
                if (type == 0x1)
                {
                    typeText.Paste("Multiplayer");
                }
                if (type == 0x2)
                {
                    typeText.Paste("Main Menu");
                }
                if (type == 0x3)
                {
                    typeText.Paste("Shared (Shared)");
                }
                if (type == 0x4)
                {
                    typeText.Paste("Shared (Campaign)");
                }
                if (type != 0x0 && type != 0x1 && type != 0x2 && type != 0x3 && type != 0x4)
                {
                    typeText.Paste("Unknown Type!");
                }

                if (engine == 0x0)
                {
                    engineText.Paste("None");
                }
                if (engine == 0x2 || engine == 0x3)
                {
                    engineText.Paste("Multiplayer");
                }
                if (engine == 0x6 || engine == 0x7)
                {
                    engineText.Paste("Single Player");
                }
                if (engine != 0x0 && engine != 0x2 && engine != 0x3 && engine != 0x6 && engine != 0x7)
                {
                    engineText.Paste("Unknown Engine!");
                }

                indexheadaddText.Paste(o + indexheaderaddress.ToString("X"));
                internalText.Paste(Internal);
                scenarioText.Paste(Scenario);
                sizeText.Paste(o + filesize.ToString("X"));
                stringcountText.Paste(stringcount.ToString());
                stringindtableszText.Paste(o + stringindextablesize.ToString("X"));
                stringtablesizeText.Paste(o + stringtablesize.ToString("X"));
                tagnamecountText.Paste(tagnamecount.ToString());
                tagnameindtableszText.Paste(o + tagnamecountsize.ToString("X"));
                tagnametablesizeText.Paste(o + tagtablesize.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void AllAftH3BNoH4Tags()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] commonnoh4 = HV.CommonNoH4(_path);

                int tagmask = commonnoh4[21];
                int indexheaderoffset = commonnoh4[22];

                int[] indexheadervals = IHF.IndexHeaderValues(_path, indexheaderoffset, tagmask);

                int classcount = indexheadervals[0];
                int classsize = indexheadervals[1];
                int classoff = indexheadervals[2];
                int tagcount = indexheadervals[3];
                int tagindexsize = indexheadervals[4];
                int tagtableoff = indexheadervals[5];

                tagclassText.Paste(classcount.ToString());
                tagclasssizeText.Paste(o + classsize.ToString("X"));
                tagclasstableText.Paste(o + classoff.ToString("X"));
                tagcountText.Paste(tagcount.ToString());
                tagtablesizeText.Paste(o + tagindexsize.ToString("X"));
                tagindextableText.Paste(o + tagtableoff.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void AllAftH3BNoH4Vals()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] commonnoh4 = HV.CommonNoH4(_path);

                int virtualbase = commonnoh4[0];
                int part0 = commonnoh4[1];
                int part0sz = commonnoh4[2];
                int part1 = commonnoh4[3];
                int part1sz = commonnoh4[4];
                int part2 = commonnoh4[5];
                int part2sz = commonnoh4[6];
                int part3 = commonnoh4[7];
                int part3sz = commonnoh4[8];
                int part4 = commonnoh4[9];
                int part4sz = commonnoh4[10];
                int part5 = commonnoh4[11];
                int part5sz = commonnoh4[12];
                int assetdata = commonnoh4[13];
                int localemagic = commonnoh4[14];
                int stringmagic = commonnoh4[15];
                int assetdatasize = commonnoh4[16];
                int localeindextablerawoffset = commonnoh4[17];
                int localetotalsize = commonnoh4[18];
                int metaoff = commonnoh4[19];
                int metasize = commonnoh4[20];
                int tagmask = commonnoh4[21];
                int indexheaderoffset = commonnoh4[22];
                int localeindextableoffset = commonnoh4[23];

                rawText.Paste(o + assetdata.ToString("X"));
                rawszText.Paste(o + assetdatasize.ToString("X"));
                indexheadText.Paste(o + indexheaderoffset.ToString("X"));
                tagmaskText.Paste(o + tagmask.ToString("X"));
                stringmaskText.Paste(o + (stringmagic).ToString("X"));
                localemaskText.Paste(o + (localemagic).ToString("X"));
                localetotalsizeText.Paste(o + localetotalsize.ToString("X"));
                localedataoffsetText.Paste(o + localeindextableoffset.ToString("X"));
                metaText.Paste(o + metaoff.ToString("X"));
                metaszText.Paste(o + metasize.ToString("X"));
                part0Text.Paste(o + part0.ToString("X"));
                partsz0Text.Paste(o + part0sz.ToString("X"));
                part1Text.Paste(o + part1.ToString("X"));
                partsz1Text.Paste(o + part1sz.ToString("X"));
                part2Text.Paste(o + part2.ToString("X"));
                partsz2Text.Paste(o + part2sz.ToString("X"));
                part3Text.Paste(o + part3.ToString("X"));
                partsz3Text.Paste(o + part3sz.ToString("X"));
                part4Text.Paste(o + part4.ToString("X"));
                partsz4Text.Paste(o + part4sz.ToString("X"));
                part5Text.Paste(o + part5.ToString("X"));
                partsz5Text.Paste(o + part5sz.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void H3Values()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] h3vals = HV.HeaderSpecificValuesNoH4(_path, halo3header);

                int stringindextableoffset = h3vals[0];
                int stringtableoffset = h3vals[1];
                int tagindextableoffset = h3vals[2];
                int tagdatatableoffset = h3vals[3];

                int[] values = HV.CommonNoH4(_path);

                int tagmask = values[21];
                int indexheaderoffset = values[22];

                Reader r = new Reader(_path);

                int scnroffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 4, tagmask);

                r.Position = scnroffset + 0x8;
                int mapID = r.ReadInt32();

                mapidText.Paste(mapID.ToString());
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ReachBetaValues()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] reachbetavals = HV.HeaderSpecificValuesNoH4(_path, reachbetaheader);

                int stringindextableoffset = reachbetavals[0];
                int stringtableoffset = reachbetavals[1];
                int tagindextableoffset = reachbetavals[2];
                int tagdatatableoffset = reachbetavals[3];

                int[] values = HV.CommonNoH4(_path);

                int tagmask = values[21];
                int indexheaderoffset = values[22];

                Reader r = new Reader(_path);

                int scnroffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 5, tagmask);

                r.Position = scnroffset + 0x8;
                int mapID = r.ReadInt32();

                mapidText.Paste(mapID.ToString());
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ReachValues()
        {
            try
            {
                string _path = directoryBox.Text;
                int[] reachvals = HV.HeaderSpecificValuesNoH4(_path, reachheader);

                int stringindextableoffset = reachvals[0];
                int stringtableoffset = reachvals[1];
                int tagindextableoffset = reachvals[2];
                int tagdatatableoffset = reachvals[3];

                int[] values = HV.CommonNoH4(_path);

                int tagmask = values[21];
                int indexheaderoffset = values[22];

                Reader r = new Reader(_path);

                int scnroffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 5, tagmask);

                r.Position = scnroffset + 0xC;
                int mapID = r.ReadInt32();

                mapidText.Paste(mapID.ToString());
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void H4NoRawValues()
        {
            try
            {
                string _path = directoryBox.Text;

                int[] h4norawvalues = HV.Halo4NoRawValues(_path);

                int metaoff = h4norawvalues[0];
                int stringindextableoffset = h4norawvalues[1];
                int stringtableoffset = h4norawvalues[2];
                int tagindextableoffset = h4norawvalues[3];
                int tagdatatableoffset = h4norawvalues[4];
                int alternaterawoffset = h4norawvalues[5];
                int tagmask = h4norawvalues[6];
                int indexheaderoffset = h4norawvalues[7];

                Reader r = new Reader(_path);

                int[] indexheadervalues = IHF.IndexHeaderValues(_path, indexheaderoffset, tagmask);

                int classcount = indexheadervalues[0];
                int classsize = indexheadervalues[1];
                int classoff = indexheadervalues[2];
                int tagcount = indexheadervalues[3];
                int tagindexsize = indexheadervalues[4];
                int tagtableoff = indexheadervalues[5];
                int imptagcount = indexheadervalues[6];

                if (imptagcount == 7)
                {
                    int scnroffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 5, tagmask);
                    r.Position = scnroffset + 0x18;
                    int mapID = r.ReadInt32();

                    mapidText.Paste(mapID.ToString());
                }

                if (imptagcount != 7)
                    mapidText.Paste(na);

                tagclassText.Paste(classcount.ToString());
                tagclasssizeText.Paste(o + classsize.ToString("X"));
                tagclasstableText.Paste(o + classoff.ToString("X"));
                tagcountText.Paste(tagcount.ToString());
                tagtablesizeText.Paste(o + tagindexsize.ToString("X"));
                tagindextableText.Paste(o + tagtableoff.ToString("X"));
                metaText.Paste(o + metaoff.ToString("X"));
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));
                rawText.Paste(o + alternaterawoffset.ToString("X"));
                tagmaskText.Paste(o + tagmask.ToString("X"));
                indexheadText.Paste(o + indexheaderoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void H4StandardValues()
        {
            try
            {
                string _path = directoryBox.Text;

                int[] h4standardvalues = HV.Halo4StandardValues(_path);

                int metaoff = h4standardvalues[0];
                int stringindextableoffset = h4standardvalues[1];
                int stringtableoffset = h4standardvalues[2];
                int tagindextableoffset = h4standardvalues[3];
                int tagdatatableoffset = h4standardvalues[4];
                int assetdata = h4standardvalues[5];
                int tagmask = h4standardvalues[6];
                int indexheaderoffset = h4standardvalues[7];

                Reader r = new Reader(_path);

                int[] indexheadervalues = IHF.IndexHeaderValues(_path, indexheaderoffset, tagmask);

                int classcount = indexheadervalues[0];
                int classsize = indexheadervalues[1];
                int classoff = indexheadervalues[2];
                int tagcount = indexheadervalues[3];
                int tagindexsize = indexheadervalues[4];
                int tagtableoff = indexheadervalues[5];
                int imptagcount = indexheadervalues[6];

                if (imptagcount == 7)
                {
                    int scnroffset = IHF.ImportantTagOffsetFinder(indexheaderoffset, _path, 5, tagmask);
                    r.Position = scnroffset + 0x18;
                    int mapID = r.ReadInt32();

                    mapidText.Paste(mapID.ToString());
                }

                if (imptagcount != 7)
                    mapidText.Paste(na);

                tagclassText.Paste(classcount.ToString());
                tagclasssizeText.Paste(o + classsize.ToString("X"));
                tagclasstableText.Paste(o + classoff.ToString("X"));
                tagcountText.Paste(tagcount.ToString());
                tagtablesizeText.Paste(o + tagindexsize.ToString("X"));
                tagindextableText.Paste(o + tagtableoff.ToString("X"));
                metaText.Paste(o + metaoff.ToString("X"));
                stringindtableText.Paste(o + stringindextableoffset.ToString("X"));
                stringtableText.Paste(o + stringtableoffset.ToString("X"));
                tagnameindtableText.Paste(o + tagindextableoffset.ToString("X"));
                tagnametableText.Paste(o + tagdatatableoffset.ToString("X"));
                rawText.Paste(o + assetdata.ToString("X"));
                tagmaskText.Paste(o + tagmask.ToString("X"));
                indexheadText.Paste(o + indexheaderoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void H4Values()
        {
            try
            {
                string _path = directoryBox.Text;

                int[] h4values = HV.Halo4Values(_path);

                int virtualbase = h4values[0];
                int part0 = h4values[1];
                int part0sz = h4values[2];
                int part1 = h4values[3];
                int part1sz = h4values[4];
                int part2 = h4values[5];
                int part2sz = h4values[6];
                int part3 = h4values[7];
                int part3sz = h4values[8];
                int part4 = h4values[9];
                int part4sz = h4values[10];
                int part5 = h4values[11];
                int part5sz = h4values[12];
                int assetdata = h4values[13];
                int localemagic = h4values[14];
                int stringmagic = h4values[15];
                int alternaterawoffset = h4values[16];
                int assetdatasize = h4values[17];
                int localeindextablerawoffset = h4values[18];
                int localetotalsize = h4values[19];
                int metasize = h4values[20];

                int localeindextableoffset = (localeindextablerawoffset + localemagic);

                metaszText.Paste(o + metasize.ToString("X"));
                part0Text.Paste(o + part0.ToString("X"));
                partsz0Text.Paste(o + part0sz.ToString("X"));
                part1Text.Paste(o + part1.ToString("X"));
                partsz1Text.Paste(o + part1sz.ToString("X"));
                part2Text.Paste(o + part2.ToString("X"));
                partsz2Text.Paste(o + part2sz.ToString("X"));
                part3Text.Paste(o + part3.ToString("X"));
                partsz3Text.Paste(o + part3sz.ToString("X"));
                part4Text.Paste(o + part4.ToString("X"));
                partsz4Text.Paste(o + part4sz.ToString("X"));
                part5Text.Paste(o + part5.ToString("X"));
                partsz5Text.Paste(o + part5sz.ToString("X"));
                stringmaskText.Paste(o + stringmagic.ToString("X"));
                localemaskText.Paste(o + localemagic.ToString("X"));
                rawszText.Paste(o + assetdatasize.ToString("X"));
                localetotalsizeText.Paste(o + localetotalsize.ToString("X"));
                localedataoffsetText.Paste(o + localeindextableoffset.ToString("X"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void H3BForceLoad()
        {
            gameText.Paste("Halo 3 Beta (Force-Load)");
            H3BValues();
        }

        public void H3ForceLoad()
        {
            gameText.Paste("Halo 3 (Force-Load)");
            AllAftH3BValues();
            AllAftH3BNoH4Tags();
            AllAftH3BNoH4Vals();
            H3Values();
        }

        public void ODSTForceLoad()
        {
            gameText.Paste("Halo 3: ODST (Force-Load)");
            AllAftH3BValues();
            AllAftH3BNoH4Tags();
            AllAftH3BNoH4Vals();
            H3Values();
        }

        public void ReachBetaForceLoad()
        {
            gameText.Paste("Halo: Reach Beta (Force-Load)");
            AllAftH3BValues();
            AllAftH3BNoH4Tags();
            AllAftH3BNoH4Vals();
            ReachBetaValues();
        }

        public void ReachForceLoad()
        {
            gameText.Paste("Halo: Reach (Force-Load)");
            AllAftH3BValues();
            AllAftH3BNoH4Tags();
            AllAftH3BNoH4Vals();
            ReachValues();
        }

        public void H4StandardForceLoad()
        {
            gameText.Paste("Halo 4 (Force-Load)");
            AllAftH3BValues();
            H4Values();
            H4StandardValues();
        }

        public void H4NoRawForceLoad()
        {
            gameText.Paste("Halo 4 (No Raw) [Force-Load]");
            AllAftH3BValues();
            H4Values();
            H4NoRawValues();
        }

        public void ClearSharedText()
        {
            mapidText.Clear();
            tagmaskText.Clear();
            stringmaskText.Clear();
            localemaskText.Clear();
            stringtableText.Clear();
            stringindtableText.Clear();
            tagnametableText.Clear();
            tagnameindtableText.Clear();
            localedataoffsetText.Clear();
            indexheadaddText.Clear();
            indexheadText.Clear();
            tagclasstableText.Clear();
            tagindextableText.Clear();
            metaText.Clear();
            part0Text.Clear();
            part1Text.Clear();
            part2Text.Clear();
            part3Text.Clear();
            part4Text.Clear();
            part5Text.Clear();
            stringcountText.Clear();
            tagnamecountText.Clear();
            tagclassText.Clear();
            tagcountText.Clear();
            stringtablesizeText.Clear();
            stringindtableszText.Clear();
            tagnametablesizeText.Clear();
            tagnameindtableszText.Clear();
            localetotalsizeText.Clear();
            tagclasssizeText.Clear();
            tagtablesizeText.Clear();
            metaszText.Clear();
            partsz0Text.Clear();
            partsz1Text.Clear();
            partsz2Text.Clear();
            partsz3Text.Clear();
            partsz4Text.Clear();
            partsz5Text.Clear();
        }

        public void ClearText()
        {
            mapidText.Clear();
            typeText.Clear();
            sizeText.Clear();
            internalText.Clear();
            scenarioText.Clear();
            gameText.Clear();
            engineText.Clear();
            buildText.Clear();
            rawText.Clear();
            rawszText.Clear();
            indexheadText.Clear();
            indexheadaddText.Clear();
            tagclassText.Clear();
            tagclasssizeText.Clear();
            tagclasstableText.Clear();
            tagcountText.Clear();
            tagtablesizeText.Clear();
            tagindextableText.Clear();
            tagmaskText.Clear();
            stringmaskText.Clear();
            localemaskText.Clear();
            stringcountText.Clear();
            stringindtableszText.Clear();
            stringtablesizeText.Clear();
            stringindtableText.Clear();
            stringtableText.Clear();
            tagnamecountText.Clear();
            tagnameindtableszText.Clear();
            tagnametablesizeText.Clear();
            tagnametableText.Clear();
            tagnameindtableText.Clear();
            localetotalsizeText.Clear();
            localedataoffsetText.Clear();
            metaText.Clear();
            metaszText.Clear();
            part0Text.Clear();
            partsz0Text.Clear();
            part1Text.Clear();
            partsz1Text.Clear();
            part2Text.Clear();
            partsz2Text.Clear();
            part3Text.Clear();
            partsz3Text.Clear();
            part4Text.Clear();
            partsz4Text.Clear();
            part5Text.Clear();
            partsz5Text.Clear();
        }

        public void getValues()
        {
            ClearText();

            directoryBox.Clear();
            directoryBox.Paste(path);
            Reader r = new Reader(path);

            r.Position = 0x0;
            string headmagic = r.ReadString(0x4);

            if (headmagic == "head")
            {
                r.Position = 0x8;
                string filesize = r.ReadInt32().ToString("X");

                r.Position = 0x10;
                int indexheaderaddress = r.ReadInt32();

                r.Position = 0x11C;
                string game = r.ReadString(0x20);

                buildText.Paste(game);

                r.Position = 0x13C;
                short type = r.ReadInt16();
                bool ingamemap = (type != 0x3 && type != 0x4);

                bool halo3beta = (buildText.Text == halo3betabuild);

                bool halo3 = (buildText.Text == halo3build);
                bool halo3review = (buildText.Text == halo3reviewbuild);
                bool halo3epsilon = (buildText.Text == halo3epsilonbuild);
                bool halo3mythic = (buildText.Text == halo3mythicbuild);

                bool halo3odst = (buildText.Text == halo3odstbuild);

                bool reachprebeta = (buildText.Text == reachprebetabuild);
                bool reachbeta = (buildText.Text == reachbetabuild);
                bool reachbetas = (reachprebeta || reachbeta);

                bool reach = (buildText.Text == reachbuild);
                bool reachdemo = (buildText.Text == reachdemobuild);
                bool reachbuilds = (reach || reachdemo);

                bool halo4tu0 = (buildText.Text == halo4build);
                bool halo4tu2 = (buildText.Text == halo4tu2build);
                bool halo4tu3 = (buildText.Text == halo4tu3build);
                bool halo4tu4 = (buildText.Text == halo4tu4build);
                bool halo4tu5 = (buildText.Text == halo4tu5build);
                bool halo4tu6 = (buildText.Text == halo4tu6build);
                bool halo4tu7 = (buildText.Text == halo4tu7build);

                bool allhalo3noODSTorEpsilon = (halo3 || halo3mythic);
                bool allhalo3builds = (allhalo3noODSTorEpsilon || halo3odst || halo3review || halo3epsilon);
                bool allreachbuilds = (reachbetas || reachbuilds);
                bool halo4 = (halo4tu0 || halo4tu2 || halo4tu3 || halo4tu4 || halo4tu5 || halo4tu6 || halo4tu7);

                bool thirdgennoH3B = (allhalo3builds || allreachbuilds || halo4);

                bool thirdgen = (thirdgennoH3B || halo3beta);

                if (!halo3beta && !allhalo3builds && !allreachbuilds && !halo4)
                {
                    UnrecognizedBuildDialog();
                }
                if (halo3beta)
                {
                    gameText.Paste("Halo 3 Beta");
                    H3BValues();
                }

                if (thirdgennoH3B)
                {
                    AllAftH3BValues();

                    if (allhalo3builds || allreachbuilds)
                    {
                        AllAftH3BNoH4Vals();

                        if (allhalo3builds)
                        {
                            if (halo3)
                            {
                                gameText.Paste("Halo 3");
                            }
                            if (halo3review)
                            {
                                gameText.Paste("Halo 3 Review");
                            }
                            if (halo3epsilon)
                            {
                                gameText.Paste("Halo 3 Epsilon");
                            }
                            if (halo3mythic)
                            {
                                gameText.Paste("Halo 3 Mythic");
                            }
                            if (halo3odst)
                            {
                                gameText.Paste("Halo 3: ODST");
                            }

                            if (ingamemap)
                                H3Values();
                        }

                        if (reachbetas)
                        {
                            if (reachprebeta)
                            {
                                gameText.Paste("Halo: Reach Pre-Beta");
                            }
                            if (reachbeta)
                            {
                                gameText.Paste("Halo: Reach Beta");
                            }

                            if (ingamemap)
                                ReachBetaValues();
                        }

                        if (reachbuilds)
                        {
                            if (reach)
                            {
                                gameText.Paste("Halo: Reach Demo");
                            }
                            if (reachdemo)
                            {
                                gameText.Paste("Halo: Reach");
                            }

                            if (ingamemap)
                                ReachValues();
                        }

                        if (ingamemap)
                        {
                            AllAftH3BNoH4Tags();
                        }

                        if (!ingamemap)
                        {
                            ClearSharedText();
                        }
                    }
                    if (halo4)
                    {
                        gameText.Paste("Halo 4");

                        H4Values();

                        r.Position = 0x488;
                        int localemagic = r.ReadInt32();

                        if (localemagic != 0)
                            H4StandardValues();
                        if (localemagic == 0)
                            H4NoRawValues();

                        if (!ingamemap)
                        {
                            ClearSharedText();
                            rawText.Clear();
                            rawText.Paste(o + HV.Halo4Values(directoryBox.Text)[13].ToString("X"));
                        }
                    }

                    if (!ingamemap)
                    {
                        mapidText.Paste(na);
                        tagmaskText.Paste(na);
                        stringmaskText.Paste(na);
                        localemaskText.Paste(na);
                        stringtableText.Paste(na);
                        stringindtableText.Paste(na);
                        tagnametableText.Paste(na);
                        tagnameindtableText.Paste(na);
                        localedataoffsetText.Paste(na);
                        indexheadaddText.Paste(na);
                        indexheadText.Paste(na);
                        tagclasstableText.Paste(na);
                        tagindextableText.Paste(na);
                        metaText.Paste(na);
                        part0Text.Paste(na);
                        part1Text.Paste(na);
                        part2Text.Paste(na);
                        part3Text.Paste(na);
                        part4Text.Paste(na);
                        part5Text.Paste(na);
                        stringcountText.Paste("0");
                        tagnamecountText.Paste("0");
                        tagclassText.Paste("0");
                        tagcountText.Paste("0");
                        stringtablesizeText.Paste("0x0");
                        stringindtableszText.Paste("0x0");
                        tagnametablesizeText.Paste("0x0");
                        tagnameindtableszText.Paste("0x0");
                        localetotalsizeText.Paste("0x0");
                        tagclasssizeText.Paste("0x0");
                        tagtablesizeText.Paste("0x0");
                        metaszText.Paste("0x0");
                        partsz0Text.Paste("0x0");
                        partsz1Text.Paste("0x0");
                        partsz2Text.Paste("0x0");
                        partsz3Text.Paste("0x0");
                        partsz4Text.Paste("0x0");
                        partsz5Text.Paste("0x0");
                    }
                }
            }
            if (headmagic == "daeh")
            {
                Non3rdGenDialog();
            }
            if (headmagic != "daeh" && headmagic != "head")
            {
                BadHeaderMagDialog();
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Blam .map Files (*.map)|*.map";

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = open.FileName;
                getValues();
            }
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            path = files[files.Length - 1];
            getValues();
        }

        public void Non3rdGenDialog()
        {
            MessageBox.Show("Only Third Generation map files are supported.", "Error: Not Third Gen");
        }

        public void BadHeaderMagDialog()
        {
            MessageBox.Show("Unrecognized Header Magic! Only Third Generation map files are supported.", "Error: Unrecognized Header Magic");
        }

        public void UnrecognizedBuildDialog()
        {
            UnrecognizedBuild UB = new UnrecognizedBuild(buildText.Text, this);
            UB.ShowDialog();
        }
    }
}
