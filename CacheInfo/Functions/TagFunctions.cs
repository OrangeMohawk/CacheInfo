using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderIO;

namespace Functions
{
    class IndexHeaderFunctions
    {
        public int[] IndexHeaderValues(string _path, int _indexheaderoffset, int _tagmask)
        {
            Reader r = new Reader(_path);

            r.Position = _indexheaderoffset;
            int classcount = r.ReadInt32();
            int classsize = (classcount * 0x10);

            r.Position = _indexheaderoffset + 0x4;
            int classadd = r.ReadInt32();
            int classoff = (classadd - _tagmask);

            r.Position = _indexheaderoffset + 0x8;
            int tagcount = r.ReadInt32();
            int tagindexsize = (tagcount * 0x8);

            r.Position = _indexheaderoffset + 0xC;
            int tagtableadd = r.ReadInt32();
            int tagtableoff = (tagtableadd - _tagmask);

            r.Position = _indexheaderoffset + 0x10;
            int imptagcount = r.ReadInt32();

            r.Position = _indexheaderoffset + 0x14;
            int imptagsadd = r.ReadInt32();
            int imptagsoff = (imptagsadd - _tagmask);


            int[] indexheadervalues = {
                                          classcount,
                                          classsize,
                                          classoff,
                                          tagcount,
                                          tagindexsize,
                                          tagtableoff,
                                          imptagcount,
                                          imptagsoff
                                      };
            return indexheadervalues;
        }

        /// <summary>
        /// Finds the Tag Table with the Index Header Offset
        /// </summary>
        /// <param name="_path">The file that is being read.</param>
        /// <param name="__indexheaderoffset">Value of the Index Header Offset.</param>
        /// <param name="_tagmask">The file's tagmask.</param>
        public int FindTagTable(string _path, int _indexheaderoffset, int _tagmask)
        {
            Reader r = new Reader(_path);

            r.Position = _indexheaderoffset + 0xC;
            int tagtableadd = r.ReadInt32();

            int tagtableoff = (tagtableadd - _tagmask);

            return tagtableoff;

        }

        /// <summary>
        /// Finds the offset of a tag in the "Important Tag Class" table.
        /// </summary>
        /// <param name="_indexheaderoffset">Value of the Index Header Offset.</param>
        /// <param name="_path">The file that is being read.</param>
        /// <param name="importanttagsindex">The index of the important tag class table to be found.</param>
        /// <param name="_tagmask">The file's tagmask.</param>
        // Indexes:
        // Halo 3 Beta: draw = 0, ugh! = 1, zone = 2, scnr = 3, matg = 4
        // Halo 3/ODST: draw = 0, play = 1, ugh! = 2, zone = 3, scnr = 4, matg = 5
        // Reach Beta - Halo 4: draw = 0, gpix = 1, play = 2, ugh! = 3, zone = 4, scnr = 5, matg = 6
        // Halo 4 Patch Maps - draw = 0, gpix = 1, play = 2, ugh! = 3, zone = 4, patg = 5
        public int ImportantTagOffsetFinder(int _indexheaderoffset, string _path, int importanttagsindex, int _tagmask)
        {
            FindTagTable(_path, _indexheaderoffset, _tagmask);

            int imptagspos = (0x8 * importanttagsindex) + 0x6;

            int[] indexheadervalues = IndexHeaderValues(_path, _indexheaderoffset, _tagmask);
            int tagtableoff = indexheadervalues[5];
            int imptagsoff = indexheadervalues[7];

            
            Reader r = new Reader(_path);

            r.Position = imptagsoff + imptagspos;
            short impclassindex = r.ReadInt16();

            int impclasstableindex = (impclassindex * 0x8);

            r.Position = tagtableoff + impclasstableindex + 0x4;
            int impclassaddress = r.ReadInt32();

            int impclassoffset = (impclassaddress - _tagmask);

            return impclassoffset;
        }
    }
}
