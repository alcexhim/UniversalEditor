#if UE_CHUNKED_RIFF_INCLUDE_METADATA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Chunked
{
    public class RIFFMetadataItem
    {
        public class RIFFMetadataItemCollection
            : System.Collections.ObjectModel.Collection<RIFFMetadataItem>
        {
            public RIFFMetadataItem Add(CommonMetadataType Type, object Value)
            {
                RIFFMetadataItem item = new RIFFMetadataItem();
                item.Type = Type;
                item.Value = Value;
                base.Items.Add(item);
                return item;
            }
            public RIFFMetadataItem Add(string Name, object Value)
            {
                RIFFMetadataItem item = new RIFFMetadataItem();
                item.Name = Name;
                item.Value = Value;
                base.Items.Add(item);
                return item;
            }
        }

        public CommonMetadataType Type
        {
            get
            {
                switch (mvarName)
                {
                    case "AGES": return CommonMetadataType.RatingAGES;
                    case "CMNT": return CommonMetadataType.CommentCMNT;
                    case "CODE": return CommonMetadataType.EncodedByCODE;
                    case "COMM": return CommonMetadataType.Comments;
                    case "DIRC": return CommonMetadataType.Directory;
                    case "DISP": return CommonMetadataType.SoundSchemeTitle;
                    case "DTIM": return CommonMetadataType.DateTimeOriginal;
                    case "GENR": return CommonMetadataType.GenreGENR;
                    case "IARL": return CommonMetadataType.ArchivalLocation;
                    case "IART": return CommonMetadataType.Artist;
                    case "IAS1": return CommonMetadataType.FirstLanguage;
                    case "IAS2": return CommonMetadataType.SecondLanguage;
                    case "IAS3": return CommonMetadataType.ThirdLanguage;
                    case "IAS4": return CommonMetadataType.FourthLanguage;
                    case "IAS5": return CommonMetadataType.FifthLanguage;
                    case "IAS6": return CommonMetadataType.SixthLanguage;
                    case "IAS7": return CommonMetadataType.SeventhLanguage;
                    case "IAS8": return CommonMetadataType.EighthLanguage;
                    case "IAS9": return CommonMetadataType.NinthLanguage;
                    case "IBSU": return CommonMetadataType.BaseURL;
                    case "ICAS": return CommonMetadataType.DefaultAudioStream;
                    case "ICDS": return CommonMetadataType.CostumeDesigner;
                    case "ICMS": return CommonMetadataType.Commissioned;
                    case "ICMT": return CommonMetadataType.CommentICMT;
                    case "ICNM": return CommonMetadataType.Cinematographer;
                    case "ICNT": return CommonMetadataType.Country;
                    case "ICOP": return CommonMetadataType.Copyright;
                    case "ICRD": return CommonMetadataType.DateCreated;
                    case "ICRP": return CommonMetadataType.Cropped;
                    case "IDIM": return CommonMetadataType.Dimensions;
                    case "IDPI": return CommonMetadataType.DotsPerInch;
                    case "IDST": return CommonMetadataType.DistributedBy;
                    case "IEDT": return CommonMetadataType.EditedBy;
                    case "IENC": return CommonMetadataType.EncodedByIENC;
                    case "IENG": return CommonMetadataType.Engineer;
                    case "IGNR": return CommonMetadataType.GenreIGNR;
                    case "IKEY": return CommonMetadataType.Keywords;
                    case "ILGT": return CommonMetadataType.Lightness;
                    case "ILGU": return CommonMetadataType.LogoURL;
                    case "ILIU": return CommonMetadataType.LogoIconURL;
                    case "ILNG": return CommonMetadataType.LanguageILNG;
                    case "IMBI": return CommonMetadataType.MoreInfoBannerImage;
                    case "IMBU": return CommonMetadataType.MoreInfoBannerURL;
                    case "IMED": return CommonMetadataType.Medium;
                    case "IMIT": return CommonMetadataType.MoreInfoText;
                    case "IMIU": return CommonMetadataType.MoreInfoURL;
                    case "IMUS": return CommonMetadataType.MusicBy;
                    case "INAM": return CommonMetadataType.TitleINAM;
                    case "IPDS": return CommonMetadataType.ProductionDesigner;
                    case "IPLT": return CommonMetadataType.NumColors;
                    case "IPRD": return CommonMetadataType.Product;
                    case "IPRO": return CommonMetadataType.ProducedBy;
                    case "IRIP": return CommonMetadataType.RippedBy;
                    case "IRTD": return CommonMetadataType.RatingIRTD;
                    case "ISBJ": return CommonMetadataType.Subject;
                    case "ISFT": return CommonMetadataType.Software;
                    case "ISGN": return CommonMetadataType.SecondaryGenre;
                    case "ISHP": return CommonMetadataType.Sharpness;
                    case "ISRC": return CommonMetadataType.Source;
                    case "ISRF": return CommonMetadataType.SourceForm;
                    case "ISTD": return CommonMetadataType.ProductionStudio;
                    case "ISTR": return CommonMetadataType.StarringISTR;
                    case "ITCH": return CommonMetadataType.Technician;
                    case "IWMU": return CommonMetadataType.WatermarkURL;
                    case "IWRI": return CommonMetadataType.WrittenBy;
                    case "LANG": return CommonMetadataType.LanguageLANG;
                    case "LOCA": return CommonMetadataType.Location;
                    case "PRT1": return CommonMetadataType.Part;
                    case "PRT2": return CommonMetadataType.NumberOfParts;
                    case "RATE": return CommonMetadataType.Rate;
                    case "STAR": return CommonMetadataType.StarringSTAR;
                    case "STAT": return CommonMetadataType.Statistics;
                    case "TAPE": return CommonMetadataType.TapeName;
                    case "TCDO": return CommonMetadataType.EndTimecode;
                    case "TCOD": return CommonMetadataType.StartTimecode;
                    case "TITL": return CommonMetadataType.TitleTITL;
                    case "TLEN": return CommonMetadataType.Length;
                    case "TORG": return CommonMetadataType.Organization;
                    case "TRCK": return CommonMetadataType.TrackNumber;
                    case "TURL": return CommonMetadataType.URL;
                    case "TVER": return CommonMetadataType.Version;
                    case "VMAJ": return CommonMetadataType.VegasVersionMajor;
                    case "VMIN": return CommonMetadataType.VegasVersionMinor;
                    case "YEAR": return CommonMetadataType.Year;
                    #region Exif 2.3 specification
                    case "ecor": return CommonMetadataType.ExifMake;
                    case "emdl": return CommonMetadataType.ExifModel;
                    case "emnt": return CommonMetadataType.ExifMakerNotes;
                    case "erel": return CommonMetadataType.ExifRelatedImageFile;
                    case "etim": return CommonMetadataType.ExifTimeCreated;
                    case "eucm": return CommonMetadataType.ExifUserComment;
                    case "ever": return CommonMetadataType.ExifVersion;
                    #endregion
                }
                return CommonMetadataType.Unknown;
            }
            set
            {
                switch (value)
                {
                    case CommonMetadataType.RatingAGES: mvarName = "AGES"; break;
                    case CommonMetadataType.CommentCMNT: mvarName = "CMNT"; break;
                    case CommonMetadataType.EncodedByCODE: mvarName = "CODE"; break;
                    case CommonMetadataType.Comments: mvarName = "COMM"; break;
                    case CommonMetadataType.Directory: mvarName = "DIRC"; break;
                    case CommonMetadataType.SoundSchemeTitle: mvarName = "DISP"; break;
                    case CommonMetadataType.DateTimeOriginal: mvarName = "DTIM"; break;
                    case CommonMetadataType.GenreGENR: mvarName = "GENR"; break;
                    case CommonMetadataType.ArchivalLocation: mvarName = "IARL"; break;
                    case CommonMetadataType.Artist: mvarName = "IART"; break;
                    case CommonMetadataType.FirstLanguage: mvarName = "IAS1"; break;
                    case CommonMetadataType.SecondLanguage: mvarName = "IAS2"; break;
                    case CommonMetadataType.ThirdLanguage: mvarName = "IAS3"; break;
                    case CommonMetadataType.FourthLanguage: mvarName = "IAS4"; break;
                    case CommonMetadataType.FifthLanguage: mvarName = "IAS5"; break;
                    case CommonMetadataType.SixthLanguage: mvarName = "IAS6"; break;
                    case CommonMetadataType.SeventhLanguage: mvarName = "IAS7"; break;
                    case CommonMetadataType.EighthLanguage: mvarName = "IAS8"; break;
                    case CommonMetadataType.NinthLanguage: mvarName = "IAS9"; break;
                    case CommonMetadataType.BaseURL: mvarName = "IBSU"; break;
                    case CommonMetadataType.DefaultAudioStream: mvarName = "ICAS"; break;
                    case CommonMetadataType.CostumeDesigner: mvarName = "ICDS"; break;
                    case CommonMetadataType.Commissioned: mvarName = "ICMS"; break;
                    case CommonMetadataType.CommentICMT: mvarName = "ICMT"; break;
                    case CommonMetadataType.Cinematographer: mvarName = "ICNM"; break;
                    case CommonMetadataType.Country: mvarName = "ICNT"; break;
                    case CommonMetadataType.Copyright: mvarName = "ICOP"; break;
                    case CommonMetadataType.DateCreated: mvarName = "ICRD"; break;
                    case CommonMetadataType.Cropped: mvarName = "ICRP"; break;
                    case CommonMetadataType.Dimensions: mvarName = "IDIM"; break;
                    case CommonMetadataType.DotsPerInch: mvarName = "IDPI"; break;
                    case CommonMetadataType.DistributedBy: mvarName = "IDST"; break;
                    case CommonMetadataType.EditedBy: mvarName = "IEDT"; break;
                    case CommonMetadataType.EncodedByIENC: mvarName = "IENC"; break;
                    case CommonMetadataType.Engineer: mvarName = "IENG"; break;
                    case CommonMetadataType.GenreIGNR: mvarName = "IGNR"; break;
                    case CommonMetadataType.Keywords: mvarName = "IKEY"; break;
                    case CommonMetadataType.Lightness: mvarName = "ILGT"; break;
                    case CommonMetadataType.LogoURL: mvarName = "ILGU"; break;
                    case CommonMetadataType.LogoIconURL: mvarName = "ILIU"; break;
                    case CommonMetadataType.LanguageILNG: mvarName = "ILNG"; break;
                    case CommonMetadataType.MoreInfoBannerImage: mvarName = "IMBI"; break;
                    case CommonMetadataType.MoreInfoBannerURL: mvarName = "IMBU"; break;
                    case CommonMetadataType.Medium: mvarName = "IMED"; break;
                    case CommonMetadataType.MoreInfoText: mvarName = "IMIT"; break;
                    case CommonMetadataType.MoreInfoURL: mvarName = "IMIU"; break;
                    case CommonMetadataType.MusicBy: mvarName = "IMUS"; break;
                    case CommonMetadataType.TitleINAM: mvarName = "INAM"; break;
                    case CommonMetadataType.ProductionDesigner: mvarName = "IPDS"; break;
                    case CommonMetadataType.NumColors: mvarName = "IPLT"; break;
                    case CommonMetadataType.Product: mvarName = "IPRD"; break;
                    case CommonMetadataType.ProducedBy: mvarName = "IPRO"; break;
                    case CommonMetadataType.RippedBy: mvarName = "IRIP"; break;
                    case CommonMetadataType.RatingIRTD: mvarName = "IRTD"; break;
                    case CommonMetadataType.Subject: mvarName = "ISBJ"; break;
                    case CommonMetadataType.Software: mvarName = "ISFT"; break;
                    case CommonMetadataType.SecondaryGenre: mvarName = "ISGN"; break;
                    case CommonMetadataType.Sharpness: mvarName = "ISHP"; break;
                    case CommonMetadataType.Source: mvarName = "ISRC"; break;
                    case CommonMetadataType.SourceForm: mvarName = "ISRF"; break;
                    case CommonMetadataType.ProductionStudio: mvarName = "ISTD"; break;
                    case CommonMetadataType.StarringISTR: mvarName = "ISTR"; break;
                    case CommonMetadataType.Technician: mvarName = "ITCH"; break;
                    case CommonMetadataType.WatermarkURL: mvarName = "IWMU"; break;
                    case CommonMetadataType.WrittenBy: mvarName = "IWRI"; break;
                    case CommonMetadataType.LanguageLANG: mvarName = "LANG"; break;
                    case CommonMetadataType.Location: mvarName = "LOCA"; break;
                    case CommonMetadataType.Part: mvarName = "PRT1"; break;
                    case CommonMetadataType.NumberOfParts: mvarName = "PRT2"; break;
                    case CommonMetadataType.Rate: mvarName = "RATE"; break;
                    case CommonMetadataType.StarringSTAR: mvarName = "STAR"; break;
                    case CommonMetadataType.Statistics: mvarName = "STAT"; break;
                    case CommonMetadataType.TapeName: mvarName = "TAPE"; break;
                    case CommonMetadataType.EndTimecode: mvarName = "TCDO"; break;
                    case CommonMetadataType.StartTimecode: mvarName = "TCOD"; break;
                    case CommonMetadataType.TitleTITL: mvarName = "TITL"; break;
                    case CommonMetadataType.Length: mvarName = "TLEN"; break;
                    case CommonMetadataType.Organization: mvarName = "TORG"; break;
                    case CommonMetadataType.TrackNumber: mvarName = "TRCK"; break;
                    case CommonMetadataType.URL: mvarName = "TURL"; break;
                    case CommonMetadataType.Version: mvarName = "TVER"; break;
                    case CommonMetadataType.VegasVersionMajor: mvarName = "VMAJ"; break;
                    case CommonMetadataType.VegasVersionMinor: mvarName = "VMIN"; break;
                    case CommonMetadataType.Year: mvarName = "YEAR"; break;
                    #region Exif 2.3 specification
                    case CommonMetadataType.ExifMake: mvarName = "ecor"; break;
                    case CommonMetadataType.ExifModel: mvarName = "emdl"; break;
                    case CommonMetadataType.ExifMakerNotes: mvarName = "emnt"; break;
                    case CommonMetadataType.ExifRelatedImageFile: mvarName = "erel"; break;
                    case CommonMetadataType.ExifTimeCreated: mvarName = "etim"; break;
                    case CommonMetadataType.ExifUserComment: mvarName = "eucm"; break;
                    case CommonMetadataType.ExifVersion: mvarName = "ever"; break;
                    #endregion
                    default: throw new NotImplementedException();
                }
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private object mvarValue = null;
        public object Value { get { return mvarValue; } set { mvarValue = value; } }

    }
}

#endif
